using Grace_DAL.DAL.Inventory.Transaction;
using Grace_DAL.Shared.Inventory.Transaction;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Transaction
{
    public class T14038Controller : Controller
    {
        T14038DAL repository = new T14038DAL();       
        [HttpPost]
        public ActionResult GetAllTransferStock(T14020Parm paramList)
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetAllTransferStock(paramList.T_FROM_DATE,paramList.T_TO_DATE, shopId);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetTransferStock_Report(string fDate,string tdate, string shopId)
        {
           // var head = repository.GettHeaderData(id, shopId);
            var details = repository.GetAllTransferStock(fDate, tdate, shopId);
            ReportViewer rv = new ReportViewer();
            rv.LocalReport.ReportPath = "Reports/RDLC/T14038_Stock_Transfer.rdlc";
            rv.LocalReport.EnableExternalImages = true;
            // for setting parameter 
            List<ReportParameter> param1 = new List<ReportParameter>();
            param1.Add(new ReportParameter("fdate", fDate));
            param1.Add(new ReportParameter("tdate", tdate));

          //  ReportDataSource rdhead = new ReportDataSource();
            ReportDataSource rddetails = new ReportDataSource();
          //  rdhead.Name = "HeaderData";//This refers to the dataset name in the RDLC file
           // rdhead.Value = head;

            rddetails.Name = "T14038_Stock_Transfer";//This refers to the dataset name in the RDLC file
            rddetails.Value = details;

            rv.ProcessingMode = ProcessingMode.Local;
           // rv.LocalReport.DataSources.Add(rdhead);
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