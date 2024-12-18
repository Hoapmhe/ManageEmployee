﻿using ManageEmployee.ValidateCustom;
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
        [ValidateDate]
        public DateTime IssuedDate { get; set; }
        [Required]
        public int IssuedByProvinceId { get; set; }
        [ForeignKey(nameof(IssuedByProvinceId))]
        public Province? IssuedByProvince { get; set; }
        [ExpiryDateAfterIssuedDate]
        public DateTime? ExpiryDate { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee { get; set; }
    }
}
