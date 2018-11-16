using Microsoft.AspNet.Identity;
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


    public class HomeController : Controller
    {
        SalamiTVDB salamiContext = new SalamiTVDB();

        public ActionResult Index()
        {

            var program = salamiContext.TvChannels.Select(x => x);//Printar tvprogrammen i som finns i programcategories

            return View(program.ToList());
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