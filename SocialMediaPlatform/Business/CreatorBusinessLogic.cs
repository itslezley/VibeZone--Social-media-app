using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace SocialMediaPlatform.Business
{
    public class CreatorBusinessLogic
    {
        public CreatorBusinessLogic()
        {
            
        }
        public bool IsValidPassword(string password)
        {
            var pattern = @"^(?=.*[a-z])(?=.*[A-Z]).{8,}$";
            return Regex.IsMatch(password, pattern);
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedPasswordBytes);
            }
        }
    }
}
