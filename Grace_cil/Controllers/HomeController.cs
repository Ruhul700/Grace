using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grace_cil.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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
        public ActionResult H00001()
        {
            return View();
            //if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            //{
            //    var role = Session["T_ROLE"].ToString();
            //    var data = repository.Parmision("H00001", role);
            //    if (data.Rows.Count > 0)
            //    {
            //        return View();
            //    }
            //    else
            //    {
            //        Session.Clear();
            //        return RedirectToAction("Login", "Login");
            //    }
            //}
            //else
            //{
            //    Session.Clear();
            //    return RedirectToAction("Login", "Login");
            //}
        }
    }
}