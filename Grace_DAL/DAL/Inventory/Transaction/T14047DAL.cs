using Grace_DAL.Shared.Inventory.Transaction;
using System.Data;

namespace Grace_DAL.DAL.Inventory.Transaction
{
    public class T14047DAL:CommonDAL
    {
        public DataTable GetReceiveSummery(T14020Parm paramList, string shopId)
        {
            DataTable sql = new DataTable();
            sql = Query($"select ROW_NUMBER() OVER(Order by T_BALANCE_ID)SL, T_BALANCE_ID, T_MEM_INV_NO, T14080.T_CUSTOMER_ID, T14010.T_CUSTOMER_NAME, T11020.T_USER_NAME , T_PURCHASE_ID, T_TOTAL, T_PAYMENT, T_DUE, (CASE WHEN T_VERIFY_FLG='1' then 'Verified' else '' end) T_VERIFY_FLG, T14080.T_ENTRY_DATE from T14080 JOIN T14010 ON T14080.T_CUSTOMER_ID = T14010.T_CUSTOMER_ID JOIN T11020 ON T14080.T_ENTRY_USER = T11020.T_USER_CODE where T14080.T_FORM_CODE ='T14040' AND T_SHOP_ID ='{shopId}' AND CONVERT(date, T14080.T_ENTRY_DATE ,104) BETWEEN CONVERT(date, '{paramList.T_FROM_DATE}' ,104) AND CONVERT(date, '{paramList.T_TO_DATE}' ,104)");
            return sql;
        }
        public DataTable GetReceiveSummeryReport(string fromDate, string toDate, string shopId)
        {
            DataTable sql = new DataTable();
            sql = Query($"select  T_MEM_INV_NO, T14080.T_CUSTOMER_ID, T14010.T_CUSTOMER_NAME, T11020.T_USER_NAME , T_PURCHASE_ID, T_TOTAL, T_PAYMENT, T_DUE, (CASE WHEN T_VERIFY_FLG='1' then 'Verified' else '' end) T_VERIFY_FLG, T14080.T_ENTRY_DATE from T14080 JOIN T14010 ON T14080.T_CUSTOMER_ID = T14010.T_CUSTOMER_ID JOIN T11020 ON T14080.T_ENTRY_USER = T11020.T_USER_CODE where T14080.T_FORM_CODE ='T14040' AND CONVERT(date, T14080.T_ENTRY_DATE ,104) BETWEEN CONVERT(date, '{fromDate}' ,104) AND CONVERT(date, '{toDate}' ,104)");
            return sql;
        }
    }
}
