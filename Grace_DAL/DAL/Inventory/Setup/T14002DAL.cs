using Grace_DAL.Shared.Inventory.Setup;
using System.Data;

namespace Grace_DAL.DAL.Inventory.Setup
{
    public class T14002DAL : CommonDAL
    {

        public DataTable LoadData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_ITEM_ID,T_ITEM_CODE, T_ITEM_NAME  from T14002 ORDER BY T_ITEM_CODE DESC");
            return dt;
        }
        public string SaveData(T14002Data t14002)
        {
            string sms = "";
            if (t14002.T_ITEM_ID == 0)
            {
                var maxCode = Query($"select MAX( cast( T_ITEM_CODE as int)+1)T_ITEM_CODE from T14002").Rows[0]["T_ITEM_CODE"].ToString();
                var sa = Command($"INSERT INTO T14002 (T_ITEM_CODE,T_ITEM_NAME) VALUES('{maxCode}','{t14002.T_ITEM_NAME}')");
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
                var sa = Command($"UPDATE T14002 SET T_ITEM_NAME='{t14002.T_ITEM_NAME}'WHERE T_ITEM_ID ='{t14002.T_ITEM_ID}'");
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


