using Grace_DAL.DAL.Transaction;
using Grace_DAL.Shared.Transaction;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Transaction
{
    public class T18020Controller : Controller
    {
        T18020DAL repository = new T18020DAL();
        // GET: T18020      

        [HttpPost]
        public ActionResult GetClientData(string param)
        {
            try
            {
                var data = repository.GetClientData(param);
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
        public ActionResult SaveData(T18020Data model)
        {
            if (string.IsNullOrEmpty(Session["T_EMP_ID"] as string)) { return Json("Logout-0", JsonRequestBehavior.AllowGet); }
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
    }
}