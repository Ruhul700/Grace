using System.Data;

namespace Grace_DAL.DAL.Inventory.Transaction
{
    public class T14077DAL : CommonDAL
    {

        public DataTable GetVoucherList()
        {
            DataTable sql = new DataTable();
            sql = Query($"select T_VOUCHER_ID, T14076.T_VOUCHER_CODE, T14076.T_PROJECT_CODE, T14072.T_PROJECT_NAME, T14076.T_PARTY_CODE, T14070.T_PARTY_NAME, T14071.T_HEAD_NAME, T14077.T_TOTAL_TAKA from T14076 JOIN T14070 ON T14076.T_PARTY_CODE = T14070.T_PARTY_CODE JOIN T14072 ON T14076.T_PROJECT_CODE = T14072.T_PROJECT_CODE JOIN T14077 ON T14076.T_VOUCHER_CODE = T14077.T_VOUCHER_CODE JOIN T14071 ON T14077.T_HEAD_CODE = T14071.T_HEAD_CODE");
            return sql;
        }
        //public DataTable GetPurchaseSummeryReport(string fromDate, string toDate, string shopId)
        //{
        //    DataTable sql = new DataTable();
        //    sql = Query($"SELECT ROW_NUMBER() OVER(Order by T_PURCHASE_ID)SL, T14020.T_INVOICE_NO, T14020.T_TYPE_CODE, T14010.T_CUSTOMER_NAME, T_TOTAL_PRICE, T_DISCOUNT, T_AFTER_DISCOUNT, T_TOTAL_QUT, T_CASH, T_DUE, T14001.T_TYPE_NAME, T14020.T_ENTRY_DATE, T14020.T_ENTRY_USER , T11020.T_USER_NAME T_EMP_NAME, CASE when T_VERIFY_FLG ='1' then 'Verified' else '' end T_VERIFY_FLG FROM T14020 JOIN T14010 ON T14020.T_CUSTOMER_ID = T14010.T_CUSTOMER_ID JOIN T14001 ON T14020.T_TYPE_CODE = T14001.T_TYPE_CODE JOIN T11020 ON T14020.T_ENTRY_USER = T11020.T_USER_CODE  WHERE T_SHOP_ID='{shopId}' AND CONVERT(date, T14020.T_ENTRY_DATE ,104) BETWEEN CONVERT(date, '{fromDate}' ,104) AND CONVERT(date, '{toDate}' ,104)");
        //    return sql;
        //}
        //public DataTable GettHeaderData(int id, string shopId)
        //{
        //    DataTable sql = new DataTable();
        //    sql = Query($"SELECT T14020.T_PURCHASE_ID, T14010.T_CUSTOMER_NAME, T14020.T_INVOICE_NO, T14001.T_TYPE_NAME, T_TOTAL_PRICE, T_DISCOUNT, T_CASH, T_DUE FROM T14020 JOIN T14010 ON T14020.T_CUSTOMER_ID = T14010.T_CUSTOMER_ID JOIN T14001 ON T14020.T_TYPE_CODE = T14001.T_TYPE_CODE JOIN T11020 ON T14020.T_ENTRY_USER = T11020.T_USER_CODE where T14020.T_PURCHASE_ID='{id}' AND t14020.T_SHOP_ID='{shopId}'");
        //    return sql;
        //}
        //public DataTable GetPurchaseDetails(int id)
        //{
        //    DataTable sql = new DataTable();
        //    sql = Query($"SELECT T14003.T_PRODUCT_NAME, T14004.T_PACK_NAME, T14021.T_PURCHASE_PRICE, T14021.T_QUANTITY, T14021.T_TOTAL_PUR_PRICE FROM T14021 JOIN T14003 ON T14021.T_PRODUCT_CODE = T14003.T_PRODUCT_CODE JOIN T14004 ON T14021.T_PACK_CODE = T14004.T_PACK_CODE where T14021.T_PURCHASE_ID='15'");
        //    return sql;
        //}
    }
}

 
