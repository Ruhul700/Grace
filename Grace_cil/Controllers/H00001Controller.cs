using Grace_DAL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grace_cil.Controllers
{
    public class H00001Controller : Controller
    {
        // GET: H00001
        H00001DAL repository = new H00001DAL();
        [HttpPost]
        public ActionResult PanelPermission(string module)
        {
            try
            {
                //if (string.IsNullOrEmpty(Session["T_ROLE"] as string)) { return UnauthorizedAccessException(); }
                Session["site"] = "1";
                Session["module"] = module;
                var userid = Session["T_EMP_ID"].ToString();
                var roll = Session["T_ROLE"].ToString();
                // var pass = Session["T_PASSWORD"].ToString();
                var reData = repository.PanelPermission(roll, module);
                // string JSONString = string.Empty;
                // JSONString = JsonConvert.SerializeObject(reData);
                return Json(reData, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult GetShopId()
        {
            var shopId = Session["site"].ToString();
            return Json(shopId, JsonRequestBehavior.AllowGet);
        }
    }
}