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
using SalamiTV.ViewModels;

namespace SalamiTV.Controllers
{
    public class UserTablauController : Controller
    {
        private SalamiTVDB dbContext = new SalamiTVDB();

        [Authorize]
        public ActionResult Index(int page = 0)
        {
            //var userID = HttpContext.User.Identity.GetUserId();
            var model = new SearchProgramVM
            {
                Page = page,
                AspNetUserID = User.Identity.GetUserId()
            };

            return View(model);

        }//

        public ActionResult AddChannelToTablau()
        {
            var userId = User.Identity.GetUserId();
            var model = new HandleTablauChannels
            {
                AddedTvChannels = dbContext.UserTablaus.Select(x => x).Where(x => x.AspNetUsersId == userId).ToList(),
                AllTvChannels = dbContext.TvChannels.ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddChannelToTablau(TvChannel tvChannel)
        {
            /*
             * Fullösningar is da new black!
             * Tar ut en tvChannel från vyn och konverterar den till userTablau som kan skickas till databasen. 
             * Model.IsValid funkar inte eftersom det är en tvkanal som kommer från vyn men en usertablau som ska in till databasen
             */
            var userID = HttpContext.User.Identity.GetUserId();
            UserTablau userTablau = new UserTablau()
            {
                TvChannelID = tvChannel.ID,
                AspNetUsersId = HttpContext.User.Identity.GetUserId()
            };

            if (userTablau.AspNetUsersId != null && userTablau.TvChannelID != null)
            {
                dbContext.UserTablaus.Add(userTablau);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var userTablaus = dbContext.UserTablaus.Where(x => x.AspNetUsersId == userID).Include(u => u.TvChannel);


            return View(await userTablaus.ToListAsync());
        }
        public ActionResult RemoveChannelFromTablau()
        {
            var userId = User.Identity.GetUserId();
            var model = new HandleTablauChannels
            {
                AddedTvChannels = dbContext.UserTablaus.Select(x => x).Where(x => x.AspNetUsersId == userId).ToList(),
            };
            return View(model);
        }


        // GET: UserTablau/Details/5
        public async Task<ActionResult> Details(UserTablau userTablau)
        {
            if (userTablau.ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userTablau = await dbContext.UserTablaus.FindAsync(userTablau.ID);
            if (userTablau == null)
            {
                return HttpNotFound();
            }
            return View(userTablau);
        }

        // GET: UserTablau/Create
        public ActionResult Create()
        {
            ViewBag.TvChannelID = new SelectList(dbContext.TvChannels, "ID", "Name");
            //ViewBag.AspNetUsersId = new SelectList(dbContext.UserInfoes, "ID", "UserName");
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
                dbContext.UserTablaus.Add(userTablau);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TvChannelID = new SelectList(dbContext.TvChannels, "ID", "Name", userTablau.TvChannelID);
            //ViewBag.AspNetUsersId = new SelectList(dbContext.UserInfoes, "ID", "UserName", userTablau.AspNetUsersId);
            return View(userTablau);
        }

        // GET: UserTablau/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTablau userTablau = await dbContext.UserTablaus.FindAsync(id);
            if (userTablau == null)
            {
                return HttpNotFound();
            }
            ViewBag.TvChannelID = new SelectList(dbContext.TvChannels, "ID", "Name", userTablau.TvChannelID);
            //ViewBag.AspNetUsersId = new SelectList(dbContext.UserInfoes, "ID", "UserName", userTablau.AspNetUsersId);
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
                dbContext.Entry(userTablau).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TvChannelID = new SelectList(dbContext.TvChannels, "ID", "Name", userTablau.TvChannelID);
            //ViewBag.AspNetUsersId = new SelectList(dbContext.UserInfoes, "ID", "UserName", userTablau.AspNetUsersId);
            return View(userTablau);
        }

        // GET: UserTablau/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            //id är PK för usertablau i databasen.Raderar hela raden, kanske skulle haft datum på raden? Skyller på GDPR att man inte ska spara info mer än nöädvändigt
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTablau userTablau = await dbContext.UserTablaus.FindAsync(id);
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
            // Genomför själva raderingen.. Sparar och ätervänder till förstasidan på min sida
            UserTablau userTablau = await dbContext.UserTablaus.FindAsync(id);
            dbContext.UserTablaus.Remove(userTablau);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //rensar querystängen
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
