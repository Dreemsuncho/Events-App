﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

using Events.Web.Core;
using Events.Web.Models;
using System.Threading.Tasks;
using System.Net;

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
            ActionResult response = null;

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(model.FirstName))
                errors.Add("Invalid first name");

            if (string.IsNullOrWhiteSpace(model.LastName))
                errors.Add("Invalid last name");

            if (errors.Any())
                response = StatusCode((int)HttpStatusCode.BadRequest, errors);
            else
                response = StatusCode((int)HttpStatusCode.OK);

            return response;
        }

        [HttpPost("register/validate2")]
        public async Task<ActionResult> RegisterValidation2(RegisterModel model)
        {
            ActionResult response = null;

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(model.LoginEmail))
                errors.Add("Login email is invalid");

            if (await _securityAdapter.UserExists(model.LoginEmail))
                errors.Add("User with same 'Login Email' exist");

            if (!Regex.IsMatch(model.LoginEmail, @".*@.*\..*"))
                errors.Add("Login email must contains \'@\', \'.\', symbols successively");

            if (model.Password.Length < 1)
                errors.Add("Password must be at least 1 symbol");

            if (model.Password != model.ConfirmPassword)
                errors.Add("Passwords do not match");

            if (errors.Any())
                response = StatusCode((int)HttpStatusCode.BadRequest, errors);
            else
                response = StatusCode((int)HttpStatusCode.OK);

            return response;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            ActionResult response = null;

            bool registerSuccess = false;

            bool validationSuccess = RegisterValidation1(model) is StatusCodeResult &&
                               await RegisterValidation2(model) is StatusCodeResult;

            if (validationSuccess)
                registerSuccess = await _securityAdapter.Register(model.LoginEmail, model.FirstName, model.LastName, model.Password);

            if (registerSuccess)
            {
                await _securityAdapter.Login(model.LoginEmail, model.Password, model.RememberMe);
                response = StatusCode((int)HttpStatusCode.Created, model.LoginEmail);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.BadRequest, new List<string> { "Unable to register user" });
            }

            return response;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            ActionResult response = null;

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(model.LoginEmail))
                errors.Add("Login email is invalid");

            if (!Regex.IsMatch(model.LoginEmail, @".*@.*\..*"))
                errors.Add("Login email must contains \'@\', \'.\', symbols successively");

            if (model.Password.Length < 1)
                errors.Add("Password must be at least 1 symbol");

            if (!await _securityAdapter.UserExists(model.LoginEmail) ||
                !await _securityAdapter.CheckPassword(model.LoginEmail, model.Password))
                errors.Add("Invalid username or password");

            bool loginSuccess = false;

            if (!errors.Any())
                loginSuccess = await _securityAdapter.Login(model.LoginEmail, model.Password, model.RememberMe);

            if (loginSuccess)
                response = StatusCode((int)HttpStatusCode.OK, new { model.LoginEmail, model.ReturnUrl });
            else
                response = StatusCode((int)HttpStatusCode.BadRequest, errors);

            return response;
        }

        [HttpPost("chpassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            ActionResult response = null;

            var errors = new List<string>();

            if (!await _securityAdapter.CheckPassword(User.Identity.Name, model.OldPassword))
                errors.Add("Old password is incorrect");

            if (string.IsNullOrWhiteSpace(model.NewPassword))
                errors.Add("New password is invalid");

            if (model.NewPassword != model.ConfirmPassword)
                errors.Add("Passwords do not match");

            bool changePasswordSuccess = false;

            if (!errors.Any())
                changePasswordSuccess = await _securityAdapter.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);

            if (changePasswordSuccess)
                response = StatusCode((int)HttpStatusCode.OK);
            else
                response = StatusCode((int)HttpStatusCode.BadRequest, errors);

            return response;
        }
    }
}
