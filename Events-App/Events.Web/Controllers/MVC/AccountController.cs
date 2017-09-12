using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Events.Web.Core;
using Events.Web.Models;
using Microsoft.AspNetCore.Authorization;

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
        public ActionResult Login(string returnUrl)
        {
            return View(new LoginModel { ReturnUrl = returnUrl});
        }

        [Authorize]
        [HttpGet("changepw")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            HttpContext.Session.Clear();    
            await _securityAdapter.Logout();
            return  RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
