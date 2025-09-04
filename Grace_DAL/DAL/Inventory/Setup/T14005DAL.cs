using Grace_DAL.Shared.Inventory.Setup;
using System;
using System.Data;

namespace Grace_DAL.DAL.Inventory.Setup
{
    public class T14005DAL : CommonDAL
    {
        public DataTable LoadData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_PRICE_ID, T_PRICE_CODE, T14005.T_PACK_CODE,T14005.T_PRODUCT_CODE, T_PACK_NAME, T_PURCHASE_PRICE, T_HOLE_SALE_PRICE, T_RETAIL_SALE_PRICE, T14004.T_PACK_NAME, T14003.T_PRODUCT_NAME from T14005 JOIN T14003 ON T14005.T_PRODUCT_CODE = T14003.T_PRODUCT_CODE JOIN T14004 ON T14005.T_PACK_CODE = T14004.T_PACK_CODE ORDER BY T_PRICE_CODE DESC");
            return dt;
        }
        public DataTable LoadProductData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_PRODUCT_CODE, T_PRODUCT_NAME  from T14003  ORDER BY T_PRODUCT_CODE DESC");
            return dt;
            //where T14003.T_PRODUCT_CODE  not in (select T14005.T_PRODUCT_CODE from T14005)
        }
        public DataTable LoadPackData()
        {
            DataTable dt = new DataTable();
            dt = Query($"select T_PACK_CODE,T_PACK_NAME from T14004");
            return dt;
        }
        public DataTable LoadPackList()
        {
            DataTable dt = new DataTable();
            dt = Query($"select T_PRODUCT_CODE,T_PACK_CODE,T_PACK_NAME from T14004");
            return dt;
        }
        public string SaveData(T14005Data t14005,string user)
        {
            string sms = "";
            var date = DateTime.Now.ToString("dd-MM-yyyy");
            if (t14005.T_PRICE_ID == 0)
            {
                var chk = Query($"select count(*) T_COUNT from t14005 where T_PRODUCT_CODE='{t14005.T_PRODUCT_CODE}' and T_PACK_CODE='{t14005.T_PACK_CODE}'").Rows[0]["T_COUNT"].ToString();
                if (chk=="0")
                {
                    var maxCode = Query($"select MAX( cast( T_PRICE_CODE as int)+1)T_PRICE_CODE from T14005").Rows[0]["T_PRICE_CODE"].ToString();
                    var sa = Command($"INSERT INTO T14005 (T_PRICE_CODE,T_PRODUCT_CODE,T_PACK_CODE,T_PURCHASE_PRICE, T_HOLE_SALE_PRICE, T_RETAIL_SALE_PRICE,T_ENTRY_USER,T_ENTRY_DATE) VALUES('{maxCode}','{t14005.T_PRODUCT_CODE}', '{t14005.T_PACK_CODE}', '{t14005.T_PURCHASE_PRICE}', '{t14005.T_HOLE_SALE_PRICE}','{t14005.T_RETAIL_SALE_PRICE}','{user}','{date}')");
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
                    sms = "Already Exist-0";
                }
                
            }
            else
            {
                var sa = Command($"UPDATE T14005 SET T_PRODUCT_CODE='{t14005.T_PRODUCT_CODE}', T_PACK_CODE='{t14005.T_PACK_CODE}',T_PURCHASE_PRICE='{t14005.T_PURCHASE_PRICE}',T_HOLE_SALE_PRICE='{t14005.T_HOLE_SALE_PRICE}', T_RETAIL_SALE_PRICE='{t14005.T_RETAIL_SALE_PRICE}',T_UPDATE_USER='{user}',T_UPDATE_DATE='{date}'  WHERE T_PRICE_ID ='{t14005.T_PRICE_ID}'");
                if (sa == true)
                {
                    sms = "Update Successfully-1";
                }
                else
                {
                    sms = "Do not Update-0";
                }
            }

            return sms;
        }
    }
}

