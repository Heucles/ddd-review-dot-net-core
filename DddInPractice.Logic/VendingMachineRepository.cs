using System;
using NHibernate;

namespace DddInPractice.Logic
{
    public class VendingMachineRepository : Repository<VendingMachine>
    {
        private ISession _session;
        public VendingMachineRepository(ISession session) : base(session)
        {
            this._session = session;
        }
    }
}
