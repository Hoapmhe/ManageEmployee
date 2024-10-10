using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageEmployee.Models
{
    [Table("Commune")]
    public class Commune
    {
        [Key]
        public int CommuneId { get; set; }
        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string CommuneName { get; set; }
        // Foreign key to District
        public int DistrictId { get; set; }
        public District District { get; set; }
    }

}
