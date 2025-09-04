using Grace_DAL.Shared.Inventory.Setup;
using System.Data;

namespace Grace_DAL.DAL.Inventory.Setup
{
    public class T11101DAL:CommonDAL
    {
        public DataTable LoadData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_GENDER_ID,T_GENDER_CODE,T_GENDER_NAME from T11101 ORDER BY T_GENDER_ID DESC");
            return dt;
        }
        public string SaveData(T11101Data t11101)
        {
            string sms = "";
            if (t11101.T_GENDER_ID == 0)
            {
                var maxCode = Query($"select MAX( cast( T_GENDER_CODE as int)+1)T_GENDERE_CODE from T11101").Rows[0]["T_GENDERE_CODE"].ToString();
                var sa = Command($"INSERT INTO T11101 (T_GENDER_CODE,T_GENDER_NAME) VALUES('{maxCode}','{t11101.T_GENDER_NAME}')");
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
                var sa = Command($"UPDATE T11101 SET T_GENDER_NAME='{t11101.T_GENDER_NAME}'WHERE T_GENDER_ID ='{t11101.T_GENDER_ID}'");
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
