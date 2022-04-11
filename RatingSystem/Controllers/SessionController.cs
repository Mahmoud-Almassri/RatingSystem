using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RatingSystem.Context;
using RatingSystem.Models;
using RatingSystem.Models.ViewModel;

namespace RatingSystem.Controllers
{
    public class SessionController : Controller
    {
        private readonly RatingSystemContext _context;

        public SessionController(RatingSystemContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SessionDetails(int userId)
        {
            //the userId should come from User Manager or Identity
            TempData["UserId"] = userId;
            SessionVM sessionVM = new SessionVM();
            List<Session> sesstionList = new List<Session>();
            var Rate = (from x in _context.Rates where x.User_Id == userId select x.Session_Id);

            var Session = (from e in _context.Sessions where !Rate.Contains(e.Id) select new { e.Id, e.SessionName, e.Presenter }).ToList();
            foreach (var item in Session)
            {
                Session sessionObj = new Session();
                sessionObj.Id = item.Id;
                sessionObj.SessionName = item.SessionName;
                sessionObj.Presenter = item.Presenter;
                sesstionList.Add(sessionObj);

            }
            sessionVM.SesstionList = sesstionList;
            return View(sessionVM);
        }
        public IActionResult AddRate()
        {
            Rate rate = new Rate();
            // The data should come from model
            rate.Session_Id = Convert.ToInt32(Request.Form["SessionId"]);
            rate.User_Id = Convert.ToInt32(TempData["UserId"]);
            rate.RateDegree = Convert.ToInt32(Request.Form["Rate"]);
            rate.Comment = Request.Form["Comment"];
            _context.Rates.Add(rate);
            _context.SaveChanges();
            return RedirectToAction("SessionDetails", new { userId = rate.User_Id });
        }
    }
}
