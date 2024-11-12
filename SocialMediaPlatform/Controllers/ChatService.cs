using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SocialMediaPlatform.Data;
using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Controllers
{
    public class ChatService : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ILogger<CreatorController> _logger;

        public ChatService(ILogger<CreatorController> logger, ApplicationDBContext dBContext)
        {
            _logger = logger;
            _dbContext = dBContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Chat()
        {

            var serializedUserFromSession = HttpContext.Session.GetString("Creator");
            if (serializedUserFromSession == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var creator = JsonConvert.DeserializeObject<Creator>(serializedUserFromSession);


            var creatorId = creator.CreatorID;
            var creators = await _dbContext.Creators.Where(c => c.CreatorID != creatorId).ToListAsync();
            return View(creators);
        }



        [HttpGet]
        public IActionResult Message()
        {
            return View();
        }

    }
}
