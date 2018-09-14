using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;   
using System.Web;
using System.Web.Mvc;
using SurveyApp.Models;

namespace SurveyApp.Controllers
{
   
       public class UserQuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Admin")]
        // GET: UserQuestions
        public ActionResult Index()
        {
            var userQuestion = db.UserQuestion.Include(u => u.survey);
            return View(userQuestion.ToList());

        }
            //Method to take survey by the candidate

        [HttpGet]
        public ActionResult SurveyQuestions(int sId)
        {
            List<UserQuestion> uq = db.UserQuestion.Where(u => u.SurveyId == sId).ToList();
            return View(uq);
        }



        [Authorize(Roles = "Admin")]
        // GET: UserQuestions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuestion userQuestion = db.UserQuestion.Find(id);
            if (userQuestion == null)
            {
                return HttpNotFound();
            }
            return View(userQuestion);
        }
        [Authorize(Roles = "Admin")]
        // GET: UserQuestions/Create
        public ActionResult Create()
        {
            ViewBag.SurveyIDList = new SelectList(db.Surveys.Select(x=> new { SurveyId = x.SurveyId, SurveyName = x.SurveyName }), "SurveyId", "SurveyName");
            return View();
        }

        //POST: UserQuestions/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.



        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public ActionResult Create(FormCollection formCollection)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            UserQuestion userQuestion = new UserQuestion();
            userQuestion.SurveyId = Int32.Parse(formCollection["SurveyId"]);
            int sqno;
            var qCount = (formCollection.Count - 1)/2;
            if (!db.UserQuestion.Any(u => u.SurveyId == userQuestion.SurveyId))
            {
                sqno = 0;
            }
            else
              sqno = (from s in db.UserQuestion
                                             where s.SurveyId == userQuestion.SurveyId
                                             select s.SurveyQuesNo).Max() ;
            for (int i = 1; i <= qCount; i++)
            {           
                userQuestion.Question = formCollection["Question" + i];
                userQuestion.Type = formCollection["type" + i];
                userQuestion.SurveyQuesNo = sqno + i;
                db.UserQuestion.Add(userQuestion);//Error handling in both the lines needed
                db.SaveChanges();
            }


            return RedirectToAction("Index");
        }




        // GET: UserQuestions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuestion userQuestion = db.UserQuestion.Find(id);
            if (userQuestion == null)
            {
                return HttpNotFound();
            }
            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyName", userQuestion.SurveyId);
            return View(userQuestion);
        }

        // POST: UserQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UQuestionId,SurveyId,SurveyQuesNo,Question,Type")] UserQuestion userQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyName", userQuestion.SurveyId);
            return View(userQuestion);
        }

        [Authorize(Roles = "Admin")]

        // GET: UserQuestions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuestion userQuestion = db.UserQuestion.Find(id);
            if (userQuestion == null)
            {
                return HttpNotFound();
            }
            return View(userQuestion);
        }

        // POST: UserQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserQuestion userQuestion = db.UserQuestion.Find(id);
            db.UserQuestion.Remove(userQuestion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
