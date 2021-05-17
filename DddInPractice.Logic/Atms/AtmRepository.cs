using DddInPractice.Logic.Common;
using NHibernate;

namespace DddInPractice.Logic.Atms
{
    public class AtmRepository : Repository<Atm>
    {
        public AtmRepository(ISession session) : base(session)
        {
        }
    }
}
