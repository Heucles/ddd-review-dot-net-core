using DddInPractice.Logic.Common;
using NHibernate;

namespace DddInPractice.Logic.Management
{
    public class HeadOfficeRepository : Repository<HeadOffice>
    {
        public HeadOfficeRepository(ISession session) : base(session)
        {
        }
    }
}
