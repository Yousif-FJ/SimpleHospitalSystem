using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHospitalModel.DBModel
{
    public class Department
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        public ICollection<Bed> Beds { get; set; }
    }
}
