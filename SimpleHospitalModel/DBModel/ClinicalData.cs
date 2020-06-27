using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHospitalModel.DBModel
{
    public class ClinicalData
    {
        [Key]
        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        [MaxLength(10000)]
        public string ChiefComplaint { get; set; }
        [MaxLength(10000)]
        public string History { get; set; }
        [MaxLength(10000)]
        public string Diagnosis { get; set; }
        public Patient Patient { get; set; }
        public long PatientId { get; set; }
    }
}
