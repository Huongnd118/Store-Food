using System.ComponentModel.DataAnnotations;

namespace Store_Food.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        
        public string? Name { get; set; }
        [StringLength(50)]
        
        public string? Email { get; set; }
        [StringLength(50)]
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
