using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleHospitalModel.DBModel;
using SimpleHospitalModel.HospitalRepository;

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
        public async Task OnGetAsync()
        {
            Departments = await repository.GetDepartmentsWithBedAsync();
        }
    }
}