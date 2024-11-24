using System.ComponentModel.DataAnnotations;

namespace HP.Models
{
    public class Patient_History
    {
        public int Id { set; get; }
        public int patientID { set; get; }
        public int doctorID { set; get; }
        public bool Treatment_Stat { get; set; }
        public DateTime time { set; get; }
    }
}
