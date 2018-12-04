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
        SalamiTVDB dbContext = new SalamiTVDB();

        public ActionResult Index(int? page)
        {
            HomePageVM hpVM = new HomePageVM();
            var userId = HttpContext.User.Identity.GetUserId();


            //Sätter pagenumber till 0 om värdet är null
            int pageNumber = (page ?? 0);
            var searchDate = DateTime.Now.AddDays(pageNumber);
            if (pageNumber != 0)
            {
                searchDate = DateTime.Now.AddDays(pageNumber).Date;
            }
            var tomorrow = searchDate.AddDays(1).Date;

            var newContext = new SalamiTVDB();
            //Hämtar higlightade program (aka puffar)
            hpVM.HighlightedProgram =  newContext.TvChannels.SelectMany(x => x.TvPrograms).Where(x => x.IsInFocus == true).ToList();

            // LazyLoading = false; för att det första statementet måste exikviera för att det ska kunna användas i den andra funktionen.
            dbContext.Configuration.LazyLoadingEnabled = false;
            hpVM.TvChannels = dbContext.TvChannels.Select(c => new { c, programs = c.TvPrograms.Where(p => searchDate <= p.Broadcasting && p.Broadcasting <= tomorrow).GroupBy(p => p.Broadcasting) /*ASC == default*/ }).ToList().Select(x => x.c).ToList();


            return View(hpVM);
        }

        [ChildActionOnly]
        public async Task<ActionResult> PartialTvChannel(int id)
        {
            var userId = HttpContext.User.Identity.GetUserId();
            //var userTablaus = await salamiContext.UserTablaus.Where(x => x.AspNetUsersId == userId).Include(u => u.TvChannel);

            var channel = await dbContext.TvChannels.Include(x => x.TvPrograms).FirstOrDefaultAsync(x => x.ID == id).ConfigureAwait(false);
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