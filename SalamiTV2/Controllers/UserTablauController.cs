using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalamiTV2.DAL;


namespace SalamiTV2.Models
{
    public class UserTablausController : Controller
    {

        //Kallas från UserInfoes-vyn
        private SalamiDB db = new SalamiDB();

        // GET: UserTablaus
        public ActionResult Index()
        {
            var userTablaus = db.UserTablaus.Include(u => u.TvChannel).Include(u => u.UserInfo);
            return View(userTablaus.ToList());
        }

        // GET: UserTablaus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTablau userTablau = db.UserTablaus.Find(id);
            if (userTablau == null)
            {
                return HttpNotFound();
            }
            return View(userTablau);
        }

        // GET: UserTablaus/Create
        public ActionResult Create()
        {
            ViewBag.TvChannelID = new SelectList(db.TvChannels, "ID", "Name");
            ViewBag.UserID = new SelectList(db.UserInfoes, "ID", "UserName");
            return View();
        }

        // POST: UserTablaus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TvChannelID,UserID")] UserTablau userTablau)
        {
            if (ModelState.IsValid)
            {
                db.UserTablaus.Add(userTablau);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TvChannelID = new SelectList(db.TvChannels, "ID", "Name", userTablau.TvChannelID);
            ViewBag.UserID = new SelectList(db.UserInfoes, "ID", "UserName", userTablau.UserID);
            return View(userTablau);
        }

        // GET: UserTablaus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTablau userTablau = db.UserTablaus.Find(id);
            if (userTablau == null)
            {
                return HttpNotFound();
            }
            ViewBag.TvChannelID = new SelectList(db.TvChannels, "ID", "Name", userTablau.TvChannelID);
            ViewBag.UserID = new SelectList(db.UserInfoes, "ID", "UserName", userTablau.UserID);
            return View(userTablau);
        }

        // POST: UserTablaus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TvChannelID,UserID")] UserTablau userTablau)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userTablau).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TvChannelID = new SelectList(db.TvChannels, "ID", "Name", userTablau.TvChannelID);
            ViewBag.UserID = new SelectList(db.UserInfoes, "ID", "UserName", userTablau.UserID);
            return View(userTablau);
        }

        // GET: UserTablaus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTablau userTablau = db.UserTablaus.Find(id);
            if (userTablau == null)
            {
                return HttpNotFound();
            }
            return View(userTablau);
        }

        // POST: UserTablaus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserTablau userTablau = db.UserTablaus.Find(id);
            db.UserTablaus.Remove(userTablau);
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
