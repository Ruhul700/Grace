using System.Data;

namespace Grace_DAL.DAL.Inventory.Transaction
{
    public class T14035DAL : CommonDAL
    {
        public DataTable GetStockData(string procuct, string pack, string shopId)
        {
            DataTable sql = new DataTable();

            var param = "0";

            if (procuct == null && pack == null)
            {
                param = "T_STOCK !='0'";
            }
            else if (procuct != null && pack == null)
            {
                param = $@"T_STOCK !='0' AND T_PRODUCT_CODE = '{procuct}'";
            }
            else if (procuct == null && pack != null)
            {
                param = $@"T_STOCK !='0' AND T_PACK_CODE = '{pack}'";
            }
            else
            {
                param = $@"T_STOCK !='0' AND T_PRODUCT_CODE = '{procuct}'AND T_PACK_CODE = '{pack}'";
            }
            if (shopId == "1")
            {
                sql = Query($@"SELECT stk.T_PRODUCT_CODE,T14001.T_TYPE_NAME, stk.T_PRODUCT_NAME, stk.T_PACK_NAME, T14004.T_PACK_WEIGHT, T_STOCK T_STOCK_PIECE, cast((T_STOCK/T14004.T_UM)as decimal(12,2) )T_STOCK_BOX FROM VIEW_STOCK stk JOIN T14003 ON stk.T_PRODUCT_CODE = T14003.T_PRODUCT_CODE JOIN T14004 ON T14003.T_PRODUCT_CODE = T14004.T_PRODUCT_CODE JOIN T14005 ON T14004.T_PACK_CODE = T14005.T_PACK_CODE JOIN T14001 ON T14003.T_TYPE_CODE = T14001.T_TYPE_CODE WHERE  T_SHOP_ID ='{shopId}' AND {param}");
            }
            //if (shopId == "2")
            //{
            //    sql = Query($@"SELECT stk.T_PRODUCT_CODE, T14001.T_TYPE_NAME, T_PRODUCT_NAME, stk.T_PACK_NAME, T14004.T_PACK_WEIGHT, T_STOCK T_STOCK_PIECE, cast((T_STOCK/T14004.T_UM)as decimal(12,2) )T_STOCK_BOX FROM VIEW_STOCK_2 stk JOIN T14003 ON stk.T_PRODUCT_CODE = T14003.T_PRODUCT_CODE JOIN T14004 ON T14003.T_PRODUCT_CODE = T14004.T_PRODUCT_CODE JOIN T14005 ON T14004.T_PACK_CODE = T14005.T_PACK_CODE JOIN T14001 ON T14003.T_TYPE_CODE = T14001.T_TYPE_CODE WHERE {param}");
            //}
            //if (shopId == "3")
            //{
            //    sql = Query($@"SELECT  stk.T_PRODUCT_CODE,T14001.T_TYPE_NAME, T_PRODUCT_NAME, stk.T_PACK_NAME, T14004.T_PACK_WEIGHT, T_STOCK T_STOCK_PIECE, cast((T_STOCK/T14004.T_UM)as decimal(12,2) )T_STOCK_BOX FROM VIEW_STOCK_3 stk JOIN T14003 ON stk.T_PRODUCT_CODE = T14003.T_PRODUCT_CODE JOIN T14004 ON T14003.T_PRODUCT_CODE = T14004.T_PRODUCT_CODE JOIN T14005 ON T14004.T_PACK_CODE = T14005.T_PACK_CODE JOIN T14001 ON T14003.T_TYPE_CODE = T14001.T_TYPE_CODE WHERE {param}");
            //}

            return sql;
        }
        public DataTable GetStockReportData(string procuct, string pack)
        {
            DataTable sql = new DataTable();

            var param = "0";

            if (procuct == null && pack == null)
            {
                param = "T_STOCK !='0'";
            }
            else if (procuct != null && pack == null)
            {
                param = $@"T_STOCK !='0' AND T_PRODUCT_CODE = '{procuct}'";
            }
            else if (procuct == null && pack != null)
            {
                param = $@"T_STOCK !='0' AND T_PACK_CODE = '{pack}'";
            }
            else
            {
                param = $@"T_STOCK !='0' AND T_PRODUCT_CODE = '{procuct}'AND T_PACK_CODE = '{pack}'";
            }
            sql = Query($@"SELECT stk.T_PRODUCT_CODE,T14001.T_TYPE_NAME,T14003.T_PRODUCT_NAME, stk.T_PACK_NAME, T14004.T_PACK_WEIGHT, T_STOCK T_STOCK_PIECE, cast((T_STOCK/T14004.T_UM)as decimal(12,2) )T_STOCK_BOX FROM VIEW_STOCK stk JOIN T14003 ON stk.T_PRODUCT_CODE = T14003.T_PRODUCT_CODE JOIN T14004 ON T14003.T_PRODUCT_CODE = T14004.T_PRODUCT_CODE JOIN T14005 ON T14004.T_PACK_CODE = T14005.T_PACK_CODE JOIN T14001 ON T14003.T_TYPE_CODE = T14001.T_TYPE_CODE WHERE T_STOCK !='0' ORDER BY T14004.T_PACK_WEIGHT");
            
            //sql = Query($@"SELECT T_PRODUCT_NAME, T_PACK_NAME,T_STOCK  FROM VIEW_STOCK WHERE T_STOCK !='0'");
            return sql;
        }
        public DataTable GetProduct()
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT T_PRODUCT_CODE,T_PRODUCT_NAME FROM T14003");
            return sql;
        }

        public DataTable GetPack()
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT T_PACK_CODE,T_PACK_NAME FROM T14004");
            return sql;
        }
        public DataTable GetStockWithAmount()
        {
            DataTable sql = new DataTable();
            sql = Query($@"select 
                T14035.T_PURCHASE_ID,
                T14020.T_INVOICE_NO,
                T14035.T_PRODUCT_CODE,
                T14003.T_PRODUCT_NAME,
                T_STOCK,
                T_PURCHASE_PRICE,
                CAST((T_STOCK * T_PURCHASE_PRICE)AS decimal(12, 2)) TOTAL_PRICE
                from T14035
                JOIN T14003 ON T14035.T_PRODUCT_CODE = T14003.T_PRODUCT_CODE
                JOIN T14020 ON T14035.T_PURCHASE_ID = T14020.T_PURCHASE_ID
                WHERE T_STOCK != 0");
            return sql;
        }
    }
}
