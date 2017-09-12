using Events.Data.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Events.Data.Entities
{
    public class ApplicationRole : IdentityRole<Guid> { }

    public class Account : IdentityUser<Guid>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
