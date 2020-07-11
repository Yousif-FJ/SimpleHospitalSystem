using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleHospitalModel.DBModel;
using SimpleHospitalModel.HospitalRepository;

namespace SimpleHospitalSystem.Pages.Patients
{
    public class AddPatientModel : PageModel
    {
        private readonly IHospitalRepository repository;

        public AddPatientModel(IHospitalRepository repository)
        {
            this.repository = repository;
        }

        [BindProperty]
        public Patient Patient { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await repository.AddPatientAsync(Patient);
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }

}