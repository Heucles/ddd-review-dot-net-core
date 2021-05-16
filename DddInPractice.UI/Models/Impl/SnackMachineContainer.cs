using System;
using DddInPractice.Logic.SnackMachines;
using DddInPractice.Logic.Utils;
using NHibernate;

namespace DddInPractice.UI.Models.Impl
{
    public class SnackMachineContainer : ISnackMachineContainer
    {
        private SnackMachine _snackMachine;
        private SnackMachineRepository _snackMachineRepository;

        public SnackMachine SnackMachine
        {
            get
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

                return this._snackMachine;
            }
        }
    }
}