using Grace_DAL.Shared.Inventory.Transaction;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace Grace_DAL.DAL.Inventory.Transaction
{
    public class T14022DAL:CommonDAL
    {
        public DataTable GetPurchaseVerifyData(string shopId)
        {
            DataTable sql = new DataTable();
            sql = Query($@"SELECT ROW_NUMBER() OVER(Order by T_PURCHASE_ID)SL,T14020.T_PURCHASE_ID, T14020.T_INVOICE_NO, T14020.T_TYPE_CODE,T14020.T_CUSTOMER_ID, T14010.T_CUSTOMER_NAME, T_TOTAL_PRICE, T_DISCOUNT, T_AFTER_DISCOUNT, T_TOTAL_QUT, T_CASH, T_DUE, T14001.T_TYPE_NAME, T14020.T_ENTRY_DATE, T14020.T_ENTRY_USER, T11020.T_USER_NAME T_EMP_NAME FROM T14020 JOIN T14010 ON T14020.T_CUSTOMER_ID = T14010.T_CUSTOMER_ID JOIN T14001 ON T14020.T_TYPE_CODE = T14001.T_TYPE_CODE JOIN T11020 ON T14020.T_ENTRY_USER = T11020.T_USER_CODE  WHERE T_SHOP_ID='{shopId}' AND T14020.T_VERIFY_FLG IS NULL ORDER BY T_PURCHASE_ID ASC");
            return sql;
        }
        public string SavePurVerifyData(List<T14020Data> t14020, string user)
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
                    foreach (var i in t14020)
                    {
                        var vutrMax = Query_2($"SELECT TOP 1 CAST(VOUCHER_NO  AS INT)+1 VOUCHER_NO FROM AT13001 ORDER BY CAST(VOUCHER_NO  AS INT)DESC", objConn, objTrans).Rows[0]["VOUCHER_NO"].ToString();
                        var save_01 = $"INSERT INTO AT13001 (VOUCHER_NO,T_CUSTOMER_ID, VOUCHER_DATE, CENTER_CODE, VAUCHER_TYPE_CODE,TRANSACTION_TYPE_CODE,PARTY_TYPE_CODE,PARTY_CODE,TOTAL_DEBIT, TOTAL_CREDIT,ENTRY_USER,ENTRY_DATE)VALUES('{vutrMax}','{i.T_CUSTOMER_ID}', '{i.T_ENTRY_DATE}', '101', '101', '101','101','101',{i.T_TOTAL_PRICE},{i.T_TOTAL_PRICE},'{user}','{date}')";
                        command_2(save_01, objConn, objTrans);
                        for (var k=0;k< 4;k ++)
                        {
                            var head = "";
                            var purpous = "";
                            var narration = "";
                            decimal debit = 0;
                            decimal credit = 0;
                            if (k == 0 && i.T_TOTAL_PRICE != 0)
                            {
                                head = "102";
                                purpous = "102";
                                narration = "Purchase Product for Interior Shop";
                                debit = i.T_TOTAL_PRICE;
                                credit = 0;
                            }                            
                            else if (k == 1 && i.T_DISCOUNT != 0)
                            {                               
                                head = "104";
                                purpous = "102";
                                narration = "Purchase Product for Interior Shop";
                                debit = 0;
                                credit = i.T_DISCOUNT;
                            }
                            else if (k == 2 && i.T_CASH != 0)
                            {                               
                                head = "101";
                                purpous = "104";
                                narration = "Purchase Product for Interior Shop";
                                debit = 0;
                                credit = i.T_CASH;
                            }
                            else if (k == 3 && i.T_DUE != 0)
                            {                                
                                head = "107";
                                purpous = "104";
                                narration = "Purchase Product for Interior Shop";
                                debit =0;
                                credit = i.T_DUE;
                            }
                            if (debit != 0 || credit != 0)
                            {                                
                                var save02 = $"INSERT INTO AT13002 (VOUCHER_NO, ACCOUNT_HEADER_CODE, DEPARTMENT_CODE, PURPOSE_CODE,VOU_DESCRIPTION,PARTY_CODE,DEBIT,CREDIT)VALUES('{vutrMax}', '{head}', '101', '{purpous}', '{narration}', '101', {debit}, {credit})";
                                command_2(save02, objConn, objTrans);
                            }
                        }
                        //------ T14080 -----------
                        var max_80 = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN MAX(T_BALANCE_ID)+1 ELSE 1 END T_BALANCE_ID FROM T14080", objConn, objTrans).Rows[0]["T_BALANCE_ID"].ToString();
                        decimal total = i.T_TOTAL_PRICE - i.T_DISCOUNT;
                        var ins_80 = $"INSERT INTO T14080 (T_BALANCE_ID,T_PURCHASE_ID,T_MEM_INV_NO,T_CUSTOMER_ID,T_TOTAL, T_PAYMENT,T_DUE, T_SHOP_ID,T_FORM_CODE,T_VERIFY_FLG,T_ENTRY_USER,T_ENTRY_DATE)VALUES('{max_80}','{i.T_PURCHASE_ID}', '{i.T_INVOICE_NO}','{i.T_CUSTOMER_ID}','{total}', '{i.T_CASH}', '{i.T_DUE}', '1','T14020', '1','{user}','{i.T_ENTRY_DATE}')";
                        command_2(ins_80, objConn, objTrans);
                        //-------------------
                        var update_11 = $"UPDATE T14020 SET T_VERIFY_FLG='{i.T_VERIFY_FLG}' WHERE T_PURCHASE_ID='{i.T_PURCHASE_ID}'";
                        command_2(update_11, objConn, objTrans);
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
