using Grace_DAL.Shared.Inventory.Setup;
using System.Data;

namespace Grace_DAL.DAL.Inventory.Setup
{
    public class T14072DAL : CommonDAL
    {

        public DataTable LoadData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_PROJECT_ID,T_PROJECT_CODE, T_PROJECT_NAME  from T14072 ORDER BY T_PROJECT_CODE DESC");
            return dt;
        }
        public string SaveData(T14072Data t14072)
        {
            string sms = "";
            if (t14072.T_PROJECT_ID == 0)
            {
                var maxCode = Query($"select MAX( cast( T_PROJECT_CODE as int)+1)T_PROJECT_CODE from T14072").Rows[0]["T_PROJECT_CODE"].ToString();
                var sa = Command($"INSERT INTO T14072 (T_PROJECT_CODE,T_PROJECT_NAME) VALUES('{maxCode}','{t14072.T_PROJECT_NAME}')");
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
                var sa = Command($"UPDATE T14072 SET T_PROJECT_NAME='{t14072.T_PROJECT_NAME}'WHERE T_PROJECT_ID ='{t14072.T_PROJECT_ID}'");
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


