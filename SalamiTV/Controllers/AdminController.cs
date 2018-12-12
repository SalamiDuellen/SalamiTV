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
            var programs = dbContex.TvPrograms.Select(x => x).GroupBy(x => x.Title).Select(x => x.FirstOrDefault()).ToList();

            return View(programs);
        }
    }
}