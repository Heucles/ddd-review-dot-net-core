using System.Collections.Generic;
using System.Linq;
using DddInPractice.Logic.Common;
using NHibernate;

namespace DddInPractice.Logic.Atms
{
    public class AtmRepository : Repository<Atm>
    {
        public AtmRepository(ISession session) : base(session)
        {
        }

        public IReadOnlyList<AtmDto> GetAtmList()
        {
            return base._session.Query<Atm>()
            .ToList()// Fetch data into memory --> if the collection is too big, we should move it into Dapper or ADO
            .Select(x => new AtmDto(x.Id, x.MoneyInside.Amount))
            .ToList();
        }
    }
}
