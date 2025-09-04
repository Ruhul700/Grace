using Grace_DAL.DAL.Inventory.Transaction;
using Grace_DAL.Shared.Inventory.Transaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Transaction
{
    public class T14043Controller : Controller
    {
        // GET: T14023
        T14043DAL repository = new T14043DAL();

        [HttpPost]
        public ActionResult GetMemoNo(string param)
        {
            try
            {
                var data = repository.GetMemoNo(param);
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
        public ActionResult GetProductByCode(string param)
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetProductByCode(param, shopId);
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
        public ActionResult GetAllProduct()
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetAllProduct(shopId);
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
        public ActionResult GetMemoDetails(int param)
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetMemoDetails(param);
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
        public ActionResult GetProduct(string param)
        {
            try
            {
                var data = repository.GetProduct(param);
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
        public ActionResult GetPack(string param)
        {
            try
            {
                var data = repository.GetPack(param);
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
        public ActionResult GetPackList()
        {
            try
            {
                var data = repository.GetPackList();
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
        public ActionResult GetTypeData()
        {
            try
            {
                var data = repository.GetTypeData();
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
        public ActionResult GetCustomerDetails()
        {
            try
            {
                var data = repository.GetCustomerDetails();
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
        //[HttpPost]
        //public ActionResult GetSalePrice(string pro, string pack)
        //{
        //    try
        //    {
        //        var data = repository.GetSalePrice(pro, pack);
        //        string JSONString = string.Empty;
        //        JSONString = JsonConvert.SerializeObject(data);
        //        return Json(JSONString, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message, JsonRequestBehavior.AllowGet);
        //    }

        //}

        [HttpPost]
        public ActionResult SaveData(T14040Data model, List<T14041Data> list)
        {
            try
            {
                var shopId = Session["site"].ToString();
                var user = Session["T_EMP_ID"].ToString();
                var data = repository.SaveData(model, list, shopId, user);

                // SendSms(t14010, t14013);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        //[HttpPost]
        //public ActionResult SaveCCNData(T14010Data t14010, T14013Data t14013, List<T14014Data> t14014)
        //{
        //    try
        //    {
        //        var user = Session["T_USER"].ToString();
        //        var data = repository.SaveCCNData(t14010, t14013, t14014, user);
        //        // string JSONString = string.Empty;
        //        //JSONString = JsonConvert.SerializeObject(data);
        //      //  SendSms(t14010, t14013);
        //        return Json(data, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpPost]
        public ActionResult GetMemoList(string param)
        {
            try
            {
                var data = repository.GetMemoList(param);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
    }
}