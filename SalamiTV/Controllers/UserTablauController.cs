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

            var userTablaus = db.UserTablaus.Where(x => x.AspNetUsersId == userId).Include(u => u.TvChannel);
            return View(await userTablaus.ToListAsync());
        }

        // GET: UserTablau/Details/5
        public async Task<ActionResult> Details(int? id)
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
            return View(userTablau);
        }

        // GET: UserTablau/Create
        public ActionResult Create()
        {
            ViewBag.TvChannelID = new SelectList(db.TvChannels, "ID", "Name");
            ViewBag.UserID = new SelectList(db.UserInfoes, "ID", "UserName");
            return View();
        }

        // POST: UserTablau/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,TvChannelID,AspNetUsersId")] UserTablau userTablau)
        {
            if (ModelState.IsValid)
            {
                db.UserTablaus.Add(userTablau);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TvChannelID = new SelectList(db.TvChannels, "ID", "Name", userTablau.TvChannelID);
            ViewBag.UserID = new SelectList(db.UserInfoes, "ID", "UserName", userTablau.AspNetUsersId);
            return View(userTablau);
        }

        // GET: UserTablau/Edit/5
        public async Task<ActionResult> Edit(UserTablauAddViewModel addChannel)
        {
            //Hämtar id för AspNetUsers från databasen
            addChannel.AspNetUsersId = HttpContext.User.Identity.GetUserId();

            if (addChannel.AspNetUsersId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Hämtar userns individuella tablå (kanalerna hen valt)
            addChannel.UserTablau = await db.UserTablaus.Include(y => y.TvChannel).FirstOrDefaultAsync(x => x.AspNetUsersId == addChannel.AspNetUsersId).ConfigureAwait(false);

            //Ska hämta alla kanaler för att användaren ska kunna lägga till dem till sin tablå
            addChannel.AvailibleChannels = db.TvChannels.Select(x => x);
            //Skickar in kanalerna till vyn eftersom det itne finns någon vuymodell för skiten
        
            return View(addChannel);
        }

        // POST: UserTablau/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,TvChannelID,AspNetUsersId")] UserTablau userTablau)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userTablau).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TvChannelID = new SelectList(db.TvChannels, "ID", "Name", userTablau.TvChannelID);
            ViewBag.UserID = new SelectList(db.UserInfoes, "ID", "UserName", userTablau.AspNetUsersId);
            return View(userTablau);
        }

        // GET: UserTablau/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
