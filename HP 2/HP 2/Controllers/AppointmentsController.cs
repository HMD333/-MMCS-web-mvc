using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HP.Data;
using HP.Models;
using HP.Service;

namespace HP.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IPatientService _Patient_Service;
        private readonly IDoctorService _Doctor_Service;
        private readonly IAppointmentService _Appointment_Service;

        public AppointmentsController(IPatientService Patient_Service, IDoctorService Doctor_Service, IAppointmentService Appointment_Service)
        {
            _Patient_Service = Patient_Service;
            _Doctor_Service = Doctor_Service;
            _Appointment_Service = Appointment_Service;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var patient = await _Patient_Service.GetPatient();

            var PH = await _Patient_Service.GetPatient_History();

            var doctor = await _Doctor_Service.Get();

            var appointment = await _Appointment_Service.GetAppointment();

            Show_Appointments SA = new Show_Appointments()
            {
                appointments = appointment,
                patient_Histories = PH,
                doctors = doctor,
                patients = patient
            };
            return View(SA);
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = _Appointment_Service.GetAppointmentById(id);
            
            var PH = _Patient_Service.GetPatient_HistorytById(appointment.PatientHistotyid);

            var patient = await _Patient_Service.GetPatient();

            var PHs = await _Patient_Service.GetPatient_History();

            var doctor = await _Doctor_Service.Get();

            var appointments = await _Appointment_Service.GetAppointment();

            Make_Appointment MA = new Make_Appointment()
            {
                appointment = appointment,
                appointments = appointments,
                patient_Histories = PHs,
                patient_Historie = PH,
                doctors = doctor,
                patients = patient
            };
            if (appointment == null)
            {
                return NotFound();
            }

            return View(MA);
        }

        // GET: Appointments/Create
        public async Task<IActionResult> Create()
        {
            var patient = await _Patient_Service.GetPatient();

            var PH = await _Patient_Service.GetPatient_History();

            var doctor = await _Doctor_Service.Get();

            var appointment = await _Appointment_Service.GetAppointment();

            Make_Appointment MA = new Make_Appointment()
            {
                appointments = appointment,
                patient_Histories = PH,
                doctors = doctor,
                patients = patient
            };
            return View(MA);
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Make_Appointment MA)
        {
            _Appointment_Service.AddAppointment(MA.appointment);
            _Doctor_Service.Save();
            return RedirectToAction(nameof(Index));
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = _Appointment_Service.FindAppointmenttById(id);

            var PH = _Patient_Service.GetPatient_HistorytById(appointment.PatientHistotyid);

            var patient = await _Patient_Service.GetPatient();

            var PHs = await _Patient_Service.GetPatient_History();

            var doctor = await _Doctor_Service.Get();

            var appointments = await _Appointment_Service.GetAppointment();

            Make_Appointment MA = new Make_Appointment()
            {
                appointment = appointment,
                appointments = appointments,
                patient_Histories = PHs,
                patient_Historie = PH,
                doctors = doctor,
                patients = patient
            };
            if (appointment == null)
            {
                return NotFound();
            }
            return View(MA);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Make_Appointment MA)
        {
            if (id != MA.appointment.id)
            {
                return NotFound();
            }
            try
            {
                Appointment appointment = new Appointment()
                {
                    id = id,
                    PatientHistotyid = MA.appointment.PatientHistotyid,
                    duration = MA.appointment.duration,
                    Visits = MA.appointment.Visits
                };
                _Appointment_Service.UpdateAppointment(appointment);
                _Doctor_Service.Save();

                Patient_History PH = new Patient_History()
                {
                    Id = MA.appointment.PatientHistotyid,
                    Treatment_Stat = MA.patient_Historie.Treatment_Stat

                };
                _Patient_Service.UpdatePatient_History(PH);
                _Doctor_Service.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(MA.appointment.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = _Appointment_Service.GetAppointmentById(id);

            var PH = _Patient_Service.GetPatient_HistorytById(appointment.PatientHistotyid);

            var patient = await _Patient_Service.GetPatient();

            var PHs = await _Patient_Service.GetPatient_History();

            var doctor = await _Doctor_Service.Get();

            var appointments = await _Appointment_Service.GetAppointment();

            Make_Appointment MA = new Make_Appointment()
            {
                appointment = appointment,
                appointments = appointments,
                patient_Histories = PHs,
                patient_Historie = PH,
                doctors = doctor,
                patients = patient
            };

            if (appointment == null)
            {
                return NotFound();
            }

            return View(MA);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = _Appointment_Service.FindAppointmenttById(id);
            if (appointment != null)
            {
                _Appointment_Service.DeleteAppointment(appointment);
            }

            _Doctor_Service.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _Appointment_Service.IfAny(id);
        }
    }
}
