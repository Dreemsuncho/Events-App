using Events.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Web.Core
{
    public interface ISecurityAdapter
    {
        Task<bool> Login(string loginEmail, string password, bool rememberMe);
        Task<bool> Register(string loginEmail, string firstName, string lastName, string password);
        Task<bool> UserExists(string loginEmail);
        Task<bool> CheckPassword(string loginEmail, string password);
        Task<bool> ChangePassword(string loginEmail, string oldPassword, string newPassword);
        Task Logout();
    }
}
