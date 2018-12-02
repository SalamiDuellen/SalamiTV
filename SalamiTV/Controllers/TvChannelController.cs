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
    public class TvChannelController : Controller
    {
        private SalamiTVDB salamiContext = new SalamiTVDB();

        //[ChildActionOnly]
        public async Task<ActionResult> PartialTvChannel(int id)
        {
            var channel = await salamiContext.TvChannels.Include(x => x.TvPrograms).FirstOrDefaultAsync(x => x.ID == id).ConfigureAwait(false);
            return PartialView(channel);
        }

        // GET: TvChannel
        public ActionResult Index(int? id)
        {
            var searchDate = DateTime.Now;
            var tomorrow = searchDate.AddDays(1);

            //salamiContext.Configuration.LazyLoadingEnabled = false;
            var channel = salamiContext.TvChannels.Select(c => new { c, programs = c.TvPrograms.Where(p => searchDate <= p.Broadcasting && p.Broadcasting < tomorrow) }).ToList().Where(x => x.c.ID == id).Select(x => x.c).ToList();

            return View(channel);
        }

        // GET: TvChannel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvChannel tvChannel = await salamiContext.TvChannels.FindAsync(id);
            if (tvChannel == null)
            {
                return HttpNotFound();
            }
            return View(tvChannel);
        }

        // GET: TvChannel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TvChannel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name")] TvChannel tvChannel)
        {
            if (ModelState.IsValid)
            {
                salamiContext.TvChannels.Add(tvChannel);
                await salamiContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tvChannel);
        }

        // GET: TvChannel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvChannel tvChannel = await salamiContext.TvChannels.FindAsync(id);
            if (tvChannel == null)
            {
                return HttpNotFound();
            }
            return View(tvChannel);
        }

        // POST: TvChannel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name")] TvChannel tvChannel)
        {
            if (ModelState.IsValid)
            {
                salamiContext.Entry(tvChannel).State = EntityState.Modified;
                await salamiContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tvChannel);
        }

        // GET: TvChannel/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvChannel tvChannel = await salamiContext.TvChannels.FindAsync(id);
            if (tvChannel == null)
            {
                return HttpNotFound();
            }
            return View(tvChannel);
        }

        // POST: TvChannel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TvChannel tvChannel = await salamiContext.TvChannels.FindAsync(id);
            salamiContext.TvChannels.Remove(tvChannel);
            await salamiContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                salamiContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
