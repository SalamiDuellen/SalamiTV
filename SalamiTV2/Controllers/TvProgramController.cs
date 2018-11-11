using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalamiTV2.Models;

namespace SalamiTV2.Controllers
{
    public class TvProgramController : Controller
    {
        private SalamiDB db = new SalamiDB();

        // GET: TvProgram
        public ActionResult Index()
        {
            var tvPrograms = db.TvPrograms.Include(t => t.TvChannel);
            return View(tvPrograms.ToList());
        }

        // GET: TvProgram/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvProgram tvProgram = db.TvPrograms.Find(id);
            if (tvProgram == null)
            {
                return HttpNotFound();
            }
            return View(tvProgram);
        }

        // GET: TvProgram/Create
        public ActionResult Create()
        {
            ViewBag.TvChannelID = new SelectList(db.TvChannels, "ID", "Name");
            return View();
        }

        // POST: TvProgram/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Details,Broadcasting,Duration,TvChannelID")] TvProgram tvProgram)
        {
            if (ModelState.IsValid)
            {
                db.TvPrograms.Add(tvProgram);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TvChannelID = new SelectList(db.TvChannels, "ID", "Name", tvProgram.TvChannelID);
            return View(tvProgram);
        }

        // GET: TvProgram/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvProgram tvProgram = db.TvPrograms.Find(id);
            if (tvProgram == null)
            {
                return HttpNotFound();
            }
            ViewBag.TvChannelID = new SelectList(db.TvChannels, "ID", "Name", tvProgram.TvChannelID);
            return View(tvProgram);
        }

        // POST: TvProgram/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Details,Broadcasting,Duration,TvChannelID")] TvProgram tvProgram)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tvProgram).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TvChannelID = new SelectList(db.TvChannels, "ID", "Name", tvProgram.TvChannelID);
            return View(tvProgram);
        }

        // GET: TvProgram/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvProgram tvProgram = db.TvPrograms.Find(id);
            if (tvProgram == null)
            {
                return HttpNotFound();
            }
            return View(tvProgram);
        }

        // POST: TvProgram/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TvProgram tvProgram = db.TvPrograms.Find(id);
            db.TvPrograms.Remove(tvProgram);
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
