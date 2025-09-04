using Grace_DAL.Shared.Inventory.Transaction;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Grace_DAL.DAL.Inventory.Transaction
{
    public class T14046DAL:CommonDAL
    {
        public DataTable GetReceiveVerifyData(string shopId)
        {
            DataTable sql = new DataTable();
            sql = Query($@"SELECT ROW_NUMBER() OVER(Order by T_BALANCE_ID)SL,T_BALANCE_ID, T14080.T_MEM_INV_NO, T14010.T_CUSTOMER_NAME,T14010.T_CUSTOMER_ID, T_PAYMENT, T_DUE, T14080.T_ENTRY_DATE, T11020.T_USER_NAME T_EMP_NAME FROM T14080 JOIN T14010 ON T14080.T_CUSTOMER_ID = T14010.T_CUSTOMER_ID JOIN T11020 ON T14080.T_ENTRY_USER = T11020.T_USER_CODE WHERE T_SHOP_ID='{shopId}' AND T14080.T_FORM_CODE='T14040' AND T14080.T_VERIFY_FLG IS NULL");
            return sql;
        }
        public string SaveReceiveVerifyData(List<T14046Data> t14024, string user)
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
                    foreach (var i in t14024)
                    {
                        //---------------------
                        var vutrMax = Query_2($"SELECT TOP 1 CAST(VOUCHER_NO  AS INT)+1 VOUCHER_NO FROM AT13001 ORDER BY CAST(VOUCHER_NO  AS INT)DESC", objConn, objTrans).Rows[0]["VOUCHER_NO"].ToString();
                        var save_01 = $"INSERT INTO AT13001 (VOUCHER_NO,T_CUSTOMER_ID, VOUCHER_DATE, CENTER_CODE, VAUCHER_TYPE_CODE,TRANSACTION_TYPE_CODE,PARTY_TYPE_CODE,PARTY_CODE,TOTAL_DEBIT, TOTAL_CREDIT,ENTRY_USER,ENTRY_DATE)VALUES('{vutrMax}','{i.T_CUSTOMER_ID}', '{i.T_ENTRY_DATE}', '101', '101', '101','101','101',{i.T_PAYMENT},{i.T_PAYMENT},'{user}','{date}')";
                        command_2(save_01, objConn, objTrans);
                        for (var k = 0; k < 4; k++)
                        {
                            var head = "";
                            var purpous = "";
                            var narration = "Receive in Interior Shop";
                            decimal debit = 0;
                            decimal credit = 0;
                            if (k == 0 && i.T_PAYMENT != 0)
                            {
                                head = "106";
                                purpous = "103";                                
                                debit = 0;
                                credit = i.T_PAYMENT;
                            }
                            else if (k == 1 && i.T_PAYMENT != 0)
                            {
                                head = "101";
                                purpous = "103";                                
                                debit = i.T_PAYMENT;
                                credit = 0;
                            }
                            if (debit != 0 || credit != 0)
                            {
                                var save02 = $"INSERT INTO AT13002 (VOUCHER_NO, ACCOUNT_HEADER_CODE, DEPARTMENT_CODE, PURPOSE_CODE,VOU_DESCRIPTION,PARTY_CODE,DEBIT,CREDIT)VALUES('{vutrMax}', '{head}', '101', '{purpous}', '{narration}', '101', {debit}, {credit})";
                                command_2(save02, objConn, objTrans);
                            }
                        }
                        //--------------------
                        var update_11 = $"UPDATE T14080 SET T_VERIFY_FLG='{i.T_VERIFY_FLG}' WHERE T_BALANCE_ID='{i.T_BALANCE_ID}'";
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
