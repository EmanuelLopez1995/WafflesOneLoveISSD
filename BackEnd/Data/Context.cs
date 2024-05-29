using Common.Entities;
using FC.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeShift> EmployeeShifts { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<Shift> Shift { get; set; }
        public DbSet<PaymentBox> PaymentBox { get; set; }
        public DbSet<Salary> Salary { get; set; }
        public DbSet<PaymentBoxInitialActive> PaymentBoxInitialActive { get; set; }
        public DbSet<Stock> Stock { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

     
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShiftConfiguration).Assembly);
       




        }
    }
}

