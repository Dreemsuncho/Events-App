using Events.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Web.Models
{
    public class IndexModel
    {
        public int Page { get; set; }
        public DateTime StartDate { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
