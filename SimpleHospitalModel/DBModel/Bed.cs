using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHospitalModel.DBModel
{
    public class Bed
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(32)]
        public string RoomNumber { get; set; }
        [MaxLength(32)]
        public string BedNumber { get; set; }
        public Patient Patient { get; set; }
        public Department Department { get; set; }
        public long DepartmentId { get; set; }
    }
}
