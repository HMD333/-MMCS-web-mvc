namespace HP.Models
{
    public class Make_Appointment
    {
        public IEnumerable<Appointment> appointments { get; set; }
        public IEnumerable<Patient_History> patient_Histories { get; set; }
        public IEnumerable<Patient> patients { get; set; }
        public IEnumerable<Doctor> doctors { get; set; }
        public Appointment appointment { get; set; }
        public Patient_History patient_Historie { get; set; }
    }
}
