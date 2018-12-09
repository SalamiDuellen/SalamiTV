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

        //public ActionResult Index2(int? page)
        //{
        //    SearchProgramVM spVM = new SearchProgramVM();
        //    var userId = HttpContext.User.Identity.GetUserId();


        //    //Sätter pagenumber till 0 om värdet är null
        //    int pageNumber = (page ?? 0);
        //    var searchDate = DateTime.Now.AddDays(pageNumber);
        //    if (pageNumber != 0)
        //    {
        //        searchDate = DateTime.Now.AddDays(pageNumber).Date;
        //    }
        //    var tomorrow = searchDate.AddDays(1).Date;

        //    var newContext = new SalamiTVDB();
        //    //Hämtar higlightade program (aka puffar)
        //    spVM.HighlightedProgram = newContext.TvChannels.SelectMany(x => x.TvPrograms).Where(x => x.IsInFocus == true).ToList();

        //    // LazyLoading = false; för att det första statementet måste exikviera för att det ska kunna användas i den andra funktionen.
        //    dbContext.Configuration.LazyLoadingEnabled = false;
        //    spVM.TvChannels = dbContext.TvChannels.
        //        Select(c => new
        //        {
        //            c,
        //            programs = c.TvPrograms
        //        .Where(p => searchDate <= p.Broadcasting && p.Broadcasting <= tomorrow)
        //        .GroupBy(p => p.Broadcasting) /*ASC == default*/
        //        }).ToList().Select(x => x.c).ToList();


        //    return View(spVM);
        //}

        public ActionResult Index(int page = 0)
        {
            //var channels = dbContext.TvChannels.ToList();
            var model = new TemporaryViewModel
            {
                InFocusPrograms = dbContext.TvPrograms.Select(x => x).Where(x => x.IsInFocus == true).ToList(),
                Page = page
            };
            return View(model);
        }


        //GET TvChannels
        [ChildActionOnly]
        public ActionResult PartialTvChannel(/*int? id, */int page)
        {
            //TODO: måste kunna hämta kanal utefterid. Men som det såg ut förut så skapade  en ny
            //TemporaryViewModel varje ggn vybn loopade igenom listan med id och bara den sista hämtade kanalen syntes.

            var userID = HttpContext.User.Identity.GetUserId();
            if (userID == null)
            {
                var model = new TemporaryViewModel
                {
                    TvChannels = dbContext.TvChannels.ToList(),
                    Page = page
                };
                return PartialView(model);
            }
            else
            {
                var model = new TemporaryViewModel
                {
                    TvChannels = dbContext.UserTablaus.Where(y => y.AspNetUsersId == userID).Select(x => x.TvChannel).ToList(),
                    Page = page
                };
                return PartialView(model);
            }

        }

        //GET TvPrograms
        public ActionResult Partial2(int id, int page = 0)
        {
            var from = DateTime.Now;

            if (page > 0)
            {
                from = DateTime.Today.AddDays(page);
            }

            var to = from.Date.AddDays(1);

            var programs = dbContext.TvChannels
                .Find(id)?
                .TvPrograms.Where(p => from < p.Broadcasting && p.Broadcasting < to)
                .ToList();

            return PartialView("_partialIndex", programs);
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

        //public ActionResult PartialTvChannel(int channelID, int page = 0)
        //{
        //    var tvChannel = dbContext.TvChannels.Where(x => x.ID == channelID).ToList();
        //    return View();
        //}

    }
}


//public class TemporaryViewModel
//{
//    public int Page { get; set; } = 0;
//    public IEnumerable<TvChannel> TvChannels { get; set; }
//}
