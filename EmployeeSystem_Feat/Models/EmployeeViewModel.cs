using System.ComponentModel.DataAnnotations;

namespace EmployeeSystem_Feat.Models
{
    public class EmployeeViewModel
    {
        [Key]
        public int EmpId { get; set; }
        [Required]
        public string EmpName { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Salary cannot be less than 1")]
        public int Salary { get; set; }
        public int Manager { get; set; }
    }
}
