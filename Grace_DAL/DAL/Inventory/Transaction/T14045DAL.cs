using Grace_DAL.Shared.Inventory.Transaction;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Grace_DAL.DAL.Inventory.Transaction
{
    public class T14045DAL:CommonDAL
    {
        public DataTable GetSaleReceiveData(string shopId)
        {
            DataTable sql = new DataTable();
            sql = Query($@"select T_SALE_ID, t40.T_CUSTOMER_ID, t10.T_CUSTOMER_NAME, T_MEMO_NO, t40.T_TYPE_CODE, t1.T_TYPE_NAME, T_GRAND_TOTAL T_TOTAL_PRICE, T_DISCOUNT, T_PAMENT T_CASH, T_DUE,T_DUE BCK_DUE, t40.T_ENTRY_DATE from T14040 t40 JOIN T14010 t10 ON t40.T_CUSTOMER_ID = t10.T_CUSTOMER_ID LEFT JOIN T14001 t1 ON t40.T_TYPE_CODE = t1.T_TYPE_CODE where T_DUE !=0 AND T_SHOP_ID ='{shopId}'");
            return sql;
        }
        public string SaveReceivePayment(List<T14045Data> t14045, string user, string shopId)
        {
            var sms = "";
            var date = DateTime.Now.ToString("dd-MM-yyyy");
            SqlTransaction objTrans = null;

            using (SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString))
            {

                try
                {
                    objConn.Open();
                    objTrans = objConn.BeginTransaction();
                    foreach (var i in t14045)
                    {
                        DataTable getData = Query_2($"select T_PAMENT,T_DUE from T14040 where T_SALE_ID ='{i.T_SALE_ID}'", objConn, objTrans);
                        decimal cash = Convert.ToDecimal(getData.Rows[0]["T_PAMENT"].ToString()) + i.T_PAYMENT;
                        decimal due = Convert.ToDecimal(getData.Rows[0]["T_DUE"].ToString()) - i.T_PAYMENT;

                        var update_20 = $"UPDATE T14040 SET T_PAMENT='{cash}',T_DUE='{due}' WHERE T_SALE_ID='{i.T_SALE_ID}'";
                        command_2(update_20, objConn, objTrans);
                        var max_80 = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN MAX(T_BALANCE_ID)+1 ELSE 1 END T_BALANCE_ID FROM T14080", objConn, objTrans).Rows[0]["T_BALANCE_ID"].ToString();
                        var ins_80 = $"INSERT INTO T14080 (T_BALANCE_ID,T_SALE_ID,T_MEM_INV_NO,T_CUSTOMER_ID,T_TOTAL, T_PAYMENT,T_DUE, T_SHOP_ID,T_FORM_CODE,T_ENTRY_USER,T_ENTRY_DATE)VALUES('{max_80}','{i.T_SALE_ID}', '{i.T_MEMO_NO}', '{i.T_CUSTOMER_ID}','00','{i.T_PAYMENT}', '{due}', '{shopId}','T14040', '{user}','{date}')";
                        command_2(ins_80, objConn, objTrans);
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
    }
}
