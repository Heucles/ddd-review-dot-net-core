using System;
using NHibernate;

namespace DddInPractice.Logic.Common
{
    public abstract class Repository<T> where T : AggregateRoot
    {
        protected ISession _session { private set; get; }
        public Repository(ISession session)
        {
            this._session = session;
        }

        public T GetById(long id)
        {
            return _session.Get<T>(id);
        }

        public void Save(T aggregateRoot)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(aggregateRoot);
                transaction.Commit();
            }
        }


    }
}
