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
    public class SurveyCandidatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SurveyCandidates
        public ActionResult Index()
        {
            var surveyCandidate = db.SurveyCandidate.Include(s => s.Candidate).Include(s => s.Survey);
            return View(surveyCandidate.ToList());
        }

        // GET: SurveyCandidates/Details/5
        public ActionResult Details(int SurveyId, int CandidateId)
        {
            if (SurveyId == 0 || CandidateId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyCandidate surveyCandidate = db.SurveyCandidate.Find(SurveyId, CandidateId);
           

            if (surveyCandidate == null)
            {
                return HttpNotFound();
            }
          
            return View(surveyCandidate);
        }

        // GET: SurveyCandidates/Create
        public ActionResult Create()
        {
            ViewBag.CandidateId = new SelectList(db.Candidate, "CandidateId", "Name");
            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyName");
            return View();
        }

        // POST: SurveyCandidates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SurveyId,CandidateId")] SurveyCandidate surveyCandidate)
        {
            if (ModelState.IsValid)
            {
                db.SurveyCandidate.Add(surveyCandidate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CandidateId = new SelectList(db.Candidate, "CandidateId", "Name", surveyCandidate.CandidateId);
            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyName", surveyCandidate.SurveyId);
            return View(surveyCandidate);
        }

        // GET: SurveyCandidates/Edit/5
        //way to handle composite primary key
        public ActionResult Edit(int SurveyId, int CandidateId)
        {
            if (SurveyId == 0|| CandidateId==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyCandidate surveyCandidate = db.SurveyCandidate.Find(SurveyId, CandidateId);
            if (surveyCandidate == null)
            {
                return HttpNotFound();
            }
            ViewBag.CandidateId = new SelectList(db.Candidate, "CandidateId", "Name", surveyCandidate.CandidateId);
            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyName", surveyCandidate.SurveyId);
            return View(surveyCandidate);
        }

        // POST: SurveyCandidates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SurveyId,CandidateId")] SurveyCandidate surveyCandidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surveyCandidate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CandidateId = new SelectList(db.Candidate, "CandidateId", "Name", surveyCandidate.CandidateId);
            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyName", surveyCandidate.SurveyId);
            return View(surveyCandidate);
        }

        // GET: SurveyCandidates/Delete/5
        public ActionResult Delete(int SurveyId, int CandidateId)
        {
            if (SurveyId == 0 || CandidateId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyCandidate surveyCandidate = db.SurveyCandidate.Find(SurveyId,CandidateId);
            if (surveyCandidate == null)
            {
                return HttpNotFound();
            }
            return View(surveyCandidate);
        }

        // POST: SurveyCandidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int SurveyId, int CandidateId)
        {
            SurveyCandidate surveyCandidate = db.SurveyCandidate.Find(SurveyId, CandidateId);
            db.SurveyCandidate.Remove(surveyCandidate);
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
