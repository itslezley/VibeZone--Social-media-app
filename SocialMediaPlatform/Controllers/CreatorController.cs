using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SocialMediaPlatform.Business;
using SocialMediaPlatform.Data;
using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Controllers
{
    public class CreatorController : Controller
    {
        private readonly ILogger<CreatorController> _logger;
        private readonly ApplicationDBContext _dbContext;

        public CreatorController(ILogger<CreatorController> logger, ApplicationDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var serializedUserFromSession = HttpContext.Session.GetString("Creator");
            if (serializedUserFromSession != null)
            {
                var creator = JsonConvert.DeserializeObject<Creator>(serializedUserFromSession);

                return View(creator);
             
            }

            return RedirectToAction("login", "Home"); ;
        }

        [HttpPost]
        public async Task<IActionResult> Profile(IFormFile MediaFile)
        {
            Console.Write("Here");
            Console.WriteLine("Here");

            var serializedUserFromSession = HttpContext.Session.GetString("Creator");
            if (serializedUserFromSession != null)
            {
                var creator = JsonConvert.DeserializeObject<Creator>(serializedUserFromSession);

                if (creator == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                _dbContext.Attach(creator);

                if (MediaFile != null && MediaFile.Length > 0)
                {
                    var fileName = Path.GetFileName(MediaFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image/Profile", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        MediaFile.CopyTo(stream);
                    }

                    var profileImg = "/Image/Profile/" + fileName;
                    creator.ProfileImage = profileImg;
                    _dbContext.Update(creator);
                    await _dbContext.SaveChangesAsync();

                    return View(creator);
                }
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
                return View();
            }
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public IActionResult Video()
        {
            var videos = _dbContext.Videos.ToList();
            return View(videos);
        }


        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();     
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm]Post post, IFormFile MediaFile)
        {


            if (post.Content != null)
            {

                var serializedUserFromSession = HttpContext.Session.GetString("Creator");
                var creator = JsonConvert.DeserializeObject<Creator>(serializedUserFromSession);

                if (creator == null)
                {
                    return RedirectToAction("Login","Home");
                }

                _dbContext.Attach(creator);
                post.Creator = creator;

                if (MediaFile != null && MediaFile.Length > 0)
                {
                    var fileName = Path.GetFileName(MediaFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Posts", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        MediaFile.CopyTo(stream);
                    }


                    var fileExtension = Path.GetExtension(fileName).ToLower();
                    if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == ".jpeg")
                    {
                        post.Photo = new Photo { PhotoUrl = "Posts/" + fileName };
                    }
                    else if (fileExtension == ".mp4" || fileExtension == ".avi" || fileExtension == ".mov")
                    {
                        post.Video = new Video { VideoUrl = "Posts/" + fileName };
                    }
                }

                await _dbContext.Posts.AddAsync(post);
                await _dbContext.SaveChangesAsync();
                TempData["SuccessMessage"] = "Post successfully posted";

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLikesReactions(int id, [FromBody] Post updatedPost)
        {
            var post = _dbContext.Posts.FirstOrDefault(p => p.PostID == id);

            if (post == null)
            {
                return NotFound();
            }

            post.Likes = updatedPost.Likes;
            post.Reactions = updatedPost.Reactions;

            _dbContext.SaveChanges();
            return Ok(post);
        }
    }
}
