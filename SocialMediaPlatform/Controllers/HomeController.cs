using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using SocialMediaPlatform.Data;
using System.Text;
using SocialMediaPlatform.Business;
using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Models.Dtos;

namespace SocialMediaPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _dbContext;
        private CreatorBusinessLogic businessLogic = new CreatorBusinessLogic();

        public HomeController(ILogger<HomeController> logger, ApplicationDBContext dbContext)
        {
            _logger = logger;
           _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var creators = await _dbContext.Creators.ToListAsync();
            ViewData["Creators"] = creators;
            var posts = await _dbContext.Posts.Include(p =>p.Photo)
                                              .Include(p => p.Video)
                                              .Where(p => p.PhotoID != null || p.VideoID != null)
                                              .ToListAsync();
            ViewData["Posts"]= posts;

           
            return View();
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto userDetails)
        {
            if (ModelState.IsValid)
            {
                var creator = await _dbContext.Creators.FirstOrDefaultAsync(u => userDetails.UserEmail == userDetails.UserEmail &&
                                     u.UserDetails.UserPassword == businessLogic.HashPassword(userDetails.UserPassword));
                if (creator != null)
                {
                    var serializedUser = JsonConvert.SerializeObject(creator);
                    HttpContext.Session.SetString("Creator", serializedUser);
                    return RedirectToAction("Index");
                }
               
                ModelState.AddModelError("", "User Details are not accurate.");
                return View(userDetails);
            }
            return View(userDetails);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Creator");

            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(Creator creator)
        {

            if (ModelState.IsValid)
            {

                if (businessLogic.IsValidPassword(creator.UserDetails.UserPassword))
                {
                    var existingUser =  _dbContext.Creators.FirstOrDefault(u => u.UserDetails.UserEmail == creator.UserDetails.UserEmail || u.Username == creator.Username);
                    if (existingUser != null)
                    {

                        if (existingUser.Username == creator.Username)
                        {
                            ModelState.AddModelError("Username", "Username already exists.");
                        }
                        if (existingUser.UserDetails.UserEmail == creator.UserDetails.UserEmail)
                        {
                            ModelState.AddModelError("UserDetails.UserEmail", "Email already exists.");
                        }
                        return View(creator);
                    }


                creator.UserDetails.UserPassword=businessLogic.HashPassword(creator.UserDetails.UserPassword);
                var serializedUser = JsonConvert.SerializeObject(creator);
                HttpContext.Session.SetString("Creator", serializedUser);

                return RedirectToAction("CaptureImage");
                }
                else
                {
                    ModelState.AddModelError("UserDetails.UserPassword", "Password must be at least 8 characters long and contain at least one uppercase and one lowercase letter.");
                    return View(creator);
                }
            }
            ModelState.AddModelError("", "Error.");
            return View(creator);
        }


        [HttpGet]
        public IActionResult CaptureImage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CaptureImage(string PedictionImage)
        {
            
            try
            {
                if (!string.IsNullOrEmpty(PedictionImage))
                {
                    var base64Data = PedictionImage.Split(",")[1];
                    byte[] imageBytes = Convert.FromBase64String(base64Data);
                    var tempFileName = $"{Guid.NewGuid()}.png";
                    var tempFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image/temp", tempFileName);
                    System.IO.File.WriteAllBytes(tempFilePath, imageBytes);
                   
                   //Checking if omage contains face
                    bool containsFace = CheckImageForFace(tempFilePath);
                    if (containsFace)
                    {
                        var permanentFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image/Captured_Image", tempFileName);
                        System.IO.File.Move(tempFilePath, permanentFilePath);

                         var serializedUserFromSession = HttpContext.Session.GetString("Creator");
                         var creator = JsonConvert.DeserializeObject<Creator>(serializedUserFromSession);

                         if (creator == null)
                         {
                             return RedirectToAction("Index");
                         }

                        var agePrediction = CallPythonScriptForAgeDetection(permanentFilePath);
                        if (agePrediction > 15)
                        {
                            
                         creator.PredictionImage = permanentFilePath;
                         creator.Age = agePrediction;
                            _dbContext.Creators.Add(creator);
                             _dbContext.SaveChanges();
                            TempData["SuccessMessage"] = "Login successful!,\nYour predicted age is : " + agePrediction;
                            return RedirectToAction("Login");
                        }
                        else
                        {
                            HttpContext.Session.Clear();
                            return RedirectToAction("Blocked");
                        }
                        
                        
                    }
                    else
                    {
                        System.IO.File.Delete(tempFilePath);
                        ModelState.AddModelError("", "The uploaded image does not contain a face. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the image.");
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
            }

            return View();
    }

        [HttpGet]
        public IActionResult Blocked()
        {
            return View();
        }

    public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private int CallPythonScriptForAgeDetection(string imagePath)
{
  
    var startInfo = new ProcessStartInfo
    {
        FileName = "python", // Ensure Python is in your system's PATH
        Arguments = $"Python_Models/ImageProcessing.py \"{imagePath}\"",
        RedirectStandardOutput = true,
        RedirectStandardError = true, // Capture errors from the script
        UseShellExecute = false,
        CreateNoWindow = true
    };

    try
    {
        using (var process = Process.Start(startInfo))
        {
            using (var reader = process.StandardOutput)
            {
            string result = reader.ReadToEnd().Trim(); // Trim to remove any extra spaces or newlines
            string errorResult = process.StandardError.ReadToEnd().Trim(); // Read any error output
            Console.Write($"\nRaw Python Output: {result}");
            Console.Write($"\nRaw Python Error Output: {errorResult}"); // Log error output for debugging


    // Attempt to parse the output
                if (int.TryParse(result.Trim(), out int agePrediction))
                {
                    return agePrediction;
                }
                else
                {
                    // Log if the result cannot be parsed
                    Console.WriteLine("Failed to parse Python output as integer.");
                    return -1; // Return -1 for errors or unexpected output
                }
            }
                // Capture and log errors from Python
    using (var errorReader = process.StandardError)
    {
        string errorResult = errorReader.ReadToEnd();
        if (!string.IsNullOrEmpty(errorResult))
        {
            Console.WriteLine("Python Error Output: " + errorResult);  // Log any errors from the Python script
        }
    }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error running Python script: " + ex.Message);
        return -1; // Return -1 for exceptions
    }
}

        private bool CheckImageForFace(string imagePath)
        {
          
            string pythonScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Python_Models", "facedetection.py");
            // This method will call a Python script to check if the image contains a face
            var start = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"{pythonScriptPath} {imagePath}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
                
            };

            using (var process = Process.Start(start))
            {
                using (var reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    process.WaitForExit();

                    return result.Trim().ToLower() == "true";
                }
            }
        }
    }
}
