using Grace_DAL.DAL.Inventory.Transaction;
using Grace_DAL.Shared.Inventory.Transaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Transaction
{
    public class T14024Controller : Controller
    {
        // GET: T14024
        T14024DAL repository = new T14024DAL();
        [HttpPost]
        public ActionResult GetPaymentVerifyData()
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetPaymentVerifyData(shopId);
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
        public ActionResult SavePaymentVerifyData(List<T14024Data> list)
        {
            try
            {
                var user = Session["T_EMP_ID"].ToString();
                var data = repository.SavePaymentVerifyData(list, user);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}