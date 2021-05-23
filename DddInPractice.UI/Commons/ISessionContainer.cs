using NHibernate;

namespace DddInPractice.UI.Commons
{
    public interface ISessionContainer
    {
        ISession Session { get; }
    }
}