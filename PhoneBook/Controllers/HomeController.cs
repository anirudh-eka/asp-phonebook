using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /about

        public ActionResult About()
        {
            ViewBag.Message = "Awesomeness";

            return View();
        }

    }
}
