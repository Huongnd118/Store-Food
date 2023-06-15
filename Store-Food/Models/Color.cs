using System.ComponentModel.DataAnnotations;

namespace Store_Food.Models
{
    public class Color
    {
        [Key]
        public int ColorId { get; set; }
        [StringLength(30)]
        public string? ColorName { get; set; }
    }
}
