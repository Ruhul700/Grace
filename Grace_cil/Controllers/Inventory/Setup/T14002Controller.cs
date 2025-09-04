using Grace_DAL.DAL.Inventory.Setup;
using Grace_DAL.Shared.Inventory.Setup;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Setup
{
    public class T14002Controller : Controller
    {
        // GET: T14002
        T14002DAL repository = new T14002DAL();
        [HttpPost]
        public ActionResult SaveData(T14002Data model)
        {
            try
            {
                //var role = Session["T_ROLE"].ToString();
                //var mod = model.T_COMPANY_ID == 0 ? "INS" : "UPD";
                var per = repository.permission(Session["T_ROLE"].ToString(), "T14002", model.T_ITEM_ID == 0 ? "INS" : "UPD");
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
    }
}