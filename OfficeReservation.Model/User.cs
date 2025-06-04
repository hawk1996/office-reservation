using System.ComponentModel.DataAnnotations;

namespace OfficeReservation.Model
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Email can't be longer than 50 characters")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(64, MinimumLength = 64, ErrorMessage = "Password must be exactly 64 characters")]
        public string Password { get; set; }
    }
}
