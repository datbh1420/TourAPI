using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TourAPI.Models.Request
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }

        public static explicit operator IdentityUser(RegisterRequest request)
        {
            return new IdentityUser
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };
        }
    }
}
