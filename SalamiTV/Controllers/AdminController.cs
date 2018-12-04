using SalamiTV.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SalamiTV.Controllers
{

    public class AdminController : Controller
    {
        SalamiTVDB dbContex = new SalamiTVDB();

        // GET: Admin
        public ActionResult Index()
        {
            var programs = dbContex.TvPrograms.Select(x => x).GroupBy(x=>x.Title).Select(x=>x.FirstOrDefault()).ToList();

            return View(programs);
        }
        // POST: TvProgram/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Title,Details,Broadcasting,Duration,TvChannelID, IsInFocus")] TvProgram tvProgram)
        {
            if (ModelState.IsValid)
            {
                dbContex.Entry(tvProgram).State = EntityState.Modified;
                await dbContex.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.TvChannelID = new SelectList(dbContex.TvChannels, "ID", "Name", tvProgram.TvChannelID);
            return View(tvProgram);
        //}
        //public ActionResult Edit(int? id)
        //{
        //    var program = dbContex.TvPrograms.Select(x => x).Where(x => x.ID == id);
        //    return View(program);
        //}

    }
}