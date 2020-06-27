using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SimpleHospitalModel.DBModel;
using SimpleHospitalModel.HospitalRepository;

namespace SimpleHospitalSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHospitalRepository repository;

        public IEnumerable<Patient> Patients { get; set; }
        public IndexModel(IHospitalRepository repository)
        {
            this.repository = repository;
        }

        public async Task OnGetAsync()
        {
            Patients = await repository.GetPatientsAsync();
        }
    }
}
