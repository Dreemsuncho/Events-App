using Events.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Web.Core
{
    public interface ISecurityAdapter
    {
        bool Login(string loginEmail, string password, bool rememberMe);
        bool Register(string loginEmail, string firstName, string lastName, string password);
        bool UserExists(string loginEmail);
        bool CheckPassword(string loginEmail, string password);
        bool ChangePassword(Account user, string oldPassword, string newPassword);
        void Logout();
    }
}
