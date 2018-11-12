using SalamiTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalamiTV.Controllers
{


    public class HomeController : Controller
    {
        SalamiTVDB salamiContext = new SalamiTVDB();

        public ActionResult Index(TablauViewModel tablauView)
        {
            //List<SelectListItem> list = new List<SelectListItem>();
            ////var pRullar = context.TvChannels.SelectMany(x => x.Programs.Where(y => y.Category.Name == "Vuxenfilm"));
            //foreach (var item in salamiContext.TvPrograms)
            //{
            //}

            //var program = salamiContext.TvProgramCategories.Select(x => x.TvProgram);//Printar tvprogrammen i som finns i programcategories
            var program = salamiContext.TvChannels.Select(x => x);//Printar tvprogrammen i som finns i programcategories

        
            return View(program.ToList());
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