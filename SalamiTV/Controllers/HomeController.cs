using Microsoft.AspNet.Identity;
using SalamiTV.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SalamiTV.ViewModels;

namespace SalamiTV.Controllers
{


    public class HomeController : Controller
    {
        SalamiTVDB salamiContext = new SalamiTVDB();

        //public ViewResult Index(DateTime searchDate, DateTime pageDate, int? page)
        //{
        //    // För att skicka med sökdatumet under hela actionen
        //    ViewBag.CurrentDate = searchDate;
        //    var programs = salamiContext.TvChannels.SelectMany(x => x.TvPrograms);

        //    if (!String.IsNullOrEmpty(searchDate.ToString()))
        //    {
        //        programs = programs.Where(x => x.Broadcasting == searchDate);
        //    }

        //    // återkommer till första sidan om page är null.
        //    int pageNumber = (page ?? 1);
        //    if (pageNumber == 1)
        //    {
        //        pageDate = Convert.ToDateTime(DateTime.Now.Day);// ingen aning om detta kan funka
        //    }
        //    else
        //    {
        //        // Är tänkt att utgå från dagens datum och sedan lägga på dagar utefter sökta sidan
        //        pageDate = Convert.ToDateTime(DateTime.Now.AddDays(page.Value));
        //    }

        //    return View();
        //}


        public ActionResult Index(int? page)
        {

            HomePageVM hpVM = new HomePageVM();

            //Sätter pagenumber till 0 om värdet är null
            int pageNumber = (page ?? 0);

            var searchDate = DateTime.Now.AddDays(pageNumber).Date;
            var tomorrow = searchDate.AddDays(1);

            // LazyLoading = false; för att det första statementet måste exikviera för att det ska kunna användas i den andra funktionen.
            salamiContext.Configuration.LazyLoadingEnabled = false;
            hpVM.TvChannels = salamiContext.TvChannels.Select(c => new { c, programs = c.TvPrograms.Where(p => searchDate <= p.Broadcasting && p.Broadcasting < tomorrow) }).ToList().Select(x => x.c).ToList();

            return View(hpVM);
        }

        [ChildActionOnly]
        public async Task<ActionResult> PartialTvChannel(int id)
        {
            var userId = HttpContext.User.Identity.GetUserId();
            //var userTablaus = await salamiContext.UserTablaus.Where(x => x.AspNetUsersId == userId).Include(u => u.TvChannel);

            var channel = await salamiContext.TvChannels.Include(x => x.TvPrograms).FirstOrDefaultAsync(x => x.ID == id).ConfigureAwait(false);
            return PartialView(channel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}