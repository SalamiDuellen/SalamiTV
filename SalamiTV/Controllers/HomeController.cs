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

        public ActionResult Index(bool showAll = false, int page = 0)
        {
            //var channels = dbContext.TvChannels.ToList();
            if (showAll == false)
            {
                var model = new SearchProgramVM
                {
                    InFocusPrograms = dbContext.TvPrograms.Select(x => x).Where(x => x.IsInFocus == true).ToList(),
                    Page = page,
                    AspNetUserID = User.Identity.GetUserId()
                };
                return View(model);

            }
            else
            {

                var model = new SearchProgramVM
                {
                    InFocusPrograms = dbContext.TvPrograms.Select(x => x).Where(x => x.IsInFocus == true).ToList(),
                    Page = page
                };
                return View(model);
            }
        }


        //GET TvChannels
        [ChildActionOnly]
        public ActionResult PartialTvChannel(string id, int page = 0)
        {
            //TODO: usertablaulistan visas i fel order

            //var userID = HttpContext.User.Identity.GetUserId();
            if (id == null)
            {
                var model = new TemporaryViewModel
                {
                    TvChannels = dbContext.TvChannels.OrderByDescending(x => x.ID).ToList(),
                    Page = page
                };
                return PartialView(model);
            }
            else
            {
                var model = new TemporaryViewModel
                {
                    TvChannels = dbContext.UserTablaus.Where(y => y.AspNetUsersId == id).Select(x => x.TvChannel).ToList(),

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
            .TvPrograms.Where(p => from < p.EndTime && p.Broadcasting < to).OrderBy(x => x.Broadcasting).ToList();

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
