using HP.Data;
using HP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace HP.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly HPDbContext _context;

        public DoctorService(HPDbContext context)
        {
            _context = context;
        }

        public void Add(Doctor Result)
        {
            _context.Add(Result);
        }

        public void Delete(Doctor Result)
        {
            _context.doctors.Remove(Result);
        }

        public Doctor FindById(int id)
        {
            var Result = _context.doctors.Find(id);

            return (Result);
        }

        public async Task<IEnumerable<Doctor>> Get()
        {
            var Result = await _context.doctors.ToListAsync();

            return (Result);
        }

        public Doctor GetById(int id)
        {
            var Result = _context.doctors.FirstOrDefault(m => m.Id == id);

            return (Result);
        }

        public bool IfAny(int id)
        {
            return _context.doctors.Any(e => e.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Doctor Update(Doctor Result)
        {
            _context.Update(Result);

            return (Result);
        }
    }
}
