using System;
using DddInPractice.Logic;

namespace DddInPractice.UI.Models.Impl
{
    public class SnackMachineContainer : ISnackMachineContainer
    {
        private SnackMachine _snackMachine;

        public SnackMachine SnackMachine
        {
           get {
                if (this._snackMachine == null)
                {
                    this._snackMachine = new SnackMachine();
                }

                return this._snackMachine;
            }
        }
    }
}