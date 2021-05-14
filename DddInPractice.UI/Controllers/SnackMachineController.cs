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
    public class SnackMachineController : CommandController
    {
        private readonly ISnackMachineContainer _snackMachineContainer;

        // ILogger<SnackMachineController> _logger;

        public SnackMachineController(ISnackMachineContainer snackMachineContainer) : base(snackMachineContainer)
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

        [HttpPost("buy-snack/{position}")]
        public IActionResult BuySnack(int position)
        {
            try
            {
                string errorWhileBuying = _snackMachineContainer.SnackMachine.CanBuySnack(position);

                if (errorWhileBuying != string.Empty)
                {
                    return BadRequest(base.SnackMachineStateResult("message", errorWhileBuying));
                }


                this._snackMachineContainer.SnackMachine.BuySnack(position);

                using (ISession session = SessionFactory.OpenSession())
                {
                    var repository = new SnackMachineRepository(session);
                    repository.Save(this._snackMachineContainer.SnackMachine);
                }

                return AcceptedAtAction("Snack bought ",
                    base.SnackMachineStateResult("message", "Thanks for your purchase!"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
