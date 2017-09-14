using Events.Web.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Web.Controllers.MVC
{
    public class CustomerController : CustomControllerBase
    {
        public CustomerController()
        {
        }

        public ActionResult CreateEvent()
        {
            return View();
        }
    }
}
