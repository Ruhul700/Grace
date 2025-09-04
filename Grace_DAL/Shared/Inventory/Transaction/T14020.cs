namespace Grace_DAL.Shared.Inventory.Transaction
{
    public class T14020Data
    {
        public int T_PURCHASE_ID { get; set; }
        public int? T_CUSTOMER_ID { get; set; }
        public string T_CHALAN_NO { get; set; }
        public string T_COMPANY_NAME { get; set; }
        public string T_CUSTOMER_MOBILE { get; set; }
        public string T_CUSTOMER_ADDRESS { get; set; }
        public string T_INVOICE_NO { get; set; }
        public string T_PUR_MEMO { get; set; }
        public string T_LOT_NO { get; set; }
        public string T_GROSS_WEIGHT { get; set; }
        public string TOTAL_SACK { get; set; }
        public string T_NET_WEIGHT { get; set; }
        public decimal T_TOTAL_QUT { get; set; }
        public decimal T_CASH { get; set; }
        public decimal T_DUE { get; set; }
        public decimal T_TOTAL_PRICE { get; set; }
        public decimal T_DISCOUNT { get; set; }
        public decimal T_AFTER_DISCOUNT { get; set; }
        public string T_ENTRY_USER { get; set; }
        public string T_ENTRY_DATE { get; set; }
        public string T_UPDATE_USER { get; set; }
        public string T_UPDATE_DATE { get; set; }
        public decimal T_OTHER_COST { get; set; }
        public decimal T_RATE { get; set; }
        public string T_VERIFY_FLG { get; set; }
        public string T_TYPE_CODE { get; set; }
        public string T_TYPE_NAME { get; set; }

    }
    public class T14021Data
    {
        public int T_PUR_DETL_ID { get; set; }
        public string T_PURCHASE_TYPE { get; set; }
        public string T_INVOICE_NO { get; set; }
        public int T_CHALAN_ID { get; set; }
        public string T_PRODUCT_CODE { get; set; }
        public string T_UM { get; set; }
        public string T_MODEL_NO { get; set; }
        public string T_SERIAL_NO { get; set; }
        public string T_PACK_CODE { get; set; }
        public string T_GROSS_WEIGHT { get; set; }
        public string T_QUANTITY { get; set; }
        public string T_STOCK { get; set; }
        public string T_RETURN { get; set; }
        public string T_SALE { get; set; }
        public string T_PURCHASE_DATE { get; set; }
        public decimal T_PURCHASE_PRICE { get; set; }
        public decimal T_TOTAL_PUR_PRICE { get; set; }
        public decimal T_TOTAL_PRICE { get; set; }

    }
    public class T14020Parm
    {
        public string T_FROM_DATE { get; set; }
        public string T_TO_DATE { get; set; }
    }
}
