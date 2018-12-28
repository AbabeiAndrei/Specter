using System.ComponentModel.DataAnnotations;

namespace Specter.Api.Models
{
    public class DateFilterIntervalModel
    {
        private string _from;
        private string _to;

        [Required]
        public virtual string From
        {
            get => _from;
            set => _from = value;
        }

        [Required]
        public virtual string To 
        { 
            get => _to; 
            set => _to = value; 
        }

        public DateFilterIntervalModel()
        {

        }

        public DateFilterIntervalModel(string from, string to)
            : this()
        {
            _from = from;
            _to = to;
        }
    }
}