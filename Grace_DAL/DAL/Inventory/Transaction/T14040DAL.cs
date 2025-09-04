using Grace_DAL.Shared.Inventory.Transaction;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Grace_DAL.DAL.Inventory.Transaction
{
    public class T14040DAL : CommonDAL
    {
        public T14040Data GetMemoNo(string date)
        {
            T14040Data t013 = new T14040Data();
            string fmt = "000.##";
            // string s = "You win some. You lose some.";
            string[] subs = date.Split('-');
            // var max = obj.T15003.Where(x => x.T_SALE_DATE == date).Max(u => u.T_MEMO_SQ);
            var max = Query($"SELECT MAX(T_MEMO_SQ )T_MEMO_SQ FROM T14040 where CONVERT(date, T_SALE_DATE, 104) =  CONVERT(date, '{date}', 104)").Rows[0]["T_MEMO_SQ"].ToString();
            var max1 = max == "" ? "0" : max;
            int val = Convert.ToInt32(max1);
            int val1 = val == 0 ? 1 : val + 1;
            // var d = val1.ToString(fmt) +"-"+ day + Month + Year;
            var d = val1.ToString(fmt) + "-" + subs[0] + subs[1] + subs[2];
            t013.T_MEMO_NO = d;
            t013.T_MEMO_SQ = val1;
            t013.T_SALE_DATE = date;
            return t013;
        }
        public DataTable GetTypeData(string shopId)
        {
            DataTable sql = new DataTable();
            if (shopId=="1")
            {
                sql = Query($"SELECT DISTINCT T14001.T_TYPE_CODE,T_TYPE_NAME FROM T14001 JOIN T14003 ON T14001.T_TYPE_CODE = T14003.T_TYPE_CODE JOIN VIEW_STOCK stk ON T14003.T_PRODUCT_CODE = stk.T_PRODUCT_CODE JOIN T14004 ON T14003.T_PRODUCT_CODE = T14004.T_PRODUCT_CODE JOIN T14005 ON T14004.T_PACK_CODE = T14005.T_PACK_CODE where stk.T_STOCK > 0 ORDER BY T_TYPE_NAME ASC");
            }
            else if (shopId=="2")
            {
                //sql = Query($"SELECT DISTINCT T14001.T_TYPE_CODE,T_TYPE_NAME FROM T14001 JOIN T14003 ON T14001.T_TYPE_CODE = T14003.T_TYPE_CODE JOIN VIEW_STOCK_2 stk ON T14003.T_PRODUCT_CODE = stk.T_PRODUCT_CODE JOIN T14004 ON T14003.T_PRODUCT_CODE = T14004.T_PRODUCT_CODE JOIN T14005 ON T14004.T_PACK_CODE = T14005.T_PACK_CODE where stk.T_STOCK > 0");
            }
            else if (shopId=="3")
            {
                //sql = Query($"SELECT DISTINCT T14001.T_TYPE_CODE,T_TYPE_NAME FROM T14001 JOIN T14003 ON T14001.T_TYPE_CODE = T14003.T_TYPE_CODE JOIN VIEW_STOCK_3 stk ON T14003.T_PRODUCT_CODE = stk.T_PRODUCT_CODE JOIN T14004 ON T14003.T_PRODUCT_CODE = T14004.T_PRODUCT_CODE JOIN T14005 ON T14004.T_PACK_CODE = T14005.T_PACK_CODE where stk.T_STOCK > 0");
            }           
            return sql;
        }
        public DataTable GetProductByCode(string code,string shopId)
        {
            DataTable sql = new DataTable();
            if (shopId=="1")
            {
                sql = Query($@"SELECT T14001.T_TYPE_CODE, T14001.T_TYPE_NAME, T14003.T_PRODUCT_CODE, CONCAT(T14003.T_PRODUCT_NAME ,' - ', T14004.T_PACK_NAME,' - ',T14004.T_PACK_WEIGHT) T_PRODUCT_NAME, T14004.T_UM,T14004.T_PACK_NAME,T14004.T_PACK_CODE,T14004.T_PACK_WEIGHT,cast((T14005.T_PURCHASE_PRICE)as decimal(12,3) ) T_PURCHASE_PRICE,cast((T14005.T_RETAIL_SALE_PRICE)as decimal(12,2) )T_SALE_PRICE, VIEW_STOCK.T_STOCK FROM T14003 JOIN VIEW_STOCK ON T14003.T_PRODUCT_CODE = VIEW_STOCK.T_PRODUCT_CODE JOIN T14004 ON T14003.T_PRODUCT_CODE = T14004.T_PRODUCT_CODE JOIN T14005 ON T14004.T_PACK_CODE = T14005.T_PACK_CODE JOIN T14001 ON T14003.T_TYPE_CODE = T14001.T_TYPE_CODE WHERE T14003.T_PRODUCT_CODE = '{code}' AND VIEW_STOCK.T_STOCK > 0");
             //   sql = Query($@"SELECT T14001.T_TYPE_CODE, T14001.T_TYPE_NAME, T14003.T_PRODUCT_CODE, CONCAT(T14003.T_PRODUCT_NAME ,' - ', T14004.T_PACK_NAME,' - ',T14004.T_PACK_WEIGHT) T_PRODUCT_NAME, T14004.T_UM,T14004.T_PACK_NAME,T14004.T_PACK_CODE,T14004.T_PACK_WEIGHT,cast((T14005.T_PURCHASE_PRICE/T14004.T_UM)as decimal(12,3) ) T_PURCHASE_PRICE,cast((T14005.T_HOLE_SALE_PRICE/T14004.T_UM)as decimal(12,2) )T_SALE_PRICE, VIEW_STOCK.T_STOCK FROM T14003 JOIN VIEW_STOCK ON T14003.T_PRODUCT_CODE = VIEW_STOCK.T_PRODUCT_CODE JOIN T14004 ON T14003.T_PRODUCT_CODE = T14004.T_PRODUCT_CODE JOIN T14005 ON T14004.T_PACK_CODE = T14005.T_PACK_CODE JOIN T14001 ON T14003.T_TYPE_CODE = T14001.T_TYPE_CODE WHERE T14003.T_PRODUCT_CODE = '{code}' AND VIEW_STOCK.T_STOCK > 0");
            }
            else if(shopId=="2")
            {
                //sql = Query($@"SELECT T14001.T_TYPE_CODE, T14001.T_TYPE_NAME, T14003.T_PRODUCT_CODE, CONCAT(T14003.T_PRODUCT_NAME ,' - ', T14004.T_PACK_NAME,' - ',T14004.T_PACK_WEIGHT) T_PRODUCT_NAME, T14004.T_UM,T14004.T_PACK_NAME,T14004.T_PACK_CODE,T14004.T_PACK_WEIGHT,cast((T14005.T_PURCHASE_PRICE)as decimal(12,3) ) T_PURCHASE_PRICE,cast((T14005.T_HOLE_SALE_PRICE)as decimal(12,2) )T_SALE_PRICE , VIEW_STOCK.T_STOCK FROM T14003 JOIN VIEW_STOCK_2 VIEW_STOCK ON T14003.T_PRODUCT_CODE = VIEW_STOCK.T_PRODUCT_CODE JOIN T14004 ON T14003.T_PRODUCT_CODE = T14004.T_PRODUCT_CODE JOIN T14005 ON T14004.T_PACK_CODE = T14005.T_PACK_CODE JOIN T14001 ON T14003.T_TYPE_CODE = T14001.T_TYPE_CODE WHERE T14003.T_PRODUCT_CODE = '{code}' AND VIEW_STOCK.T_STOCK > 0");
             
            }
            else if (shopId=="3") {
                //sql = Query($@"SELECT T14001.T_TYPE_CODE, T14001.T_TYPE_NAME, T14003.T_PRODUCT_CODE, CONCAT(T14003.T_PRODUCT_NAME ,' - ', T14004.T_PACK_NAME,' - ',T14004.T_PACK_WEIGHT) T_PRODUCT_NAME, T14004.T_UM,T14004.T_PACK_NAME,T14004.T_PACK_CODE,T14004.T_PACK_WEIGHT,cast((T14005.T_PURCHASE_PRICE)as decimal(12,3) ) T_PURCHASE_PRICE,cast((T14005.T_RETAIL_SALE_PRICE)as decimal(12,2) )T_SALE_PRICE , VIEW_STOCK.T_STOCK FROM T14003 JOIN VIEW_STOCK_3 VIEW_STOCK ON T14003.T_PRODUCT_CODE = VIEW_STOCK.T_PRODUCT_CODE JOIN T14004 ON T14003.T_PRODUCT_CODE = T14004.T_PRODUCT_CODE JOIN T14005 ON T14004.T_PACK_CODE = T14005.T_PACK_CODE JOIN T14001 ON T14003.T_TYPE_CODE = T14001.T_TYPE_CODE WHERE T14003.T_PRODUCT_CODE = '{code}' AND VIEW_STOCK.T_STOCK > 0");
               
            }
          
            return sql;
        }
        public DataTable GetProduct(string type)
        {
            DataTable sql = new DataTable();           
            sql = Query($@"SELECT T14003.T_PRODUCT_CODE,T14003.T_PRODUCT_NAME FROM T14003 JOIN VIEW_STOCK ON T14003.T_PRODUCT_CODE = VIEW_STOCK.T_PRODUCT_CODE  WHERE T_TYPE_CODE = '{type}' AND VIEW_STOCK.T_STOCK > 0");
            return sql;
        }
        public DataTable GetAllProduct(string shopId)
        {
            DataTable sql = new DataTable();
            if (shopId=="1")
            {
                sql = Query($@"SELECT CONCAT(T14003.T_PRODUCT_NAME ,' - ', T14004.T_PACK_NAME,' - ',T14004.T_PACK_WEIGHT) T_PRODUCT_NAME, T14003.T_PRODUCT_CODE,T14003.T_TYPE_CODE, T14005.T_PACK_CODE, T14004.T_PACK_NAME, T_PURCHASE_PRICE, T14004.T_UM FROM T14003 JOIN T14004 ON T14003.T_PRODUCT_CODE = T14004.T_PRODUCT_CODE JOIN T14005 ON T14004.T_PACK_CODE = T14005.T_PACK_CODE JOIN VIEW_STOCK stk ON T14003.T_PRODUCT_CODE = stk.T_PRODUCT_CODE WHERE stk.T_STOCK > 0");
            }
            else if (shopId=="2")
            {
                sql = Query($@"SELECT CONCAT(T14003.T_PRODUCT_NAME ,' - ', T14004.T_PACK_NAME,' - ',T14004.T_PACK_WEIGHT) T_PRODUCT_NAME, T14003.T_PRODUCT_CODE,T14003.T_TYPE_CODE, T14005.T_PACK_CODE, T14004.T_PACK_NAME, T_PURCHASE_PRICE, T14004.T_UM FROM T14003 JOIN T14004 ON T14003.T_PRODUCT_CODE = T14004.T_PRODUCT_CODE JOIN T14005 ON T14004.T_PACK_CODE = T14005.T_PACK_CODE JOIN VIEW_STOCK_2 stk ON T14003.T_PRODUCT_CODE = stk.T_PRODUCT_CODE WHERE stk.T_STOCK > 0");
            }
            else if (shopId=="3")
            {
                sql = Query($@"SELECT CONCAT(T14003.T_PRODUCT_NAME ,' - ', T14004.T_PACK_NAME,' - ',T14004.T_PACK_WEIGHT) T_PRODUCT_NAME, T14003.T_PRODUCT_CODE,T14003.T_TYPE_CODE, T14005.T_PACK_CODE, T14004.T_PACK_NAME, T_PURCHASE_PRICE, T14004.T_UM FROM T14003 JOIN T14004 ON T14003.T_PRODUCT_CODE = T14004.T_PRODUCT_CODE JOIN T14005 ON T14004.T_PACK_CODE = T14005.T_PACK_CODE JOIN VIEW_STOCK_3 stk ON T14003.T_PRODUCT_CODE = stk.T_PRODUCT_CODE WHERE stk.T_STOCK > 0");
            }
           
           
            return sql;
        }

        public DataTable GetPack(string pro)
        {
            DataTable sql = new DataTable();
            sql = Query($@"SELECT T14005.T_PACK_CODE,T14004.T_PACK_NAME ,cast((T14005.T_HOLE_SALE_PRICE/T14004.T_UM)as decimal(12,0) )T_HOLE_SALE_PRICE,
cast((T14005.T_RETAIL_SALE_PRICE/T14004.T_UM)as decimal(12,0) )T_RETAIL_SALE_PRICE ,cast((T14005.T_PURCHASE_PRICE/T14004.T_UM)as decimal(12,3) ) T_PURCHASE_PRICE FROM T14005 JOIN T14004 ON T14005.T_PACK_CODE = T14004.T_PACK_CODE JOIN VIEW_STOCK ON T14005.T_PRODUCT_CODE = VIEW_STOCK.T_PRODUCT_CODE  WHERE T14005.T_PRODUCT_CODE = '{pro}' AND VIEW_STOCK.T_STOCK > 0");
            return sql;
        }
        public DataTable GetPackList()
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT T_PRODUCT_CODE,T_PACK_CODE,T_PACK_NAME FROM T14004 ");
            return sql;
        }
        public DataTable GetMemoList(string date,string shopId,string siteCode)
        {
            DataTable sql = new DataTable();
            sql = Query($"select T_SALE_ID, T_MEMO_NO, T_SHOP_ID, T_TYPE_CODE, T14040.T_CUSTOMER_ID, T_GRAND_TOTAL, T_LABAUR_COST, T_DISCOUNT, T_AFTER_DISCOUNT,T_VAT_TAX,T_TOTAL_VAT, T_PAMENT, T_DUE, T_SALE_TOTAL, T14040.T_ENTRY_DATE, T14010.T_CUSTOMER_NAME, T14010.T_CUSTOMER_ADDRESS, T14010.T_CUSTOMER_MOBILE from T14040 JOIN T14010 ON T14040.T_CUSTOMER_ID =T14010.T_CUSTOMER_ID where T_VERIFY_FLG is null AND T14040.T_SHOP_ID='{shopId}' AND T14040.T_SALE_DATE = '{date}' AND T_SITE_CODE='{siteCode}'");
            return sql;
        }
        public DataTable GetMemoDetails(int saleId)
        {
            DataTable sql = new DataTable();
            sql = Query($"select ROW_NUMBER() OVER(Order by T14040.T_SALE_ID)sl, T14040.T_SALE_ID, T14040.T_MEMO_NO, T_GRAND_TOTAL, T_DISCOUNT, T_PAMENT, T_DUE, T_SALE_DATE,T14041.T_PRODUCT_CODE, T14003.T_PRODUCT_NAME,T14041.T_TYPE_CODE, T14001.T_TYPE_NAME, T14004.T_PACK_NAME,T14004.T_PACK_CODE,T14004.T_PACK_WEIGHT, T14041.T_SALE_PRICE,T14041.T_PURCHASE_PRICE, T14041.T_SALE_QUANTITY,cast((T14041.T_SALE_QUANTITY/T14004.T_UM)as decimal(12,2) )T_BOX, T14041.T_TOTAL_PRICE from T14040 JOIN T14041 ON T14040.T_MEMO_NO = T14041.T_MEMO_NO JOIN T14003 ON T14041.T_PRODUCT_CODE = T14003.T_PRODUCT_CODE JOIN T14001 ON T14041.T_TYPE_CODE = T14001.T_TYPE_CODE  JOIN T14004 ON T14041.T_PACK_CODE = T14004.T_PACK_CODE where T14040.T_SALE_ID='{saleId}'");
            return sql;
        }
        public DataTable GetSalePrice(string procuct, string pack)
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT T_PRICE_ID,T_SALE_PRICE FROM T14003 WHERE T_PRODUCT_CODE='{procuct}' AND T_PACK_CODE='{pack}'");
            return sql;
        }
        public DataTable GetCustomerDetails()
        {
            DataTable sql = new DataTable();
            sql = Query($"select T_CUSTOMER_ID, T_CUSTOMER_NAME, T_CUSTOMER_ADDRESS, T_CUSTOMER_MOBILE from T14010 WHERE T_FORM_CODE='T14040'");
            return sql;
            //where T_CUSTOMER_MOBILE='{mobile}'
        }
        public DataTable GetStockData(string pro, string pack, string shopId)
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT T_STOCK  FROM VIEW_STOCK WHERE T_SHOP_ID ='{shopId}' AND T_STOCK !='0' AND T_PRODUCT_CODE = '{pro}'AND T_PACK_CODE = '{pack}'");
            return sql;
        }
        public string SaveData(T14040Data t14040, List<T14041Data> t14014, string shopId, string user,string siteCode)
        {
            string sms = "";
            string saleId = "";
            var date = DateTime.Now.ToString("dd-MM-yyyy");
            SqlTransaction objTrans = null;
            DataTable stockList = new DataTable();
            using (SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString))
            {
                try
                {
                    var maxId = "";
                    var custId = "";
                    int count = 0;
                    objConn.Open();
                    objTrans = objConn.BeginTransaction();
                    //----------------------------
                    if (t14040.T_CUSTOMER_ID == 0)
                    {
                        var chk = Query_2($" select count(*)T_COUNT from T14010 where T_CUSTOMER_MOBILE='{t14040.T_CUSTOMER_MOBILE}' AND T_FORM_CODE='T14040'", objConn, objTrans).Rows[0]["T_COUNT"].ToString();
                        if (t14040.T_CUSTOMER_MOBILE==null || chk == "0")
                        {
                            var save_10 = $"INSERT INTO T14010 (T_CUSTOMER_NAME,T_CUSTOMER_ADDRESS,T_CUSTOMER_MOBILE,T_FORM_CODE,T_ENTRY_USER,T_ENTRY_DATE)VALUES('{t14040.T_CUSTOMER_NAME}','{t14040.T_CUSTOMER_ADDRESS}','{t14040.T_CUSTOMER_MOBILE}','T14040','{user}','{t14040.T_ENTRY_DATE}')";
                            command_2(save_10, objConn, objTrans);
                            //------------------------
                            SqlCommand command = new SqlCommand("SELECT MAX(T_CUSTOMER_ID)T_CUSTOMER_ID FROM T14010 where T_FORM_CODE='T14040'", objConn, objTrans);
                            var sqlDataAdapter = new SqlDataAdapter(command);
                            var dataTable = new DataTable();
                            sqlDataAdapter.Fill(dataTable);
                            custId = dataTable.Rows[0]["T_CUSTOMER_ID"].ToString();
                        }
                        else
                        {
                            var cId = Query_2($"select T_CUSTOMER_ID from T14010 where T_CUSTOMER_MOBILE='{t14040.T_CUSTOMER_MOBILE}' AND T_FORM_CODE='T14040'", objConn, objTrans).Rows[0]["T_CUSTOMER_ID"].ToString();
                            var save_10 = $"UPDATE  T14010 SET T_CUSTOMER_NAME='{t14040.T_CUSTOMER_NAME}',T_CUSTOMER_ADDRESS='{t14040.T_CUSTOMER_ADDRESS}' WHERE T_CUSTOMER_MOBILE='{t14040.T_CUSTOMER_MOBILE}' AND T_CUSTOMER_ID='{cId}'";
                            command_2(save_10, objConn, objTrans);
                            custId = cId;
                        }                   

                    }
                    else
                    {
                        var save_10 = $"UPDATE  T14010 SET T_CUSTOMER_NAME='{t14040.T_CUSTOMER_NAME}',T_CUSTOMER_ADDRESS='{t14040.T_CUSTOMER_ADDRESS}' WHERE T_CUSTOMER_MOBILE='{t14040.T_CUSTOMER_MOBILE}' AND T_CUSTOMER_ID='{t14040.T_CUSTOMER_ID}'";
                        command_2(save_10, objConn, objTrans);
                        custId = t14040.T_CUSTOMER_ID.ToString();

                    }
                    if (t14040.T_SALE_ID==0) {
                        if (shopId == "1")
                        {
                            stockList = Query_2($"SELECT T_PUR_STOCK_ID,T_PURCHASE_ID,T_STOCK,T_PRODUCT_CODE,T_PACK_CODE FROM T14035 WHERE T_STOCK !='0' AND T_PRODUCT_CODE is not null AND T_PACK_CODE is not null AND T_SHOP_ID ='{shopId}'", objConn, objTrans);
                        }
                        else if (shopId == "2")
                        {
                            //stockList = Query_2($"SELECT T_PET2_STOCK_ID T_PUR_STOCK_ID,T_PURCHASE_ID,T_STOCK,T_PRODUCT_CODE,T_PACK_CODE FROM T14036 WHERE T_STOCK !='0' AND T_PRODUCT_CODE is not null AND T_PACK_CODE is not null ", objConn, objTrans);
                        }
                        else if (shopId == "3")
                        {
                            //stockList = Query_2($"SELECT T_OUTLET_STOCK_ID T_PUR_STOCK_ID,T_PURCHASE_ID,T_STOCK,T_PRODUCT_CODE,T_PACK_CODE FROM T14037 WHERE T_STOCK !='0' AND T_PRODUCT_CODE is not null AND T_PACK_CODE is not null ", objConn, objTrans);
                        }
                        var memo = GeneratMemo(t14040.T_ENTRY_DATE);
                        t14040.T_MEMO_NO = memo.T_MEMO_NO;
                        t14040.T_MEMO_SQ = memo.T_MEMO_SQ;
                       // t14040.T_SALE_DATE = memo.T_SALE_DATE;
                        //--------------------------
                        var max_40 = Query_2($"select CASE WHEN COUNT(*)>0 THEN MAX(T_SALE_ID)+1 ELSE 1 END T_SALE_ID from T14040", objConn, objTrans).Rows[0]["T_SALE_ID"].ToString();
                        saleId = max_40;
                        var save_40 = $"INSERT INTO T14040 (T_SALE_ID,T_MEMO_NO,T_MEMO_SQ, T_CUSTOMER_ID,T_TYPE_CODE, T_GRAND_TOTAL,T_DISCOUNT,T_AFTER_DISCOUNT, T_TOTAL_VAT,T_VAT_TAX,T_PAMENT, T_DUE,T_SALE_TOTAL,T_SALE_DATE,T_SHOP_ID,T_PENDING_FLG,T_ENTRY_USER,T_ENTRY_DATE,T_SITE_CODE)VALUES('{max_40}','{t14040.T_MEMO_NO}', '{t14040.T_MEMO_SQ}',  '{custId}','{t14040.T_TYPE_CODE}', '{t14040.T_GRAND_TOTAL}', '{t14040.T_DISCOUNT}', '{t14040.T_AFTER_DISCOUNT}','{t14040.T_TOTAL_VAT}','{t14040.T_VAT_TAX}', '{t14040.T_PAMENT}','{t14040.T_BALANCE}','{t14040.T_SALE_TOTAL}', '{t14040.T_ENTRY_DATE}','{shopId}','1','{user}','{date}','{siteCode}')";
                        command_2(save_40, objConn, objTrans);


                        //------ T14080 -----------
                        // decimal total = t14040.T_AFTER_DISCOUNT;
                        //var max_80 = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN MAX(T_BALANCE_ID)+1 ELSE 1 END T_BALANCE_ID FROM T14080", objConn, objTrans).Rows[0]["T_BALANCE_ID"].ToString();
                        //var ins_80 = $"INSERT INTO T14080 (T_BALANCE_ID,T_SALE_ID,T_MEM_INV_NO,T_CUSTOMER_ID,T_TOTAL, T_PAYMENT,T_DUE, T_SHOP_ID,T_FORM_CODE,T_ENTRY_USER,T_ENTRY_DATE)VALUES('{max_80}','{max_40}', '{t14040.T_MEMO_NO}','{custId}','{t14040.T_AFTER_DISCOUNT}', '{t14040.T_PAMENT}', '{t14040.T_BALANCE}', '{shopId}','T14040', '{user}','{t14040.T_ENTRY_DATE}')";
                        //command_2(ins_80, objConn, objTrans);

                        foreach (var i in t14014)
                        {
                            double stockRest = 0;
                            //int saleRest = 0;
                            double sale = 0;
                            var status = "0";
                            double stk = 0;

                            var dtFiltered = stockList.AsEnumerable().Where(r => r.Field<String>("T_PRODUCT_CODE") == i.T_PRODUCT_CODE && r.Field<String>("T_PACK_CODE") == i.T_PACK_CODE).OrderBy(x => x.Field<int>("T_PUR_STOCK_ID")).CopyToDataTable();
                            sale = sale + Convert.ToDouble(i.T_SALE_QUANTITY);
                            foreach (DataRow k in dtFiltered.Rows)
                            {

                                if (status == "0")
                                {
                                    var stock = k["T_STOCK"].ToString();
                                    var purDtlId = k["T_PUR_STOCK_ID"].ToString();
                                    var purId_stock = k["T_PURCHASE_ID"].ToString();
                                    int prDId = Convert.ToInt32(purDtlId);
                                    stk = stk + Convert.ToDouble(stock);

                                    if (stk >= sale)
                                    {
                                        var max_41 = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN MAX(T_SALE_DETAILS_ID)+1 ELSE 1 END T_SALE_DETAILS_ID FROM T14041", objConn, objTrans).Rows[0]["T_SALE_DETAILS_ID"].ToString();
                                        stockRest = stk - sale;
                                        var insert41 = $"INSERT INTO T14041(T_SALE_DETAILS_ID,T_MEMO_NO,T_TYPE_CODE, T_PRODUCT_CODE, T_PACK_CODE, T_SALE_QUANTITY, T_SALE_PRICE,T_PURCHASE_PRICE, T_TOTAL_PRICE)VALUES('{max_41}','{t14040.T_MEMO_NO}','{i.T_TYPE_CODE}', '{i.T_PRODUCT_CODE}', '{i.T_PACK_CODE}', '{i.T_SALE_QUANTITY}', '{i.T_SALE_PRICE}', '{i.T_PURCHASE_PRICE}',' {i.T_TOTAL_PRICE}')";
                                        command_2(insert41, objConn, objTrans);
                                        //-----------------------
                                        var totalPrice = sale * Convert.ToDouble(i.T_SALE_PRICE);
                                        var max_41_dtl = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN MAX(T_SALE_DTL_ID)+1 ELSE 1 END T_SALE_DTL_ID FROM T14041_dtl", objConn, objTrans).Rows[0]["T_SALE_DTL_ID"].ToString();
                                        var insert41_dtl = $"INSERT INTO T14041_dtl(T_SALE_DTL_ID,T_MEMO_NO,T_PURCHASE_ID,T_TYPE_CODE, T_PRODUCT_CODE, T_PACK_CODE, T_SALE_QUANTITY, T_SALE_PRICE,T_PURCHASE_PRICE, T_TOTAL_PRICE)VALUES('{max_41_dtl}','{t14040.T_MEMO_NO}','{purId_stock}', '{i.T_TYPE_CODE}', '{i.T_PRODUCT_CODE}', '{i.T_PACK_CODE}', '{sale}', '{i.T_SALE_PRICE}', '{i.T_PURCHASE_PRICE}',' {i.T_TOTAL_PRICE}')";
                                        command_2(insert41_dtl, objConn, objTrans);

                                        //-----------updat T14035--------------
                                        if (shopId == "1")
                                        {
                                            var update12 = $"UPDATE T14035 SET T_STOCK ={stockRest} WHERE T_PUR_STOCK_ID={prDId}";
                                            command_2(update12, objConn, objTrans);
                                        }
                                        else if (shopId == "2")
                                        {
                                            var update12 = $"UPDATE T14036 SET T_STOCK ={stockRest} WHERE T_PET2_STOCK_ID={prDId}";
                                            command_2(update12, objConn, objTrans);
                                        }
                                        else if (shopId == "3")
                                        {
                                            var update12 = $"UPDATE T14037 SET T_STOCK ={stockRest} WHERE T_OUTLET_STOCK_ID={prDId}";
                                            command_2(update12, objConn, objTrans);
                                        }
                                        status = "1";
                                        count = 1 + count;
                                        sale = 0;
                                    }
                                    else
                                    {
                                        var totalPrice = stk * Convert.ToDouble(i.T_SALE_PRICE);
                                        var max_41_dtl = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN MAX(T_SALE_DTL_ID)+1 ELSE 1 END T_SALE_DTL_ID FROM T14041_dtl", objConn, objTrans).Rows[0]["T_SALE_DTL_ID"].ToString();
                                        var insert41_dtl = $"INSERT INTO T14041_dtl(T_SALE_DTL_ID,T_MEMO_NO,T_PURCHASE_ID,T_TYPE_CODE, T_PRODUCT_CODE, T_PACK_CODE, T_SALE_QUANTITY, T_SALE_PRICE,T_PURCHASE_PRICE, T_TOTAL_PRICE)VALUES('{max_41_dtl}','{t14040.T_MEMO_NO}','{purId_stock}', '{i.T_TYPE_CODE}', '{i.T_PRODUCT_CODE}', '{i.T_PACK_CODE}', '{stk}', '{i.T_SALE_PRICE}', '{i.T_PURCHASE_PRICE}','{totalPrice}')";
                                        command_2(insert41_dtl, objConn, objTrans);                                       
                                        sale = sale - stk;
                                        stk = 0;
                                        //-----------updat T14035--------------
                                        if (shopId == "1")
                                        {
                                            var update12 = $"UPDATE T14035 SET T_STOCK =0 WHERE T_PUR_STOCK_ID={prDId}";
                                            command_2(update12, objConn, objTrans);
                                        }
                                        else if (shopId == "2")
                                        {
                                            var update12 = $"UPDATE T14036 SET T_STOCK =0 WHERE T_PUR_STOCK_ID={prDId}";
                                            command_2(update12, objConn, objTrans);
                                        }
                                        else if (shopId == "3")
                                        {
                                            var update12 = $"UPDATE T14037 SET T_STOCK =0 WHERE T_PUR_STOCK_ID={prDId}";
                                            command_2(update12, objConn, objTrans);
                                        }
                                    }
                                    //-------------
                                }
                                else
                                {

                                }
                            }
                        }
                        if (count == t14014.Count)
                        {
                            objTrans.Commit();
                            sms = "Save successfully-1/" + saleId;// + ust_id;
                        }
                        else
                        {
                            sms = " Do not Save-0";
                            objTrans.Rollback();

                        }
                    } else
                    {
                        saleId = t14040.T_SALE_ID.ToString();
                        //(T_SALE_ID,T_MEMO_NO
                        var update_40 = $"UPDATE T14040 SET   T_CUSTOMER_ID ='{custId}',T_TYPE_CODE='{t14040.T_TYPE_CODE}', T_GRAND_TOTAL='{t14040.T_GRAND_TOTAL}', T_DISCOUNT='{t14040.T_DISCOUNT}', T_AFTER_DISCOUNT='{t14040.T_AFTER_DISCOUNT}',T_TOTAL_VAT='{t14040.T_TOTAL_VAT}',T_VAT_TAX='{t14040.T_VAT_TAX}', T_PAMENT='{t14040.T_PAMENT}', T_DUE='{t14040.T_BALANCE}', T_SALE_TOTAL='{t14040.T_SALE_TOTAL}', T_SHOP_ID='{shopId}',T_UPDATE_USER='{user}',T_SALE_DATE='{t14040.T_ENTRY_DATE}',T_UPDATE_DATE='{date}',T_SITE_CODE='{siteCode}' WHERE T_SALE_ID='{t14040.T_SALE_ID}'";
                        command_2(update_40, objConn, objTrans);
                        //----------T14080---------
                        //var update_80 = $"UPDATE T14080  SET T_CUSTOMER_ID='{custId}',T_TOTAL='{t14040.T_AFTER_DISCOUNT}', T_PAYMENT='{t14040.T_PAMENT}', T_DUE='{t14040.T_BALANCE}', T_SHOP_ID='{shopId}' WHERE T_SALE_ID ='{t14040.T_SALE_ID}'";
                        //command_2(update_80, objConn, objTrans);
                        //--------Restor stock-------

                     var saleList_41_dtl = Query_2($"select T_SALE_DTL_ID,T_MEMO_NO,T_PURCHASE_ID,T_PRODUCT_CODE,T_PACK_CODE,T_SALE_QUANTITY from T14041_dtl where T_MEMO_NO ='{t14040.T_MEMO_NO}'", objConn, objTrans);

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
                        else if (shopId == "2") {
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
                        var dele_41 = command_2($@"Delete From T14041 WHERE T_MEMO_NO='{t14040.T_MEMO_NO}'", objConn, objTrans);
                        var dele_41_dtl = command_2($@"Delete From T14041_dtl WHERE T_MEMO_NO='{t14040.T_MEMO_NO}'", objConn, objTrans);
                        //-----------get StockList---------------
                        if (shopId == "1")
                        {
                            stockList = Query_2($"SELECT T_PUR_STOCK_ID,T_PURCHASE_ID,T_STOCK,T_PRODUCT_CODE,T_PACK_CODE FROM T14035 WHERE T_STOCK !='0' AND T_PRODUCT_CODE is not null AND T_PACK_CODE is not null AND T_SHOP_ID ='{shopId}'", objConn, objTrans);
                        }
                        else if (shopId == "2")
                        {
                            stockList = Query_2($"SELECT T_PET2_STOCK_ID T_PUR_STOCK_ID,T_PURCHASE_ID,T_STOCK,T_PRODUCT_CODE,T_PACK_CODE FROM T14036 WHERE T_STOCK !='0' AND T_PRODUCT_CODE is not null AND T_PACK_CODE is not null ", objConn, objTrans);
                        }
                        else if (shopId == "3")
                        {
                            stockList = Query_2($"SELECT T_OUTLET_STOCK_ID T_PUR_STOCK_ID,T_PURCHASE_ID,T_STOCK,T_PRODUCT_CODE,T_PACK_CODE FROM T14037 WHERE T_STOCK !='0' AND T_PRODUCT_CODE is not null AND T_PACK_CODE is not null ", objConn, objTrans);
                        }
                        //--------Insert data----------
                        foreach (var i in t14014)
                        {
                            double stockRest = 0;
                            //int saleRest = 0;
                            double sale = 0;
                            var status = "0";
                            double stk = 0;

                            var dtFiltered = stockList.AsEnumerable().Where(r => r.Field<String>("T_PRODUCT_CODE") == i.T_PRODUCT_CODE && r.Field<String>("T_PACK_CODE") == i.T_PACK_CODE).OrderBy(x => x.Field<int>("T_PUR_STOCK_ID")).CopyToDataTable();
                            sale = sale + Convert.ToDouble(i.T_SALE_QUANTITY);
                            foreach (DataRow k in dtFiltered.Rows)
                            {

                                if (status == "0")
                                {
                                    var stock = k["T_STOCK"].ToString();
                                    var purDtlId = k["T_PUR_STOCK_ID"].ToString();
                                    var purId_stock = k["T_PURCHASE_ID"].ToString();
                                    int prDId = Convert.ToInt32(purDtlId);
                                    stk = stk + Convert.ToDouble(stock);

                                    if (stk >= sale)
                                    {
                                        var max_41 = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN MAX(T_SALE_DETAILS_ID)+1 ELSE 1 END T_SALE_DETAILS_ID FROM T14041", objConn, objTrans).Rows[0]["T_SALE_DETAILS_ID"].ToString();
                                        stockRest = stk - sale;
                                        var insert41 = $"INSERT INTO T14041(T_SALE_DETAILS_ID,T_MEMO_NO,T_TYPE_CODE, T_PRODUCT_CODE, T_PACK_CODE, T_SALE_QUANTITY, T_SALE_PRICE,T_PURCHASE_PRICE, T_TOTAL_PRICE)VALUES('{max_41}','{t14040.T_MEMO_NO}','{i.T_TYPE_CODE}', '{i.T_PRODUCT_CODE}', '{i.T_PACK_CODE}', '{i.T_SALE_QUANTITY}', '{i.T_SALE_PRICE}', '{i.T_PURCHASE_PRICE}','{i.T_TOTAL_PRICE}')";
                                        command_2(insert41, objConn, objTrans);
                                        //-----------------------
                                        var totalPrice = sale * Convert.ToDouble(i.T_SALE_PRICE);
                                        var max_41_dtl = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN MAX(T_SALE_DTL_ID)+1 ELSE 1 END T_SALE_DTL_ID FROM T14041_dtl", objConn, objTrans).Rows[0]["T_SALE_DTL_ID"].ToString();
                                        var insert41_dtl = $"INSERT INTO T14041_dtl(T_SALE_DTL_ID,T_MEMO_NO,T_PURCHASE_ID,T_TYPE_CODE, T_PRODUCT_CODE, T_PACK_CODE, T_SALE_QUANTITY, T_SALE_PRICE,T_PURCHASE_PRICE, T_TOTAL_PRICE)VALUES('{max_41_dtl}','{t14040.T_MEMO_NO}','{purId_stock}', '{i.T_TYPE_CODE}', '{i.T_PRODUCT_CODE}', '{i.T_PACK_CODE}', '{sale}', '{i.T_SALE_PRICE}', '{i.T_PURCHASE_PRICE}','{i.T_TOTAL_PRICE}')";
                                        command_2(insert41_dtl, objConn, objTrans);

                                        //-----------updat T14035--------------
                                        if (shopId == "1")
                                        {
                                            var update35 = $"UPDATE T14035 SET T_STOCK ={stockRest} WHERE T_PUR_STOCK_ID={prDId}";
                                            command_2(update35, objConn, objTrans);
                                        }
                                        else if (shopId == "2")
                                        {
                                            var update36 = $"UPDATE T14036 SET T_STOCK ={stockRest} WHERE T_PET2_STOCK_ID={prDId}";
                                            command_2(update36, objConn, objTrans);
                                        }
                                        else if (shopId == "3")
                                        {
                                            var update37 = $"UPDATE T14037 SET T_STOCK ={stockRest} WHERE T_OUTLET_STOCK_ID={prDId}";
                                            command_2(update37, objConn, objTrans);
                                        }
                                        status = "1";
                                        count = 1 + count;
                                        sale = 0;
                                    }
                                    else
                                    {
                                        var totalPrice = stk * Convert.ToDouble(i.T_SALE_PRICE);
                                        var max_41_dtl = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN MAX(T_SALE_DTL_ID)+1 ELSE 1 END T_SALE_DTL_ID FROM T14041_dtl", objConn, objTrans).Rows[0]["T_SALE_DTL_ID"].ToString();
                                        var insert41_dtl = $"INSERT INTO T14041_dtl(T_SALE_DTL_ID,T_MEMO_NO,T_PURCHASE_ID,T_TYPE_CODE, T_PRODUCT_CODE, T_PACK_CODE, T_SALE_QUANTITY, T_SALE_PRICE,T_PURCHASE_PRICE, T_TOTAL_PRICE)VALUES('{max_41_dtl}','{t14040.T_MEMO_NO}','{purId_stock}', '{i.T_TYPE_CODE}', '{i.T_PRODUCT_CODE}', '{i.T_PACK_CODE}', '{stk}', '{i.T_SALE_PRICE}', '{i.T_PURCHASE_PRICE}','{totalPrice}')";
                                        command_2(insert41_dtl, objConn, objTrans);
                                        sale = sale - stk;
                                        stk = 0;
                                        //-----------updat T14035--------------
                                        if (shopId == "1")
                                        {
                                            var update35 = $"UPDATE T14035 SET T_STOCK =0 WHERE T_PUR_STOCK_ID={prDId}";
                                            command_2(update35, objConn, objTrans);
                                        }
                                        else if (shopId == "2")
                                        {
                                            var update36 = $"UPDATE T14036 SET T_STOCK =0 WHERE T_PET2_STOCK_ID={prDId}";
                                            command_2(update36, objConn, objTrans);
                                        }
                                        else if (shopId == "3")
                                        {
                                            var update37 = $"UPDATE T14037 SET T_STOCK =0 WHERE T_OUTLET_STOCK_ID={prDId}";
                                            command_2(update37, objConn, objTrans);
                                        }
                                    }
                                    //-------------
                                }
                                else
                                {

                                }
                            }
                        }
                        if (count == t14014.Count)
                        {
                            objTrans.Commit();
                            sms = "Update successfully-1/" + saleId;// + ust_id;
                        }
                        else
                        {
                            sms = " Do not Update-0";
                            objTrans.Rollback();

                        }
                    }
                }
                catch (Exception ex)
                {
                    var kk = ex.Message;
                    sms = " Do not Save-0";
                    objTrans.Rollback();
                }
                finally
                {
                    objConn.Close();
                }
            }
            return sms;
        }
        public T14040Data GeneratMemo(string date)
        {
            T14040Data t013 = new T14040Data();
            //var date = DateTime.Now.ToString("dd-MM-yyyy");
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
        public DataTable Invoice(int saleId,string shopId)
        {
            DataTable sql = new DataTable();
            sql = Query($"select T14040.T_SALE_ID, T14040.T_MEMO_NO, T14010.T_CUSTOMER_NAME,T14010.T_CUSTOMER_MOBILE,T14010.T_CUSTOMER_ADDRESS, T_GRAND_TOTAL,T_AFTER_DISCOUNT,T_VAT_TAX,T_TOTAL_VAT, T_DISCOUNT, T_PAMENT, T_DUE, T_SALE_DATE from T14040 JOIN T14010 ON T14040.T_CUSTOMER_ID = T14010.T_CUSTOMER_ID where T14040.T_SALE_ID='{saleId}'");
            return sql;
        }
        public DataTable InvoiceDetails(int saleId)
        {
            DataTable sql = new DataTable();
            sql = Query($"select T14040.T_SALE_ID, T14040.T_MEMO_NO, T14010.T_CUSTOMER_NAME, T_GRAND_TOTAL, T_DISCOUNT, T_PAMENT, T_DUE, T_SALE_DATE,T14001.T_TYPE_NAME,T14002.T_ITEM_NAME, T14003.T_PRODUCT_NAME, T14004.T_PACK_NAME,T14004.T_PACK_WEIGHT, cast (T14041.T_SALE_PRICE as decimal(12,0))T_SALE_PRICE, cast (T14041.T_SALE_QUANTITY as decimal(12,0))T_SALE_QUANTITY,cast ((T14041.T_SALE_QUANTITY/T14004.T_UM)as decimal(12,0) )T_BOX, cast (T14041.T_TOTAL_PRICE as decimal(12,0))T_TOTAL_PRICE from T14040 JOIN T14010 ON T14040.T_CUSTOMER_ID = T14010.T_CUSTOMER_ID JOIN T14041 ON T14040.T_MEMO_NO = T14041.T_MEMO_NO JOIN T14003 ON T14041.T_PRODUCT_CODE = T14003.T_PRODUCT_CODE JOIN T14004 ON T14041.T_PACK_CODE = T14004.T_PACK_CODE JOIN T14001 ON T14041.T_TYPE_CODE = T14001.T_TYPE_CODE JOIN T14002 ON T14003.T_ITEM_CODE = T14002.T_ITEM_CODE  where T14040.T_SALE_ID='{saleId}' ORDER BY T14041.T_SALE_DETAILS_ID DESC");
            return sql;
        }
    }
}
