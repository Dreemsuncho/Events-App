using Events.Data.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Events.Data.Entities
{
    public class Account : IdentityUser, IEntity
    {
        public Account()
        {
            Events = new HashSet<Event>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual  ICollection<Comment> Comments { get; set; }
    }
}
