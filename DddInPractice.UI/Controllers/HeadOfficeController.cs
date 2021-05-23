using System;
using Microsoft.AspNetCore.Mvc;
using DddInPractice.UI.Commons;
using DddInPractice.Logic.Atms;
using DddInPractice.UI.ViewModels;
using DddInPractice.Logic.SnackMachines;
using Microsoft.AspNetCore.Routing;
using DddInPractice.Logic.Management;

namespace DddInPractice.UI.Controllers
{
    public class HeadOfficeController : CommandController
    {
        private AtmRepository AtmRepository;
        private SnackMachineRepository SnackMachineRepository;

        public HeadOfficeController(ISessionContainer sessionContainer)
        {
            this.AtmRepository = new AtmRepository(sessionContainer.Session);
            this.SnackMachineRepository = new SnackMachineRepository(sessionContainer.Session);
        }

        public IActionResult Index()
        {
            return View(HeadOfficeViewModel.Create(
                this.AtmRepository.GetAtmList(),
                this.SnackMachineRepository.GetSnackMachineList()));
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost("command-head-office/take-money-from-snack-machine/{snackMachineId}")]
        public IActionResult TakeMoneyFromSnackMachine(long snackMachineId)
        {
            try
            {
                SnackMachine snackMachine = SnackMachineRepository.GetById(snackMachineId);

                HeadOffice headOffice = HeadOfficeInstance.Instance;

                headOffice.UnloadCashFromSnackMachine(snackMachine);

                this.SnackMachineRepository.Save(snackMachine);

                return AcceptedAtAction(
                    "Money Removed from Snack Machine",
                    new RouteValueDictionary {
                        {
                            "moneyInsideSnackMachine",
                            snackMachine.MoneyInside.Amount
                        },
                        {
                            "cash",
                            headOffice.Cash.Amount
                        },
                        {
                            "balance",
                            headOffice.Balance
                        }
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("command-head-office/send-money-into-atm/{atmId}")]
        public IActionResult SendMoneyIntoAtm(long atmId)
        {
            try
            {
                Atm atm = AtmRepository.GetById(atmId);

                HeadOffice headOffice = HeadOfficeInstance.Instance;

                headOffice.LoadCashToAtm(atm);

                this.AtmRepository.Save(atm);

                return AcceptedAtAction(
                    "Money Sent into Atm",
                    new RouteValueDictionary {
                        {
                            "moneyInsideAtm",
                            atm.MoneyInside.Amount
                        },
                        {
                            "cash",
                            headOffice.Cash.Amount
                        },
                        {
                            "balance",
                            headOffice.Balance
                        }
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}