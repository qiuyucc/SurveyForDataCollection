using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using chensFyp.Models;

namespace chensFyp.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        
        ApplicationDbContext _db;

        public ReportsController()
        {
            _db = new ApplicationDbContext();
        }
      

        [HttpGet]
        public ActionResult Index()
        {
            var surveys = _db.Surveys.ToList();
            return View(surveys);
        }

        [HttpGet]
        public ActionResult Details(int id, int? departmentId, DateTime? startDate, DateTime? endDate)
        {
            var questions = new List<QuestionViewModel>();
            startDate = startDate ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            endDate = endDate ?? DateTime.Now;

            var survey = _db.Surveys.Single(s => s.Id == id);

            _db.Questions
               .Where(q => q.SurveyId == id)
               .OrderBy(q => q.Priority)
               .Select(q => new
               {
                   q.Title,
                   q.Body,
                   q.Type,
                   Answers = _db.Answers.Where(a => a.QuestionId == q.Id &&
                                                    a.Response.CreatedOn >= startDate.Value &&
                                                    a.Response.CreatedOn <= endDate.Value)
               })
               .ToList()
               .ForEach(r => questions.Add(new QuestionViewModel
               {
                   Title = r.Title,
                   Body = r.Body,
                   Type = r.Type,
                   Answers = r.Answers.ToList()
               }));

            var vm = new ReportViewModel
            {
                StartDate = startDate.Value,
                EndDate = endDate.Value,
                Survey = survey,
                Responses = questions
            };

            return View(vm);
        }
    }
}
