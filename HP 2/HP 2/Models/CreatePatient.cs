namespace HP.Models
{
    public class CreatePatient
    {
        public Patient patient { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }
        public Doctor Doctor { get; set; }
    }
}
