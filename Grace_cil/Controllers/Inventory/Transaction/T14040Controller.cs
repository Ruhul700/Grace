using GenCode128;
using Grace_DAL.DAL.Inventory.Transaction;
using Grace_DAL.Shared.Inventory.Transaction;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Mvc;
using static Glorious.Models.ConvertToWord;


namespace Grace_cil.Controllers.Inventory.Transaction
{
    public class T14040Controller : Controller
    {
        T14040DAL repository = new T14040DAL();

        [HttpPost]
        public ActionResult GetMemoNo(string param)
        {
            try
            {
                var data = repository.GetMemoNo(param);
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
        public ActionResult GetProductByCode(string param)
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetProductByCode(param, shopId);
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
        public ActionResult GetMemoList(string param)
        {
            try
            {
                var siteCode = Session["T_SITE_CODE"].ToString();
                var shopId = Session["site"].ToString();
                var data = repository.GetMemoList(param, shopId,siteCode);
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
        public ActionResult GetMemoDetails(int param)
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetMemoDetails(param);
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
        public ActionResult GetAllProduct()
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetAllProduct(shopId);
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
        public ActionResult GetProduct(string param)
        {
            try
            {
                var data = repository.GetProduct(param);
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
        public ActionResult GetPack(string param)
        {
            try
            {
                var data = repository.GetPack(param);
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
        public ActionResult GetPackList()
        {
            try
            {
                var data = repository.GetPackList();
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
        public ActionResult GetTypeData()
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetTypeData(shopId);
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
        public ActionResult GetCustomerDetails()
        {
            try
            {
                var data = repository.GetCustomerDetails();
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
        public ActionResult GetStockData(T14040Parm paramList)
        {
            try
            {
                var shopId = Session["site"].ToString();
                var data = repository.GetStockData(paramList.T_PRODUCT, paramList.T_PACK, shopId);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(data);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //[HttpPost]
        //public ActionResult GetSalePrice(string pro, string pack)
        //{
        //    try
        //    {
        //        var data = repository.GetSalePrice(pro, pack);
        //        string JSONString = string.Empty;
        //        JSONString = JsonConvert.SerializeObject(data);
        //        return Json(JSONString, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message, JsonRequestBehavior.AllowGet);
        //    }

        //}

        [HttpPost]
        public ActionResult SaveData( T14040Data model, List<T14041Data> list)
        {
            try
            {
                var siteCode = Session["T_SITE_CODE"].ToString();
                var shopId = Session["site"].ToString();
                var user = Session["T_EMP_ID"].ToString();
                var outletCode = Session["T_OUTLET_CODE"].ToString();
                var data = repository.SaveData(model, list, shopId, user,siteCode,outletCode);
               
               // SendSms(t14010, t14013);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        //[HttpPost]
        //public ActionResult SaveCCNData(T14010Data t14010, T14013Data t14013, List<T14014Data> t14014)
        //{
        //    try
        //    {
        //        var user = Session["T_USER"].ToString();
        //        var data = repository.SaveCCNData(t14010, t14013, t14014, user);
        //        // string JSONString = string.Empty;
        //        //JSONString = JsonConvert.SerializeObject(data);
        //      //  SendSms(t14010, t14013);
        //        return Json(data, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //public ActionResult GetMemoDat()
        //{
        //    try
        //    {
        //        var data = repository.GetMemoDat();
        //        string JSONString = string.Empty;
        //        JSONString = JsonConvert.SerializeObject(data);
        //        return Json(JSONString, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message, JsonRequestBehavior.AllowGet);
        //    }

        //}
        public ActionResult Invoice(int id,string shopId)
        {
            var data = repository.Invoice(id, shopId);
            string kd = data.Rows[0]["T_MEMO_NO"].ToString();
            string grandTotal = data.Rows[0]["T_GRAND_TOTAL"].ToString();
          string inWords=  NumberToWords(Convert.ToInt32(grandTotal));
             var details = repository.InvoiceDetails(id);
            data.Columns.Add("T_BARCODE", typeof(byte[])); // for adding column into datatable
                                                           // string kd = data.Rows[0]["T_MEMO_NO"].ToString(); // for gettin value from table row           
            //for genarating barcode 
            Image myimg = Code128Rendering.MakeBarcodeImage(kd, int.Parse("2"), true);
           // DataRow dr = data.NewRow();
           // data.Rows.Add(dr);
            data.Rows[0]["T_BARCODE"] = (byte[])new ImageConverter().ConvertTo(myimg, typeof(byte[])); // add value into table row
            ReportViewer rv = new ReportViewer();
            rv.LocalReport.ReportPath = "Reports/RDLC/T14040_Invoice_2.rdlc";
            rv.LocalReport.EnableExternalImages = true;
            // for setting parameter 
            List<ReportParameter> param1 = new List<ReportParameter>();
            param1.Add(new ReportParameter("barcode", "012-66-55"));
            param1.Add(new ReportParameter("InWords", inWords));
            // report.SetParameters(param1);
            // report.Refresh();

            ReportDataSource rds = new ReportDataSource();
            ReportDataSource rddetails = new ReportDataSource();
            rds.Name = "T14040_Inv_Header_2";//This refers to the dataset name in the RDLC file
            rds.Value = data;
            rddetails.Name = "T14040_Invoice_2";//This refers to the dataset name in the RDLC file
            rddetails.Value = details;
            rv.ProcessingMode = ProcessingMode.Local;
            
            rv.LocalReport.DataSources.Add(rds);
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
        public ActionResult ChalanReport(int id, string shopId)
        {
            var data = repository.Invoice(id, shopId);
            string kd = data.Rows[0]["T_MEMO_NO"].ToString();
            string grandTotal = data.Rows[0]["T_GRAND_TOTAL"].ToString();
            string inWords = NumberToWords(Convert.ToInt32(grandTotal));
            var details = repository.InvoiceDetails(id);
            data.Columns.Add("T_BARCODE", typeof(byte[])); // for adding column into datatable
                                                           // string kd = data.Rows[0]["T_MEMO_NO"].ToString(); // for gettin value from table row           
                                                           //for genarating barcode 
            Image myimg = Code128Rendering.MakeBarcodeImage(kd, int.Parse("2"), true);
            // DataRow dr = data.NewRow();
            // data.Rows.Add(dr);
            data.Rows[0]["T_BARCODE"] = (byte[])new ImageConverter().ConvertTo(myimg, typeof(byte[])); // add value into table row
            ReportViewer rv = new ReportViewer();
            rv.LocalReport.ReportPath = "Reports/RDLC/T14040_Chalan.rdlc";
            rv.LocalReport.EnableExternalImages = true;
            // for setting parameter 
            List<ReportParameter> param1 = new List<ReportParameter>();
            param1.Add(new ReportParameter("barcode", "012-66-55"));
            param1.Add(new ReportParameter("InWords", inWords));
            // report.SetParameters(param1);
            // report.Refresh();

            ReportDataSource rds = new ReportDataSource();
            ReportDataSource rddetails = new ReportDataSource();
            rds.Name = "T14040_Chalan_Header";//This refers to the dataset name in the RDLC file
            rds.Value = data;
            rddetails.Name = "T14040_Chalan";//This refers to the dataset name in the RDLC file
            rddetails.Value = details;
            rv.ProcessingMode = ProcessingMode.Local;

            rv.LocalReport.DataSources.Add(rds);
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