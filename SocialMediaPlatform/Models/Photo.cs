using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaPlatform.Models
{
    public class Photo
    {
        [Key]
        public int PhotoID { get; set; }

        [Required]
        [StringLength(255)]
        public string PhotoUrl { get; set; } = string.Empty;

        public int? PostID { get; set; }  // Nullable to allow posts without photos
        public virtual Post? Post { get; set; }  // Navigation back to the Post
    }
}
