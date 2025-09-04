using Grace_DAL.DAL.Inventory.Transaction;
using Grace_DAL.Shared.Inventory.Transaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Transaction
{
    public class T14023Controller : Controller
    {
        // GET: T14023
        T14023DAL repository = new T14023DAL();
       
        [HttpPost]
        public ActionResult GetPurchasePaymentData()
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetPurchasePaymentData(shopId);
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
        public ActionResult SavePurPayment(List<T14023Data> list)
        {
            try
            {
                var user = Session["T_EMP_ID"].ToString();
                var shopId = Session["site"].ToString();
                var data = repository.SavePurPayment(list, user,shopId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}