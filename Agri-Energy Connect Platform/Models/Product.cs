using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agri_Energy_Connect_Platform.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime dateTime { get; set; }
       
        //Foreign key to Farmer
        public string FarmerId { get; set; }
        [ForeignKey("FarmerId")]
        public Farmer Farmer { get; set; }
    }
}
