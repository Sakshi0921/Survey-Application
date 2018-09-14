using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SurveyApp.Models;

namespace SurveyApp.Controllers
{
    
    public class SurveysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Candidate")]
        public ActionResult SurveyList()
        {
            string useremail = User.Identity.GetUserName();
            var org = db.Candidate.Select(c => c.Email == useremail);
            var surveys = db.Surveys.Include(s => s.Organization);
            return View(surveys.ToList());
        }


        [Authorize(Roles = "Admin")]
        // GET: Surveys
        public ActionResult Index()
        {
            var surveys = db.Surveys.Include(s => s.Organization);
            return View(surveys.ToList());
        }
        [Authorize(Roles = "Admin")]
        // GET: Surveys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }
        [Authorize(Roles = "Admin")]
        // GET: Surveys/Create
        public ActionResult Create()
        {
            ViewBag.OrgId = new SelectList(db.Organization, "OrgId", "OrgName");
            return View();
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SurveyId,StartDate,EndDate,SurveyDesc,OrgId")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Surveys.Add(survey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrgId = new SelectList(db.Organization, "OrgId", "OrgName", survey.OrgId);
            return View(survey);
        }


        [Authorize(Roles = "Admin")]

        // GET: Surveys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrgId = new SelectList(db.Organization, "OrgId", "OrgName", survey.OrgId);
            return View(survey);
        }


        [Authorize(Roles = "Admin")]

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SurveyId,StartDate,EndDate,SurveyDesc,OrgId")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrgId = new SelectList(db.Organization, "OrgId", "OrgName", survey.OrgId);
            return View(survey);
        }

        [Authorize(Roles = "Admin")]
        // GET: Surveys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        [Authorize(Roles = "Admin")]

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Survey survey = db.Surveys.Find(id);
            db.Surveys.Remove(survey);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
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
