namespace Grace_DAL.Shared.Inventory.Setup
{
    public class T14003Data
    {

    }
    public class T14003_Img_Ins
    {
        public int T_PRODUCT_ID { get; set; }
        public string T_PRODUCT_CODE { get; set; }
        public string T_PRODUCT_NAME { get; set; }
        public string T_PRODUCT_IMAGE { get; set; }
        public string T_TYPE_CODE { get; set; }       
        public string T_ITEM_CODE { get; set; }       
        public string T_ENTRY_USER { get; set; }
        //-------------T14004-------
        public int T_PACK_ID { get; set; }
        public string T_PACK_CODE { get; set; }
        public string T_PACK_NAME { get; set; }
        public string T_PACK_WEIGHT { get; set; }
        public string T_UM { get; set; }
        //---------T14005--------
        public int T_PRICE_ID { get; set; }
        public string T_PRICE_CODE { get; set; }
        public string T_PURCHASE_PRICE { get; set; }
        public string T_HOLE_SALE_PRICE { get; set; }
        public string T_RETAIL_SALE_PRICE { get; set; }

    }
}
