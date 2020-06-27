using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHospitalModel.DBModel
{
    public class Patient
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        public int Age { get; set; }
        public ICollection<ClinicalData> ClinicalData { get; set; }
        public Bed Bed { get; set; }
        public long? BedId { get; set; }
    }
}

