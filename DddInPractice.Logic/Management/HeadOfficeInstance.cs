using System;
using DddInPractice.Logic.Utils;
using NHibernate;

namespace DddInPractice.Logic.Management
{
    // Singleton implementation for the headoffice, since we know that we only have one HeadOffice within the domain
    // This class resides in the same layer as repositories within the onion architecture
    // This should not be implemented within the repository because it violates the Single Responsability principle, repositories should 
    // not hold instances of other classes
    public class HeadOfficeInstance
    {

        private const long HeadOfficeId = 1;

        public static HeadOffice Instance { get; private set; }
        public static void Init()
        {
            ISession session = SessionFactory.OpenSession();
            var repository = new HeadOfficeRepository(session);
            Instance = repository.GetById(HeadOfficeId);
        }

    }
}
