namespace HP.Models
{
    public class Show_Appointments
    {
        public IEnumerable<Appointment> appointments { get; set; }
        public IEnumerable<Patient_History> patient_Histories { get; set; }
        public IEnumerable<Patient> patients { get; set; }
        public IEnumerable<Doctor> doctors { get; set; }
    }
}
