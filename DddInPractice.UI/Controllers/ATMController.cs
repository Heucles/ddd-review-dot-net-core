using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DddInPractice.UI.Commons;
using DddInPractice.Logic;
using DddInPractice.UI.Models;
using NHibernate;
using System.Diagnostics;
using Microsoft.AspNetCore.Routing;

namespace DddInPractice.UI.Controllers
{
    public class ATMController : CommandController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}