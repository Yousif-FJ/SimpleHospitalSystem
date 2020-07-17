using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [BindProperty]
        public Patient Patient { get; set; }
        [BindProperty]
        public bool EditMode { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id = null)
        {
            if (id == null)
            {
                EditMode = false;
                return Page();
            }
            var patient = await repository.GetPatientAsync(id.Value);
            if (patient == null)
            {
                return NotFound();
            }
            Patient = patient;
            EditMode = true;
            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (ModelState.IsValid)
            {
                var patient = await repository.AddPatientAsync(Patient);
                return RedirectToPage("Patient", new { patient.Id });
            }
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (ModelState.IsValid)
            {
                await repository.UpdatePatientAsync(Patient);
                return RedirectToPage("Patient", new { Patient.Id });
            }
            return Page();
        }
    }

}