using Grace_DAL.Shared.Inventory.Transaction;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Grace_DAL.DAL.Inventory.Transaction
{
    public class T14044DAL : CommonDAL
    {
        public DataTable GetPreOrderData(string shopId)
        {
            DataTable dtSale = new DataTable();
            dtSale = Query($"SELECT ROW_NUMBER() OVER(Order by T_SALE_ID)SL, T_SALE_ID, T14040.T_MEMO_NO, T14040.T_TYPE_CODE,T14040.T_CUSTOMER_ID, T14010.T_CUSTOMER_NAME, T_GRAND_TOTAL , T_AFTER_DISCOUNT, T_PAMENT, T_DUE, T_SALE_TOTAL, T14001.T_TYPE_NAME, T14040.T_ENTRY_DATE, T14040.T_ENTRY_USER, T11020.T_USER_NAME T_EMP_NAME,CASE when T_PRE_ORDER ='1' then 'Preorder' else 'Normal' end T_SALE_TYPE,T_PRE_ORDER,'0' T_ROW_FLAG,case when T_PENDING_FLG='1'then '1' else '0' end T_PENDING_FLG,case when T_CONFIRM_FLG='1'then '1' else '0' end T_CONFIRM_FLG,case when T_DELIVER_FLG='1'then '1' else '0' end T_DELIVER_FLG FROM T14040 JOIN T14010 ON T14040.T_CUSTOMER_ID = T14010.T_CUSTOMER_ID LEFT JOIN T14001 ON T14040.T_TYPE_CODE = T14001.T_TYPE_CODE JOIN T11020 ON T14040.T_ENTRY_USER = T11020.T_USER_CODE WHERE T_VERIFY_FLG IS NULL  AND T_SHOP_ID ='{shopId}' AND (T_DELIVER_FLG is null OR T_DELIVER_FLG ='0') AND T_CANCEL_FLG IS NULL ORDER BY T_SALE_ID ASC");
            return dtSale;
        }
        public string SaveData(List<T14044Data> t14040, string shopId, string user)
        {
            var sms = "";
            SqlTransaction objTrans = null;
            int count = 0;
            DataTable stockList = Query($"SELECT T_PUR_STOCK_ID,T_STOCK,T_PRODUCT_CODE,T_PACK_CODE FROM T14035 WHERE T_STOCK !='0' AND T_PRODUCT_CODE is not null AND T_PACK_CODE is not null AND T_SHOP_ID ='{shopId}'");

            using (SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString))
            {

                try
                {
                    objConn.Open();
                    objTrans = objConn.BeginTransaction();

                    foreach (var i in t14040)
                    {
                        if (i.T_PRE_ORDER == "1")
                        {
                            var update_40 = $"UPDATE T14040 SET T_PRE_ORDER='0' WHERE T_SALE_ID='{i.T_SALE_ID}'";
                            command_2(update_40, objConn, objTrans);
                            //-------------
                            DataTable saleList = Query_2($"select T_SALE_DETAILS_ID,T_MEMO_NO,T_PRODUCT_CODE,T_PACK_CODE,T_SALE_QUANTITY from T14041 where T_MEMO_NO='{i.T_MEMO_NO}'", objConn, objTrans);
                            foreach (DataRow k in saleList.Rows)
                            {
                                double stockRest = 0;
                                double sale = 0;
                                var status = "0";
                                double stk = 0;

                                var dtFiltered = stockList.AsEnumerable().Where(r => r.Field<String>("T_PRODUCT_CODE") == k["T_PRODUCT_CODE"].ToString() && r.Field<String>("T_PACK_CODE") == k["T_PACK_CODE"].ToString()).OrderBy(x => x.Field<int>("T_PUR_STOCK_ID")).CopyToDataTable();
                                sale = sale + Convert.ToDouble(k["T_SALE_QUANTITY"].ToString());
                                foreach (DataRow m in dtFiltered.Rows)
                                {
                                    if (status == "0")
                                    {
                                        var stock = m["T_STOCK"].ToString();
                                        var purDtlId = m["T_PUR_STOCK_ID"].ToString();
                                        int prDId = Convert.ToInt32(purDtlId);
                                        stk = stk + Convert.ToDouble(stock);
                                        if (stk >= sale)
                                        {
                                            stockRest = stk - sale;
                                            //-----------updat T14012--------------
                                            var update12 = $"UPDATE T14035 SET T_STOCK ={stockRest} WHERE T_PUR_STOCK_ID={prDId}";
                                            command_2(update12, objConn, objTrans);
                                            status = "1";
                                            count = 1 + count;
                                            sale = 0;
                                        }
                                        else
                                        {
                                            //  sale = sale;                               
                                            //-----------updat T14012--------------
                                            var update12 = $"UPDATE T14035 SET T_STOCK =0 WHERE T_PUR_STOCK_ID={prDId}";
                                            command_2(update12, objConn, objTrans);
                                        }
                                        //-------------
                                    }
                                    else
                                    {
                                    }
                                }
                            }

                        }
                        else
                        {
                            var update_40 = $"UPDATE T14040 SET T_CONFIRM_FLG='{i.T_CONFIRM_FLG}',T_DELIVER_FLG='{i.T_DELIVER_FLG}'  WHERE T_SALE_ID='{i.T_SALE_ID}'";
                            command_2(update_40, objConn, objTrans);

                        }
                    }
                    objTrans.Commit();
                    sms = "Save Successfully-1";
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
        public string CancelOrder(string saleId,string shopId)
        {
            var sms = "";
            SqlTransaction objTrans = null;
            using (SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString))
            {
                try
                {
                    objConn.Open();
                    objTrans = objConn.BeginTransaction();
                    DataTable dt = Query_2($@"select T_SALE_ID,T_MEMO_NO,case when T_CONFIRM_FLG='1' then '1'else '0' end T_CONFIRM_FLG,case when T_PRE_ORDER='1' then '1'else '0' end T_PRE_ORDER from T14040 WHERE T_SALE_ID ='{saleId}'", objConn, objTrans);
                    var memo = dt.Rows[0]["T_MEMO_NO"].ToString();
                    var confirm = dt.Rows[0]["T_CONFIRM_FLG"].ToString();
                    var priorder = dt.Rows[0]["T_PRE_ORDER"].ToString();
                    if (priorder == "1")
                    {
                        if (confirm == "1")
                        {
                            DataTable dt_41 = Query_2($@"select  T_SALE_ID,T_MEMO_NO,T_TYPE_CODE,T_PRODUCT_CODE,T_PACK_CODE,T_PURCHASE_PRICE, T_SALE_PRICE,T_SALE_QUANTITY,T_TOTAL_PRICE from T14041 WHERE T_MEMO_NO ='{memo}'", objConn, objTrans);
                            //---------------------
                            var update_40 = $"UPDATE T14040 SET T_CANCEL_FLG='1'  WHERE T_SALE_ID='{saleId}'";
                            command_2(update_40, objConn, objTrans);

                            foreach (DataRow i in dt_41.Rows)
                            {
                                var TypeCode = i["T_TYPE_CODE"].ToString();
                                var ProCode = i["T_PRODUCT_CODE"].ToString();
                                var PackCode = i["T_PACK_CODE"].ToString();
                                var PurPrice = i["T_PURCHASE_PRICE"].ToString();
                                var SalePrice = i["T_SALE_PRICE"].ToString();
                                var SaleQuty = i["T_SALE_QUANTITY"].ToString();
                                var TotalPrice = i["T_TOTAL_PRICE"].ToString();
                                //-----------
                                var insert41 = $"INSERT INTO T14041_can(T_SALE_ID,T_MEMO_NO,T_TYPE_CODE, T_PRODUCT_CODE, T_PACK_CODE, T_SALE_QUANTITY, T_SALE_PRICE,T_PURCHASE_PRICE, T_TOTAL_PRICE)VALUES('{saleId}','{memo}','{TypeCode}', '{ProCode}', '{PackCode}', '{SaleQuty}', '{SalePrice}', '{PurPrice}',' {TotalPrice}')";
                                command_2(insert41, objConn, objTrans);
                            }
                            //--------Restor stock-------

                            var saleList_41_dtl = Query_2($"select T_SALE_DTL_ID,T_MEMO_NO,T_PURCHASE_ID,T_PRODUCT_CODE,T_PACK_CODE,T_SALE_QUANTITY from T14041_dtl where T_MEMO_NO ='{memo}'", objConn, objTrans);

                            if (shopId == "1")
                            {
                                foreach (DataRow m in saleList_41_dtl.Rows)
                                {
                                    var purId = m["T_PURCHASE_ID"].ToString();
                                    var sal_memo = m["T_MEMO_NO"].ToString();
                                    var sal_ProCode = m["T_PRODUCT_CODE"].ToString();
                                    var sal_PackCode = m["T_PACK_CODE"].ToString();
                                    var sale_qut = m["T_SALE_QUANTITY"].ToString();

                                    var get_35 = Query_2($"select T_PUR_STOCK_ID,T_STOCK from T14035 where T_PURCHASE_ID = '{purId}' AND T_PRODUCT_CODE='{sal_ProCode}' AND T_PACK_CODE='{sal_PackCode}'", objConn, objTrans);

                                    var stockId = get_35.Rows[0]["T_PUR_STOCK_ID"].ToString();
                                    var stockQut = get_35.Rows[0]["T_STOCK"].ToString();
                                    var stockRestor = Convert.ToDouble(stockQut) + Convert.ToDouble(sale_qut);

                                    var update_35 = $"UPDATE T14035  SET T_STOCK='{stockRestor}' WHERE T_PUR_STOCK_ID ='{stockId}'";
                                    command_2(update_35, objConn, objTrans);
                                }
                            }
                            else if (shopId == "2")
                            {
                                foreach (DataRow m in saleList_41_dtl.Rows)
                                {
                                    var purId = m["T_PURCHASE_ID"].ToString();
                                    var sal_memo = m["T_MEMO_NO"].ToString();
                                    var sal_ProCode = m["T_PRODUCT_CODE"].ToString();
                                    var sal_PackCode = m["T_PACK_CODE"].ToString();
                                    var sale_qut = m["T_SALE_QUANTITY"].ToString();

                                    var get_35 = Query_2($"select T_PET2_STOCK_ID,T_STOCK from T14036 where T_PURCHASE_ID = '{purId}' AND T_PRODUCT_CODE='{sal_ProCode}' AND T_PACK_CODE='{sal_PackCode}'", objConn, objTrans);
                                    //-----------------
                                    var stockId = get_35.Rows[0]["T_PET2_STOCK_ID"].ToString();
                                    var stockQut = get_35.Rows[0]["T_STOCK"].ToString();
                                    var stockRestor = Convert.ToDouble(stockQut) + Convert.ToDouble(sale_qut);
                                    //--------------------------
                                    var update_36 = $"UPDATE T14036  SET T_STOCK='{stockRestor}' WHERE T_PET2_STOCK_ID ='{stockId}'";
                                    command_2(update_36, objConn, objTrans);
                                }
                            }
                            else if (shopId == "3")
                            {
                                foreach (DataRow m in saleList_41_dtl.Rows)
                                {
                                    var purId = m["T_PURCHASE_ID"].ToString();
                                    var sal_memo = m["T_MEMO_NO"].ToString();
                                    var sal_ProCode = m["T_PRODUCT_CODE"].ToString();
                                    var sal_PackCode = m["T_PACK_CODE"].ToString();
                                    var sale_qut = m["T_SALE_QUANTITY"].ToString();

                                    var get_35 = Query_2($"select T_OUTLET_STOCK_ID,T_STOCK from T14037 where T_PURCHASE_ID = '{purId}' AND T_PRODUCT_CODE='{sal_ProCode}' AND T_PACK_CODE='{sal_PackCode}'", objConn, objTrans);
                                    //-----------------
                                    var stockId = get_35.Rows[0]["T_OUTLET_STOCK_ID"].ToString();
                                    var stockQut = get_35.Rows[0]["T_STOCK"].ToString();
                                    var stockRestor = Convert.ToDouble(stockQut) + Convert.ToDouble(sale_qut);
                                    //--------------------------
                                    var update_37 = $"UPDATE T14037  SET T_STOCK='{stockRestor}' WHERE T_OUTLET_STOCK_ID ='{stockId}'";
                                    command_2(update_37, objConn, objTrans);
                                }
                            }
                            //--------Delete Data From T14041 and T14041_dtl---------------
                            var dele_41 = command_2($@"Delete From T14041 WHERE T_MEMO_NO='{memo}'", objConn, objTrans);
                            var dele_41_dtl = command_2($@"Delete From T14041_dtl WHERE T_MEMO_NO='{memo}'", objConn, objTrans);

                        }
                        else
                        {
                            var update_40 = $"UPDATE T14040 SET T_CANCEL_FLG='1'  WHERE T_SALE_ID='{saleId}'";
                            command_2(update_40, objConn, objTrans);
                        }
                    }
                    else
                    {
                        DataTable dt_41 = Query_2($@"select  T_SALE_ID,T_MEMO_NO,T_TYPE_CODE,T_PRODUCT_CODE,T_PACK_CODE,T_PURCHASE_PRICE, T_SALE_PRICE,T_SALE_QUANTITY,T_TOTAL_PRICE from T14041 WHERE T_MEMO_NO ='{memo}'", objConn, objTrans);
                        //---------------------
                        var update_40 = $"UPDATE T14040 SET T_CANCEL_FLG='1'  WHERE T_SALE_ID='{saleId}'";
                        command_2(update_40, objConn, objTrans);

                        foreach (DataRow i in dt_41.Rows)
                        {
                            var TypeCode = i["T_TYPE_CODE"].ToString();
                            var ProCode = i["T_PRODUCT_CODE"].ToString();
                            var PackCode = i["T_PACK_CODE"].ToString();
                            var PurPrice = i["T_PURCHASE_PRICE"].ToString();
                            var SalePrice = i["T_SALE_PRICE"].ToString();
                            var SaleQuty = i["T_SALE_QUANTITY"].ToString();
                            var TotalPrice = i["T_TOTAL_PRICE"].ToString();
                            //-----------
                            var insert41 = $"INSERT INTO T14041_can(T_SALE_ID,T_MEMO_NO,T_TYPE_CODE, T_PRODUCT_CODE, T_PACK_CODE, T_SALE_QUANTITY, T_SALE_PRICE,T_PURCHASE_PRICE, T_TOTAL_PRICE)VALUES('{saleId}','{memo}','{TypeCode}', '{ProCode}', '{PackCode}', '{SaleQuty}', '{SalePrice}', '{PurPrice}',' {TotalPrice}')";
                            command_2(insert41, objConn, objTrans);
                        }
                        //--------Restor stock-------

                        var saleList_41_dtl = Query_2($"select T_SALE_DTL_ID,T_MEMO_NO,T_PURCHASE_ID,T_PRODUCT_CODE,T_PACK_CODE,T_SALE_QUANTITY from T14041_dtl where T_MEMO_NO ='{memo}'", objConn, objTrans);

                        if (shopId == "1")
                        {
                            foreach (DataRow m in saleList_41_dtl.Rows)
                            {
                                var purId = m["T_PURCHASE_ID"].ToString();
                                var sal_memo = m["T_MEMO_NO"].ToString();
                                var sal_ProCode = m["T_PRODUCT_CODE"].ToString();
                                var sal_PackCode = m["T_PACK_CODE"].ToString();
                                var sale_qut = m["T_SALE_QUANTITY"].ToString();

                                var get_35 = Query_2($"select T_PUR_STOCK_ID,T_STOCK from T14035 where T_PURCHASE_ID = '{purId}' AND T_PRODUCT_CODE='{sal_ProCode}' AND T_PACK_CODE='{sal_PackCode}'", objConn, objTrans);

                                var stockId = get_35.Rows[0]["T_PUR_STOCK_ID"].ToString();
                                var stockQut = get_35.Rows[0]["T_STOCK"].ToString();
                                var stockRestor = Convert.ToDouble(stockQut) + Convert.ToDouble(sale_qut);

                                var update_35 = $"UPDATE T14035  SET T_STOCK='{stockRestor}' WHERE T_PUR_STOCK_ID ='{stockId}'";
                                command_2(update_35, objConn, objTrans);
                            }
                        }
                        else if (shopId == "2")
                        {
                            foreach (DataRow m in saleList_41_dtl.Rows)
                            {
                                var purId = m["T_PURCHASE_ID"].ToString();
                                var sal_memo = m["T_MEMO_NO"].ToString();
                                var sal_ProCode = m["T_PRODUCT_CODE"].ToString();
                                var sal_PackCode = m["T_PACK_CODE"].ToString();
                                var sale_qut = m["T_SALE_QUANTITY"].ToString();

                                var get_35 = Query_2($"select T_PET2_STOCK_ID,T_STOCK from T14036 where T_PURCHASE_ID = '{purId}' AND T_PRODUCT_CODE='{sal_ProCode}' AND T_PACK_CODE='{sal_PackCode}'", objConn, objTrans);
                                //-----------------
                                var stockId = get_35.Rows[0]["T_PET2_STOCK_ID"].ToString();
                                var stockQut = get_35.Rows[0]["T_STOCK"].ToString();
                                var stockRestor = Convert.ToDouble(stockQut) + Convert.ToDouble(sale_qut);
                                //--------------------------
                                var update_36 = $"UPDATE T14036  SET T_STOCK='{stockRestor}' WHERE T_PET2_STOCK_ID ='{stockId}'";
                                command_2(update_36, objConn, objTrans);
                            }
                        }
                        else if (shopId == "3")
                        {
                            foreach (DataRow m in saleList_41_dtl.Rows)
                            {
                                var purId = m["T_PURCHASE_ID"].ToString();
                                var sal_memo = m["T_MEMO_NO"].ToString();
                                var sal_ProCode = m["T_PRODUCT_CODE"].ToString();
                                var sal_PackCode = m["T_PACK_CODE"].ToString();
                                var sale_qut = m["T_SALE_QUANTITY"].ToString();

                                var get_35 = Query_2($"select T_OUTLET_STOCK_ID,T_STOCK from T14037 where T_PURCHASE_ID = '{purId}' AND T_PRODUCT_CODE='{sal_ProCode}' AND T_PACK_CODE='{sal_PackCode}'", objConn, objTrans);
                                //-----------------
                                var stockId = get_35.Rows[0]["T_OUTLET_STOCK_ID"].ToString();
                                var stockQut = get_35.Rows[0]["T_STOCK"].ToString();
                                var stockRestor = Convert.ToDouble(stockQut) + Convert.ToDouble(sale_qut);
                                //--------------------------
                                var update_37 = $"UPDATE T14037  SET T_STOCK='{stockRestor}' WHERE T_OUTLET_STOCK_ID ='{stockId}'";
                                command_2(update_37, objConn, objTrans);
                            }
                        }
                        //--------Delete Data From T14041 and T14041_dtl---------------
                        var dele_41 = command_2($@"Delete From T14041 WHERE T_MEMO_NO='{memo}'", objConn, objTrans);
                        var dele_41_dtl = command_2($@"Delete From T14041_dtl WHERE T_MEMO_NO='{memo}'", objConn, objTrans);
                        var dele_80 = command_2($@"Delete From T14080 WHERE T_SALE_ID='{saleId}'", objConn, objTrans);
                    }
                    objTrans.Commit();                   
                    sms = "Cancel Successfully-1";
                }
                catch (Exception ex)
                {
                    var kk = ex.Message;
                    sms = "Do not Cancel-0";
                    objTrans.Rollback();
                }
                finally
                {
                    objConn.Close();
                }
            }
            return sms;
        }
      public DataTable GetPreOrderDetails(string saleId, string shopId)
        {
            DataTable dtSale = new DataTable();
            dtSale = Query($"select T_SALE_ID,T_SALE_DETAILS_ID, T_MEMO_NO, T_CUSTOMER_ID, T_CUSTOMER_NAME, T_GRAND_TOTAL, T_DISCOUNT, T_AFTER_DISCOUNT, T_PAMENT, T_DUE, T_PRODUCT_CODE, T_PRODUCT_NAME, T_PACK_CODE, T_PACK_NAME, T_PACK_WEIGHT, T_SALE_QUANTITY,T_QUANTITY, T_PURCHASE_PRICE, T_SALE_PRICE,T_VAT_TAX,cast(((T_TOTAL_PRICE*T_VAT_TAX)/100) as decimal(12,2))T_TOTAL_VAT, cast(((T_TOTAL_PRICE*T_VAT_TAX)/100)+T_TOTAL_PRICE as decimal(12,2))T_TOTAL_PRICE, T_TOTAL_PRICE, case when T_STOCK is null then 0 else T_STOCK end T_STOCK from ( select T14040.T_SALE_ID,T14041.T_SALE_DETAILS_ID, T14040.T_MEMO_NO, T14040.T_CUSTOMER_ID,T14010.T_CUSTOMER_NAME, T_GRAND_TOTAL, T_VAT_TAX, T_DISCOUNT, T_AFTER_DISCOUNT, T_PAMENT, T_DUE, T14041.T_PRODUCT_CODE,T14003.T_PRODUCT_NAME, T14041.T_PACK_CODE, T14004.T_PACK_NAME, T14004.T_PACK_WEIGHT, T14041.T_SALE_QUANTITY,T14041.T_SALE_QUANTITY T_QUANTITY, T14041.T_PURCHASE_PRICE, T14041.T_SALE_PRICE, T14041.T_TOTAL_PRICE, case when '{shopId}'='1' then (select T_STOCK from VIEW_STOCK Where T_PRODUCT_CODE =T14041.T_PRODUCT_CODE AND T_PACK_CODE =T14041.T_PACK_CODE AND T_STOCK>0)  else '0' end T_STOCK from T14040 JOIN T14041 ON T14040.T_MEMO_NO =T14041.T_MEMO_NO JOIN T14010 ON T14040.T_CUSTOMER_ID =T14010.T_CUSTOMER_ID JOIN T14003 ON T14041.T_PRODUCT_CODE =T14003.T_PRODUCT_CODE JOIN T14004 ON T14041.T_PACK_CODE =T14004.T_PACK_CODE where T_PRE_ORDER ='1' AND T_SHOP_ID='1' AND T14040.T_SALE_ID ='{saleId}')t_1");
            return dtSale;  
            
        }
        public string SavePreOrderData(List<T14044PreOrderData> list_1, List<T14044PreOrderData> list_2, string shopId, string user)
        {
            var sms = "";
            var date = DateTime.Now.ToString("dd-MM-yyyy");
            double total_sale_Price = 0;
            double total_sale_Qut = 0;
            var saleId = 0;
            var memo = "";
            int custId = 0;
            SqlTransaction objTrans = null;
            int count = 0;
            var memoNew = GeneratMemo();
            foreach (var i in list_1)
            {
                total_sale_Price = total_sale_Price + Convert.ToDouble(i.T_TOTAL_PRICE);
                total_sale_Qut = total_sale_Qut + Convert.ToDouble(i.T_QUANTITY);
                saleId = i.T_SALE_ID;
                memo = i.T_MEMO_NO;
                custId = i.T_CUSTOMER_ID;
            }
            var saleDetails = Query($"select T_PAMENT,T_DISCOUNT from T14040 WHERE T_SALE_ID = '{saleId}'");
            var payment = saleDetails.Rows[0]["T_PAMENT"].ToString();
            var discount = saleDetails.Rows[0]["T_DISCOUNT"].ToString();
            var newPay = total_sale_Price - Convert.ToDouble(discount);
            var due = newPay - Convert.ToDouble(payment);
            using (SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString))
            {

                try
                {
                    objConn.Open();
                    objTrans = objConn.BeginTransaction();
                    //------------------------
                    var update_40 = $"UPDATE T14040 SET T_PRE_ORDER='0',T_PENDING_FLG='1',T_CONFIRM_FLG='1',T_GRAND_TOTAL='{total_sale_Price}', T_AFTER_DISCOUNT='{total_sale_Price}', T_PAMENT='{payment}',T_DUE='{due}',T_SALE_TOTAL='{total_sale_Qut}' WHERE T_SALE_ID='{saleId}'";
                    command_2(update_40, objConn, objTrans);
                    //----------T14080---------
                    //------ T14080 -----------
                    // decimal total = t14040.T_AFTER_DISCOUNT;
                    var max_80 = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN MAX(T_BALANCE_ID)+1 ELSE 1 END T_BALANCE_ID FROM T14080", objConn, objTrans).Rows[0]["T_BALANCE_ID"].ToString();
                    var ins_80 = $"INSERT INTO T14080 (T_BALANCE_ID,T_SALE_ID,T_MEM_INV_NO,T_CUSTOMER_ID,T_TOTAL, T_PAYMENT,T_DUE, T_SHOP_ID,T_FORM_CODE,T_ENTRY_USER,T_ENTRY_DATE)VALUES('{max_80}','{saleId}', '{memo}','{custId}','{total_sale_Price}', '0', '{total_sale_Price}', '{shopId}','T14040', '{user}','{date}')";
                    command_2(ins_80, objConn, objTrans);

                    //-------------
                    if (shopId == "1")
                    {
                        DataTable stockList = Query_2($"SELECT T_PUR_STOCK_ID,T_PURCHASE_ID,T_STOCK,T_PRODUCT_CODE,T_PACK_CODE FROM T14035 WHERE T_STOCK !='0' AND T_PRODUCT_CODE is not null AND T_PACK_CODE is not null AND T_SHOP_ID ='{shopId}'", objConn, objTrans);
                        foreach (var k in list_1)
                        {
                            double stockRest = 0;
                            double sale = 0;
                            var status = "0";
                            double stk = 0;

                            var dtFiltered = stockList.AsEnumerable().Where(r => r.Field<String>("T_PRODUCT_CODE") == k.T_PRODUCT_CODE.ToString() && r.Field<String>("T_PACK_CODE") == k.T_PACK_CODE.ToString()).OrderBy(x => x.Field<int>("T_PUR_STOCK_ID")).CopyToDataTable();
                            sale = sale + Convert.ToDouble(k.T_QUANTITY.ToString());
                            foreach (DataRow m in dtFiltered.Rows)
                            {
                                if (status == "0")
                                {
                                    var stock = m["T_STOCK"].ToString();
                                    var purDtlId = m["T_PUR_STOCK_ID"].ToString();
                                    var purId_stock = m["T_PURCHASE_ID"].ToString();
                                    int prDId = Convert.ToInt32(purDtlId);
                                    stk = stk + Convert.ToDouble(stock);
                                    if (stk >= sale)
                                    {
                                        stockRest = stk - sale;
                                        //-----------updat T14041--------------
                                        var update_41 = $"UPDATE T14041 SET T_SALE_QUANTITY='{k.T_QUANTITY}',T_TOTAL_PRICE='{k.T_TOTAL_PRICE}' WHERE T_SALE_DETAILS_ID='{k.T_SALE_DETAILS_ID}'";
                                        command_2(update_41, objConn, objTrans);
                                        //-----------------------
                                        var totalPrice = sale * Convert.ToDouble(k.T_SALE_PRICE);
                                        var max_41_dtl = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN MAX(T_SALE_DTL_ID)+1 ELSE 1 END T_SALE_DTL_ID FROM T14041_dtl", objConn, objTrans).Rows[0]["T_SALE_DTL_ID"].ToString();
                                        var insert41_dtl = $"INSERT INTO T14041_dtl(T_SALE_DTL_ID,T_MEMO_NO,T_PURCHASE_ID,T_TYPE_CODE, T_PRODUCT_CODE, T_PACK_CODE, T_SALE_QUANTITY, T_SALE_PRICE,T_PURCHASE_PRICE, T_TOTAL_PRICE)VALUES('{max_41_dtl}','{memo}','{purId_stock}', '{k.T_TYPE_CODE}', '{k.T_PRODUCT_CODE}', '{k.T_PACK_CODE}', '{sale}', '{k.T_SALE_PRICE}', '{k.T_PURCHASE_PRICE}','{k.T_TOTAL_PRICE}')";
                                        command_2(insert41_dtl, objConn, objTrans);
                                        //-----------updat T14035--------------
                                        var update12 = $"UPDATE T14035 SET T_STOCK ={stockRest} WHERE T_PUR_STOCK_ID={prDId}";
                                        command_2(update12, objConn, objTrans);
                                        status = "1";
                                        count = 1 + count;
                                        sale = 0;
                                    }
                                    else
                                    {
                                        //  sale = sale;
                                        var max_41_dtl = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN MAX(T_SALE_DTL_ID)+1 ELSE 1 END T_SALE_DTL_ID FROM T14041_dtl", objConn, objTrans).Rows[0]["T_SALE_DTL_ID"].ToString();
                                        var insert41_dtl = $"INSERT INTO T14041_dtl(T_SALE_DTL_ID,T_MEMO_NO,T_PURCHASE_ID,T_TYPE_CODE, T_PRODUCT_CODE, T_PACK_CODE, T_SALE_QUANTITY, T_SALE_PRICE,T_PURCHASE_PRICE, T_TOTAL_PRICE)VALUES('{max_41_dtl}','{memo}','{purId_stock}', '{k.T_TYPE_CODE}', '{k.T_PRODUCT_CODE}', '{k.T_PACK_CODE}', '{sale}', '{k.T_SALE_PRICE}', '{k.T_PURCHASE_PRICE}','{k.T_TOTAL_PRICE}')";
                                        command_2(insert41_dtl, objConn, objTrans);
                                        //-----------updat T14035--------------
                                        var update12 = $"UPDATE T14035 SET T_STOCK =0 WHERE T_PUR_STOCK_ID={prDId}";
                                        command_2(update12, objConn, objTrans);
                                    }
                                    //-------------
                                }
                                else
                                {
                                }
                            }
                        }
                    }
                    else if (shopId == "2")
                    {

                    }

                    //--------------------
                    if (list_2 != null)
                    {
                        foreach (var i in list_2)
                        {
                            total_sale_Price = Convert.ToDouble(i.T_TOTAL_PRICE);
                            total_sale_Qut = Convert.ToDouble(i.T_SALE_QUANTITY);
                            saleId = i.T_SALE_ID;
                            //  memo = i.T_MEMO_NO;
                            custId = i.T_CUSTOMER_ID;
                        }
                        // var memoNew = GeneratMemo();
                        var T_MEMO_NO = memoNew.T_MEMO_NO;
                        var T_MEMO_SQ = memoNew.T_MEMO_SQ;
                        var T_SALE_DATE = memoNew.T_SALE_DATE;
                        //--------------------------
                        var max_40 = Query_2($"select CASE WHEN COUNT(*)>0 THEN MAX(T_SALE_ID)+1 ELSE 1 END T_SALE_ID from T14040", objConn, objTrans).Rows[0]["T_SALE_ID"].ToString();
                        //  saleId = max_40;
                        var save_40 = $"INSERT INTO T14040 (T_SALE_ID,T_MEMO_NO,T_MEMO_SQ, T_CUSTOMER_ID, T_GRAND_TOTAL,T_DISCOUNT,T_AFTER_DISCOUNT, T_PAMENT,T_DUE,T_SALE_TOTAL,T_SALE_DATE,T_SHOP_ID,T_PRE_ORDER,T_ENTRY_USER,T_ENTRY_DATE)VALUES('{max_40}','{T_MEMO_NO}', '{T_MEMO_SQ}',  '{custId}', '{total_sale_Price}', '0', '{total_sale_Price}', '0','{total_sale_Price}','{total_sale_Qut}','{date}','{shopId}','1','{user}','{date}')";
                        command_2(save_40, objConn, objTrans);
                        foreach (var m in list_2)
                        {
                            var update_41 = $"UPDATE T14041 SET T_MEMO_NO='{T_MEMO_NO}' WHERE T_SALE_DETAILS_ID='{m.T_SALE_DETAILS_ID}'";
                            command_2(update_41, objConn, objTrans);
                        }
                    }


                    objTrans.Commit();
                    sms = "Save Successfully-1";
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
        public T14040Data GeneratMemo()
        {
            T14040Data t013 = new T14040Data();
            var date = DateTime.Now.ToString("dd-MM-yyyy");
            string fmt = "000.##";
            string[] subs = date.Split('-');
            var max = Query($"SELECT MAX(T_MEMO_SQ )T_MEMO_SQ FROM T14040 where CONVERT(date, T_SALE_DATE, 104) =  CONVERT(date, '{date}', 104)").Rows[0]["T_MEMO_SQ"].ToString();
            var max1 = max == "" ? "0" : max;
            int val = Convert.ToInt32(max1);
            int val1 = val == 0 ? 1 : val + 1;
            var d = val1.ToString(fmt) + "-" + subs[0] + subs[1] + subs[2];
            t013.T_MEMO_NO = d;
            t013.T_MEMO_SQ = val1;
            t013.T_SALE_DATE = date;
            return t013;
        }
    }
}
