using Microsoft.AspNetCore.Mvc;
using Events.Web.Core;

namespace Events.Web.Controllers
{
    [Route("account")]
    public class AccountController : CustomControllerBase
    {
        private readonly ISecurityAdapter _securityAdapter;

        public AccountController(ISecurityAdapter securityAdapter)
        {
            _securityAdapter = securityAdapter;
        }

        [HttpGet("register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet("changepw")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpGet("logout")]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();    
            _securityAdapter.Logout();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
