using System.ComponentModel.DataAnnotations;

namespace LogBook.Models
{
    public class Log
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public float Frequency { get; set; }
        public string Mode { get; set; }
        public int Power { get; set; }
        public string Callsign { get; set; }
        public int ReportSent { get; set; }
        public int ReportReceived { get; set; }
    }
}
