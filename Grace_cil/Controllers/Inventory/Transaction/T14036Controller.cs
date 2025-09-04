using Grace_DAL.DAL.Inventory.Transaction;
using Grace_DAL.Shared.Inventory.Transaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Transaction
{
    public class T14036Controller : Controller
    {
        // GET: T14036
        T14036DAL repository = new T14036DAL();
        [HttpPost]
        public ActionResult GetShop()
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetShop(shopId);
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
        public ActionResult GetStockData(T14040Parm paramList)
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetStockData(paramList.T_PRODUCT, paramList.T_PACK, shopId);
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
        public ActionResult SaveData(List<T14036Data> list)
        {
            try
            {
                var shopId = Session["site"].ToString();
                var user = Session["T_EMP_ID"].ToString();
                var data = repository.SaveData(list,shopId,user);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}