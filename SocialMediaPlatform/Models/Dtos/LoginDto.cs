using System.ComponentModel.DataAnnotations;

namespace SocialMediaPlatform.Models.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "User Email is required")]
        public string UserEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100)]
        public string UserPassword { get; set; } = string.Empty;

    }
}
