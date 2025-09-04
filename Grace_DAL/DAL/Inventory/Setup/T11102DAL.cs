using Grace_DAL.Shared.Inventory.Setup;
using System.Data;

namespace Grace_DAL.DAL.Inventory.Setup
{
    public class T11102DAL:CommonDAL
    {
        public DataTable LoadData()
        {
            DataTable dt = new DataTable();
            dt = Query("select  T_RELIGION_ID,T_RELIGION_CODE, T_RELIGION_NAME  from T11102");
            return dt;
        }
        public string SaveData(T11102Data t11102)
        {
            string sms = "";
            if (t11102.T_RELIGION_ID == 0)
            {
                var maxCode = Query($"select MAX( cast( T_RELIGION_CODE as int)+1)T_RELIGION_CODE from t11102").Rows[0]["T_RELIGION_CODE"].ToString();
                var sa = Command($"INSERT INTO T11102 (T_RELIGION_CODE,T_RELIGION_NAME) VALUES('{maxCode}','{t11102.T_RELIGION_NAME}')");
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
                var sa = Command($"UPDATE T11102 SET T_RELIGION_NAME='{t11102.T_RELIGION_NAME}' WHERE T_RELIGION_ID ='{t11102.T_RELIGION_ID}'");
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
