using Events.Web.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Web.Controllers
{
    public class HomeController : CustomControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
