using System.ComponentModel.DataAnnotations;

namespace OrderSystem.PL.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        // Admin Or Customer
        [RegularExpression("Admin|Customer", ErrorMessage ="It Must Only \'Admin\' Or \'Customer\'")]
        public string Role { get; set; }
    }
}
