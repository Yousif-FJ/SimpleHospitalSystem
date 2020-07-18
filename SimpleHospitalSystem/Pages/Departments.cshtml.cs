using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleHospitalModel.DBModel;
using SimpleHospitalModel.HospitalRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHospitalSystem.Pages
{
    public class DepartmentsModel : PageModel
    {
        private readonly IHospitalRepository repository;

        public DepartmentsModel(IHospitalRepository repository)
        {
            this.repository = repository;
        }
        public IList<Department> Departments { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if (!await repository.IsInitializedAsync())
            {
                return RedirectToPage(pageName: "Initialize");
            }
            Departments = await repository.GetDepartmentsWithBedAsync();
            return Page();
        }
    }
}