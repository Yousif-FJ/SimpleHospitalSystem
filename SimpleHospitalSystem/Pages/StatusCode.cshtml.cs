using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SimpleHospitalSystem.Pages
{
    public class StatusCodeModel : PageModel
    {
        public int Code { get; set; }
        public void OnGet(int code)
        {
            Code = code;
        }
    }
}