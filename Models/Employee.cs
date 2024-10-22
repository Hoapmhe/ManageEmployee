using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageEmployee.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Range(18, 50, ErrorMessage = "Employee age must be between 18 and 50.")]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DOB.Year;
                if (DOB.Date > today.AddYears(-age)) age--;  
                return age;
            }
            set
            {
                
            }
        }
        public string Ethnicity { get; set; }
        public string Job {  get; set; }
        [Required]
        public string CitizenId { get; set; }
        public string PhoneNumber { get; set; }

    }
}
