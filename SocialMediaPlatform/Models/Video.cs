using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaPlatform.Models
{
    public class Video
    {
        [Key]
        public int VideoID { get; set; }

        [Required]
        [StringLength(255)]
        public string VideoUrl { get; set; }

        public int? PostID { get; set; }  
        public virtual Post? Post { get; set; }  
    }
}
