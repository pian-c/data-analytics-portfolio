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

//3. To migrate abnormal sales figures due to special offers, calculate rolling unit price (average unit price in past 3 months)

rolling_unit_price_eur = 
VAR CurrentDate = SELECTEDVALUE(UnitPriceTable[sales_date])
VAR PastThreeMonths =
    DATESINPERIOD(
        UnitPriceTable[sales_date],
        CurrentDate,
        -3,
        MONTH
    )
VAR TotalSalesInPeriod =
    CALCULATE(
        SUM(UnitPriceTable[ttl_sales]),
        PastThreeMonths,
        ALLEXCEPT(UnitPriceTable, UnitPriceTable[region], UnitPriceTable[cluster], UnitPriceTable[product_key_activation])
    )
VAR TotalQuantityInPeriod =
    CALCULATE(
        SUM(UnitPriceTable[ttl_quantity]),
        PastThreeMonths,
        ALLEXCEPT(UnitPriceTable, UnitPriceTable[region], UnitPriceTable[cluster], UnitPriceTable[product_key_activation])
    )
RETURN
    DIVIDE(TotalSalesInPeriod, TotalQuantityInPeriod, 0)

//4. Create Date Table

DateTable = 
ADDCOLUMNS (
    CALENDAR ( DATE ( 2022, 1, 1 ), DATE ( 2025, 12, 31 ) ),
    //CALENDAR ( DATE ( 2022, 1, 1 ), TODAY()-2 ),
    "Date_ID", FORMAT ( [Date], "YYYYMMDD" ),
    "Year", YEAR ( [Date] ),
    "Month_Number", MONTH ( [Date] ),
    "YearMonth_Number", FORMAT ( [Date], "YYYY/MM" ),
    "YearMonth_Short", FORMAT ( [Date], "YYYY,mmm" ),
    "Month", FORMAT ( [Date], "mmm" ),
    "Month_Long", FORMAT ( [Date], "mmmm" ),
	"Day", DAY ( [Date] ),
    "Weekday_Number", WEEKDAY ( [Date], 2 ),  -- Consistent numbering system (Monday=1)
    "Weekday_Short", FORMAT ( [Date], "ddd" ),
	"Weekday_Long", FORMAT ( [Date], "dddd" ),
    "Quarter", "Q" & FORMAT ( [Date], "Q" ),
    "Year_Quarter", FORMAT ( [Date], "YYYY" ) & "/Q" & FORMAT ( [Date], "Q" ),
    "Week_of_Year", WEEKNUM ( [Date], 2 )  -- Starts week on Monday
)

//5. Create a table for full list (every month, every market) of rolling unit price

FullUnitPriceTable = 
GENERATE(
    SUMMARIZE(
        'ProductActivation',
        'ProductActivation'[product_key_activation],
        'ProductActivation'[_category],
        'ProductActivation'[region],
        'ProductActivation'[cluster]
    ),
    ADDCOLUMNS(
        FirstDayOfMonthTable,
        "full_date_range", [Date]
    )
)

//6. Add a new column to fill in rolling unit price

filled_unit_price_rolling_c = 
//rolling average unit price of past 3 months
VAR CurrentFull_date_range = FullUnitPriceTable[full_date_range]
VAR CurrentProduct_key_activation = FullUnitPriceTable[product_key_activation]
VAR Current_category = FullUnitPriceTable[_category]
VAR CurrentRegion = FullUnitPriceTable[region]
VAR CurrentCluster = FullUnitPriceTable[cluster]

VAR ExactMatchPrice = 
    CALCULATE(
        [rolling_unit_price_eur],
        FILTER(
            UnitPriceTable,
            UnitPriceTable[product_key_activation] = CurrentProduct_key_activation &&
            UnitPriceTable[region] = CurrentRegion &&
            UnitPriceTable[cluster] = CurrentCluster &&
            UnitPriceTable[sales_date] = CurrentFull_date_range
        
        )
    )

VAR LatestProductMarketPrice = 
    CALCULATE(
        [rolling_unit_price_eur],
        TOPN(
            1, 
            FILTER(
                UnitPriceTable,
                UnitPriceTable[product_key_activation] = CurrentProduct_key_activation &&
                UnitPriceTable[region] = CurrentRegion &&
                UnitPriceTable[cluster] = CurrentCluster &&
                UnitPriceTable[sales_date] <= CurrentFull_date_range
            
            ),
            UnitPriceTable[sales_date], 
            DESC
        )
    )

VAR LatestProductRegionPrice = 
    CALCULATE(
        [rolling_unit_price_eur],
        TOPN(
            1, 
            FILTER(
                UnitPriceTable,
                UnitPriceTable[product_key_activation] = CurrentProduct_key_activation &&
                UnitPriceTable[region] = CurrentRegion &&
                UnitPriceTable[sales_date] <= CurrentFull_date_range
            
            ),
            UnitPriceTable[sales_date], 
            DESC
        )
    )

VAR Latest_categorycluster = 
    CALCULATE(
        [rolling_unit_price_eur],
        TOPN(
            1, 
            FILTER(
                UnitPriceTable,
                UnitPriceTable[_category] = Current_category &&
                UnitPriceTable[cluster] = CurrentCluster &&
                UnitPriceTable[sales_date] <= CurrentFull_date_range
            
            ),
            UnitPriceTable[sales_date], 
            DESC
        )
    )

VAR Latest_categoryRegion =
    CALCULATE(
        [rolling_unit_price_eur],
        TOPN(
            1, 
            FILTER(
                UnitPriceTable,
                UnitPriceTable[_category] = Current_category &&
                UnitPriceTable[region] = CurrentRegion &&
                UnitPriceTable[sales_date] <= CurrentFull_date_range
            
            ),
            UnitPriceTable[sales_date], 
            DESC
        )
    )

VAR LatestProductPrice = 
    CALCULATE(
        [rolling_unit_price_eur],
        TOPN(
            1, 
            FILTER(
                UnitPriceTable,
                UnitPriceTable[product_key_activation] = CurrentProduct_key_activation &&
                UnitPriceTable[sales_date] <= CurrentFull_date_range
            
            ),
            UnitPriceTable[sales_date], 
            DESC
        )
    )

VAR Latest_category = 
    CALCULATE(
        [rolling_unit_price_eur],
        TOPN(
            1, 
            FILTER(
                UnitPriceTable,
                UnitPriceTable[_category] = Current_category &&
                UnitPriceTable[sales_date] <= CurrentFull_date_range
            
            ),
            UnitPriceTable[sales_date], 
            DESC
        )
    )

RETURN
    IF(
        NOT(ISBLANK(ExactMatchPrice)),
        ExactMatchPrice,
        IF(
            NOT(ISBLANK(LatestProductMarketPrice)),
            LatestProductMarketPrice,
            IF(
                NOT(ISBLANK(LatestProductRegionPrice)),
                LatestProductRegionPrice,
                IF(
                    NOT(ISBLANK(Latest_categorycluster)),
                    Latest_categorycluster,
                    IF(
                        NOT(ISBLANK(Latest_categoryRegion)),
                        Latest_categoryRegion,
                        IF(
                            NOT(ISBLANK(LatestProductPrice)),
                            LatestProductPrice,
                            IF(
                                NOT(ISBLANK(Latest_category)),
                                Latest_category,
                                0
                            )
                        )
                    )
                )
            )
        )
    )

//7. Create a Measure Selection Table, so that users can select which mesaure to be used

ITM = 
VAR selection =
SELECTEDVALUE(MeasureSelectionTable[measures])
RETURN
SWITCH(
    TRUE(),
    Selection = "sales values", [sales_values],
    Selection = "total products", [total_products]
)

ITM_LY = 
VAR selection =
SELECTEDVALUE(MeasureSelectionTable[measures])
RETURN
SWITCH(
    TRUE(),
    Selection = "sales values", [sales_values_LY],
    Selection = "total products", [total_products_LY]
)
