using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Web.Models
{
    public class CommentModel
    {
        public string Text { get; set; }
        public Guid EventId { get; set; }
    }
}
