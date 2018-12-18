using System;

namespace Specter.Api.Data.Entities
{
    public class Timesheet
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int Time { get; set; }

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
