using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hos.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        // GET: Home
        //[Route("Home")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Book")]
        public ActionResult Book()
        {
            return View();
        }

        [Route("Reschedule")]
        public ActionResult Reschedule()
        {
            return View();
        }
    }
}