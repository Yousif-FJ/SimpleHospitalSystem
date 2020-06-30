﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleHospitalModel.DBModel;
using SimpleHospitalModel.HospitalRepository;

namespace SimpleHospitalSystem.Pages.Patients
{
    public class PatientModel : PageModel
    {
        private readonly IHospitalRepository repository;

        public PatientModel(IHospitalRepository repository)
        {
            this.repository = repository;
        }

        public Patient Patient { get; set; }
        public async Task<IActionResult> OnGetAsync(long Id)
        {
            Patient = await repository.GetPatientDetailsAsync(Id);
            return Page();
        }
    }
}