using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DddInPractice.UI.Commons;
using DddInPractice.Logic;
using DddInPractice.UI.Models;
using NHibernate;

namespace DddInPractice.UI.Controllers
{

    [Route("command")]
    public class SnackMachineViewModelController : CommandController
    {
        private readonly ISnackMachineContainer _snackMachineContainer;

        // ILogger<SnackMachineViewModelController> _logger;

        public SnackMachineViewModelController(ISnackMachineContainer snackMachineContainer) : base(snackMachineContainer)
        {
            this._snackMachineContainer = snackMachineContainer;
        }


        [HttpGet("get-snackmachine-state")]
        public IActionResult GetSnackMachineState()
        {
            try
            {
                return Ok(
                    base.SnackMachineStateResult());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // [HttpGet]
        // public IActionResult Get()
        // {
        //     try
        //     {
        //         return Ok();
        //     }
        //     catch (Exception)
        //     {
        //         _logger.LogError("Failed to execute GET");
        //         return BadRequest();
        //     }
        // }


        [HttpPost("add-cent")]
        public IActionResult AddCent() => base.InsertMoney(MoneyAdded.Cent);

        [HttpPost("add-ten-cent")]
        public IActionResult AddTenCent() => base.InsertMoney(MoneyAdded.TenCent);
        [HttpPost("add-quarter")]
        public IActionResult AddQuarter() => base.InsertMoney(MoneyAdded.Quarter);
        [HttpPost("add-dollar")]
        public IActionResult AddDollar() => base.InsertMoney(MoneyAdded.Dollar);
        [HttpPost("add-five-dollar")]
        public IActionResult AddFiveDollar() => base.InsertMoney(MoneyAdded.FiveDollar);
        [HttpPost("add-twenty-dollar")]
        public IActionResult AddTwentyDollar() => base.InsertMoney(MoneyAdded.TwentyDollar);

        [HttpPost("return-money")]
        public IActionResult ReturnMoney()
        {
            try
            {
                this._snackMachineContainer.SnackMachine.ReturnMoney();

                return AcceptedAtAction("Returned the money ",
                    base.SnackMachineStateResult());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("buy-snack")]
        public IActionResult BuySnack()
        {
            try
            {
                // TODO: FIX HERE
                this._snackMachineContainer.SnackMachine.BuySnack(1);

                using(ISession session = SessionFactory.OpenSession()){
                    using(ITransaction transaction = session.BeginTransaction()){
                        session.SaveOrUpdate(_snackMachineContainer.SnackMachine);
                        transaction.Commit();
                    }
                }

                return AcceptedAtAction("Snack bought ",
                    base.SnackMachineStateResult("Message","Thanks for your purchase!"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
