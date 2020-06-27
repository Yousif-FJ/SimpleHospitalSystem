using System;
using SimpleHospitalModel.DBModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHospitalModel.HospitalRepository
{
    public interface IHospitalRepository
    {
        Task<IEnumerable<Patient>> GetPatientsAsync();
        Task<Patient> GetPatientDetailsAsync(long patientId);
        Task<Patient> AddPatientAsync(Patient patient);
        Task InitializeAsync(IEnumerable<(string,long)> DepartmentNameBedCountTuples);
        Task ResetInitializationAsync();
        Task<bool> IsInitializedAsync();
    }
}
