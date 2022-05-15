using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogBook.Models
{
    public class Log
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        [Required]
        public DateTime Time { get; set; }
        public float Frequency { get; set; }
        public string Mode { get; set; }
        public int Power { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(12)]
        public string Callsign { get; set; }
        [Display(Name ="Report Sent")]
        public int ReportSent { get; set; }
        [Display(Name ="Report Received")]
        public int ReportReceived { get; set; }
    }
}
