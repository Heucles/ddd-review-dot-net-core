using DddInPractice.Logic.Atms;
using DddInPractice.Logic.SharedKernel;
using DddInPractice.Logic.Utils;
using NHibernate;

namespace DddInPractice.UI.ViewModels
{
    public class AtmViewModel
    {
        private readonly Atm _atm;
        private readonly AtmRepository _atmRepository;
        public static AtmViewModel Create()
        {
            return new AtmViewModel();
        }
        
        private AtmViewModel()
        {
            if (this._atm == null)
            {
                using (ISession session = SessionFactory.OpenSession())
                {
                    this._atmRepository = new AtmRepository(session);
                    this._atm = _atmRepository.GetById(1L);

                }
                if (this._atm == null)
                {
                    this._atm = new Atm();
                }
            }

        }

        public string MoneyCharged => _atm.MoneyCharged.ToString();
        public Money MoneyInside => _atm.MoneyInside;
        
    }
}
