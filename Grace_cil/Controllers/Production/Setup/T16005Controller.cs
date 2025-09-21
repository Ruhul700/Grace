using Grace_DAL.DAL.Production.Setup;
using Grace_DAL.Shared.Production.Setup;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Production.Setup

{
    public class T16005Controller : Controller
    {
        T16005DAL repository = new T16005DAL();
        // GET: T11010
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
        public ActionResult SaveData(T16005Data model)
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