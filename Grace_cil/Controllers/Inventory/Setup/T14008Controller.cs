using GenCode128;
using Grace_DAL.DAL.Inventory.Setup;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.Mvc;

namespace Grace_cil.Controllers.Inventory.Setup
{
    public class T14008Controller : Controller
    {
        // GET: T14008
        T14008DAL repository = new T14008DAL();       
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
        public ActionResult GenerateBarCode_Report(string ProCode,int Number,string SalPrice,string ProName)
        {
            DataTable data = new DataTable();           
            data.Columns.Add("T_BARCODE", typeof(byte[])); // for adding column into datatable  
            data.Columns.Add("T_PRO_CODE", typeof(string)); 
            data.Columns.Add("T_SALE_PRICE", typeof(string)); 
            data.Columns.Add("T_PRODUCT_NAME", typeof(string)); 
            for (var i= 0; i < Number; i++)
            {
                DataRow dtRow = data.NewRow();
                data.Rows.InsertAt(dtRow, i);                                                                        
                Image myimg = Code128Rendering.MakeBarcodeImage(ProCode, int.Parse("2"), true);                
                data.Rows[i]["T_BARCODE"] = (byte[])new ImageConverter().ConvertTo(myimg, typeof(byte[])); // add value into table row
                data.Rows[i]["T_PRO_CODE"] = ProCode; // add value into table row
                data.Rows[i]["T_SALE_PRICE"] = SalPrice; // add value into table row
                data.Rows[i]["T_PRODUCT_NAME"] = ProName; // add value into table row
            }
            ReportViewer rv = new ReportViewer();
           // rv.LocalReport.ReportPath = "Reports/RDLC/T14008_GenBarcode.rdlc";           
            rv.LocalReport.ReportPath = "Reports/RDLC/T14008_GenBarcode_2.rdlc";           
            List<ReportParameter> param1 = new List<ReportParameter>();
            param1.Add(new ReportParameter("fdate", "01-09-2024"));           
            param1.Add(new ReportParameter("ProName", "Golden hand ball for child"));           

            ReportDataSource rddetails = new ReportDataSource();
           // rddetails.Name = "T14008_GenBarcode";//This refers to the dataset name in the RDLC file
            rddetails.Name = "T14008_GenBarcode_2";//This refers to the dataset name in the RDLC file
            rddetails.Value = data;

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