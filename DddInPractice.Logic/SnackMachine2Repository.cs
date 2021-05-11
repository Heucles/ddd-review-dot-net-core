using System;
using NHibernate;

namespace DddInPractice.Logic
{
    public class SnackMachine2Repository : Repository<SnackMachine2>
    {
        private ISession _session;
        public SnackMachine2Repository(ISession session) : base(session)
        {
            this._session = session;
        }
    }
}
