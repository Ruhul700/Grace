using System.Data;

namespace Grace_DAL.DAL.Inventory.Transaction
{
    public class T14049DAL:CommonDAL
    {
        public DataTable GetQutationSummery(string fromDate, string toDate)
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT ROW_NUMBER() OVER(Order by T_QUATATION_ID)SL,T_QUATATION_ID, T14048.T_MEMO_NO, T14048.T_TYPE_CODE, T14010.T_CUSTOMER_NAME, T_GRAND_TOTAL ,T_TOTAL_VAT, T_PAMENT, T_DUE,T_DISCOUNT, T_SALE_TOTAL, T14048.T_SALE_DATE T_ENTRY_DATE, T14048.T_ENTRY_USER, T11020.T_USER_NAME T_EMP_NAME, CASE when T_VERIFY_FLG ='1' then 'Verified' else '' end T_VERIFY_FLG,CASE when T_PRE_ORDER ='1' then 'Preorder' else 'Normal' end T_ORDER_STATUS,T_PRE_ORDER  FROM T14048 JOIN T14010 ON T14048.T_CUSTOMER_ID = T14010.T_CUSTOMER_ID JOIN T11020 ON T14048.T_ENTRY_USER = T11020.T_USER_CODE WHERE T_CANCEL_FLG is null  AND CONVERT(date, T14048.T_SALE_DATE ,104) BETWEEN CONVERT(date, '{fromDate}' ,104) AND CONVERT(date, '{toDate}' ,104)");
            return sql;
        }
    }
}
