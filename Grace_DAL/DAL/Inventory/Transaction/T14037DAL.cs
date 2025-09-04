using System.Data;

namespace Grace_DAL.DAL.Inventory.Transaction
{
    public class T14037DAL:CommonDAL
    {
        public DataTable GetStockShortList(string procuct, string pack, string shopId)
        {
            DataTable sql = new DataTable();

            var param = "0";

            if (procuct == null && pack == null)
            {
                param = "T_STOCK <80";
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
                sql = Query($@"SELECT T_PRODUCT_NAME, T_PACK_NAME,T_STOCK  FROM VIEW_STOCK WHERE  T_SHOP_ID ='{shopId}' AND {param}");
            }
            if (shopId == "2")
            {
                sql = Query($@"SELECT T_PRODUCT_NAME, T_PACK_NAME,T_STOCK  FROM VIEW_STOCK_2 WHERE {param}");
            }
            if (shopId == "3")
            {
                sql = Query($@"SELECT T_PRODUCT_NAME, T_PACK_NAME,T_STOCK FROM VIEW_STOCK_3 WHERE {param}");
            }

            return sql;
        }
        public DataTable StockShortListReport(string procuct, string pack, string shopId)
        {
            DataTable sql = new DataTable();

            var param = "0";

            if (procuct == "0" && pack == "0")
            {
                param = "T_STOCK <80";
            }
            else if (procuct != "0" && pack == "0")
            {
                param = $@"T_STOCK  <80 AND T_PRODUCT_CODE = '{procuct}'";
            }
            else if (procuct == "0" && pack != "0")
            {
                param = $@"T_STOCK  <80 AND T_PACK_CODE = '{pack}'";
            }
            else
            {
                param = $@"T_STOCK  <80 AND T_PRODUCT_CODE = '{procuct}'AND T_PACK_CODE = '{pack}'";
            }
            if (shopId == "1")
            {
                sql = Query($@"SELECT T_PRODUCT_NAME, T_PACK_NAME,T_STOCK  FROM VIEW_STOCK WHERE  T_SHOP_ID ='{shopId}' AND {param}");
            }
            if (shopId == "2")
            {
                sql = Query($@"SELECT T_PRODUCT_NAME, T_PACK_NAME,T_STOCK  FROM VIEW_STOCK_2 WHERE {param}");
            }
            if (shopId == "3")
            {
                sql = Query($@"SELECT T_PRODUCT_NAME, T_PACK_NAME,T_STOCK FROM VIEW_STOCK_3 WHERE {param}");
            }

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
    }
}
