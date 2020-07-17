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
    public class NewAdmissionModel : PageModel
    {
        private readonly IHospitalRepository repository;

        public NewAdmissionModel(IHospitalRepository repository)
        {
            this.repository = repository;
        }
        [BindProperty]
        [Required]
        public long? SelectedDepartmentId { get; set; }
        public IList<Department> Departments{ get; set; }
        [Required]
        [BindProperty]
        public long? PatientId { get; set; }
        [MaxLength(32)]
        [BindProperty]
        public string RoomNumber { get; set; }
        [MaxLength(32)]
        [BindProperty]
        public string BedNumber { get; set; }
        public async Task<IActionResult> OnGetAsync(long Id)
        {
            if (await repository.GetPatientAsync(Id) == null)
            {
                return NotFound();
            }
            PatientId = Id;
            Departments = await repository.GetDepartmentsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await repository.NewAdmissionAsync(PatientId.Value, SelectedDepartmentId.Value, BedNumber, RoomNumber);
                if (result == false)
                {
                    ModelState.AddModelError("SelectedDepartmentId", "The selected department is full");
                }
                else
                {
                    return RedirectToPage("Patient", new { Id = PatientId });
                }
            }
            Departments = await repository.GetDepartmentsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostReleaseAsync(long patientId)
        {
            await repository.ReleaseAdmission(patientId);
            return RedirectToPage("Patient", new { Id = patientId });
        }
    }
}