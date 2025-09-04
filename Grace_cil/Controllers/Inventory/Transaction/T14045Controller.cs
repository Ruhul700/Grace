using Grace_DAL.DAL.Inventory.Transaction;
using Grace_DAL.Shared.Inventory.Transaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Transaction
{
    public class T14045Controller : Controller
    {
        // GET: T14045
        T14045DAL repository = new T14045DAL();

        [HttpPost]
        public ActionResult GetSaleReceiveData()
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetSaleReceiveData(shopId);
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
        public ActionResult SaveReceivePayment(List<T14045Data> list)
        {
            try
            {
                var user = Session["T_EMP_ID"].ToString();
                var shopId = Session["site"].ToString();
                var data = repository.SaveReceivePayment(list, user, shopId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}