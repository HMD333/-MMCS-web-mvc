using HP.Data;
using HP.Models;
using Microsoft.EntityFrameworkCore;

namespace HP.Service
{
    public class PatientService : IPatientService
    {
        private readonly HPDbContext _context;

        public PatientService(HPDbContext context)
        {
            _context = context;
        }
        public void AddPatient(Patient patient)
        {
            _context.Add(patient);
        }

        public void AddPatient_History(Patient patient , Doctor doctor)
        {
            _context.Patient_Histories.Add(new Patient_History
            {
                patientID = patient.Id,
                doctorID = doctor.Id,
                time = DateTime.Now,
                Treatment_Stat = false
            });
        }

        public void DeletePatient(Patient patient)
        {
            _context.patients.Remove(patient);
        }

        public void DeletePatient_History(Patient patient, int id)
        {
            foreach (var Patient_History in _context.Patient_Histories.Where(h => h.patientID == id))
            {
                _context.Patient_Histories.Remove(Patient_History);
            }
        }

        public Patient FindPatientById(int id)
        {
            var Result = _context.patients.Find(id);

            return (Result);
        }

        public async Task<IEnumerable<Patient_History>> FindPatient_HistoryByPatientId(int id)
        {
            var Result = await _context.Patient_Histories.Where(d => d.patientID == id).ToListAsync();

            return (Result);
        }

        public async Task<IEnumerable<Patient>> GetPatient()
        {
            var Result = await _context.patients.ToListAsync();

            return (Result);
        }

        public Patient GetPatientById(int id)
        {
            var Result = _context.patients.FirstOrDefault(m => m.Id == id);

            return (Result);
        }

        public Patient_History GetPatient_HistorytById(int id)
        {
            var Result = _context.Patient_Histories.FirstOrDefault(m => m.Id == id);

            return (Result);
        }

        public async Task<IEnumerable<Patient_History>> GetPatient_History()
        {
            var Result = await _context.Patient_Histories.ToListAsync();

            return (Result);
        }

        public bool IfAny(int id)
        {
            return _context.patients.Any(e => e.Id == id);
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }

        public void UpdatePatient(Patient patient)
        {
            _context.Update(patient);
        }

        public void UpdatePatient_History(Patient_History patient_History)
        {
            _context.Update(patient_History);
        }
    }
}
