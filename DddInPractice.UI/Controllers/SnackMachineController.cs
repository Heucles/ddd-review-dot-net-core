using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DddInPractice.UI.Commons;
using DddInPractice.UI.Models;
using NHibernate;
using System.Diagnostics;
using Microsoft.AspNetCore.Routing;
using DddInPractice.Logic.SnackMachines;
using DddInPractice.Logic.SharedKernel;
using DddInPractice.Logic.Utils;

namespace DddInPractice.UI.Controllers
{
    public class SnackMachineController : CommandController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });

        private readonly ISnackMachineContainer _container;

        // ILogger<SnackMachineController> _logger;

        public SnackMachineController(ISnackMachineContainer snackMachineContainer) : base()
        {
            this._container = snackMachineContainer;
        }

        [HttpGet("command/get-snackmachine-state")]
        public IActionResult GetSnackMachineState()
        {
            try
            {
                return Ok(
                  this.SnackMachineStateResult());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("command/add-cent")]
        public IActionResult AddCent() => this.InsertMoney(MoneyAdded.Cent);

        [HttpPost("command/add-ten-cent")]
        public IActionResult AddTenCent() => this.InsertMoney(MoneyAdded.TenCent);
        [HttpPost("command/add-quarter")]
        public IActionResult AddQuarter() => this.InsertMoney(MoneyAdded.Quarter);
        [HttpPost("command/add-dollar")]
        public IActionResult AddDollar() => this.InsertMoney(MoneyAdded.Dollar);
        [HttpPost("command/add-five-dollar")]
        public IActionResult AddFiveDollar() => this.InsertMoney(MoneyAdded.FiveDollar);
        [HttpPost("command/add-twenty-dollar")]
        public IActionResult AddTwentyDollar() => this.InsertMoney(MoneyAdded.TwentyDollar);

        [HttpPost("command/return-money")]
        public IActionResult ReturnMoney()
        {
            try
            {
                this._container.SnackMachine.ReturnMoney();

                return AcceptedAtAction("Returned the money ",
                  this.SnackMachineStateResult());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("command/buy-snack/{position}")]
        public IActionResult BuySnack(int position)
        {
            try
            {
                string errorWhileBuying = _container.SnackMachine.CanBuySnack(position);

                if (errorWhileBuying != string.Empty)
                {
                    return BadRequest(this.SnackMachineStateResult("message", errorWhileBuying));
                }

                this._container.SnackMachine.BuySnack(position);

                using (ISession session = SessionFactory.OpenSession())
                {
                    var repository = new SnackMachineRepository(session);
                    repository.Save(this._container.SnackMachine);
                }

                return AcceptedAtAction("Snack bought ",
                  this.SnackMachineStateResult("message", "Thanks for your purchase!"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private RouteValueDictionary SnackMachineStateResult() => SnackMachineStateResult(String.Empty, String.Empty);

        private RouteValueDictionary SnackMachineStateResult(string specialLabel, string specialValue) => new RouteValueDictionary {
        {
            specialLabel,
            specialValue
        },
             {
                "moneyInTransaction",
                this._container.SnackMachine.MoneyInTransaction
            },
             {
                "moneyInside",
                this._container.SnackMachine.MoneyInside
            },
            {
                "snackPiles",
                this._container.SnackMachine.GetAllSnackPiles()
            },
        };
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

    }
}