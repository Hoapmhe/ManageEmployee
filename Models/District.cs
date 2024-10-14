using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManageEmployee.Models
{
    [Table("District")]
    public class District
    {
        [Key]
        public int DistrictId { get; set; }
        [Required(ErrorMessage = "Please enter the district name.")]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string DistrictName { get; set; }

        // Foreign key to Province
        [Required]
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
        public ICollection<Commune>? Communes { get; set; }
    }

}
