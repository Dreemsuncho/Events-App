using System;
using System.Collections.Generic;
using Events.Data.Contracts;

namespace Events.Data.Entities
{
    public class Event : IEntity
    {
        public Event()
        {
            IsPublic = true;
            Comments = new HashSet<Comment>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public TimeSpan Duration { get; set; }
        public string AuthorId { get; set; }
        public Account Author { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsPublic { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
