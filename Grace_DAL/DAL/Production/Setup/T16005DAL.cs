using Grace_DAL.Shared.Inventory.Setup;
using Grace_DAL.Shared.Production.Setup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grace_DAL.DAL.Production.Setup
{
    public class T16005DAL : CommonDAL
    {
        public DataTable LoadData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_CONTAINER_ID,T_CONTAINER_CODE,T_CONTAINER_NAME,T_CAPACITY from T16005 ORDER BY T_CONTAINER_ID DESC");
            return dt;
        }
        public string SaveData(T16005Data t16005)
        {
            string sms = "";
            if (t16005.T_CONTAINER_ID == 0)
            {
                var maxCode = Query($"select MAX( cast( T_CONTAINER_CODE as int)+1)T_CONTAINER_CODE from T16005").Rows[0]["T_CONTAINER_CODE"].ToString();
                var sa = Command($"INSERT INTO T16005 (T_CONTAINER_CODE,T_CONTAINER_NAME,T_CAPACITY) VALUES('{maxCode}','{t16005.T_CONTAINER_NAME}')");
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
                var sa = Command($"UPDATE T16005 SET T_CONTAINER_NAME='{t16005.T_CONTAINER_NAME}', T_CAPACITY='{t16005.T_CAPACITY}WHERE T_CONTAINER_ID ='{t16005.T_CONTAINER_ID}'");
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

