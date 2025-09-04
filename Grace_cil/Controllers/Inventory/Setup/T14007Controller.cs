using Grace_DAL.DAL.Inventory.Setup;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Setup
{
    public class T14007Controller : Controller
    {
        T14007DAL repository = new T14007DAL();
        // GET: T14007
        public ActionResult GetProductList()
        {
            try
            {
               // var shopId = Session["site"].ToString();
                var data = repository.GetProductList();
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetProductList_Report(string shopId)
        {
            var details = repository.GetProductList();
            // var dueTotal = repository.GetDueTotal(fdate, tdate);
            ReportViewer rv = new ReportViewer();

            rv.LocalReport.ReportPath = "Reports/RDLC/T14007_Product_List.rdlc";
            rv.LocalReport.EnableExternalImages = true;
            details.Columns.Add("IMAGE", typeof(string));
            for (var i=0;i< details.Rows.Count;i ++)
            {
                var im = details.Rows[i]["T_PRODUCT_IMAGE"].ToString();
                string path = new Uri(Server.MapPath("~/Images/"+im)).AbsoluteUri;
                details.Rows[i]["IMAGE"] = path;
            }           
            // for setting parameter 
            List<ReportParameter> param1 = new List<ReportParameter>();
            param1.Add(new ReportParameter("fdate", "01-09-2024"));
            param1.Add(new ReportParameter("tdate", "01-09-2024"));         

            ReportDataSource rddetails = new ReportDataSource();
            rddetails.Name = "T14007_Product_List";//This refers to the dataset name in the RDLC file
            rddetails.Value = details;

            rv.ProcessingMode = ProcessingMode.Local;
            rv.LocalReport.DataSources.Add(rddetails);
            rv.LocalReport.EnableHyperlinks = true;
            rv.LocalReport.SetParameters(param1);
            rv.LocalReport.Refresh();

            byte[] streamBytes = null;
            string mimeType = "";
            string encoding = "";
            string filenameExtension = "";
            string[] streamids = null;
            Warning[] warnings = null;

            streamBytes = rv.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            return File(streamBytes, "application/pdf");
        }
        //public ActionResult GetPur_Inv_Details_Report(int id, string shopId)
        //{
        //    var head = repository.GettHeaderData(id, shopId);
        //    var details = repository.GetPurchaseDetails(id);
        //    ReportViewer rv = new ReportViewer();
        //    rv.LocalReport.ReportPath = "Reports/RDLC/Pur_Inv_Details.rdlc";
        //    rv.LocalReport.EnableExternalImages = true;
        //    // for setting parameter 
        //    List<ReportParameter> param1 = new List<ReportParameter>();
        //    param1.Add(new ReportParameter("fdate", "01-01-2024"));
        //    // param1.Add(new ReportParameter("tdate", toDate));

        //    ReportDataSource rdhead = new ReportDataSource();
        //    ReportDataSource rddetails = new ReportDataSource();
        //    rdhead.Name = "HeaderData";//This refers to the dataset name in the RDLC file
        //    rdhead.Value = head;

        //    rddetails.Name = "PurchaseDetails";//This refers to the dataset name in the RDLC file
        //    rddetails.Value = details;

        //    rv.ProcessingMode = ProcessingMode.Local;
        //    rv.LocalReport.DataSources.Add(rdhead);
        //    rv.LocalReport.DataSources.Add(rddetails);
        //    rv.LocalReport.EnableHyperlinks = true;
        //    rv.LocalReport.SetParameters(param1);
        //    rv.LocalReport.Refresh();

        //    byte[] streamBytes = null;
        //    string mimeType = "";
        //    string encoding = "";
        //    string filenameExtension = "";
        //    string[] streamids = null;
        //    Warning[] warnings = null;

        //    streamBytes = rv.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
        //    return File(streamBytes, "application/pdf");
        //}
    }
}
        
    
