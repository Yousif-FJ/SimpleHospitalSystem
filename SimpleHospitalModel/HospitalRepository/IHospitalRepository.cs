using System;
using SimpleHospitalModel.DBModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHospitalModel.HospitalRepository
{
    public interface IHospitalRepository
    {
        Task<IEnumerable<Patient>> GetPatientsAsync(string search = null);
        Task<Patient> GetPatientDetailsAsync(long patientId);
        Task<Patient> AddPatientAsync(Patient patient);
        Task InitializeAsync(IEnumerable<(string,long)> departmentNameBedCountTuples);
        Task ResetInitializationAsync();
        Task<bool> IsInitializedAsync();
        Task<(int AvailableBeds, int AdmissionedPatients)> GetStatusAsync();
        Task<IList<Department>> GetDepartmentsAsync();
        Task<IList<Department>> GetDepartmentsWithBedAsync();
        Task<ClinicalData> AddClinicalDataAsync(ClinicalData clinicalData);
        Task<bool> NewAdmissionAsync(long patientId, long departmentId, string bedNumber, string roomNumber);
        Task<ClinicalData> GetClinicalDataAsync(long clinicalDataId);
        Task ReleaseAdmission(long patientId);
    }
}
