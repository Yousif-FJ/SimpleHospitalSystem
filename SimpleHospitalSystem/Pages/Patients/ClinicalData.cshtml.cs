using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleHospitalModel.DBModel;
using SimpleHospitalModel.HospitalRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHospitalSystem.Pages.Patients
{
    public class ClinicalDataModel : PageModel
    {
        private readonly IHospitalRepository repository;

        public ClinicalDataModel(IHospitalRepository repository)
        {
            this.repository = repository;
        }
        [BindProperty]
        public ClinicalData ClinicalData { get; set; }
        public string PatientName { get; set; }

        public async Task<IActionResult> OnGetAsync(long? patientId = null, long? clinicalDataId = null)
        {
            if (patientId == null)
            {
                return NotFound();
            }

            var patient = await repository.GetPatientAsync(patientId.Value);
            if (patient == null)
            {
                return NotFound();
            }
            PatientName = patient.Name;

            if (clinicalDataId == null)
            {
                ClinicalData = new ClinicalData
                {
                    PatientId = patientId.Value
                };
            }
            else
            {
                ClinicalData = await repository.GetClinicalDataAsync(clinicalDataId.Value);
                if (ClinicalData == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (ModelState.IsValid)
            {
                ClinicalData.DateTime = DateTime.Now;
                await repository.AddClinicalDataAsync(ClinicalData);
                return RedirectToPage("Patient", new { Id = ClinicalData.PatientId });
            }
            return Page();
        }
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (ModelState.IsValid)
            {
                ClinicalData.DateTime = DateTime.Now;
                await repository.UpdateClinicalDataAsync(ClinicalData);
                return RedirectToPage("Patient", new { Id = ClinicalData.PatientId });
            }
            return Page();
        }
    }
}