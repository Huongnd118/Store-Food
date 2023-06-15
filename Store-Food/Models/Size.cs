using System.ComponentModel.DataAnnotations;

namespace Store_Food.Models
{
    public class Size
    {
        [Key]
        public int SizeId { get; set; }
        [StringLength(15)]
        public string? SizeName { get; set; }
    }
}
