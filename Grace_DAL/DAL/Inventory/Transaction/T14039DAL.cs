using Grace_DAL.Shared.Inventory.Transaction;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Grace_DAL.DAL.Inventory.Transaction
{
    public class T14039DAL : CommonDAL
    {
        public DataTable GetProductByCode(string code)
        {
            DataTable sql = new DataTable();
            sql = Query($@"select T14003.T_PRODUCT_CODE, T14003.T_TYPE_CODE, T_PRODUCT_NAME,T14004.T_PACK_CODE, T_TYPE_NAME, T_PURCHASE_PRICE from T14003 JOIN T14004 ON T14003.T_PRODUCT_CODE = T14004.T_PRODUCT_CODE JOIN T14005 ON T14004.T_PACK_CODE = T14005.T_PACK_CODE JOIN T14001 ON T14003.T_TYPE_CODE = T14001.T_TYPE_CODE where T14003.T_PRODUCT_CODE ='{code}'");
            return sql;
        }
        public string SaveData(T14039Data t14039, string user)
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
                    stockList = Query_2($"SELECT T_PUR_STOCK_ID,T_PURCHASE_ID,T_STOCK,T_PRODUCT_CODE,T_PACK_CODE,T_PURCHASE_PRICE FROM T14035 WHERE T_STOCK !='0' AND T_PRODUCT_CODE is not null AND T_PACK_CODE is not null ", objConn, objTrans);
                    if (t14039.T_QUANTITY != 0)
                    {
                        //--------------------------
                        double stockRest = 0;
                        //int saleRest = 0;
                        double sale = 0;
                        var status = "0";
                        double stk = 0;
                        var dtFiltered = stockList.AsEnumerable().Where(r => r.Field<String>("T_PRODUCT_CODE") == t14039.T_PRODUCT_CODE && r.Field<String>("T_PACK_CODE") == t14039.T_PACK_CODE).OrderBy(x => x.Field<int>("T_PUR_STOCK_ID")).CopyToDataTable();
                        sale = sale + Convert.ToDouble(t14039.T_QUANTITY);
                        foreach (DataRow k in dtFiltered.Rows)
                        {
                            if (status == "0")
                            {
                                var stock = k["T_STOCK"].ToString();
                                var purDtlId = k["T_PUR_STOCK_ID"].ToString();
                                var purId_stock = k["T_PURCHASE_ID"].ToString();
                                var purPrice = k["T_PURCHASE_PRICE"].ToString();
                                int prDId = Convert.ToInt32(purDtlId);
                                stk = stk + Convert.ToDouble(stock);

                                if (stk >= sale)
                                {
                                    // var max_41 = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN MAX(T_SALE_DETAILS_ID)+1 ELSE 1 END T_SALE_DETAILS_ID FROM T14041", objConn, objTrans).Rows[0]["T_SALE_DETAILS_ID"].ToString();
                                    stockRest = stk - sale;
                                    var insert41 = $"INSERT INTO T14039(T_TYPE_CODE, T_PRODUCT_CODE, T_PACK_CODE, T_QUANTITY, T_PURCHASE_PRICE,T_ENTRY_DATE,T_ENTRY_USER)VALUES('{t14039.T_TYPE_CODE}', '{t14039.T_PRODUCT_CODE}', '{t14039.T_PACK_CODE}', '{t14039.T_QUANTITY}', '{purPrice}','{date}','{user}')";
                                    command_2(insert41, objConn, objTrans);
                                    //-----------updat T14035--------------
                                    var update12 = $"UPDATE T14035 SET T_STOCK ={stockRest} WHERE T_PUR_STOCK_ID={prDId}";
                                    command_2(update12, objConn, objTrans);
                                    status = "1";
                                    count = 1 + count;
                                    sale = 0;
                                }
                                else
                                {
                                    //    var totalPrice = stk * Convert.ToDouble(i.T_SALE_PRICE);
                                    //    var max_41_dtl = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN MAX(T_SALE_DTL_ID)+1 ELSE 1 END T_SALE_DTL_ID FROM T14041_dtl", objConn, objTrans).Rows[0]["T_SALE_DTL_ID"].ToString();
                                    //    var insert41_dtl = $"INSERT INTO T14041_dtl(T_SALE_DTL_ID,T_MEMO_NO,T_PURCHASE_ID,T_TYPE_CODE, T_PRODUCT_CODE, T_PACK_CODE, T_SALE_QUANTITY, T_SALE_PRICE,T_PURCHASE_PRICE, T_TOTAL_PRICE)VALUES('{max_41_dtl}','{t14040.T_MEMO_NO}','{purId_stock}', '{i.T_TYPE_CODE}', '{i.T_PRODUCT_CODE}', '{i.T_PACK_CODE}', '{stk}', '{i.T_SALE_PRICE}', '{i.T_PURCHASE_PRICE}','{totalPrice}')";
                                    //    command_2(insert41_dtl, objConn, objTrans);
                                    //    sale = sale - stk;
                                    //    stk = 0;
                                    //    //-----------updat T14035--------------
                                    //    var update12 = $"UPDATE T14035 SET T_STOCK =0 WHERE T_PUR_STOCK_ID={prDId}";
                                    //command_2(update12, objConn, objTrans);


                                }
                                //-------------
                            }
                            else
                            {

                            }
                        }

                        if (count == 1)
                        {
                            objTrans.Commit();
                            sms = "Save successfully-1";
                        }
                        else
                        {
                            sms = " Do not Save-0";
                            objTrans.Rollback();

                        }
                    }
                    else
                    {

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
        public DataTable GetDamageProductList()
        {
            DataTable sql = new DataTable();
            sql = Query($@"select T14039.T_PRODUCT_CODE , T14003.T_PRODUCT_NAME, T14039.T_PACK_CODE , T14004.T_PACK_NAME,T14001.T_TYPE_NAME, T14039.T_QUANTITY, T14039.T_ENTRY_DATE from T14039 JOIN T14003 ON T14039.T_PRODUCT_CODE = T14003.T_PRODUCT_CODE JOIN T14004 ON T14039.T_PACK_CODE = T14004.T_PACK_CODE JOIN T14001 ON T14039.T_TYPE_CODE = T14001.T_TYPE_CODE");
            return sql;
        }

    }
}

