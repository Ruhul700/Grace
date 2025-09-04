using Grace_DAL.DAL.Inventory.Setup;
using Grace_DAL.Shared.Inventory.Setup;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Setup
{
    public class T14004Controller : Controller
    {
        // GET: T14004
        T14004DAL repository = new T14004DAL();
        public ActionResult SaveData(T14004Data model)
        {
            try
            {
                var role = Session["T_ROLE"].ToString();
                var mod = model.T_PACK_ID == 0 ? "INS" : "UPD";
                var per = repository.permission(role, "T14004", mod);
                if (!per) { return Json("Have no permission-0", JsonRequestBehavior.AllowGet); }
                var data = repository.SaveData(model);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
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
        public ActionResult LoadProductData()
        {
            try
            {
                var data = repository.LoadProductData();
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