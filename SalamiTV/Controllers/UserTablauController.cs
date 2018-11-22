using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalamiTV.Models;
using Microsoft.AspNet.Identity;

namespace SalamiTV.Controllers
{
    public class UserTablauController : Controller
    {
        private SalamiTVDB db = new SalamiTVDB();

        // GET: UserTablau
        public async Task<ActionResult> Index()
        {
            var userId = HttpContext.User.Identity.GetUserId();

            var userTablaus = db.UserTablaus.Where(x=>x.AspNetUsersId==userId).Include(u=>u.TvChannel);

            return View(await userTablaus.ToListAsync());
        }

        public ActionResult AddChannelToTablau()
        {

            var channels = db.TvChannels.Select(x => x);

            var userId = HttpContext.User.Identity.GetUserId();// behöver denna vara här? 

            return View(channels.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddChannelToTablau(TvChannel tvChannel)
        {
            /*
             * Fullösningar is da new black!
             * Tar ut en tvChannel från vyn och konverterar den till userTablau som kan skickas till databasen. 
             * Model.IsValid fun
             */
            var userID = HttpContext.User.Identity.GetUserId();
            UserTablau userTablau = new UserTablau()
            {
                TvChannelID = tvChannel.ID,
                AspNetUsersId = HttpContext.User.Identity.GetUserId()
            };

            if (userTablau.AspNetUsersId != null && userTablau.TvChannelID != null)
            {
                db.UserTablaus.Add(userTablau);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var userTablaus = db.UserTablaus.Where(x => x.AspNetUsersId == userID).Include(u => u.TvChannel);


            return View(await userTablaus.ToListAsync());
        }



        // GET: UserTablau/Details/5
        public async Task<ActionResult> Details(UserTablau userTablau)
        {
            if (userTablau.ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userTablau = await db.UserTablaus.FindAsync(userTablau.ID);
            if (userTablau == null)
            {
                return HttpNotFound();
            }
            return View(userTablau);
        }

        // GET: UserTablau/Create
        public ActionResult Create()
        {
            ViewBag.TvChannelID = new SelectList(db.TvChannels, "ID", "Name");
            ViewBag.AspNetUsersId = new SelectList(db.UserInfoes, "ID", "UserName");
            return View();
        }

        // POST: UserTablau/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,TvChannelID,TvChannels,AspNetUsersId")] UserTablau userTablau)
        {
            if (ModelState.IsValid)
            {
                db.UserTablaus.Add(userTablau);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TvChannelID = new SelectList(db.TvChannels, "ID", "Name", userTablau.TvChannelID);
            ViewBag.AspNetUsersId = new SelectList(db.UserInfoes, "ID", "UserName", userTablau.AspNetUsersId);
            return View(userTablau);
        }

        // GET: UserTablau/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTablau userTablau = await db.UserTablaus.FindAsync(id);
            if (userTablau == null)
            {
                return HttpNotFound();
            }
            ViewBag.TvChannelID = new SelectList(db.TvChannels, "ID", "Name", userTablau.TvChannelID);
            ViewBag.AspNetUsersId = new SelectList(db.UserInfoes, "ID", "UserName", userTablau.AspNetUsersId);
            return View(userTablau);
        }

        // POST: UserTablau/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,TvChannelID,TvChannels,AspNetUsersId")] UserTablau userTablau)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userTablau).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TvChannelID = new SelectList(db.TvChannels, "ID", "Name", userTablau.TvChannelID);
            ViewBag.AspNetUsersId = new SelectList(db.UserInfoes, "ID", "UserName", userTablau.AspNetUsersId);
            return View(userTablau);
        }

        // GET: UserTablau/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            //Måste ha ID för usertablau så att hela raden raderas
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTablau userTablau = await db.UserTablaus.FindAsync(id);
            if (userTablau == null)
            {
                return HttpNotFound();
            }
            return View(userTablau);
        }

        // POST: UserTablau/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserTablau userTablau = await db.UserTablaus.FindAsync(id);
            db.UserTablaus.Remove(userTablau);
            await db.SaveChangesAsync();
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
