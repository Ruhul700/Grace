using Grace_DAL.Shared.Inventory.Setup;
using System.Data;

namespace Grace_DAL.DAL.Inventory.Setup
{
    public class T14071DAL:CommonDAL
    {

        public DataTable LoadData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_HEAD_ID,T_HEAD_CODE, T_HEAD_NAME  from T14071 ORDER BY T_HEAD_CODE DESC");
            return dt;
        }
        public string SaveData(T14071Data t14071)
        {
            string sms = "";
            if (t14071.T_HEAD_ID == 0)
            {
                var maxCode = Query($"select MAX( cast( T_HEAD_CODE as int)+1)T_HEAD_CODE from t14071").Rows[0]["T_HEAD_CODE"].ToString();
                var sa = Command($"INSERT INTO t14071 (T_HEAD_CODE,T_HEAD_NAME) VALUES('{maxCode}','{t14071.T_HEAD_NAME}')");
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
                var sa = Command($"UPDATE t14071 SET T_HEAD_NAME='{t14071.T_HEAD_NAME}'WHERE T_HEAD_ID ='{t14071.T_HEAD_ID}'");
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

