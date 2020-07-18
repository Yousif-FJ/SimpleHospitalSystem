using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using SimpleHospitalModel.DBModel;
using SimpleHospitalModel.HospitalRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHospitalSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHospitalRepository repository;

        public IEnumerable<Patient> Patients { get; set; }
        public long AdmissionedPatients { get; set; }
        public long AvailableBeds { get; set; }
        public string SearchTerm { get; set; }
        public IndexModel(IHospitalRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> OnGetAsync(string searchTerm = null)
        {
            if (!await repository.IsInitializedAsync())
            {
                return RedirectToPage(pageName: "Initialize");
            }
            Patients = await repository.GetPatientsAsync(searchTerm);
            var status = await repository.GetStatusAsync();
            AdmissionedPatients = status.AdmissionedPatients;
            AvailableBeds = status.AvailableBeds;
            return Page();
        }

        public async Task<IActionResult> OnPostResetAsync()
        {
            await repository.ResetInitializationAsync();
            return RedirectToPage(pageName: "Initialize");
        }

    }
}
