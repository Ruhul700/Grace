using Grace_DAL.Shared.Inventory.Transaction;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Grace_DAL.DAL.Inventory.Transaction
{
    public class T14020DAL: CommonDAL
    {
        public DataTable GetTypeData()
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT T_TYPE_CODE,T_TYPE_NAME FROM T14001 ORDER BY T_TYPE_NAME ASC");
            return sql;
        }
        public DataTable GetProductByCode(string code, string shopId)
        {
            DataTable sql = new DataTable();
            //sql = Query($"SELECT CONCAT(T14003.T_PRODUCT_NAME ,' - ', T14004.T_PACK_NAME,' - ',T14004.T_PACK_WEIGHT) T_PRODUCT_NAME, T14003.T_PRODUCT_CODE, T14005.T_PACK_CODE, T14004.T_PACK_NAME, T_PURCHASE_PRICE, T14004.T_UM FROM T14003 JOIN T14004 ON T14003.T_PRODUCT_CODE = T14004.T_PRODUCT_CODE JOIN T14005 ON T14004.T_PACK_CODE = T14005.T_PACK_CODE WHERE T14003.T_TYPE_CODE ='{type}'");
            return sql;
        }
        public DataTable GetProduct(string type)
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT CONCAT(T14003.T_PRODUCT_NAME ,' - ', T14004.T_PACK_NAME,' - ',T14004.T_PACK_WEIGHT) T_PRODUCT_NAME, T14003.T_PRODUCT_CODE, T14005.T_PACK_CODE,T14004.T_PACK_WEIGHT, T14004.T_PACK_NAME, T_PURCHASE_PRICE, T14004.T_UM FROM T14003 JOIN T14004 ON T14003.T_PRODUCT_CODE = T14004.T_PRODUCT_CODE JOIN T14005 ON T14004.T_PACK_CODE = T14005.T_PACK_CODE WHERE T14003.T_TYPE_CODE ='{type}'");
            return sql;
        }
        public DataTable GetPack(string pro)
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT T14005.T_PACK_CODE,T14004.T_PACK_NAME,T_PURCHASE_PRICE,T14004.T_UM FROM T14005 JOIN T14004 ON T14005.T_PACK_CODE = T14004.T_PACK_CODE WHERE T14005.T_PRODUCT_CODE ='{pro}'");
            return sql;
        }
        public DataTable GetPackList()
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT T_PRODUCT_CODE,T_PACK_CODE,T_PACK_NAME FROM T14004 ");
            return sql;
        }
        public string GetInvoiceNo()
        {
            string fmt = "Inv" + "-00.##";
            // string s = "You win some. You lose some.";
            var date = DateTime.Now.ToString("dd-MM-yyyy");
            string[] subs = date.Split('-');
            var max = Query($"SELECT MAX(T_SQ_NO )T_SQ_NO FROM T14020").Rows[0]["T_SQ_NO"].ToString();
            var max1 = max == "" ? "0" : max;
            int val = Convert.ToInt32(max1);
            int val1 = val == 0 ? 1 : val + 1;
            var invNum = val1.ToString(fmt) + "-" + subs[0] + subs[1] + subs[2];
            return invNum;
        }
        public DataTable GetPurchasePrice(string procuct, string pack)
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT T_PRICE_ID,T_PURCHASE_PRICE FROM T14003 WHERE T_PRODUCT_CODE='{procuct}' AND T_PACK_CODE='{pack}'");
            return sql;
        }
        public string SaveData(T14020Data t14020, List<T14021Data> t14021, string shopId, string user,string siteCode)
        {
            var sms = "";
            SqlTransaction objTrans = null;
            var max = Query($"SELECT MAX(T_SQ_NO )T_SQ_NO FROM T14020").Rows[0]["T_SQ_NO"].ToString();
            var max1 = max == "" ? "0" : max;
            int val = Convert.ToInt32(max1);
            int sqNo = val == 0 ? 1 : val + 1;
            using (SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString))
            {

                try
                {
                    var maxId = "";
                    //----------------------------
                   // var check20 = Query($"select COUNT(*)T_COUNT from T14020 where T_INVOICE_NO='{t14020.T_INVOICE_NO}'").Rows[0]["T_COUNT"].ToString();
                    var check20 = Query($"select COUNT(*)T_COUNT from T14020 where T_INVOICE_NO='{t14020.T_INVOICE_NO}'").Rows[0]["T_COUNT"].ToString();
                    if (Convert.ToInt32(check20) != 0)
                    {
                        var chkStock = "";
                        if (shopId == "1")
                        {
                            chkStock = Query($@"SELECT  CASE WHEN T_1.QUT > T_1.STK THEN '1' ELSE '0' END T_STATUS FROM( SELECT  SUM(CAST(T_QUANTITY AS DECIMAL(12, 2))) QUT,
                        SUM(T_STOCK) STK FROM T14035 WHERE T_PURCHASE_ID = '{t14020.T_PURCHASE_ID}')T_1").Rows[0]["T_STATUS"].ToString();
                        }                        
                        if (chkStock == "1") { return "Update not possible-0"; }
                    }
                    //-------------------------------

                    objConn.Open();
                    objTrans = objConn.BeginTransaction();
                    //----------------------------
                    if (t14020.T_CUSTOMER_ID == null)
                    {
                        var chk = Query_2($" select count(*)T_COUNT from T14010 where T_CUSTOMER_MOBILE='{t14020.T_CUSTOMER_MOBILE}' AND T_FORM_CODE='T14020'", objConn, objTrans).Rows[0]["T_COUNT"].ToString();
                        if (t14020.T_CUSTOMER_MOBILE == null || chk == "0")
                        {
                            var save_10 = $"INSERT INTO T14010 (T_CUSTOMER_NAME,T_CUSTOMER_ADDRESS,T_CUSTOMER_MOBILE,T_FORM_CODE,T_ENTRY_USER,T_ENTRY_DATE)VALUES('{t14020.T_COMPANY_NAME}','{t14020.T_CUSTOMER_ADDRESS}','{t14020.T_CUSTOMER_MOBILE}','T14020','{user}','{t14020.T_ENTRY_DATE}')";
                            SqlCommand objCmd10 = new SqlCommand(save_10, objConn, objTrans);
                            objCmd10.ExecuteNonQuery();

                            SqlCommand command = new SqlCommand("SELECT MAX(T_CUSTOMER_ID)T_CUSTOMER_ID FROM T14010", objConn, objTrans);
                            var sqlDataAdapter = new SqlDataAdapter(command);
                            var dataTable = new DataTable();
                            sqlDataAdapter.Fill(dataTable);
                            maxId = dataTable.Rows[0]["T_CUSTOMER_ID"].ToString();
                        }
                        else
                        {
                            var cId = Query_2($"select T_CUSTOMER_ID from T14010 where T_CUSTOMER_MOBILE='{t14020.T_CUSTOMER_MOBILE}' AND T_FORM_CODE='T14020'", objConn, objTrans).Rows[0]["T_CUSTOMER_ID"].ToString();
                            var save_10 = $"UPDATE  T14010 SET T_CUSTOMER_NAME='{t14020.T_COMPANY_NAME}',T_CUSTOMER_ADDRESS='{t14020.T_CUSTOMER_ADDRESS}' WHERE T_CUSTOMER_MOBILE='{t14020.T_CUSTOMER_MOBILE}' AND T_CUSTOMER_ID='{cId}'";
                            command_2(save_10, objConn, objTrans);
                            maxId = cId;
                        }

                    }
                    else
                    {
                        var save_10 = $"UPDATE  T14010 SET T_CUSTOMER_NAME='{t14020.T_COMPANY_NAME}',T_CUSTOMER_ADDRESS='{t14020.T_CUSTOMER_ADDRESS}' WHERE T_CUSTOMER_MOBILE='{t14020.T_CUSTOMER_MOBILE}' AND T_CUSTOMER_ID='{t14020.T_CUSTOMER_ID}'";
                        command_2(save_10, objConn, objTrans);
                        maxId = t14020.T_CUSTOMER_ID.ToString();
                    }

                    //----------------------------
                    if (Convert.ToInt32(check20) == 0)
                    {
                        var max_20 = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN  MAX(T_PURCHASE_ID)+1 ELSE 1 END T_PURCHASE_ID FROM T14020", objConn, objTrans).Rows[0]["T_PURCHASE_ID"].ToString();

                        var save_01 = $"INSERT INTO T14020 (T_PURCHASE_ID,T_SQ_NO,T_TYPE_CODE,T_CUSTOMER_ID, T_INVOICE_NO,T_PUR_MEMO,T_TOTAL_QUT, T_TOTAL_PRICE,T_DISCOUNT, T_AFTER_DISCOUNT, T_CASH,T_DUE,T_SHOP_ID,T_ENTRY_USER,T_ENTRY_DATE,T_SITE_CODE)VALUES('{max_20}','{sqNo}','{t14020.T_TYPE_CODE}', '{maxId}', '{t14020.T_INVOICE_NO}','{t14020.T_PUR_MEMO}', '{t14020.T_TOTAL_QUT}', '{t14020.T_TOTAL_PRICE}', '{t14020.T_DISCOUNT}', '{t14020.T_AFTER_DISCOUNT}','{t14020.T_CASH}','{t14020.T_DUE}','{shopId}', '{user}','{t14020.T_ENTRY_DATE}',{siteCode})";
                        command_2(save_01, objConn, objTrans);

                        //-------------------

                        //------ T14080 -----------
                        //var max_80 = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN MAX(T_BALANCE_ID)+1 ELSE 1 END T_BALANCE_ID FROM T14080", objConn, objTrans).Rows[0]["T_BALANCE_ID"].ToString();
                        //decimal total = t14020.T_TOTAL_PRICE - t14020.T_DISCOUNT;
                        //var ins_80 = $"INSERT INTO T14080 (T_BALANCE_ID,T_PURCHASE_ID,T_MEM_INV_NO,T_CUSTOMER_ID,T_TOTAL, T_PAYMENT,T_DUE, T_SHOP_ID,T_FORM_CODE,T_ENTRY_USER,T_ENTRY_DATE)VALUES('{max_80}','{max_20}', '{t14020.T_INVOICE_NO}','{maxId}','{total}', '{t14020.T_CASH}', '{t14020.T_DUE}', '{shopId}','T14020', '{user}','{t14020.T_ENTRY_DATE}')";
                        //command_2(ins_80, objConn, objTrans);
                        //-------------------
                        foreach (var i in t14021)
                        {
                            var max_21 = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN  MAX(T_PUR_DETL_ID)+1 ELSE 1 END T_PUR_DETL_ID FROM T14021", objConn, objTrans).Rows[0]["T_PUR_DETL_ID"].ToString();
                            decimal qut = Convert.ToDecimal(i.T_QUANTITY);// * Convert.ToDecimal(i.T_UM);
                            var save02 = $"INSERT INTO T14021 (T_PUR_DETL_ID,T_PURCHASE_ID, T_PRODUCT_CODE,T_SERIAL_NO, T_PACK_CODE, T_QUANTITY,T_STOCK, T_PURCHASE_DATE,T_PURCHASE_PRICE,T_TOTAL_PUR_PRICE)VALUES('{max_21}','{max_20}', '{i.T_PRODUCT_CODE}','{i.T_SERIAL_NO}', '{i.T_PACK_CODE}', '{i.T_QUANTITY}', '{qut}', '{t14020.T_ENTRY_DATE}','{i.T_PURCHASE_PRICE}','{i.T_TOTAL_PRICE}')";
                            command_2(save02, objConn, objTrans);
                            // var purDetlId = Query_2("select max(T_PUR_DETL_ID)T_PUR_DETL_ID from T14021", objConn, objTrans).Rows[0]["T_PUR_DETL_ID"].ToString();
                            //----------------------------------
                            if (shopId == "1")
                            {
                                var max_35 = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN  MAX(T_PUR_STOCK_ID)+1 ELSE 1 END T_PUR_STOCK_ID from T14035", objConn, objTrans).Rows[0]["T_PUR_STOCK_ID"].ToString();
                                var save35 = $"INSERT INTO T14035 (T_PUR_STOCK_ID,T_PURCHASE_ID, T_PRODUCT_CODE, T_PACK_CODE,T_QUANTITY,T_STOCK,T_PURCHASE_DATE,T_PURCHASE_PRICE,T_TOTAL_PUR_PRICE,T_SHOP_ID)VALUES('{max_35}','{max_20}', '{i.T_PRODUCT_CODE}', '{i.T_PACK_CODE}', '{qut}', '{qut}', '{t14020.T_ENTRY_DATE}','{i.T_PURCHASE_PRICE}', '{i.T_TOTAL_PUR_PRICE}','1')";
                                command_2(save35, objConn, objTrans);
                            }
                            else if (shopId == "2")
                            {
                                //var max_36 = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN  MAX(T_PET2_STOCK_ID)+1 ELSE 1 END T_PET2_STOCK_ID from T14036", objConn, objTrans).Rows[0]["T_PET2_STOCK_ID"].ToString();
                                //var save36 = $"INSERT INTO T14036 (T_PET2_STOCK_ID,T_PURCHASE_ID, T_PRODUCT_CODE, T_PACK_CODE,T_QUANTITY,T_STOCK,T_PURCHASE_DATE,T_PURCHASE_PRICE,T_TOTAL_PUR_PRICE,T_SHOP_ID)VALUES('{max_36}','{max_20}', '{i.T_PRODUCT_CODE}', '{i.T_PACK_CODE}', '{qut}', '{qut}', '{t14020.T_ENTRY_DATE}','{i.T_PURCHASE_PRICE}', '{i.T_TOTAL_PUR_PRICE}','2')";
                                //command_2(save36, objConn, objTrans);
                            }

                        }

                        //----------------------------


                        objTrans.Commit();
                        sms = "Save Successfully-1";
                    }
                    else
                    {
                        //----------------------------------------
                        var update_20 = $"UPDATE T14020 SET T_PUR_MEMO='{t14020.T_PUR_MEMO}',T_TYPE_CODE='{t14020.T_TYPE_CODE}',T_CUSTOMER_ID='{t14020.T_CUSTOMER_ID}',  T_TOTAL_QUT='{t14020.T_TOTAL_QUT}', T_TOTAL_PRICE='{t14020.T_TOTAL_PRICE}', T_DISCOUNT='{t14020.T_DISCOUNT}', T_AFTER_DISCOUNT='{t14020.T_AFTER_DISCOUNT}',T_CASH='{t14020.T_CASH}',T_DUE='{t14020.T_DUE}',T_UPDATE_USER='{user}',T_UPDATE_DATE ='{t14020.T_ENTRY_DATE}',T_SITE_CODE='{siteCode}' WHERE T_PURCHASE_ID= '{t14020.T_PURCHASE_ID}'";
                        command_2(update_20, objConn, objTrans);
                        //--------T14080----------
                        //var update_80 = $"UPDATE T14080  SET T_CUSTOMER_ID='{maxId}',T_TOTAL='{t14020.T_AFTER_DISCOUNT}', T_PAYMENT='{t14020.T_CASH}', T_DUE='{t14020.T_DUE}', T_SHOP_ID='{shopId}',T_SITE_CODE='{siteCode}' WHERE T_PURCHASE_ID ='{t14020.T_PURCHASE_ID}'";
                        //command_2(update_80, objConn, objTrans);

                        var dele_21 = Command($@"Delete From T14021 WHERE T_PURCHASE_ID={t14020.T_PURCHASE_ID}");
                        if (shopId == "1") { command_2($@"Delete From T14035 WHERE T_PURCHASE_ID={t14020.T_PURCHASE_ID}", objConn, objTrans); }
                        else if (shopId == "2") { 
                            //command_2($@"Delete From T14036 WHERE T_PURCHASE_ID={t14020.T_PURCHASE_ID}", objConn, objTrans);
                        }

                        foreach (var i in t14021)
                        {
                            var max_21 = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN  MAX(T_PUR_DETL_ID)+1 ELSE 1 END T_PUR_DETL_ID FROM T14021", objConn, objTrans).Rows[0]["T_PUR_DETL_ID"].ToString();
                            decimal qut = Convert.ToDecimal(i.T_QUANTITY);// * Convert.ToDecimal(i.T_UM);

                            var save02 = $"INSERT INTO T14021 (T_PUR_DETL_ID,T_PURCHASE_ID, T_PRODUCT_CODE, T_SERIAL_NO, T_PACK_CODE,T_QUANTITY,T_STOCK,T_PURCHASE_DATE,T_PURCHASE_PRICE,T_TOTAL_PUR_PRICE)VALUES('{max_21}','{t14020.T_PURCHASE_ID}', '{i.T_PRODUCT_CODE}','{i.T_SERIAL_NO}', '{i.T_PACK_CODE}', '{i.T_QUANTITY}', '{qut}', '{t14020.T_ENTRY_DATE}','{i.T_PURCHASE_PRICE}', '{i.T_TOTAL_PRICE}')";
                            command_2(save02, objConn, objTrans);
                            //  var purDetlId = Query_2("select max(T_PUR_DETL_ID)T_PUR_DETL_ID from T14021", objConn, objTrans).Rows[0]["T_PUR_DETL_ID"].ToString();
                            //----------------------------------
                            if (shopId == "1")
                            {
                                var max_35 = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN  MAX(T_PUR_STOCK_ID)+1 ELSE 1 END T_PUR_STOCK_ID from T14035", objConn, objTrans).Rows[0]["T_PUR_STOCK_ID"].ToString();
                                var save35 = $"INSERT INTO T14035 (T_PUR_STOCK_ID,T_PURCHASE_ID, T_PRODUCT_CODE, T_PACK_CODE,T_QUANTITY,T_STOCK,T_PURCHASE_DATE, T_PURCHASE_PRICE,T_TOTAL_PUR_PRICE,T_SHOP_ID)VALUES('{max_35}','{t14020.T_PURCHASE_ID}', '{i.T_PRODUCT_CODE}', '{i.T_PACK_CODE}', '{qut}', '{qut}', '{t14020.T_ENTRY_DATE}','{i.T_PURCHASE_PRICE}','{i.T_TOTAL_PUR_PRICE}','1')";
                                command_2(save35, objConn, objTrans);
                            }
                            else if (shopId == "2")
                            {
                                //var max_36 = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN  MAX(T_PET2_STOCK_ID)+1 ELSE 1 END T_PET2_STOCK_ID from T14036", objConn, objTrans).Rows[0]["T_PET2_STOCK_ID"].ToString();
                                //var save36 = $"INSERT INTO T14036 (T_PET2_STOCK_ID,T_PURCHASE_ID, T_PRODUCT_CODE, T_PACK_CODE,T_QUANTITY,T_STOCK,T_PURCHASE_DATE, T_PURCHASE_PRICE,T_TOTAL_PUR_PRICE,T_SHOP_ID)VALUES('{max_36}','{t14020.T_PURCHASE_ID}', '{i.T_PRODUCT_CODE}', '{i.T_PACK_CODE}', '{qut}', '{qut}', '{t14020.T_ENTRY_DATE}','{i.T_PURCHASE_PRICE}','{i.T_TOTAL_PUR_PRICE}','1')";
                                //command_2(save36, objConn, objTrans);
                            }
                        }
                        objTrans.Commit();
                        sms = "Update Successfully-1";
                    }
                }
                catch (Exception ex)
                {
                    var kk = ex.Message;
                    sms = "Do not Save-0";
                    objTrans.Rollback();
                }
                finally
                {
                    objConn.Close();
                }
            }
            return sms;
        }
        public DataTable GetCustomerDetails(string fromNo)
        {
            DataTable sql = new DataTable();
            sql = Query($"select T_CUSTOMER_ID,T_CUSTOMER_NAME,T_CUSTOMER_ADDRESS,T_CUSTOMER_MOBILE from T14010 where T_FORM_CODE='{fromNo}'");
            return sql;
            //where T_CUSTOMER_MOBILE='{mobile}'
        }
        public DataTable GetInvoiceList(string date,string siteCode)
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT ROW_NUMBER() OVER(Order by T_PURCHASE_ID)SL,T_PURCHASE_ID, T14020.T_INVOICE_NO,T14020.T_PUR_MEMO, T14020.T_TYPE_CODE,T14020.T_CUSTOMER_ID, T14010.T_CUSTOMER_NAME,T14010.T_CUSTOMER_MOBILE,T14010.T_CUSTOMER_ADDRESS, T_TOTAL_PRICE, T_DISCOUNT, T_AFTER_DISCOUNT, T_TOTAL_QUT, T_CASH, T_DUE, T14001.T_TYPE_NAME, T14020.T_ENTRY_DATE, T14020.T_ENTRY_USER, T11020.T_USER_NAME T_EMP_NAME FROM T14020 JOIN T14010 ON T14020.T_CUSTOMER_ID = T14010.T_CUSTOMER_ID JOIN T14001 ON T14020.T_TYPE_CODE = T14001.T_TYPE_CODE JOIN T11020 ON T14020.T_ENTRY_USER = T11020.T_USER_CODE  WHERE  T14020.T_ENTRY_DATE ='{date}' AND T14020.T_SITE_CODE ='{siteCode}'");
            return sql;
        }
        public DataTable GetInvoiceDetails(string invoice)
        {
            DataTable sql = new DataTable();
            sql = Query($"select ROW_NUMBER() OVER(Order by T14021.T_PUR_DETL_ID)sl, T_PUR_DETL_ID,T_PURCHASE_ID,T14021.T_PRODUCT_CODE,T_SERIAL_NO,CONCAT(T14003.T_PRODUCT_NAME ,' - ', T14004.T_PACK_NAME,' - ',T14004.T_PACK_WEIGHT) T_PRODUCT_NAME,T14021.T_PACK_CODE,t14004.T_PACK_NAME,t14004.T_PACK_WEIGHT,t14004.T_UM, T_PURCHASE_PRICE,T_QUANTITY,T_TOTAL_PUR_PRICE T_TOTAL_PRICE from T14021 JOIN T14003 ON T14021.T_PRODUCT_CODE =t14003.T_PRODUCT_CODE JOIN T14004 ON T14021.T_PACK_CODE =t14004.T_PACK_CODE where T_PURCHASE_ID ='{invoice}'");
            return sql;
        }
        //public string DeleteData(T14020Data t14020, List<T14021Data> t14021,string user)
        //{
        //    var sms = "";
        //    SqlTransaction objTrans = null;
        //    using (SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString))
        //    {
        //        try
        //        {
        //            var update_20 = $"UPDATE T14020 SET T_PUR_MEMO='{t14020.T_PUR_MEMO}',T_TYPE_CODE='{t14020.T_TYPE_CODE}',T_CUSTOMER_ID='{t14020.T_CUSTOMER_ID}',  T_TOTAL_QUT='{t14020.T_TOTAL_QUT}', T_TOTAL_PRICE='{t14020.T_TOTAL_PRICE}', T_DISCOUNT='{t14020.T_DISCOUNT}', T_AFTER_DISCOUNT='{t14020.T_AFTER_DISCOUNT}',T_CASH='{t14020.T_CASH}',T_DUE='{t14020.T_DUE}',T_UPDATE_USER='{user}',T_UPDATE_DATE ='{t14020.T_ENTRY_USER}'WHERE T_PURCHASE_ID= '{t14020.T_PURCHASE_ID}'";
        //            command_2(update_20, objConn, objTrans);
        //            foreach (var item in t14021)
        //            {
        //                var dele_21 = Command($@"Delete From T14021 WHERE T_PUR_DETL_ID={item.T_PUR_DETL_ID}");
        //                var dele_35 = Command($@"Delete From T14035 WHERE T_PUR_DETL_ID={item.T_PUR_DETL_ID}");
        //                if (dele_21 && dele_35) {
        //                    sms = "Delete Successfully-1";
        //                } else { sms = "Do not Delete-0"; }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            var kk = ex.Message;
        //            sms = "Do not Save-0";
        //            objTrans.Rollback();
        //        }
        //        finally
        //        {
        //            objConn.Close();
        //        }
        //    }

        //    return sms;
        //}
    }
}
