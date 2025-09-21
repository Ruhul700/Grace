using Grace_DAL.Shared.Inventory.Transaction;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Grace_DAL.DAL.Inventory.Transaction
{
    public class T14042DAL:CommonDAL
    {
        public DataTable GetSalesVerifyData(string userCode)
        {
            DataTable dtSale = new DataTable();
            string condition = string.Empty;
            if (userCode != "1")
            {
                condition = $"AND T14040.T_ENTRY_USER = '{userCode}'";
            }
            dtSale = Query($"SELECT ROW_NUMBER() OVER(Order by T_SALE_ID)SL, T_SALE_ID, T14040.T_MEMO_NO, T14040.T_TYPE_CODE, T14010.T_CUSTOMER_NAME,T14040.T_CUSTOMER_ID, T_GRAND_TOTAL ,T_DISCOUNT, T_AFTER_DISCOUNT,T_PAMENT, T_DUE, T_SALE_TOTAL,  T14040.T_ENTRY_DATE, T14040.T_ENTRY_USER, T11020.T_USER_NAME T_EMP_NAME FROM T14040 JOIN T14010 ON T14040.T_CUSTOMER_ID = T14010.T_CUSTOMER_ID  JOIN T11020 ON T14040.T_ENTRY_USER = T11020.T_USER_CODE WHERE T_VERIFY_FLG IS NULL AND (T_PRE_ORDER IS NULL OR T_PRE_ORDER ='0'){condition}  ORDER BY T_SALE_ID ASC");
            return dtSale;
        }
        public string SaleUpdateData(List<T14040Data> t14040, string user)
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
                    foreach (var i in t14040)
                    {
                        var vutrMax = Query_2($"SELECT TOP 1 CAST(VOUCHER_NO  AS INT)+1 VOUCHER_NO FROM AT13001 ORDER BY CAST(VOUCHER_NO  AS INT)DESC", objConn, objTrans).Rows[0]["VOUCHER_NO"].ToString();
                        var save_01 = $"INSERT INTO AT13001 (VOUCHER_NO,T_CUSTOMER_ID, VOUCHER_DATE, CENTER_CODE, VAUCHER_TYPE_CODE,TRANSACTION_TYPE_CODE,PARTY_TYPE_CODE,PARTY_CODE,TOTAL_DEBIT, TOTAL_CREDIT,ENTRY_USER,ENTRY_DATE)VALUES('{vutrMax}','{i.T_CUSTOMER_ID}', '{i.T_ENTRY_DATE}', '101', '101', '101','101','101',{i.T_GRAND_TOTAL},{i.T_GRAND_TOTAL},'{user}','{date}')";
                        command_2(save_01, objConn, objTrans);
                        for (var k = 0; k < 4; k++)
                        {
                            decimal grandTotal = Convert.ToDecimal(i.T_GRAND_TOTAL);
                            var head = "";
                            var purpous = "";
                           var narration = "Sale Product in Interior Shop";
                            decimal debit = 0;
                            decimal credit = 0;
                            if (k == 0 && grandTotal != 0)
                            {
                                head = "103";
                                purpous = "103";                               
                                debit =0 ;
                                credit = grandTotal;
                            }
                            else if (k == 1 && i.T_DISCOUNT != 0)
                            {
                                head = "105";
                                purpous = "103";                               
                                debit = i.T_DISCOUNT;
                                credit = 0;
                            }
                            else if (k == 2 && i.T_PAMENT != 0)
                            {
                                head = "101";
                                purpous = "104";                              
                                debit = i.T_PAMENT;
                                credit = 0;
                            }
                            else if (k == 3 && i.T_DUE != 0)
                            {
                                head = "106";
                                purpous = "104";                               
                                debit = i.T_DUE;
                                credit = 0;
                            }
                            if (debit != 0 || credit != 0)
                            {
                                var save02 = $"INSERT INTO AT13002 (VOUCHER_NO, ACCOUNT_HEADER_CODE, DEPARTMENT_CODE, PURPOSE_CODE,VOU_DESCRIPTION,PARTY_CODE,DEBIT,CREDIT)VALUES('{vutrMax}', '{head}', '101', '{purpous}', '{narration}', '101', {debit}, {credit})";
                                command_2(save02, objConn, objTrans);
                            }
                        }
                        //------ T14080 -----------                        
                        var max_80 = Query_2($"SELECT CASE WHEN COUNT(*)>0 THEN MAX(T_BALANCE_ID)+1 ELSE 1 END T_BALANCE_ID FROM T14080", objConn, objTrans).Rows[0]["T_BALANCE_ID"].ToString();
                        var ins_80 = $"INSERT INTO T14080 (T_BALANCE_ID,T_SALE_ID,T_MEM_INV_NO,T_CUSTOMER_ID,T_TOTAL, T_PAYMENT,T_DUE, T_SHOP_ID,T_FORM_CODE,T_ENTRY_USER,T_ENTRY_DATE)VALUES('{max_80}','{i.T_SALE_ID}', '{i.T_MEMO_NO}','{i.T_CUSTOMER_ID}','{i.T_AFTER_DISCOUNT}', '{i.T_PAMENT}', '{i.T_BALANCE}', '1','T14040', '{user}','{i.T_ENTRY_DATE}')";
                        command_2(ins_80, objConn, objTrans);
                        //------------------------
                        var update_13 = $"UPDATE T14040 SET T_VERIFY_FLG='{i.T_VERIFY_FLG}' WHERE T_SALE_ID='{i.T_SALE_ID}'";
                        command_2(update_13, objConn, objTrans);
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
