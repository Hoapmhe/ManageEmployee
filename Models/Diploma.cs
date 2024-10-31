using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageEmployee.Models
{
    [Table("Diploma")]
    public class Diploma
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime IssuedDate { get; set; }
        [Required]
        public int IssuedByProvinceId { get; set; }
        [ForeignKey(nameof(IssuedByProvinceId))]
        public Province IssuedByProvince { get; set; }
        public DateTime? ExpiryDate { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }
    }
}
