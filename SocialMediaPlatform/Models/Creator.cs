using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;

namespace SocialMediaPlatform.Models
{
    public class Creator
    {
        [Key]
        public int CreatorID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        public int Followers { get; set; }
        public int Following { get; set; }
        public int NumberOfPost { get; set; }
        public string ProfileImage { get; set; } = string.Empty;

        [Range(0, 150)]
        public int Age { get; set; }

        [StringLength(255)]
        public string PredictionImage { get; set; } = string.Empty;

        [ForeignKey("UserDetails")]
        public int UserDetailsID { get; set; }

        public virtual UserDetails UserDetails { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

        public Creator()
        {
            Followers = 0;
            Following = 0;
            NumberOfPost = 0;
            Posts = new HashSet<Post>();
        }
    }
}
