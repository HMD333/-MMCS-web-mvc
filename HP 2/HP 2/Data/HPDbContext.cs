using HP.Models;
using Microsoft.EntityFrameworkCore;

namespace HP.Data
{
    public class HPDbContext : DbContext
    {
        public HPDbContext(DbContextOptions<HPDbContext> options) : base(options) { }
        public DbSet<Patient> patients { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Patient_History> Patient_Histories { get; set; }
        public DbSet<Appointment> appointments { get; set; }
    }
}
