using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DddInPractice.UI.Commons;
using DddInPractice.Logic;
using DddInPractice.UI.Models;

namespace DddInPractice.UI.Controllers
{

    [Route("command")]
    public class SnackMachineViewModelController : CommandController
    {
        private readonly ISnackMachineContainer _snackMachineContainer;

        // ILogger<SnackMachineViewModelController> _logger;

        public SnackMachineViewModelController(ISnackMachineContainer snackMachineContainer):base(snackMachineContainer)
        {
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

    }
}
