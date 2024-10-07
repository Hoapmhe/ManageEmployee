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
        public DateTime DOB {  get; set; }
        public int Age { get; set; }
        public string Ethnicity { get; set; }
        public string Job {  get; set; }
        [Required]
        public string CitizenId { get; set; }
        public string PhoneNumber { get; set; }

    }
}
