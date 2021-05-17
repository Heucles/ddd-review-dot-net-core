using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DddInPractice.UI.Commons;
using NHibernate;
using System.Diagnostics;
using Microsoft.AspNetCore.Routing;
using DddInPractice.UI.Models;
using DddInPractice.Logic.Atms;
using DddInPractice.Logic.Utils;

namespace DddInPractice.UI.Controllers
{
    public class ATMController : CommandController
    {

        private IAtmContainer _container;
        public ATMController(IAtmContainer container){
            this._container = container;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost("command-atm/take-money/{amount}")]
        public IActionResult TakeMoney(decimal amount)
        {
            try
            {
                string errorWhileTakingTheMoney = _container.Atm.CanTakeMoney(amount);

                if (errorWhileTakingTheMoney != string.Empty)
                {
                    return BadRequest(this.AtmMachineResult("message", errorWhileTakingTheMoney));
                }

                this._container.Atm.TakeMoney(amount);

                using (ISession session = SessionFactory.OpenSession())
                {
                    var repository = new AtmRepository(session);
                    repository.Save(this._container.Atm);
                }

                return AcceptedAtAction("Money Retrieved ",
                  this.AtmMachineResult("message", "Thanks for your operation!"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

                private RouteValueDictionary AtmMachineResult(string specialLabel, string specialValue) => new RouteValueDictionary {
        {
            specialLabel,
            specialValue
        },
             {
                "moneyCharged",
                this._container.Atm.MoneyCharged
            },
             {
                "moneyInside",
                this._container.Atm.MoneyInside
            }
        };
    }
}