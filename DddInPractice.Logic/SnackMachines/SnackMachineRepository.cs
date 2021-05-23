using System;
using System.Collections.Generic;
using System.Linq;
using DddInPractice.Logic.Common;
using NHibernate;

namespace DddInPractice.Logic.SnackMachines
{
    public class SnackMachineRepository : Repository<SnackMachine>
    {
        public SnackMachineRepository(ISession session) : base(session)
        {
        }

        public Slot GetSlotById(Int64 slotId)
        {
            return base._session.Get<Slot>(slotId);
        }

        public IReadOnlyList<SnackMachineDto> GetSnackMachineList()
        {
            return base._session.Query<SnackMachine>()
            .ToList()// Fetch data into memory --> if the collection is too big, we should move it into Dapper or ADO
            .Select(x => new SnackMachineDto(x.Id, x.MoneyInside.Amount))
            .ToList();
        }

        //public IReadOnlyList<SnackMachine> GetAllWithSnack(Snack snack){}

        //public IReadOnlyList<SnackMachine> GetAllWithMoneyInside(Money money){}
    }
}
