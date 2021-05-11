using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Instances;
using NHibernate;

// This "using" statement is vital so it can work on .Net core 3.0
// https://github.com/dotnet/SqlClient/issues/222
// https://github.com/nhibernate/nhibernate-core/issues/2660
// https://github.com/nhibernate/nhibernate-core/discussions/2661
using System.Data.SqlClient;

namespace DddInPractice.Logic
{
    public static class SessionFactory
    {
        private static ISessionFactory _factory;

        // This guy keeps track of all objects loaded from the database into memory
        // and updates the corresponding roles in the database
        // according to the changes made to those objects

        // It implements the unit of work design pattern, meaning it pushes all accumulated changes at once
        // usually at the end of its lifetime

        public static ISession OpenSession()
        {
            return _factory.OpenSession();
        }

        public static void Init(string connectionString)
        {
            _factory = BuildSessionFactory(connectionString);
        }

        private static ISessionFactory BuildSessionFactory(string connectionString)
        {

            FluentConfiguration configuration = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
            .Mappings(m => m.FluentMappings
            .AddFromAssembly(Assembly.GetExecutingAssembly())
            // https://github.com/fluentmigrator/fluentmigrator
            // .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
            .Conventions.Add(
                ForeignKey.EndsWith("ID"),
                ConventionBuilder.Property.When(
                    criteria => criteria.Expect(x => x.Nullable, Is.Not.Set), x => x.Not.Nullable())
                )
            .Conventions.Add<TableNameConvention>()
            .Conventions.Add<HiLoConvention>()
            );

            return configuration.BuildSessionFactory();

        }
        public class TableNameConvention : IClassConvention
        {
            public void Apply(IClassInstance instance)
            {
                instance.Table("[dbo].[" + instance.EntityType.Name + "]");
            }
        }

        public class HiLoConvention : IIdConvention
        {
            public void Apply(IIdentityInstance instance)
            {
                instance.Column(instance.EntityType.Name + "ID");
                instance.GeneratedBy.HiLo(
                    table : "[dbo].[Ids]", 
                    column: "NextHigh", 
                    maxLo: "9", // size of the batches
                    where: "EntityName = '" + instance.EntityType.Name + "'");
            }
        }
    }
}