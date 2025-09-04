namespace Grace_DAL.Shared.Inventory.Transaction
{
    public class T14044Data
    {
        public int T_SALE_ID { get; set; }
        public int T_CUSTOMER_ID { get; set; }
        public string T_MEMO_NO { get; set; }       
        public string T_GRAND_TOTAL { get; set; }       
        public decimal T_DISCOUNT { get; set; }
        public string T_AFTER_DISCOUNT { get; set; }
        public decimal T_PAMENT { get; set; }
        public decimal T_DUE { get; set; }
        public string T_SALE_DATE { get; set; }
        public string T_SALE_TOTAL { get; set; }       
        public string T_TYPE_CODE { get; set; }
        public string T_TYPE_NAME { get; set; }
        public string T_PRE_ORDER { get; set; }     
        public string T_VERIFY_FLG { get; set; }
        public string T_PENDING_FLG { get; set; }
        public string T_CONFIRM_FLG { get; set; }
        public string T_DELIVER_FLG { get; set; }
    }
    public class T14044PreOrderData
    {
        public int T_SALE_ID { get; set; }
        public int T_SALE_DETAILS_ID { get; set; }
        public int T_CUSTOMER_ID { get; set; }
        public string T_MEMO_NO { get; set; }
        public string T_GRAND_TOTAL { get; set; }
        public decimal T_DISCOUNT { get; set; }
        public string T_AFTER_DISCOUNT { get; set; }
        public decimal T_PAMENT { get; set; }
        public decimal T_DUE { get; set; }
        public string T_TYPE_CODE { get; set; }
        public string T_PRODUCT_CODE { get; set; }
        public string T_PACK_CODE { get; set; }
        public string T_PACK_WEIGHT { get; set; }
        public string T_SALE_QUANTITY { get; set; }
        public string T_QUANTITY { get; set; }
        public string T_PURCHASE_PRICE { get; set; }
        public string T_SALE_PRICE { get; set; }
        public string T_TOTAL_PRICE { get; set; }
        public string T_STOCK { get; set; }
       
    }
}
