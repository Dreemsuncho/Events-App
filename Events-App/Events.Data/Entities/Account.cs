using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Events.Data.Entities
{
    public class Account : IdentityUser
    {
        public Account()
        {
            Events = new HashSet<Event>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
