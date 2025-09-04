using Grace_DAL.Common;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace Grace_cil.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        MenuDAL menu = new MenuDAL();
        // GET: Menu
        [HttpPost]
        public ActionResult GetAllLinkData()
        {
            try
            {
                var role = Session["T_ROLE"].ToString();
                var module = Session["module"].ToString();
                var data = menu.GetAllLinkData(role, module);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}