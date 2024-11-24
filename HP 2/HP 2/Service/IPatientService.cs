using HP.Models;

namespace HP.Service
{
    public interface IPatientService
    {
        public Task<IEnumerable<Patient>> GetPatient();
        public Task<IEnumerable<Patient_History>> GetPatient_History();
        public Patient GetPatientById(int id);
        public Patient_History GetPatient_HistorytById(int id);
        public bool IfAny(int id);
        public Patient FindPatientById(int id);
        public Task<IEnumerable<Patient_History>> FindPatient_HistoryByPatientId(int id);
        public void AddPatient(Patient patient);
        public void AddPatient_History(Patient patient ,Doctor doctor);
        public void UpdatePatient(Patient patient);
        public void UpdatePatient_History(Patient_History patient_History);
        public void DeletePatient(Patient patient);
        public void DeletePatient_History(Patient patient, int id);
    }
}
