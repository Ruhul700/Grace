using Grace_DAL.Common;
using System.Web.Mvc;

namespace Grace_cil.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        T10000DAL repository = new T10000DAL();        
        public ActionResult T14020()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14020", role);
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
            // return View();
        }
        public ActionResult T14021()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14021", role);
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
        public ActionResult T14022()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14022", role);
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
        public ActionResult T14023()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14023", role);
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
        public ActionResult T14024()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14024", role);
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
        public ActionResult T14025()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14025", role);
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
        public ActionResult T14035()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14035", role);
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
        public ActionResult T14037()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14037", role);
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
        public ActionResult T14038()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14038", role);
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
        public ActionResult T16020()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T16020", role);
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
        public ActionResult T14039()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14039", role);
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
        public ActionResult T14040()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14040", role);
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
        public ActionResult T14041()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14041", role);
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
        public ActionResult T14042()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14042", role);
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
        public ActionResult T14043()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14043", role);
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
        public ActionResult T14044()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14044", role);
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
        public ActionResult T14045()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14045", role);
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
        public ActionResult T14046()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14046", role);
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
        public ActionResult T14047()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14047", role);
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
        public ActionResult T14048()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14048", role);
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
        public ActionResult T14049()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14049", role);
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
        public ActionResult T11019()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T11019", role);
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
        public ActionResult T11020()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T11020", role);
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
        public ActionResult T11021()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T11021", role);
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
        public ActionResult T11022()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T11022", role);
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
        public ActionResult T11023()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T11023", role);
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
        public ActionResult T14076()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14076", role);
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
        public ActionResult T14077()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14077", role);
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
        public ActionResult T18020()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T18020", role);
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
        public ActionResult T18021()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T18021", role);
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
        //=========Accounts============
        public ActionResult AT13001() //voucher entry
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("AT13001", role);
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
        public ActionResult AT13002() // voucher verify
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("AT13002", role);
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
        public ActionResult AT13003() // voucher posting
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("AT13003", role);
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
        public ActionResult AT13004() // voucher list
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("AT13004", role);
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
        public ActionResult AT13005() // closing Stock Entry
        {
            if (!string.IsNullOrEmpty(Session["LOGIN_CODE"] as string))
            {
                var loginCode = Session["LOGIN_CODE"].ToString();
                var data = repository.Parmision("AT13005", loginCode);
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