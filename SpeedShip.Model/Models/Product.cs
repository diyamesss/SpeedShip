using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedShip.Model.Models
{
    public class Product
    {
        [Key]
        [Required]
        public long ProductId { get; set; }

        [DisplayName("Product Name")]
        [MaxLength(50)]
        [Required]
        public string ProductName { get; set; } = null!;

        [MaxLength(50)]
        public string? Description { get; set; }

        [Required]
        public double Price { get; set; }

        [DisplayName("Created By")]
        public string? CreatedBy { get; set; }

        [DisplayName("Date Created")]
        public DateTime? DateCreated { get; set; }

        [DisplayName("Updated By")]
        public string? UpdatedBy { get; set; }

        [DisplayName("Date Updated")]
        public DateTime? DateUpdated { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime? DateDeleted { get; set; }

        public bool IsDeleted { get; set; }
    }
}
