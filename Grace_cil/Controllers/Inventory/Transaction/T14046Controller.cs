using Grace_DAL.DAL.Inventory.Transaction;
using Grace_DAL.Shared.Inventory.Transaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Transaction
{
    public class T14046Controller : Controller
    {
        // GET: T14046
        T14046DAL repository = new T14046DAL();
        [HttpPost]
        public ActionResult GetReceiveVerifyData()
        {
            try
            {
                var shopId = Session["site"].ToString();
                var user = Session["T_EMP_ID"].ToString();
                var data = repository.GetReceiveVerifyData(shopId, user);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SaveReceiveVerifyData(List<T14046Data> list)
        {
            try
            {
                var user = Session["T_EMP_ID"].ToString();
                var data = repository.SaveReceiveVerifyData(list, user);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}