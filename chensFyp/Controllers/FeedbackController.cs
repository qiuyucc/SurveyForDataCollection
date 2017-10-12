using chensFyp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace chensFyp.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: Feedback
        ApplicationDbContext context;
        public FeedbackController()
        {
            context = new ApplicationDbContext();
        }
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            return View(context.Feedbacks.ToList());
        }
        [Authorize(Roles ="Member")]
        public ActionResult Create()
        {
            FeedbackViewModel model = new FeedbackViewModel();
            model.Answers = Common.GetAnswers();
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult> Create(FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {

                context.Feedbacks.Add(new Feedback() { Answer = model.Select, Comment = model.Comment, Email = model.Email, UserName = model.UserName });
                await context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            model.Answers = Common.GetAnswers();
            return View(model);
        }
    }
}