using HP.Data;
using HP.Models;
using Microsoft.EntityFrameworkCore;

namespace HP.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HPDbContext _context;

        public AppointmentService(HPDbContext context)
        {
            _context = context;
        }

        public void AddAppointment(Appointment appointment)
        {
            _context.appointments.Add(new Appointment
            {
                PatientHistotyid = appointment.PatientHistotyid,
                duration = appointment.duration,
                Visits = appointment.Visits
            });
        }

        public void DeleteAppointment(Appointment appointment)
        {
            _context.appointments.Remove(appointment);
        }

        public Appointment FindAppointmenttById(int id)
        {
            var Result = _context.appointments.Find(id);

            return (Result);
        }

        public Appointment GetAppointmentById(int id)
        {
            var Result = _context.appointments.FirstOrDefault(m => m.id == id);

            return (Result);
        }

        public async Task<IEnumerable<Appointment>> GetAppointment()
        {
            var Result = await _context.appointments.ToListAsync();

            return (Result);
        }

        public bool IfAny(int id)
        {
            return _context.appointments.Any(e => e.id == id);
        }

        public void UpdateAppointment(Appointment appointment)
        {
            _context.appointments.Update(appointment);
        }
    }
}
