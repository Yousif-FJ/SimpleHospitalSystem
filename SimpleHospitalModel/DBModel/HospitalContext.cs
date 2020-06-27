using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHospitalModel.DBModel
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<ClinicalData> ClinicalInfomations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //define DB relations and their names
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<Bed>().ToTable("Bed");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<ClinicalData>().ToTable("ClinicalData");

            //define one to many relation between section and beds
            modelBuilder.Entity<Bed>()
                .HasOne<Department>(bed => bed.Department)
                .WithMany(section => section.Beds)
                .HasForeignKey(bed => bed.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            //define one to one relation between patient and bed
            modelBuilder.Entity<Patient>()
                .HasOne<Bed>(patient => patient.Bed)
                .WithOne(bed => bed.Patient)
                .HasForeignKey<Patient>(patient => patient.BedId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            //define one to many relation between clinical informations and patient
            modelBuilder.Entity<ClinicalData>()
                .HasOne<Patient>(clinicalInfo => clinicalInfo.Patient)
                .WithMany(patient => patient.ClinicalData)
                .HasForeignKey(ClinicalInfo => ClinicalInfo.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
