using Grace_DAL.DAL.Inventory.Report;
using Grace_DAL.Shared.Inventory.Transaction;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Report
{
    public class R14041Controller : Controller
    {
        R14041DAL repository = new R14041DAL();
        // GET: R14041
        // [HttpPost]
        public ActionResult GetDailySummaryData(T14040Parm paramList)
        {
            try
            {
                var data = repository.GetDailySummaryData(paramList.T_FROM_DATE, paramList.T_TO_DATE);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetDailySummaryReport(string fromDate, string toDate, string shopId)
        {
            var details = repository.GetDailySummaryData(fromDate, toDate);
            // var dueTotal = repository.GetDueTotal(fdate, tdate);
            ReportViewer rv = new ReportViewer();
            rv.LocalReport.ReportPath = "Reports/RDLC/R14041_Daily_Summary.rdlc";
            rv.LocalReport.EnableExternalImages = true;
            // for setting parameter 
            List<ReportParameter> param1 = new List<ReportParameter>();
            param1.Add(new ReportParameter("fdate", fromDate));
            param1.Add(new ReportParameter("tdate", toDate));

            ReportDataSource rddetails = new ReportDataSource();
            rddetails.Name = "R14041_Daily_Summary";//This refers to the dataset name in the RDLC file
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