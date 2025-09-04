using Grace_DAL.Shared.Inventory.Setup;
using System.Data;

namespace Grace_DAL.DAL.Inventory.Setup
{
    public class T14001DAL:CommonDAL
    {
        public DataTable LoadData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_TYPE_ID,T_TYPE_CODE,T14001.T_ITEM_CODE, T_TYPE_NAME ,T14002.T_ITEM_NAME from T14001 LEFT JOIN T14002 ON T14001.T_ITEM_CODE = T14002.T_ITEM_CODE ORDER BY T_TYPE_CODE DESC");
            return dt;
        }
        public DataTable GetItem()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_ITEM_CODE, T_ITEM_NAME  from T14002 ORDER BY T_ITEM_CODE DESC");
            return dt;
        }
        public string SaveData(T14001Data t14001)
        {
            string sms = "";
            if (t14001.T_TYPE_ID == 0)
            {
                var maxCode = Query($"select MAX( cast( T_TYPE_CODE as int)+1)T_TYPE_CODE from T14001").Rows[0]["T_TYPE_CODE"].ToString();
                var sa = Command($"INSERT INTO T14001 (T_TYPE_CODE,T_ITEM_CODE,T_TYPE_NAME) VALUES('{maxCode}','{t14001.T_ITEM_CODE}','{t14001.T_TYPE_NAME}')");
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
                var sa = Command($"UPDATE T14001 SET T_ITEM_CODE='{t14001.T_ITEM_CODE}', T_TYPE_NAME='{t14001.T_TYPE_NAME}'WHERE T_TYPE_ID ='{t14001.T_TYPE_ID}'");
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
