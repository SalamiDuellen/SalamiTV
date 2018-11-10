using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalamiTV2.Models;
using SalamiTV2.DAL;

namespace SalamiTV2.Controllers
{
    public class TvChannelController : Controller
    {
        private SalamiDB db = new SalamiDB();

        // GET: TvChannels
        public ActionResult Index()
        {
            return View(db.TvChannels.ToList());
        }

        // GET: TvChannels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvChannel tvChannel = db.TvChannels.Find(id);
            if (tvChannel == null)
            {
                return HttpNotFound();
            }
            return View(tvChannel);
        }

        // GET: TvChannels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TvChannels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] TvChannel tvChannel)
        {
            if (ModelState.IsValid)
            {
                db.TvChannels.Add(tvChannel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tvChannel);
        }

        // GET: TvChannels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvChannel tvChannel = db.TvChannels.Find(id);
            if (tvChannel == null)
            {
                return HttpNotFound();
            }
            return View(tvChannel);
        }

        // POST: TvChannels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] TvChannel tvChannel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tvChannel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tvChannel);
        }

        // GET: TvChannels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvChannel tvChannel = db.TvChannels.Find(id);
            if (tvChannel == null)
            {
                return HttpNotFound();
            }
            return View(tvChannel);
        }

        // POST: TvChannels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TvChannel tvChannel = db.TvChannels.Find(id);
            db.TvChannels.Remove(tvChannel);
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
