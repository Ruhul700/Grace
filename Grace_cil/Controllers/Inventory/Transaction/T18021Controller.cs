using Grace_DAL.DAL.Transaction;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Transaction
{
    public class T18021Controller : Controller
    {
        T18021DAL repository = new T18021DAL();       

        // GET: T18021
        public ActionResult GetChatSummeryData()
        {
            try
            {
                var data = repository.GetChatSummeryData();
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetChatListByMobile(string param)
        {
            try
            {
                var data = repository.GetChatListByMobile(param);
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