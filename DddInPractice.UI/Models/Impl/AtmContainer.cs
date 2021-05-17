using System;
using DddInPractice.Logic.Atms;
using DddInPractice.Logic.Utils;
using NHibernate;

namespace DddInPractice.UI.Models.Impl
{
    public class AtmContainer : IAtmContainer
    {
        private Atm _atm;
        private AtmRepository atmRepository;

        public Atm Atm
        {
            get
            {
                if (this._atm == null)
                {
                    using (ISession session = SessionFactory.OpenSession())
                    {
                        this.atmRepository = new AtmRepository(session);
                        this._atm = atmRepository.GetById(1L);
                        
                    }
                    if (this._atm == null)
                    {
                        this._atm = new Atm();
                    }
                }

                return this._atm;
            }
        }
    }
}