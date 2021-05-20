using DddInPractice.Logic.Common;
using DddInPractice.Logic.Management;
using DddInPractice.Logic.Utils;
using NHibernate;

namespace DddInPractice.Logic.Atms
{
    public class BalanceChangedEventHandler : IHandler<BalanceChangedEvent>
    {
        public void Handle(BalanceChangedEvent domainEvent)
        {
            ISession session = SessionFactory.OpenSession();
            var repository = new HeadOfficeRepository(session);
            HeadOffice headOffice = HeadOfficeInstance.Instance;
            headOffice.ChangeBalance(domainEvent.Delta);
            repository.Save(headOffice);
        }
    }
}
