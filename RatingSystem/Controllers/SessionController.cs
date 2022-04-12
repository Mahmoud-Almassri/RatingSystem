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
            SessionListVM sessionVM = new SessionListVM();
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
        public IActionResult AddSession()
        {
            return View();
        }
        public IActionResult SubmitAddSession()
        {
            Session session = new Session();
            session.SessionName = Request.Form["SessionName"];
            session.Presenter = Request.Form["PresenterName"];
            _context.Sessions.Add(session);
            _context.SaveChanges();
            return RedirectToAction("SessionDetails", new { userId = Convert.ToInt32(TempData["UserId"]) });
        }
        public IActionResult SessionRate()
        {
            SessionRateVM sessionRateVM = new SessionRateVM();
            List<Rate> sessionRates = _context.Rates.Include(x => x.Rate_Session).Include(x => x.Rate_User).ToList();
            List<Session> Session = _context.Sessions.ToList();
            var sessionRateAvarege = from t in _context.Rates
                       group t by new
                       {
                           t.Session_Id
                       } into g
                       select new
                       {
                           Average = g.Average(p => p.RateDegree),
                           g.Key.Session_Id
                       };
            List<SessionVM> sessionList = new List<SessionVM>();
            foreach (var session in Session)
            {
                SessionVM sessionObj = new SessionVM();
                sessionObj.Id = Convert.ToInt32(session.Id);
                sessionObj.Presenter = session.Presenter;
                sessionObj.SessionName = session.SessionName;
                sessionObj.RateAverage = sessionRateAvarege.Where(x => x.Session_Id == session.Id).Select(x => x.Average).FirstOrDefault();
                sessionList.Add(sessionObj);


            }
            sessionRateVM.SesstionList = sessionList;
            sessionRateVM.Rate = sessionRates;
            
            return View(sessionRateVM);
        }
    }
}
