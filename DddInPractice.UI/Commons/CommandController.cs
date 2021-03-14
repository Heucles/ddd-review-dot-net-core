using System;
using DddInPractice.Logic;
using DddInPractice.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace DddInPractice.UI.Commons
{
    public abstract class CommandController : Controller
    {
        private ISnackMachineContainer _container;
        public CommandController(ISnackMachineContainer snackMachineContainer)
        {
            this._container = snackMachineContainer;
        }

        public ISnackMachineContainer Container { get; private set; }
        protected IActionResult InsertMoney(MoneyAdded money)
        {
            try
            {
                this._container.SnackMachine.InsertMoney(money.Money);

                return Ok(
                    new RouteValueDictionary{
                        {"Added: ", Enum.GetName(typeof(MoneyLabel), money.Label)},
                        {"TotalAmountInTransaction: ", this._container.SnackMachine.MoneyInTransaction.Amount},
                        {"TotalAmount: ", this._container.SnackMachine.MoneyInside.Amount}
                            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
