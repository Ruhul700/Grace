using Grace_DAL.Shared.Inventory.Setup;
using System.Data;

namespace Grace_DAL.DAL.Inventory.Setup
{
    public class T14004DAL : CommonDAL
    {
        public DataTable LoadData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_PACK_ID,T_PACK_CODE, T_PACK_NAME,T_PACK_WEIGHT,T_UM  from T14004 ORDER BY T_PACK_CODE DESC");
            return dt;
        }
        public DataTable LoadProductData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_PRODUCT_CODE, T_PRODUCT_NAME  from T14003 ORDER BY T_PRODUCT_CODE DESC");
            return dt;
        }
        public string SaveData(T14004Data t14004)
        {
            string sms = "";
            if (t14004.T_PACK_ID == 0)
            {
                var maxCode = Query($"select MAX( cast( T_PACK_CODE as int)+1)T_PACK_CODE from T14004").Rows[0]["T_PACK_CODE"].ToString();
                var sa = Command($"INSERT INTO T14004 (T_PACK_CODE,T_PACK_NAME,T_PACK_WEIGHT,T_UM) VALUES('{maxCode}','{t14004.T_PACK_NAME}','{t14004.T_PACK_WEIGHT}','{t14004.T_UM}')");
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
                var sa = Command($"UPDATE T14004 SET T_PACK_NAME='{t14004.T_PACK_NAME}',T_PACK_WEIGHT='{t14004.T_PACK_WEIGHT}',T_UM='{t14004.T_UM}' WHERE T_PACK_ID ='{t14004.T_PACK_ID}'");
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



