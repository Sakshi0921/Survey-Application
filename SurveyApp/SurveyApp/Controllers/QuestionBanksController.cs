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
    [Authorize(Roles ="SuperAdmin")]
    public class QuestionBanksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuestionBanks
        public ActionResult Index()
        {
            var questionBank = db.QuestionBank.Include(q => q.Category);
            return View(questionBank.ToList());
        }

        // GET: QuestionBanks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionBank questionBank = db.QuestionBank.Find(id);
            if (questionBank == null)
            {
                return HttpNotFound();
            }
            return View(questionBank);
        }

        // GET: QuestionBanks/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName");
            return View();
        }

        // POST: QuestionBanks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionId,Question,Type,CategoryId")] QuestionBank questionBank)
        {
            if (ModelState.IsValid)
            {
                db.QuestionBank.Add(questionBank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName", questionBank.CategoryId);
            return View(questionBank);
        }

        // GET: QuestionBanks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionBank questionBank = db.QuestionBank.Find(id);
            if (questionBank == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName", questionBank.CategoryId);
            return View(questionBank);
        }

        // POST: QuestionBanks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionId,Question,Type,CategoryId")] QuestionBank questionBank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionBank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName", questionBank.CategoryId);
            return View(questionBank);
        }

        // GET: QuestionBanks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionBank questionBank = db.QuestionBank.Find(id);
            if (questionBank == null)
            {
                return HttpNotFound();
            }
            return View(questionBank);
        }

        // POST: QuestionBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionBank questionBank = db.QuestionBank.Find(id);
            db.QuestionBank.Remove(questionBank);
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
