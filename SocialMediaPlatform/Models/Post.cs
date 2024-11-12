using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaPlatform.Models
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; } = string.Empty; 

        public int Likes { get; set; }
        public int Reactions { get; set; }

        public int? PhotoID { get; set; }  
        public virtual Photo? Photo { get; set; }  

        public int? VideoID { get; set; }  
        public virtual Video? Video { get; set; }  

        public int CreatorID { get; set; }  
        public virtual Creator Creator { get; set; }  

        public Post()
        {
            Likes = 0;
            Reactions = 0;
        }
    }
}
