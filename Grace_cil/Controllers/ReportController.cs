using Grace_DAL.Common;
using System.Web.Mvc;

namespace Grace_cil.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        T10000DAL repository = new T10000DAL();
        // GET: Report
        public ActionResult R14041()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("R14041", role);
                if (data.Rows.Count > 0)
                {
                    return View();
                }
                else
                {
                    Session.Clear();
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                Session.Clear();
                return RedirectToAction("Login", "Login");
            }
        }
        //=========Accounts=========
        public ActionResult AR14001()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var loginCode = Session["T_ROLE"].ToString();
                var data = repository.Parmision("AR14001", loginCode);
                if (data.Rows.Count > 0)
                {
                    return View();
                }
                else
                {
                    Session.Clear();
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                Session.Clear();
                return RedirectToAction("Login", "Login");
            }

        }
        public ActionResult AR14002()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var loginCode = Session["T_ROLE"].ToString();
                var data = repository.Parmision("AR14002", loginCode);
                if (data.Rows.Count > 0)
                {
                    return View();
                }
                else
                {
                    Session.Clear();
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                Session.Clear();
                return RedirectToAction("Login", "Login");
            }
        }
    }
}