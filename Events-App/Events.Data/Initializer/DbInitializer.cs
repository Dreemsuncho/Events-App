using System.Linq;
using Microsoft.AspNetCore.Identity;

using Events.Data.Entities;

namespace Events.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly EventsDbContext _context;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            EventsDbContext context,
            UserManager<Account> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void Initialize()
        {
            if (!_context.Roles.Any(r => r.Name == "Admin"))
            {
                var account = new Account
                {
                    FirstName = "Admin",
                    LastName = "Adminov",
                    UserName = "admin@admin.com",
                    EmailConfirmed = true
                };

                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _userManager.CreateAsync(account, "admin");
                await _userManager.AddToRoleAsync(account, "Admin");
            }
        }
    }
}
