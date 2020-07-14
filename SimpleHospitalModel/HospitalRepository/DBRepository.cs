using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
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

        public async Task<ClinicalData> AddClinicalDataAsync(ClinicalData clinicalData)
        {
            var returnEntity = await hospitalContext.ClinicalInfomations.AddAsync(clinicalData);
            await hospitalContext.SaveChangesAsync();
            return returnEntity.Entity;
        }

        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            var returnEntity = await hospitalContext.Patients.AddAsync(patient);
            await hospitalContext.SaveChangesAsync();
            return returnEntity.Entity;
        }

        public async Task<ClinicalData> GetClinicalDataAsync(long clinicalDataId)
        {
            return await hospitalContext.ClinicalInfomations.FindAsync(clinicalDataId);
        }

        public async Task<IList<Department>> GetDepartmentsAsync()
        {
            return await hospitalContext.Department.ToListAsync();
        }

        public async Task<Patient> GetPatientDetailsAsync(long patientId)
        {
            var returnPatient = await hospitalContext.Patients
                .Include(p => p.ClinicalData)
                .Include(p => p.Bed)
                .ThenInclude(bed => bed.Department)
                .FirstOrDefaultAsync(p => p.Id == patientId);
            return returnPatient;
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            var returnList = await hospitalContext.Patients.ToListAsync();
            return returnList;
        }

        public async Task<(int AvailableBeds, int AdmissionedPatients)> GetStatusAsync()
        {
            var availableBedCount = await hospitalContext.Beds.Include(bed => bed.Patient).CountAsync(bed => bed.Patient == null);
            var totalBedCount = await hospitalContext.Beds.CountAsync();
            return (availableBedCount, totalBedCount - availableBedCount);
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
                        Department = departmentEntity.Entity
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

        public async Task NewAdmissionAsync(long patientId, long departmentId, string bedNumber, string roomNumber)
        {
            var patient = await hospitalContext.Patients
                .Include(patient => patient.Bed)
                .FirstOrDefaultAsync(patient => patient.Id == patientId);
            if (patient.Bed != null)
            {
                throw new InvalidOperationException("Patient is already admissioned");
            }
            var bed = await hospitalContext.Beds.FirstOrDefaultAsync(
                bed => bed.Patient == null && bed.DepartmentId == departmentId);
            bed.BedNumber = bedNumber;
            bed.RoomNumber = roomNumber;
            patient.BedId = bed.Id;
            hospitalContext.Patients.Update(patient);
            hospitalContext.Beds.Update(bed);
            await hospitalContext.SaveChangesAsync();
        }

        public async Task ReleaseAdmission(long patientId)
        {
            var patient = await hospitalContext.Patients
                .Include(patient => patient.Bed)
                .FirstOrDefaultAsync(patient => patient.Id == patientId);
            if (patient != null && patient.Bed != null)
            {
                var bed = patient.Bed;
                bed.BedNumber = null;
                bed.RoomNumber = null;
                patient.BedId = null;
                hospitalContext.Beds.Update(bed);
                hospitalContext.Patients.Update(patient);
                await hospitalContext.SaveChangesAsync();
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
