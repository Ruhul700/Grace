using Grace_DAL.Shared.Inventory.Setup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grace_DAL.DAL.Inventory.Setup
{
    public class T11010DAL : CommonDAL
    {
        public DataTable LoadData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_OUTLET_ID,T_OUTLET_CODE,T_OUTLET_NAME,T11010.T_SITE_CODE,T00002.T_SITE_NAME from T11010 LEFT JOIN T00002 ON T11010.T_SITE_CODE =T00002.T_SITE_CODE ORDER BY T_OUTLET_ID DESC");
            return dt;
        }
        public DataTable LoadSiteData()
        {
            DataTable dt = new DataTable();
            dt = Query("SELECT T_SITE_CODE,T_SITE_NAME FROM T00002");
            return dt;
        }
        public string SaveData(T11010Data t11010)
        {
            string sms = "";
            if (t11010.T_OUTLET_ID == 0)
            {
                var maxCode = Query($"select MAX( cast( T_OUTLET_CODE as int)+1)T_OUTLET_CODE from T11010").Rows[0]["T_OUTLET_CODE"].ToString();
                var sa = Command($"INSERT INTO T11010 (T_OUTLET_CODE,T_OUTLET_NAME,T_SITE_CODE) VALUES('{maxCode}','{t11010.T_OUTLET_NAME}','{t11010.T_SITE_CODE}')");
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
                var sa = Command($"UPDATE T11010 SET T_OUTLET_NAME='{t11010.T_OUTLET_NAME}',T_SITE_CODE='{t11010.T_SITE_CODE}'  WHERE T_OUTLET_ID ='{t11010.T_OUTLET_ID}'");
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