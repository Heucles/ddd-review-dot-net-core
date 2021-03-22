using System;
using DddInPractice.Logic;
using NHibernate;

namespace DddInPractice.UI.Models.Impl
{
    public class SnackMachineContainer : ISnackMachineContainer
    {
        private SnackMachine _snackMachine;

        public SnackMachine SnackMachine
        {
            get
            {
                if (this._snackMachine == null)
                {
                    using (ISession session = SessionFactory.OpenSession())
                    {
                        this._snackMachine = session.Get<SnackMachine>(1L);
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