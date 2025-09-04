using Grace_DAL.DAL.Inventory.Setup;
using Grace_DAL.Shared.Inventory.Setup;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Setup
{
    public class T14003Controller : Controller
    {
        // GET: T14003
        T14003DAL repository = new T14003DAL();
        
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
        public ActionResult LoadTypeData()
        {
            try
            {
                var data = repository.LoadTypeData();
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

        public ActionResult Insert(T14003_Img_Ins t14003, HttpPostedFileBase attachment)
        {            
            string base64String = "";
            string mesg = "";
            string fname = "";
            var path = "";
            var user = Session["T_EMP_ID"].ToString();           
            var per = repository.permission(Session["T_ROLE"].ToString(), "T14003", t14003.T_PRODUCT_ID == 0 ? "INS" : "UPD");
            if (!per) { return Json("Have no permission-0", JsonRequestBehavior.AllowGet); }
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        var inputStream = fileContent.InputStream;
                        var fileName = Path.GetFileName(fileContent.FileName);
                        t14003.T_PRODUCT_IMAGE = fileName;
                        path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            inputStream.CopyTo(fileStream);
                        }
                    }
                }

                mesg = repository.SaveData(t14003, user);
            }
            catch (Exception ex)
            {
                mesg = ex.Message;                
            } 
            return Json(mesg, JsonRequestBehavior.AllowGet);
        }
    }
}