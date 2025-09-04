using Grace_DAL.DAL.Inventory.Transaction;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Transaction
{
    public class T14077Controller : Controller
       
    {
     T14077DAL repository = new T14077DAL();
    // GET: T14077
    public ActionResult GetProductList()
        {

            try
            {
                // var shopId = Session["site"].ToString();
                var data = repository.GetVoucherList();
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetLedger_Report(string fromDate, string toDate, string shopId)
        {
            var details = repository.GetVoucherList();
            // var dueTotal = repository.GetDueTotal(fdate, tdate);
            ReportViewer rv = new ReportViewer();
            rv.LocalReport.ReportPath = "Reports/RDLC/T14077_Ledger_Report.rdlc";
            rv.LocalReport.EnableExternalImages = true;
            // for setting parameter 
            List<ReportParameter> param1 = new List<ReportParameter>();
            param1.Add(new ReportParameter("fdate", fromDate));
            param1.Add(new ReportParameter("tdate", toDate));

            ReportDataSource rddetails = new ReportDataSource();
            rddetails.Name = "T14077_Leadger_Report";//This refers to the dataset name in the RDLC file
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
        
    

