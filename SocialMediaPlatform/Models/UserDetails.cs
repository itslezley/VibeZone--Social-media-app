using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaPlatform.Models
{
    public class UserDetails
    {
        [Key]
        public int UserDetailsID { get; set; }

        [Required(ErrorMessage = "name is required")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(100)]
        public string Surname { get; set; } = string.Empty;

        [Required(ErrorMessage = "User is required")]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100)]
        public string UserPassword { get; set; } = string.Empty;

        [NotMapped]  // This prevents the ConfirmPassword field from being persisted in the database.
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Compare("UserPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
