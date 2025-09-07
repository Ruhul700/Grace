using Grace_DAL.Common;
using System.Web.Mvc;

namespace Grace_cil.Controllers
{
    public class SetupController : Controller
    {
        // GET: Setup
        T10000DAL repository = new T10000DAL();
        // GET: Setup
        public ActionResult T14001()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14001", role);
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
        public ActionResult T14003()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14003", role);
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

        public ActionResult T11101()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T11101", role);
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
        public ActionResult T11102()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T11102", role);
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
        public ActionResult T11103()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T11103", role);
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
        public ActionResult T11010()
        {
            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T11010", role);
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
        public ActionResult T14002()
        {

            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14002", role);
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
        public ActionResult T14004()
        {

            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14004", role);
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
        public ActionResult T14005()
        {

            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14005", role);
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
        public ActionResult T14006()
        {

            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14006", role);
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
        public ActionResult T14007()
        {

            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14007", role);
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
        public ActionResult T14008()
        {

            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14008", role);
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
        public ActionResult T14070()
        {

            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14070", role);
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
        public ActionResult T14071()
        {

            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14071", role);
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
        public ActionResult T14072()
        {

            if (!string.IsNullOrEmpty(Session["T_ROLE"] as string))
            {
                var role = Session["T_ROLE"].ToString();
                var data = repository.Parmision("T14072", role);
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
        //===========Accounts================
        public ActionResult T12000()
        {
            return View();
        }
        public ActionResult T12001()
        {
            return View();
        }
        public ActionResult AS12005()
        {
            return View();
        }
        public ActionResult AS12006()
        {
            return View();
        }
        public ActionResult AS12007()
        {
            return View();
        }
        public ActionResult AS12008()
        {
            return View();
        }
        public ActionResult AS12009()
        {
            return View();
        }
        public ActionResult AS12010()
        {
            return View();
        }
        public ActionResult AS12011()
        {
            return View();
        }
        public ActionResult AS12012()
        {
            return View();
        }
    }
}