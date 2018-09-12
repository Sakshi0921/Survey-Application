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
    public class SurveyAnswersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SurveyAnswers
        public ActionResult Index()
        {
            var surveyAnswer = db.SurveyAnswer.Include(s => s.Candidate).Include(s => s.Survey).Include(s => s.userQuestion);
            return View(surveyAnswer.ToList());
        }

        // GET: SurveyAnswers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyAnswer surveyAnswer = db.SurveyAnswer.Find(id);
            if (surveyAnswer == null)
            {
                return HttpNotFound();
            }
            return View(surveyAnswer);
        }

        // GET: SurveyAnswers/Create
        public ActionResult Create()
        {
            ViewBag.CandidateId = new SelectList(db.Candidate, "CandidateId", "Name");
            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyName");
            ViewBag.UQuestionId = new SelectList(db.UserQuestion, "UQuestionId", "Question");
            return View();
        }

        // POST: SurveyAnswers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SurveyId,UQuestionId,CandidateId,SurveyQuesNo,Answer")] SurveyAnswer surveyAnswer)
        {
            if (ModelState.IsValid)
            {
                db.SurveyAnswer.Add(surveyAnswer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CandidateId = new SelectList(db.Candidate, "CandidateId", "Name", surveyAnswer.CandidateId);
            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyName", surveyAnswer.SurveyId);
            ViewBag.UQuestionId = new SelectList(db.UserQuestion, "UQuestionId", "Question", surveyAnswer.UQuestionId);
            return View(surveyAnswer);
        }

        // GET: SurveyAnswers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyAnswer surveyAnswer = db.SurveyAnswer.Find(id);
            if (surveyAnswer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CandidateId = new SelectList(db.Candidate, "CandidateId", "Name", surveyAnswer.CandidateId);
            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyName", surveyAnswer.SurveyId);
            ViewBag.UQuestionId = new SelectList(db.UserQuestion, "UQuestionId", "Question", surveyAnswer.UQuestionId);
            return View(surveyAnswer);
        }

        // POST: SurveyAnswers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SurveyId,UQuestionId,CandidateId,SurveyQuesNo,Answer")] SurveyAnswer surveyAnswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surveyAnswer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CandidateId = new SelectList(db.Candidate, "CandidateId", "Name", surveyAnswer.CandidateId);
            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyName", surveyAnswer.SurveyId);
            ViewBag.UQuestionId = new SelectList(db.UserQuestion, "UQuestionId", "Question", surveyAnswer.UQuestionId);
            return View(surveyAnswer);
        }

        // GET: SurveyAnswers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyAnswer surveyAnswer = db.SurveyAnswer.Find(id);
            if (surveyAnswer == null)
            {
                return HttpNotFound();
            }
            return View(surveyAnswer);
        }

        // POST: SurveyAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SurveyAnswer surveyAnswer = db.SurveyAnswer.Find(id);
            db.SurveyAnswer.Remove(surveyAnswer);
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
