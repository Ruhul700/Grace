using Grace_DAL.DAL.Inventory.Transaction;
using Grace_DAL.Shared.Inventory.Transaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Transaction
{
    public class T14044Controller : Controller
    {
        // GET: T14044
        T14044DAL repository = new T14044DAL();

        [HttpPost]
        public ActionResult GetPreOrderData()
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetPreOrderData(shopId);
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
        public ActionResult SaveData(List<T14044Data> list)
        {
            try
            {
                var user = Session["T_EMP_ID"].ToString();
                var shopId = Session["site"].ToString();
                var per = repository.permission(Session["T_ROLE"].ToString(), "T14002", "UPD");
                if (!per) { return Json("Have no permission-0", JsonRequestBehavior.AllowGet); }
                var data = repository.SaveData(list, shopId, user);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult CancelOrder(string param)
        {
            try
            {
                var shopId = Session["site"].ToString();
                var per = repository.permission(Session["T_ROLE"].ToString(), "T14044", "DEL");
                if (!per) { return Json("Have no permission-0", JsonRequestBehavior.AllowGet); }
                var data = repository.CancelOrder(param, shopId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult GetPreOrderDetails(string param)
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetPreOrderDetails(param, shopId);
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
        public ActionResult SavePreOrderData(List<T14044PreOrderData> list_1, List<T14044PreOrderData> list_2)
        {
            try
            {
                var user = Session["T_EMP_ID"].ToString();
                var shopId = Session["site"].ToString();
                var per = repository.permission(Session["T_ROLE"].ToString(), "T14044", "UPD");
                if (!per) { return Json("Have no permission-0", JsonRequestBehavior.AllowGet); }
                var data = repository.SavePreOrderData(list_1, list_2, shopId, user);
                //  string JSONString = string.Empty;
                //JSONString = JsonConvert.SerializeObject(data);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
    }
}