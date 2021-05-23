using DddInPractice.Logic.Utils;
using NHibernate;

namespace DddInPractice.UI.Commons.Impl
{
    public class SessionContainer : ISessionContainer
    {
        public SessionContainer()
        {
            this.Session = SessionFactory.OpenSession();
        }

        public ISession Session {get; private set;}
    }
}