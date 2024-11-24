using System.ComponentModel.DataAnnotations;

namespace HP.Models
{
    public class Patients_Docter
    {
        public IEnumerable<Patient> Patients { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }
        public IEnumerable<Patient_History> PHs { get; set; }
        public Patient patient { get; set; }
    }
}
