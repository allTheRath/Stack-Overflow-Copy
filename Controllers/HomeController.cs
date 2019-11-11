using QA_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QA_Project.Controllers
{
    public class HomeController : Controller
    {
        // Moc design implementation.
        Application_Business_Logic application_Business_Logic = new Application_Business_Logic(new Application_DataAccess());
        
        
        
        public ActionResult Index()
        {
            // A list of 10 questions..
            return View();
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