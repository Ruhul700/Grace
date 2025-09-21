using Grace_DAL.DAL.Inventory.Transaction;
using Grace_DAL.Shared.Inventory.Transaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Transaction
{
    public class T14042Controller : Controller
    {
        T14042DAL repository = new T14042DAL();
        // GET: T14042
        [HttpPost]
        public ActionResult GetSalesVerifyData()
        {
            try
            {
                var user = Session["T_EMP_ID"].ToString();
                var data = repository.GetSalesVerifyData(user);
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
        public ActionResult SaleUpdateData(List<T14040Data> list)
        {
            try
            {
                var user = Session["T_EMP_ID"].ToString();
                var data = repository.SaleUpdateData(list, user);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}