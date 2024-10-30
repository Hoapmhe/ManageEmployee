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
        [RegularExpression(@"^\d{12}$", ErrorMessage = "CitizenId must consist of exactly 12 digits.")]
        public string CitizenId { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "PhoneNumber must consist of exactly 10 digits.")]
        public string PhoneNumber { get; set; }

        public int? ProvinceId { get; set; }
        public Province? Province { get; set; }

        public int? DistrictId { get; set; }
        public District? District { get; set; }

        public int? CommuneId { get; set; }
        public Commune? Commune { get; set; }

        public string? AddressDetails { get; set; } //địa chỉ cụ thể
        // Collection of Diplomas
        public ICollection<Diploma>? Diplomas { get; set; }

        // Calculated property for total diplomas
        public int TotalDiplomas => Diplomas?.Count ?? 0;

        // Method to get count of valid (non-expired) diplomas
        public int ValidDiplomasCount => Diplomas?.Count(d => d.ExpiryDate == null || d.ExpiryDate > DateTime.Now) ?? 0;
        public bool CanAddDiploma()
        {
            return ValidDiplomasCount < 3;
        }
    }
}
