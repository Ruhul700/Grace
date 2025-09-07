using Grace_DAL.DAL.Inventory.Setup;
using Grace_DAL.Shared.Inventory.Setup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Setup

{
    public class T11010Controller : Controller
    {
        T11010DAL repository = new T11010DAL();
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
        public ActionResult LoadSiteData()
        {
            try
            {
                var data = repository.LoadSiteData();
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
        public ActionResult SaveData(T11010Data model)
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