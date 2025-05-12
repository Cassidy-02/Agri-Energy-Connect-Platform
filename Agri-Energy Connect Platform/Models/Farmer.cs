using System.ComponentModel.DataAnnotations;

namespace Agri_Energy_Connect_Platform.Models
{
    public class Farmer
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public required string FullName { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string Phone { get; set; }
        [Required]
        public required string Address { get; set; }
            public ICollection<Product> Products { get; set; }

          
    }
}
