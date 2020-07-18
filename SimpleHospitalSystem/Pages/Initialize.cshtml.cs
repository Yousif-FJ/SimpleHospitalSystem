using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleHospitalModel.HospitalRepository;
using SimpleHospitalSystem.CustomValidationAttribut;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHospitalSystem.Pages
{
    public class InitializeModel : PageModel
    {
        private readonly IHospitalRepository repository;

        public InitializeModel(IHospitalRepository repository)
        {
            this.repository = repository;
        }

        [Required]
        [NotZeroLong]
        [BindProperty]
        public long? NumberOfDepartment { get; set; }
        [Required]
        [BindProperty]
        public IList<DepartmentNameBedCount> DepartmentNameBedCountList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (await repository.IsInitializedAsync())
            {
                return RedirectToPage("Index");
            }

            return Page();
        }

        public IActionResult OnPostContinueAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostInitializeAsync()
        {
            if (ModelState.IsValid)
            {
                await repository.InitializeAsync(DepartmentNameBedCountList.Select(
                    x => (x.DepartmentName, x.BedCount)));
                return RedirectToAction("Index");
            }
            return Page();
        }
    }
    public class DepartmentNameBedCount
    {
        [Required]
        [MaxLength(64)]
        public string DepartmentName { get; set; }
        [Required]
        [NotZeroLong]
        public long BedCount { get; set; }
    }
}