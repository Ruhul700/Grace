using Grace_DAL.Shared.Inventory.Transaction;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Grace_DAL.DAL.Inventory.Transaction
{
    public class T14036DAL : CommonDAL
    {
        public DataTable GetProduct()
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT T_PRODUCT_CODE,T_PRODUCT_NAME FROM T14003");
            return sql;
        }

        public DataTable GetPack()
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT T_PACK_CODE,T_PACK_NAME FROM T14004");
            return sql;
        }
        public DataTable GetShop(string shopId)
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT distinct T_SITE_CODE,T_SITE_NAME FROM T00002 Where T_SITE_CODE !='{shopId}'");
            return sql;
        }
        public DataTable GetStockData(string procuct, string pack, string shopId)
        {
            DataTable sql = new DataTable();

            var param = "0";

            if (procuct == null && pack == null)
            {
                param = "T_STOCK !='0'";
            }
            else if (procuct != null && pack == null)
            {
                param = $@"T_STOCK !='0' AND T_PRODUCT_CODE = '{procuct}'";
            }
            else if (procuct == null && pack != null)
            {
                param = $@"T_STOCK !='0' AND T_PACK_CODE = '{pack}'";
            }
            else
            {
                param = $@"T_STOCK !='0' AND T_PRODUCT_CODE = '{procuct}'AND T_PACK_CODE = '{pack}'";
            }
            if (shopId == "1")
            {
                sql = Query($@"select T_PUR_STOCK_ID T_STOCK_ID,T_PURCHASE_ID, T14035.T_PRODUCT_CODE, T14003.T_PRODUCT_NAME, T14035.T_PACK_CODE, T14004.T_PACK_NAME, T_PURCHASE_PRICE,T_STOCK,T_STOCK T_TRASFERABLE,T_SHOP_ID from T14035 JOIN T14003 ON T14035.T_PRODUCT_CODE = T14003.T_PRODUCT_CODE JOIN T14004 ON T14035.T_PACK_CODE = T14004.T_PACK_CODE where  {param}");

            }
            else if (shopId == "2")
            {
                sql = Query($@"select T_PET2_STOCK_ID T_STOCK_ID,T_PURCHASE_ID, T14036.T_PRODUCT_CODE, T14003.T_PRODUCT_NAME, T14036.T_PACK_CODE, T14004.T_PACK_NAME, T_PURCHASE_PRICE, T_STOCK,T_STOCK T_TRASFERABLE,T_SHOP_ID from T14036 JOIN T14003 ON T14036.T_PRODUCT_CODE = T14003.T_PRODUCT_CODE JOIN T14004 ON T14036.T_PACK_CODE = T14004.T_PACK_CODE where  {param}");

            }
            else if (shopId == "3")
            {
                sql = Query($@"select T_OUTLET_STOCK_ID T_STOCK_ID,T_PURCHASE_ID, T14037.T_PRODUCT_CODE, T14003.T_PRODUCT_NAME, T14037.T_PACK_CODE, T14004.T_PACK_NAME, T_PURCHASE_PRICE, T_STOCK,T_STOCK T_TRASFERABLE,T_SHOP_ID from T14037 JOIN T14003 ON T14037.T_PRODUCT_CODE = T14003.T_PRODUCT_CODE JOIN T14004 ON T14037.T_PACK_CODE = T14004.T_PACK_CODE where  {param}");

            }
            return sql;

        }
        public string SaveData(List<T14036Data> list, string shopId, string user)
        {
            string sms = "";
            SqlTransaction objTrans = null;
            var date = DateTime.Now.ToString("dd-MM-yyyy");
            using (SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString))
            {
                try
                {
                    objConn.Open();
                    objTrans = objConn.BeginTransaction();
                    foreach (var i in list)
                    {
                        if (shopId=="1")
                        {
                            var update35 = $"UPDATE T14035 SET T_STOCK='{i.T_TRASFERABLE}' WHERE T_PUR_STOCK_ID= '{i.T_STOCK_ID}'";
                            command_2(update35, objConn, objTrans);                            
                        }
                        else if (shopId == "2")
                        {
                            var update36 = $"UPDATE T14036 SET T_STOCK='{i.T_TRASFERABLE}' WHERE T_PET2_STOCK_ID= '{i.T_STOCK_ID}'";
                            command_2(update36, objConn, objTrans);                           
                        }
                        else if (shopId == "3")
                        {
                            var update37 = $"UPDATE T14037 SET T_STOCK='{i.T_TRASFERABLE}' WHERE T_OUTLET_STOCK_ID= '{i.T_STOCK_ID}'";
                            command_2(update37, objConn, objTrans);                           
                        }
                        if (i.T_SHOP_ID=="1")
                        {
                            var save35 = $"INSERT INTO T14035 (T_PURCHASE_ID,T_REFERENCE_ID, T_PRODUCT_CODE, T_PACK_CODE,T_QUANTITY,T_STOCK, T_PURCHASE_DATE,T_PURCHASE_PRICE)VALUES('{i.T_PURCHASE_ID}', '{i.T_SHOP_ID}','{i.T_PRODUCT_CODE}', '{i.T_PACK_CODE}', '{i.T_TRANSFER_QUTY}', '{i.T_TRANSFER_QUTY}', '{date}', '{i.T_PURCHASE_PRICE}')";
                            command_2(save35, objConn, objTrans);
                        }
                       else if (i.T_SHOP_ID == "2")
                        {
                            var save35 = $"INSERT INTO T14036 (T_PURCHASE_ID,T_REFERENCE_ID, T_PRODUCT_CODE, T_PACK_CODE,T_QUANTITY,T_STOCK, T_PURCHASE_DATE,T_PURCHASE_PRICE)VALUES('{i.T_PURCHASE_ID}', '{i.T_SHOP_ID}','{i.T_PRODUCT_CODE}', '{i.T_PACK_CODE}', '{i.T_TRANSFER_QUTY}', '{i.T_TRANSFER_QUTY}', '{date}', '{i.T_PURCHASE_PRICE}')";
                            command_2(save35, objConn, objTrans);
                        }
                       else if (i.T_SHOP_ID == "3")
                        {
                            var save35 = $"INSERT INTO T14037 (T_PURCHASE_ID,T_REFERENCE_ID, T_PRODUCT_CODE, T_PACK_CODE,T_QUANTITY,T_STOCK, T_PURCHASE_DATE,T_PURCHASE_PRICE)VALUES('{i.T_PURCHASE_ID}', '{i.T_SHOP_ID}','{i.T_PRODUCT_CODE}', '{i.T_PACK_CODE}', '{i.T_TRANSFER_QUTY}', '{i.T_TRANSFER_QUTY}', '{date}', '{i.T_PURCHASE_PRICE}')";
                            command_2(save35, objConn, objTrans);
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

    }
}
