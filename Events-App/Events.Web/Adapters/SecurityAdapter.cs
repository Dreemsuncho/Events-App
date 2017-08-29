using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using Events.Web.Core;
using Events.Data.Entities;

namespace Events.Web.Adapters
{
    public class SecurityAdapter : ISecurityAdapter
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;

        public SecurityAdapter(UserManager<Account> userManager, SignInManager<Account> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public bool Login(string loginEmail, string password, bool rememberMe)
        {
            Account user = _FindUserByUsername(loginEmail);

            var signInResult = Task.Run(() => _signInManager.PasswordSignInAsync(user, password, rememberMe, false)).Result;

            return signInResult.Succeeded;
        }

        public bool Register(string loginEmail, string firstName, string lastName, string password)
        {
            var newUser = new Account { UserName = loginEmail, FirstName = firstName, LastName = lastName };

            var result = Task.Run(() => _userManager.CreateAsync(newUser, password)).Result;

            return result.Succeeded;
        }

        public bool UserExists(string loginEmail)
        {
            var task = Task.Run(() => _userManager.FindByNameAsync(loginEmail)).Result;

            return (task != null);
        }

        public bool CheckPassword(string loginEmail, string password)
        {
            Account user = _FindUserByUsername(loginEmail);

            var result = Task.Run(() => _signInManager.CheckPasswordSignInAsync(user, password, false)).Result;

            return result.Succeeded;
        }

        public bool ChangePassword(Account user, string oldPassword, string newPassword)
        {
            var result = Task.Run(() => _userManager.ChangePasswordAsync(user, oldPassword, newPassword)).Result;

            return result.Succeeded;
        }

        public void Logout()
        {
            Task.Run(() => _signInManager.SignOutAsync());
        }

        private Account _FindUserByUsername(string loginEmail)
        {
            return Task.Run(() => _userManager.FindByNameAsync(loginEmail)).Result;
        }
    }
}
