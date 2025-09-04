using Grace_DAL.Shared.Inventory.Setup;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Grace_DAL.DAL.Inventory.Setup
{
    public class T14003DAL:CommonDAL
    {
        public DataTable LoadData()
        {
            DataTable dt = new DataTable();
            dt = Query("select t03.T_PRODUCT_ID, t03.T_PRODUCT_CODE,t03.T_ITEM_CODE,t01.T_TYPE_NAME, t03.T_PRODUCT_NAME, t03.T_PRODUCT_IMAGE, t03.T_TYPE_CODE, t04.T_PACK_ID, t04.T_PACK_CODE, t04.T_PACK_NAME, t04.T_PACK_WEIGHT, t04.T_UM, t05.T_PRICE_ID, t05.T_PURCHASE_PRICE, t05.T_HOLE_SALE_PRICE, t05.T_RETAIL_SALE_PRICE from T14003 t03 JOIN T14001 t01 ON t03.T_TYPE_CODE =t01.T_TYPE_CODE JOIN T14004 t04 ON t03.T_PRODUCT_CODE =t04.T_PRODUCT_CODE JOIN T14005 t05 ON t03.T_PRODUCT_CODE =t05.T_PRODUCT_CODE ORDER BY t03.T_PRODUCT_CODE DESC"); 
            //dt = Query("select T_PRODUCT_ID,T_PRODUCT_CODE, T_PRODUCT_NAME,T_PRODUCT_IMAGE,T14003.T_TYPE_CODE,T14001.T_TYPE_NAME  from T14003 JOIN T14001 ON T14003.T_TYPE_CODE = T14001.T_TYPE_CODE ORDER BY T_PRODUCT_CODE DESC");
            return dt;
        }
        public DataTable LoadTypeData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_TYPE_CODE,T_ITEM_CODE, T_TYPE_NAME  from T14001 ORDER BY T_TYPE_CODE DESC");
            return dt;
        }
        public string SaveData(T14003_Img_Ins t14003,string user)
        {
            string sms = "";
            SqlTransaction objTrans = null;
            var date = DateTime.Now.ToString("dd-MM-yyyy");
            using (SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString)) {

                try
                {
                    objConn.Open();
                    objTrans = objConn.BeginTransaction();
                    if (t14003.T_PRODUCT_ID == 0)
                    {                       
                        var maxProCode = Query_2($"select case when (select count(*) from T14003)>0 then ( select CONCAT('P',MAX( cast( T_PRODUCT_CODE as int)+1)) T_PRODUCT_CODE FROM ( select ROW_NUMBER() OVER(ORDER BY T_PRODUCT_CODE) + 101 AS T_PRODUCT_CODE from T14003 ) t_1 ) else (select 'P101' T_PRODUCT_CODE FROM T14003 ) end T_PRODUCT_CODE", objConn, objTrans).Rows[0]["T_PRODUCT_CODE"].ToString();
                        var save_03 =$"INSERT INTO T14003 (T_PRODUCT_CODE,T_TYPE_CODE,T_ITEM_CODE,T_PRODUCT_NAME,T_PRODUCT_IMAGE,T_ENTRY_DATE,T_ENTRY_USER) VALUES('{maxProCode}','{t14003.T_TYPE_CODE}','{t14003.T_ITEM_CODE}','{t14003.T_PRODUCT_NAME}','{t14003.T_PRODUCT_IMAGE}','{date}','{user}')";
                        command_2(save_03, objConn, objTrans);
                        //----------T14004------------
                        var maxPackCode = Query_2($"select CASE when count(*) >0 Then MAX( cast( T_PACK_CODE as int)+1) else 1 end T_PACK_CODE from T14004", objConn, objTrans).Rows[0]["T_PACK_CODE"].ToString();
                        var save_04 = $"INSERT INTO T14004 (T_PRODUCT_CODE,T_PACK_CODE,T_PACK_NAME,T_PACK_WEIGHT,T_UM) VALUES('{maxProCode}','{maxPackCode}','{t14003.T_PACK_NAME}','{t14003.T_PACK_WEIGHT}','{t14003.T_UM}')";
                        command_2(save_04, objConn, objTrans);
                        //----------T14005------
                        var maxPriceCode = Query_2($"select CASE when count(*) >0 Then MAX( cast( T_PRICE_CODE as int)+1) else 1 end T_PRICE_CODE from T14005", objConn, objTrans).Rows[0]["T_PRICE_CODE"].ToString();
                        var save_05 = $"INSERT INTO T14005 (T_PRICE_CODE,T_PRODUCT_CODE,T_PACK_CODE,T_PURCHASE_PRICE, T_HOLE_SALE_PRICE, T_RETAIL_SALE_PRICE,T_ENTRY_USER,T_ENTRY_DATE) VALUES('{maxPriceCode}','{maxProCode}', '{maxPackCode}', '{t14003.T_PURCHASE_PRICE}', '{t14003.T_HOLE_SALE_PRICE}','{t14003.T_RETAIL_SALE_PRICE}','{user}','{date}')";
                      var sa=  command_2(save_05, objConn, objTrans);
                        if (sa == true)
                        {
                            sms = "Save Successfully-1";
                        }
                        else
                        {
                            sms = "Do not Save-0";
                        }
                    }
                    else
                    {
                        var update_03 = $"UPDATE T14003 SET T_TYPE_CODE='{t14003.T_TYPE_CODE}',T_ITEM_CODE='{t14003.T_ITEM_CODE}',T_PRODUCT_NAME='{t14003.T_PRODUCT_NAME}', T_PRODUCT_IMAGE='{t14003.T_PRODUCT_IMAGE}',T_UPDATE_DATE='{date}',T_UPDATE_USER='{user}' WHERE T_PRODUCT_ID ='{t14003.T_PRODUCT_ID}'";
                        command_2(update_03, objConn, objTrans);
                        //------------T14004---------
                        var update_04 = $"UPDATE T14004 SET T_PACK_NAME='{t14003.T_PACK_NAME}',T_PACK_WEIGHT='{t14003.T_PACK_WEIGHT}',T_UM='{t14003.T_UM}' WHERE T_PACK_ID ='{t14003.T_PACK_ID}'";
                        command_2(update_04, objConn, objTrans);
                        //----------T14005---------
                        var update_05 = $"UPDATE T14005 SET T_PURCHASE_PRICE='{t14003.T_PURCHASE_PRICE}',T_HOLE_SALE_PRICE='{t14003.T_HOLE_SALE_PRICE}', T_RETAIL_SALE_PRICE='{t14003.T_RETAIL_SALE_PRICE}',T_UPDATE_USER='{user}',T_UPDATE_DATE='{date}'  WHERE T_PRICE_ID ='{t14003.T_PRICE_ID}'";
                      var sa=  command_2(update_05, objConn, objTrans);
                        if (sa == true)
                        {
                            sms = "Update Successfully-1";
                        }
                        else
                        {
                            sms = "Do not Update-0";
                        }
                    }

                    objTrans.Commit();
                    sms = "Update Successfully-1";
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
