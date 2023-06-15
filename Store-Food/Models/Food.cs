using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Store_Food.Models
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }
        [Required]
        [StringLength(150)]
        public string? FoodName { get; set; }
        [StringLength(1000)]
        public string? FoodDescription { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Column(TypeName = "decimal(8,2)")]

        public decimal? FoodPrice { get; set; }
        [Column(TypeName = "decimal(2,2)")]
        public decimal? FoodDiscount { get; set; }
        [StringLength(300)]
        public string? FoodPhoto { get; set; }


        [ForeignKey("Size")]
        public int SizeId { get; set; }
        public Size? Size { get; set; }


        [ForeignKey("Color")]
        public int ColorId { get; set; }
        public Color? Color { get; set; }
        public bool IsTrandy { get; set; }
        public bool IsArriver { get; set; }
    }
}
