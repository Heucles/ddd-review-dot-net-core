using System;
using System.Collections.Generic;
using System.Linq;
using DddInPractice.Logic;
using NHibernate;

namespace DddInPractice.UI.ViewModels
{
    public class SnackMachineViewModel
    {
        private readonly SnackMachine _snackMachine;
        private readonly SnackMachineRepository _snackMachineRepository;
        public static SnackMachineViewModel Create()
        {
            return new SnackMachineViewModel();
        }
        
        private SnackMachineViewModel()
        {
            if (this._snackMachine == null)
            {
                using (ISession session = SessionFactory.OpenSession())
                {
                    this._snackMachineRepository = new SnackMachineRepository(session);
                    this._snackMachine = _snackMachineRepository.GetById(1L);

                }
                if (this._snackMachine == null)
                {
                    this._snackMachine = new SnackMachine();
                }
            }

        }

        public string Caption => "Snack Machineraaaaa";
        public string MoneyInTransaction => _snackMachine.MoneyInTransaction.ToString();
        public Money MoneyInside => _snackMachine.MoneyInside;
        public IReadOnlyList<SnackPileViewModel> Piles
        {
            get
            {
                return _snackMachine.GetAllSnackPiles()
                    .Select(x => new SnackPileViewModel(x))
                    .ToList();
            }
        }
    }
}
