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

namespace SalamiTV.Controllers
{
    public class TvProgramController : Controller
    {
        private SalamiTVDB dbContext = new SalamiTVDB();

        // GET: TvProgram
        public async Task<ActionResult> Index()
        {
            var tvPrograms = dbContext.TvPrograms.Include(t => t.TvChannel);
            return View(await tvPrograms.ToListAsync());
        }

        // GET: TvProgram/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvProgram tvProgram = await dbContext.TvPrograms.FindAsync(id);
            if (tvProgram == null)
            {
                return HttpNotFound();
            }
            return View(tvProgram);
        }

        // GET: TvProgram/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            ViewBag.TvChannelID = new SelectList(dbContext.TvChannels, "ID", "Name");
            return View();
        }

        // POST: TvProgram/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Title,Details,Broadcasting,EndTime,TvChannelID ,Duration, IsInFocus")] TvProgram tvProgram)
        {
            if (ModelState.IsValid)
            {
                dbContext.TvPrograms.Add(tvProgram);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TvChannelID = new SelectList(dbContext.TvChannels, "ID", "Name", tvProgram.TvChannelID);
            return View(tvProgram);
        }

        // GET: TvProgram/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvProgram tvProgram = await dbContext.TvPrograms.FindAsync(id);
            if (tvProgram == null)
            {
                return HttpNotFound();
            }
            ViewBag.TvChannelID = new SelectList(dbContext.TvChannels, "ID", "Name", tvProgram.TvChannelID);
            return View(tvProgram);
        }

        // POST: TvProgram/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Title,Details,Broadcasting,EndTime,TvChannelID ,Duration, IsInFocus")] TvProgram tvProgram)
        //public async Task<ActionResult> Edit([Bind(Include = "ID,Title,Details, IsInFocus")] TvProgram tvProgram)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(tvProgram).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TvChannelID = new SelectList(dbContext.TvChannels, "ID", "Name", tvProgram.TvChannelID);
            return View(tvProgram);
        }

        // GET: TvProgram/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvProgram tvProgram = await dbContext.TvPrograms.FindAsync(id);
            if (tvProgram == null)
            {
                return HttpNotFound();
            }
            return View(tvProgram);
        }

        // POST: TvProgram/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TvProgram tvProgram = await dbContext.TvPrograms.FindAsync(id);
            dbContext.TvPrograms.Remove(tvProgram);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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
