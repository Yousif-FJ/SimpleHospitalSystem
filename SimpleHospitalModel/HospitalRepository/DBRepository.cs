using Microsoft.EntityFrameworkCore;
using SimpleHospitalModel.DBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHospitalModel.HospitalRepository
{
    public class DBRepository : IHospitalRepository
    {
        private readonly HospitalContext hospitalContext;

        public DBRepository(HospitalContext hospitalContext)
        {
            this.hospitalContext = hospitalContext;
        }

        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            var returnEntity = await hospitalContext.Patients.AddAsync(patient);
            await hospitalContext.SaveChangesAsync();
            return returnEntity.Entity;
        }

        public async Task<Patient> GetPatientDetailsAsync(long patientId)
        {
            var returnPatient = await hospitalContext.Patients
                .Include(p => p.ClinicalData)
                .Include(p => p.Bed)
                .FirstOrDefaultAsync(p => p.Id == patientId);
            return returnPatient;
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            var returnList = await hospitalContext.Patients.ToListAsync();
            return returnList;
        }

        public async Task InitializeAsync(IEnumerable<(string, long)> departmentNameBedCountTupleList)
        {
            foreach (var departmentCount in departmentNameBedCountTupleList)
            {
                var departmentEntity = await hospitalContext.Department.AddAsync(new Department
                {
                    Name = departmentCount.Item1
                });

                for (long i = 0; i < departmentCount.Item2; i++)
                {
                    await hospitalContext.Beds.AddAsync(new Bed
                    {
                        Department = departmentEntity.Entity,
                        IsOccupied = false
                    });
                }
             await hospitalContext.SaveChangesAsync();
            }
        }

        public async Task<bool> IsInitializedAsync()
        {
            var firstElemnt = await hospitalContext.Beds.Select(b => b.Id).FirstOrDefaultAsync();
            if (firstElemnt == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task ResetInitializationAsync()
        {
            hospitalContext.Beds.RemoveRange(hospitalContext.Beds);
            hospitalContext.Department.RemoveRange(hospitalContext.Department);
            await hospitalContext.SaveChangesAsync();
        }
    }
}
