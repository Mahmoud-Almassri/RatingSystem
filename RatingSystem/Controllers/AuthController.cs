using Microsoft.AspNetCore.Mvc;
using RatingSystem.Context;
using RatingSystem.Models;

namespace RatingSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly RatingSystemContext _context;

        public AuthController(RatingSystemContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult RegistrationSubmit(User user)
        {
            List<User> users = _context.Users.ToList();
            var usersCheck = users.Find(x => x.UserName == user.UserName);
            if (usersCheck != null)
            {
                return View("Registration");
            }
            user.UserRole = String.Empty;
            _context.Users.Add(user);
            _context.SaveChanges();
            var credinital = _context.Users.Select(x => new { x.UserName, x.Password, x.Id }).Where(x => x.UserName == user.UserName).FirstOrDefault();
            return RedirectToAction("SessionDetails", "Session", new { userId = credinital.Id });
        }
        public IActionResult Submit(User user)
        {
             var credinital = _context.Users.Select(x => new { x.UserName, x.Password, x.Id }).Where(x => x.UserName == user.UserName).FirstOrDefault();
            if (credinital != null) {
                if (credinital.UserName.ToLower() == user.UserName.ToLower() && credinital.Password == user.Password)
                { return RedirectToAction("SessionDetails", "Session", new { userId = credinital.Id }); }
                else { return View("Login"); }
            }
            else { return View("Login"); }
            
            
        }
    }
}
