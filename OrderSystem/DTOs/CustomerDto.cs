using System.ComponentModel.DataAnnotations;

namespace OrderSystem.PL.DTOs
{
    public class CustomerDto
    {
        [Required]
        public string CustomerName { get; set; }
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; }
    }
}
