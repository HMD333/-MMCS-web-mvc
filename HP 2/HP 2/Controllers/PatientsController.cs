using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HP.Data;
using HP.Models;
using System.Numerics;
using HP.Service;

namespace HP.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientService _Patient_Service;
        private readonly IDoctorService _Doctor_Service;

        public PatientsController(IPatientService Patient_Service, IDoctorService Doctor_Service)
        {
            _Patient_Service = Patient_Service;
            _Doctor_Service = Doctor_Service;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            var patient = await _Patient_Service.GetPatient();

            var PH = await _Patient_Service.GetPatient_History();

            var doctor = await _Doctor_Service.Get();

            Patients_Docter PD = new Patients_Docter()
            {
                Doctors = doctor,
                Patients = patient,
                PHs = PH
            };
            

            return View(PD);
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _Patient_Service.GetPatientById(id);
            var PH = await _Patient_Service.FindPatient_HistoryByPatientId(id);
            var doctor = await _Doctor_Service.Get();
            Patients_Docter PD = new Patients_Docter()
            {
                Doctors = doctor,
                patient = patient,
                PHs = PH
            };
            if (patient == null)
            {
                return NotFound();
            }

            return View(PD);
        }

        // GET: Patients/Create
        public async Task<IActionResult> Create()
        {

            var doctor = await _Doctor_Service.Get();

            CreatePatient CP = new CreatePatient()
            {
                Doctors = doctor
            };


            return View(CP);
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Doctor,Doctors,patient")]  CreatePatient CP)
        {
            //ViewBag.mes = "";
            //foreach (var key in ModelState.Keys)
            //{
            //    var state = ModelState[key];
            //    if (state.Errors.Count > 0)
            //    {
            //        foreach (var error in state.Errors)
            //        {
            //            ViewBag.mes += ($"Field: {key}, Error: {error.ErrorMessage}\n");
            //        }
            //    }
            //}
            //if (ModelState.IsValid)
            //{
            _Patient_Service.AddPatient(CP.patient);
            _Doctor_Service.Save();

            _Patient_Service.AddPatient_History(CP.patient, CP.Doctor);
            _Doctor_Service.Save();

            return RedirectToAction(nameof(Index));
            //}
            //return RedirectToAction("Create");
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _Patient_Service.FindPatientById(id);
            var doctor = await _Doctor_Service.Get();

            CreatePatient CP = new CreatePatient()
            {
                patient = patient,
                Doctors = doctor
            };
            if (patient == null)
            {
                return NotFound();
            }
            return View(CP);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Doctor,patient")] CreatePatient CP)
        {
            if (id != CP.patient.Id)
            {
                return NotFound();
            }
            try
            {
                _Patient_Service.UpdatePatient(CP.patient);
                _Doctor_Service.Save();

                _Patient_Service.AddPatient_History(CP.patient, CP.Doctor);
                _Doctor_Service.Save();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(CP.patient.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _Patient_Service.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = _Patient_Service.FindPatientById(id);
            if (patient != null)
            {
                _Patient_Service.DeletePatient(patient);
                _Patient_Service.DeletePatient_History(patient, id);
            }

            _Doctor_Service.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _Patient_Service.IfAny(id);
        }
    }
}
