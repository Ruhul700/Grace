using Grace_DAL.Shared.Inventory.Setup;
using System.Data;

namespace Grace_DAL.DAL.Inventory.Setup
{
    public class T14070DAL:CommonDAL
    {
        public DataTable LoadData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_PARTY_ID,T_PARTY_CODE, T_PARTY_NAME  from T14070 ORDER BY T_PARTY_CODE DESC");
            return dt;
        }
        public string SaveData(T14070Data t14070)
        {
            string sms = "";
            if (t14070.T_PARTY_ID == 0)
            {
                var maxCode = Query($"select MAX( cast( T_PARTY_CODE as int)+1)T_PARTY_CODE from T14070").Rows[0]["T_PARTY_CODE"].ToString();
                var sa = Command($"INSERT INTO T14070 (T_PARTY_CODE,T_PARTY_NAME) VALUES('{maxCode}','{t14070.T_PARTY_NAME}')");
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
                var sa = Command($"UPDATE T14070 SET T_PARTY_NAME='{t14070.T_PARTY_NAME}'WHERE T_PARTY_ID ='{t14070.T_PARTY_ID}'");
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
