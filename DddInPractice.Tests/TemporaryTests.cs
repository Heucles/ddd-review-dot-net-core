using System;
using DddInPractice.Logic;
using NHibernate;
using Xunit;

namespace DddInPractice.Tests
{
    public class TemporaryTests
    {
        [Fact]
        public void TestRun()
        {
            SessionFactory.Init(@"Server=localhost;Database=DddInPractice;User Id=sa;Password=reviewddd@123;");

            using (ISession session = SessionFactory.OpenSession()){
                long id = 1;
                var snackMachine = session.Get<SnackMachine>(id);
                Console.Write(snackMachine.MoneyInside.OneDollarCount);
            }

        }
    }
}
