using HP.Models;

namespace HP.Service
{
    public interface IDoctorService
    {
        public Task<IEnumerable<Doctor>> Get();
        public Doctor GetById(int id);
        public bool IfAny(int id);
        public Doctor FindById(int id);
        public void Add(Doctor doctor);
        public Doctor Update(Doctor doctor);
        public void Delete(Doctor doctor);
        public void Save();
    }
}
