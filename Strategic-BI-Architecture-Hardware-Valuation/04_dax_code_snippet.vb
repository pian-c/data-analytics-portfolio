//DAX code snippet to demonstrate data transformation within Power BI

//1. Retrieve only necessary data from another sales Semantic Model via direct query mode, 

SalesTable = 
    SUMMARIZE(
        FILTER(
            SalesDataFromS,
            SalesDataFromS[sales_value] > 0 && SalesDataFromS[total_quantity] > 0 && 
            SalesDataFromS[business_unit_key] = "business_unit_key" &&
            SalesDataFromS[data_version] = "Actual"
        ),
        SalesDataFromS[product_hierarchy_lv1],
        SalesDataFromS[product_hierarchy_lv2],
        SalesDataFromS[product_hierarchy_lv3],
        CustomerDataFromS[customer_region],
        CustomerDataFromS[customer_cluster],
        SalesDataFromS[sales_date],
        "product_key_sales", SalesDataFromS[product_hierarchy_lv1] & "_" & SalesDataFromS[product_hierarchy_lv3],
        "ttl_sales_c", SUM(SalesDataFromS[sales_value]),
        "ttl_quantity_c", SUM(SalesDataFromS[total_quantity])
    )

//2. Calculate unit price per market cluster per product (down to category level)

UnitPriceTable = 
SUMMARIZE(
    SalesTable,
    ProductMapping[product_key_activation],
    SalesTable[customer_region],
    SalesTable[customer_cluster],
    SalesTable[sales_date],
    "ttl_sales", SUM(SalesTable[ttl_sales_c]),
    "ttl_quantity", SUM(SalesTable[ttl_quantity_c]),
    "avg_unit_price_eur_c", DIVIDE(SUM(SalesTable[ttl_sales_c]), SUM(SalesTable[ttl_quantity_c]))    
)
