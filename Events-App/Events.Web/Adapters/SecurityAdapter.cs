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

        public async Task<bool> Login(string loginEmail, string password, bool rememberMe)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(loginEmail, password, rememberMe, false);

            return signInResult.Succeeded;
        }

        public async Task<bool> Register(string loginEmail, string firstName, string lastName, string password)
        {
            var newUser = new Account { UserName = loginEmail, FirstName = firstName, LastName = lastName };

            var result = await _userManager.CreateAsync(newUser, password);

            return result.Succeeded;
        }

        public async Task<bool> UserExists(string loginEmail)
        {
            var user = await _FindByNameAsync(loginEmail);

            return (user != null);
        }

        public async Task<bool> CheckPassword(string loginEmail, string password)
        {
            Account user = await _FindByNameAsync(loginEmail);

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            return result.Succeeded;
        }

        public async Task<bool> ChangePassword(string loginEmail, string oldPassword, string newPassword)
        {
            Account user = await _FindByNameAsync(loginEmail);

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task<Account> _FindByNameAsync(string loginEmail)
        {
            return await _userManager.FindByNameAsync(loginEmail);
        }
    }
}
