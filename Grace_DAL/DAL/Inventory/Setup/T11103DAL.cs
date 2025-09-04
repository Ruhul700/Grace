using Grace_DAL.Shared.Inventory.Setup;
using System.Data;

namespace Grace_DAL.DAL.Inventory.Setup
{
    public class T11103DAL:CommonDAL
    {
        public DataTable LoadData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_DESIGNATION_ID , T_DESIGNATION_CODE,T_DESIGNATION_NAME   from T11103 ORDER BY T_DESIGNATION_ID DESC");
            return dt;
        }
        public string SaveData(T11103Data t11103)
        {
            string sms = "";
            if (t11103.T_DESIGNATION_ID == 0)
            {
                var maxCode = Query($"select MAX( cast( T_DESIGNATION_CODE as int)+1)T_DESIGNATION_CODE from T11103").Rows[0]["T_DESIGNATION_CODE"].ToString();
                var sa = Command($"INSERT INTO T11103 (T_DESIGNATION_CODE,T_DESIGNATION_NAME) VALUES('{maxCode}','{t11103.T_DESIGNATION_NAME}')");
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
                var sa = Command($"UPDATE T11103 SET T_DESIGNATION_NAME ='{t11103.T_DESIGNATION_NAME}' WHERE T_DESIGNATION_ID ='{t11103.T_DESIGNATION_ID}'");

                if (sa)
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
