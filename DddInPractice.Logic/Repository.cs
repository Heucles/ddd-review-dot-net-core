using System;
using NHibernate;

namespace DddInPractice.Logic
{
    public abstract class Repository<T> where T : AggregateRoot
    {
        private ISession _session;
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
            using (ITransaction transaction = _session.BeginTransaction()){
                _session.SaveOrUpdate(aggregateRoot);
                transaction.Commit();
            }
        }


    }
}
