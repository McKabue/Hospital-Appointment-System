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
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Application()
        {
            return View();
        }

        [Route("Medical")]
        public ActionResult Medical()
        {
            return View();
        }
    }
}