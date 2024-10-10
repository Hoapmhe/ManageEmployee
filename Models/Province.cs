using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageEmployee.Models
{
    [Table("Province")]
    public class Province
    {
        [Key]
        public int ProvinceId { get; set; }
        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string ProvinceName { get; set; }
        public ICollection<District> Districts { get; set; }

    }
}
