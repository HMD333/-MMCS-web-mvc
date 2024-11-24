using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HP.Data;
using HP.Models;
using System.Runtime.CompilerServices;
using HP.Service;

namespace HP.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly IDoctorService _Servise;

        public DoctorsController(IDoctorService Servise)
        {
            _Servise = Servise;
        }

        // GET: Doctors
        public async Task<IActionResult> Index()
        {
            var doctor = await _Servise.Get();

            return View(doctor);
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = _Servise.GetById(id);

            
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,department,salary,isloged")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _Servise.Add(doctor);
                _Servise.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = _Servise.FindById(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,department,salary,isloged")] Doctor doctor) // Bind entire doctors object
        {
            if (id != doctor.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _Servise.Update(doctor);

                    _Servise.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = _Servise.GetById(id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = _Servise.FindById(id);

            if (doctor != null)
            {
                _Servise.Delete(doctor);
            }

            _Servise.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return _Servise.IfAny(id);
        }
    }
}
