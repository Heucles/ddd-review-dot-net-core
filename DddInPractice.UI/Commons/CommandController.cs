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

                return AcceptedAtAction("Added: " + Enum.GetName(typeof(MoneyLabel), money.Label),
                    SnackMachineStateResult("Added", Enum.GetName(typeof(MoneyLabel), money.Label)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected RouteValueDictionary SnackMachineStateResult() => SnackMachineStateResult(String.Empty, String.Empty);

        protected RouteValueDictionary SnackMachineStateResult(string specialLabel, string specialValue) => new RouteValueDictionary{
                        {specialLabel, specialValue},
                        {"TotalAmountInTransaction", this._container.SnackMachine.MoneyInTransaction.Amount},
                        {"TotalAmount", this._container.SnackMachine.MoneyInside.Amount}
            };



    }
}
