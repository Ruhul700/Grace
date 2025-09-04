using Grace_DAL.DAL.Inventory.Transaction;
using Grace_DAL.Shared.Inventory.Transaction;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Transaction
{
    public class T14039Controller : Controller
    {
        // GET: T14039
        T14039DAL repository = new T14039DAL();
        [HttpPost]
        public ActionResult GetProductByCode(string param)
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetProductByCode(param);
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
        public ActionResult SaveData(T14039Data model)
        {
            try
            {
                var user = Session["T_EMP_ID"].ToString();
                var data = repository.SaveData(model, user);              
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult GetDamageProductList()
        {
            try
            {               
                var data = repository.GetDamageProductList();
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