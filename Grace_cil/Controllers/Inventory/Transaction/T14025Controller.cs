using Grace_DAL.DAL.Inventory.Transaction;
using Grace_DAL.Shared.Inventory.Transaction;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Transaction
{
    public class T14025Controller : Controller
    {
        // GET: T14025
        T14025DAL repository = new T14025DAL();        
        public ActionResult GetPaymentSummery(T14020Parm paramList)
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetPaymentSummery(paramList, shopId);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetPaymentSummeryReport(string fromDate, string toDate, string shopId)
        {
            var details = repository.GetPaymentSummeryReport(fromDate, toDate, shopId);
            // var dueTotal = repository.GetDueTotal(fdate, tdate);
            ReportViewer rv = new ReportViewer();
            rv.LocalReport.ReportPath = "Reports/RDLC/T14025_payment_summary.rdlc";
            rv.LocalReport.EnableExternalImages = true;
            // for setting parameter 
            List<ReportParameter> param1 = new List<ReportParameter>();
            param1.Add(new ReportParameter("fdate", fromDate));
            param1.Add(new ReportParameter("tdate", toDate));

            ReportDataSource rddetails = new ReportDataSource();
            rddetails.Name = "T14025_payment_summary";//This refers to the dataset name in the RDLC file
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
    }
}