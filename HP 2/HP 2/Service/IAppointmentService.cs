using HP.Models;

namespace HP.Service
{
    public interface IAppointmentService
    {
        public Task<IEnumerable<Appointment>> GetAppointment();
        public Appointment GetAppointmentById(int id);
        public bool IfAny(int id);
        public Appointment FindAppointmenttById(int id);
        public void AddAppointment(Appointment appointment);
        public void UpdateAppointment(Appointment appointment);
        public void DeleteAppointment(Appointment appointment);
    }
}
