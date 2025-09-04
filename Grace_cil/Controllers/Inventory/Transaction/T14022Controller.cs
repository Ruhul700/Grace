using Grace_DAL.DAL.Inventory.Transaction;
using Grace_DAL.Shared.Inventory.Transaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace Grace_cil.Controllers.Inventory.Transaction
{
    public class T14022Controller : Controller
    {
        T14022DAL repository = new T14022DAL();
        // GET: T14022
        [HttpPost]
        public ActionResult GetPurchaseVerifyData()
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetPurchaseVerifyData(shopId);
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
        public ActionResult SavePurVerifyData(List<T14020Data> list)
        {
            try
            {
                var user = Session["T_EMP_ID"].ToString();
                var data = repository.SavePurVerifyData(list, user);               
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

       
    }
}