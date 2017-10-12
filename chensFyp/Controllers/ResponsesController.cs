using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using chensFyp.Models;
using Microsoft.AspNet.Identity;
using chensFyp;
using Microsoft.AspNet.Identity.EntityFramework;

namespace chensFyp.Controllers
{
    [Authorize]
    public class ResponsesController : Controller
    {
        ApplicationDbContext _db;

        public ResponsesController()
        {
            _db = new ApplicationDbContext();
        }


        [HttpGet]
        public ActionResult Index(int surveyId)
        {
            var responses = _db.Responses
                               .Include("Survey")
                               .Include("Answers")
                               .Include("Answers.Question")
                               .Where(x => x.SurveyId == surveyId)
                               .Where(x => x.CreatedBy == User.Identity.Name)
                               .OrderByDescending(x => x.CreatedOn)
                               .ThenByDescending(x => x.Id)
                               .ToList();

            return View(responses);
        }

        [HttpGet]
        public ActionResult Details(int surveyId, int id)
        {
            var response = _db.Responses
                              .Include("Survey")
                              .Include("Answers")
                              .Include("Answers.Question")
                              .Where(x => x.SurveyId == surveyId)
                              .Where(x => x.CreatedBy == User.Identity.Name)
                              .Single(x => x.Id == id);

            response.Answers = response.Answers.OrderBy(x => x.Question.Priority).ToList();
            return View(response);
        }

        [HttpGet]
        public ActionResult Create(int surveyId)
        {
            var survey = _db.Surveys
                            .Where(s => s.Id == surveyId)
                            .Select(s => new
                            {
                                Survey = s,
                                Questions = s.Questions
                                                 .Where(q => q.IsActive)
                                                 .OrderBy(q => q.Priority)
                            })
                             .AsEnumerable()
                             .Select(x =>
                             {
                                 x.Survey.Questions = x.Questions.ToList();
                                 return x.Survey;
                             })
                             .Single();

            return View(survey);
        }

        [HttpPost]
        public ActionResult Create(int surveyId, string action, Response model)
        {
            model.Answers = model.Answers.Where(a => !String.IsNullOrEmpty(a.Value)).ToList();
            model.SurveyId = surveyId;
            model.CreatedBy = User.Identity.Name;
            model.CreatedOn = DateTime.Now;
            //var manager = context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //ApplicationUser model2 =UserManager.FindById(User.Identity.GetUserId());
            //model2.point += 3;
            //var result = await ApplicationUserManager.UpdateAsync(User)

            //var manager = context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var currentUserId = User.Identity.GetUserId();
            var manager1 = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager1.FindById(User.Identity.GetUserId());
            currentUser.point += 3;
            manager1.Update(currentUser);

            _db.Responses.Add(model);
            _db.SaveChanges();
              
            TempData["success"] = "Your response was successfully saved! Get 3 points";

            return action == "Next"
                       ? RedirectToAction("Create", new { surveyId })
                       : RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Delete(int surveyId, int id, string returnTo)
        {
            var response = new Response() { Id = id, SurveyId = surveyId };
            _db.Entry(response).State = EntityState.Deleted;
            _db.SaveChanges();
            return Redirect(returnTo ?? Url.RouteUrl("Root"));
        }
    }
}