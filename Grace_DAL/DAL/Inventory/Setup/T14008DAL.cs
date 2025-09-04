using System.Data;

namespace Grace_DAL.DAL.Inventory.Setup
{
    public class T14008DAL:CommonDAL
    {
        public DataTable GetProductList()
        {
            DataTable sql = new DataTable();
            sql = Query($"select t03.T_PRODUCT_ID, t03.T_PRODUCT_CODE, t01.T_TYPE_NAME, t03.T_PRODUCT_NAME, t03.T_PRODUCT_IMAGE, t03.T_TYPE_CODE, t04.T_PACK_ID, t04.T_PACK_CODE, t04.T_PACK_NAME, t04.T_PACK_WEIGHT, t04.T_UM, t05.T_PRICE_ID, t05.T_PURCHASE_PRICE, t05.T_HOLE_SALE_PRICE, t05.T_RETAIL_SALE_PRICE,'0' T_CHECK_FLG from T14003 t03 JOIN T14001 t01 ON t03.T_TYPE_CODE =t01.T_TYPE_CODE JOIN T14004 t04 ON t03.T_PRODUCT_CODE =t04.T_PRODUCT_CODE JOIN T14005 t05 ON t03.T_PRODUCT_CODE =t05.T_PRODUCT_CODE");
            return sql;
        }
    }
}
