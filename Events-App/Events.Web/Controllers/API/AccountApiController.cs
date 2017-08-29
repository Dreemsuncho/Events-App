using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

using Events.Web.Core;
using Events.Web.Models;

namespace Events.Web.Controllers
{
    [Route("api/account")]
    public class AccountApiController : CustomControllerBase
    {
        private readonly ISecurityAdapter _securityAdapter;

        public AccountApiController(ISecurityAdapter securityAdapter)
        {
            _securityAdapter = securityAdapter;
        }

        [HttpPost("register/validate1")]
        public ActionResult RegisterValidation1(RegisterModel model)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(model.FirstName))
                errors.Add("Invalid first name");

            if (string.IsNullOrWhiteSpace(model.LastName))
                errors.Add("Invalid last name");

            if (errors.Any())
                return BadRequest(errors);
            else
                return Ok();
        }

        [HttpPost("register/validate2")]
        public ActionResult RegisterValidation2(RegisterModel model)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(model.LoginEmail))
                errors.Add("Login email is invalid");

            if (_securityAdapter.UserExists(model.LoginEmail))
                errors.Add("User with same 'Login Email' exist");

            if (!Regex.IsMatch(model.LoginEmail, @".*@.*\..*"))
                errors.Add("Login email must contains \'@\', \'.\', symbols successively");

            if (model.Password.Length < 1)
                errors.Add("Password must be at least 1 symbol");

            if (model.Password != model.ConfirmPassword)
                errors.Add("Passwords do not match");

            if (errors.Any())
                return BadRequest(errors);
            else
                return Ok();
        }

        [HttpPost("register")]
        public ActionResult Register(RegisterModel model)
        {
            ActionResult response = null;

            bool registerSuccess = false;

            if (RegisterValidation1(model) is OkResult &&
                RegisterValidation2(model) is OkResult)
            {
                registerSuccess = _securityAdapter.Register(model.LoginEmail, model.FirstName, model.LastName, model.Password);

                if (registerSuccess)
                    _securityAdapter.Login(model.LoginEmail, model.Password, false);
            }

            if (registerSuccess)
                response = Ok();
            else
                response = BadRequest(new[] { $"Unable to register user" });

            return response;
        }

        [HttpPost("login")]
        public ActionResult Login(LoginModel model)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(model.LoginEmail))
                errors.Add("Login email is invalid");

            if (!Regex.IsMatch(model.LoginEmail, @".*@.*\..*"))
                errors.Add("Login email must contains \'@\', \'.\', symbols successively");

            if (model.Password.Length < 1)
                errors.Add("Password must be at least 1 symbol");

            if (model.Password != model.ConfirmPassword)
                errors.Add("Passwords do not match");

            if (!_securityAdapter.UserExists(model.LoginEmail) ||
                !_securityAdapter.CheckPassword(model.LoginEmail, model.Password))
                errors.Add("Invalid username or password");

            bool isLoggedIn = false;

            if (!errors.Any())
                isLoggedIn = _securityAdapter.Login(model.LoginEmail, model.Password, false);

            if (isLoggedIn)
                return Ok();
            else
                return BadRequest(errors);
        }

        //[HttpPost]
        //public ActionResult ChangePassword(string passkk)
        //{

        //}
    }
}
