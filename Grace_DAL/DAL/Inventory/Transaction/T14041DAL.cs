using System;
using System.Collections.Generic;
using System.Data;

namespace Grace_DAL.DAL.Inventory.Transaction
{
   public class T14041DAL:CommonDAL
    {
        public DataTable GetOutletData()
        {
            DataTable sql = new DataTable();            
                sql = Query($"select * from T11010");
            return sql;
        }

        public DataTable GetSiteData()
        {
            DataTable sql = new DataTable();            
                sql = Query($"select * from T11019");
            return sql;
        }



        public DataTable GetSaleSummery(string fromDate, string toDate,string userCode,string roleCode, string siteCode,string outletCode)
        {
            DataTable sql = new DataTable();
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(siteCode))
            {
                condition = $"AND T14040.T_SITE_CODE = '{siteCode}'";
            }
            if (!string.IsNullOrEmpty(outletCode))
            {
                condition = $"AND T14040.T_OUTLET_CODE = '{outletCode}'";
            }
            if (roleCode != "100")
            {
                condition = $"AND T14040.T_ENTRY_USER = '{userCode}' ";
            }
            sql = Query($"SELECT ROW_NUMBER() OVER(Order by T_SALE_ID)SL,T_SALE_ID, T14040.T_MEMO_NO, T14040.T_TYPE_CODE, T14010.T_CUSTOMER_NAME, T_GRAND_TOTAL , T_PAMENT, T_DUE,T_DISCOUNT, T_SALE_TOTAL, T14040.T_SALE_DATE T_ENTRY_DATE, T14040.T_ENTRY_USER, T11020.T_USER_NAME T_EMP_NAME, CASE when T_VERIFY_FLG ='1' then 'Verified' else '' end T_VERIFY_FLG,CASE when T_PRE_ORDER ='1' then 'Preorder' else 'Normal' end T_ORDER_STATUS,T_PRE_ORDER  FROM T14040 JOIN T14010 ON T14040.T_CUSTOMER_ID = T14010.T_CUSTOMER_ID JOIN T11020 ON T14040.T_ENTRY_USER = T11020.T_USER_CODE WHERE T_CANCEL_FLG is null  AND CONVERT(date, T14040.T_SALE_DATE ,104) BETWEEN CONVERT(date, '{fromDate}' ,104) AND CONVERT(date, '{toDate}' ,104){condition}");
            return sql;
        }

        public DataTable GetSaleSummery(string fromDate, string toDate)
        {
            DataTable sql = new DataTable();
            
            sql = Query($"SELECT ROW_NUMBER() OVER(Order by T_SALE_ID)SL,T_SALE_ID, T14040.T_MEMO_NO, T14040.T_TYPE_CODE, T14010.T_CUSTOMER_NAME, T_GRAND_TOTAL , T_PAMENT, T_DUE,T_DISCOUNT, T_SALE_TOTAL, T14040.T_SALE_DATE T_ENTRY_DATE, T14040.T_ENTRY_USER, T11020.T_USER_NAME T_EMP_NAME, CASE when T_VERIFY_FLG ='1' then 'Verified' else '' end T_VERIFY_FLG,CASE when T_PRE_ORDER ='1' then 'Preorder' else 'Normal' end T_ORDER_STATUS,T_PRE_ORDER  FROM T14040 JOIN T14010 ON T14040.T_CUSTOMER_ID = T14010.T_CUSTOMER_ID JOIN T11020 ON T14040.T_ENTRY_USER = T11020.T_USER_CODE WHERE T_CANCEL_FLG is null  AND CONVERT(date, T14040.T_SALE_DATE ,104) BETWEEN CONVERT(date, '{fromDate}' ,104) AND CONVERT(date, '{toDate}' ,104)");
            return sql;
        }
    }
}
