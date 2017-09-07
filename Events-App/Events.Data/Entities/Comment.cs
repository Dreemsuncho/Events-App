using System;
using Events.Data.Contracts;

namespace Events.Data.Entities
{
    public class Comment: IEntity
    {
        public Comment()
        {
            Date = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string AuthorId { get; set; }
        public Account Author { get; set; }
        public string EventId { get; set; }
        public Event Event { get; set; }
    }
}
