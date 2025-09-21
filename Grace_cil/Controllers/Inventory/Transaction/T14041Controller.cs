using Grace_DAL.DAL.Inventory.Transaction;
using Grace_DAL.Shared.Inventory.Transaction;
using Microsoft.Reporting.WebForms;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Transaction
{
    public class T14041Controller : Controller
    {
        T14041DAL repository = new T14041DAL();
        // GET: T14041

        public ActionResult GetOutletData()
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetOutletData();
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSiteListData()
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetSiteData();
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSaleSummery(T14040Parm paramList)
        {
            try
            {
                var user = Session["T_EMP_ID"].ToString();
                var roleCode = Session["T_ROLE"].ToString();
                var outletCode = Session["T_OUTLET_CODE"].ToString();
                var data = repository.GetSaleSummery(paramList.T_FROM_DATE, paramList.T_TO_DATE,user, roleCode, paramList.T_SITE_CODE, paramList.T_OUTLET_CODE);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaleSummaryReport(string fromDate, string toDate, string shopId)
        {
            var user = Session["T_EMP_ID"].ToString();
            var roleCode = Session["T_ROLE"].ToString();
            var outletCode = Session["T_OUTLET_CODE"].ToString();
            var details = repository.GetSaleSummery(fromDate, toDate);
            // var dueTotal = repository.GetDueTotal(fdate, tdate);
            ReportViewer rv = new ReportViewer();
            rv.LocalReport.ReportPath = "Reports/RDLC/T14041_Sale_Summery.rdlc";
            rv.LocalReport.EnableExternalImages = true;
            // for setting parameter 
            List<ReportParameter> param1 = new List<ReportParameter>();
            param1.Add(new ReportParameter("fdate", fromDate));
            param1.Add(new ReportParameter("tdate", toDate));

            ReportDataSource rddetails = new ReportDataSource();
            rddetails.Name = "T14041_Sale_Summery";//This refers to the dataset name in the RDLC file
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