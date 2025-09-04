using Grace_DAL.Shared.Inventory.Setup;
using System.Data;

namespace Grace_DAL.DAL.Inventory.Setup
{
    public class T14006DAL:CommonDAL
    {
        public DataTable LoadData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_FLAVER_ID,T_FLAVER_CODE, T_FLAVER_NAME  from T14006 ORDER BY T_FLAVER_CODE DESC");
            return dt;
        }
        public string SaveData(T14006Data t14006)
        {
            string sms = "";
            if (t14006.T_FLAVER_ID == 0)
            {
                var maxCode = Query($"select MAX( cast( T_FLAVER_CODE as int)+1)T_FLAVER_CODE from T14006").Rows[0]["T_FLAVER_CODE"].ToString();
                var sa = Command($"INSERT INTO T14006 (T_FLAVER_CODE,T_FLAVER_NAME) VALUES('{maxCode}','{t14006.T_FLAVER_NAME}')");
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
                var sa = Command($"UPDATE T14006 SET T_FLAVER_NAME='{t14006.T_FLAVER_NAME}'WHERE T_FLAVER_ID ='{t14006.T_FLAVER_ID}'");
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
