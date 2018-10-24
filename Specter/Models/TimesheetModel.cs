using System;

namespace Specter.Models
{
    public class TimesheetModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int Time { get; set; }
    }
}
