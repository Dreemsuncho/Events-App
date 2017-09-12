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

        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public Account Author { get; set; }
    }
}
