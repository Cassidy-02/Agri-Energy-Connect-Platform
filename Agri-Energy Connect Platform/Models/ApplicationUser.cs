using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agri_Energy_Connect_Platform.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Role")]
        public required string Role { get; set; }
        public string? FarmerId { get; set; }
        [ForeignKey("FarmerId")]
        public Farmer? Farmer { get; set; }


    }
}
