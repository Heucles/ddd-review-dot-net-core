using System;
using DddInPractice.Logic.Common;
using NHibernate;

namespace DddInPractice.Logic.SnackMachines
{
    public class SnackMachineRepository : Repository<SnackMachine>
    {
        private ISession _session;
        public SnackMachineRepository(ISession session) : base(session)
        {
            this._session = session;
        }

        public Slot GetSlotById(Int64 slotId){
            return this._session.Get<Slot>(slotId);
        }

        //public IReadOnlyList<SnackMachine> GetAllWithSnack(Snack snack){}

        //public IReadOnlyList<SnackMachine> GetAllWithMoneyInside(Money money){}
    }
}
