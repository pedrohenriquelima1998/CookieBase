using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthProjeto.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Entrada()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Teste()
        {
            return View();
        }
    }
}