using Grace_DAL.DAL.Inventory.Setup;
using Grace_DAL.Shared.Inventory.Setup;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Setup
{
    public class T11101Controller : Controller
    {
        T11101DAL repository = new T11101DAL();
        // GET: T11101
        [HttpPost]
        public ActionResult LoadData()
        {
            try
            {
                var data = repository.LoadData();
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
        public ActionResult SaveData(T11101Data model)
        {
            try
            {
                var data = repository.SaveData(model);
                //string JSONString = string.Empty;
                // JSONString = JsonConvert.SerializeObject(data);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}