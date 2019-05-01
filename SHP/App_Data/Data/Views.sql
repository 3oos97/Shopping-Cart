CREATE view [dbo].[SHP_Category_VIW]
AS
  
      SELECT 
             CAT.*
             ,(SELECT count(*) FROM SHP_Product WHERE CAT.ID=SHP_Product.CategoryID) AS [ProductCounter]
      FROM SHP_LKP_Category CAT

GO

CREATE VIEW [dbo].[SHP_Country_VIW]
AS
	SELECT
		  CRY.*
		, (SELECT COUNT(*) FROM SHP_Customer WHERE SHP_Customer.CountryID = CRY.ID) AS [UseCount]
	FROM
		SHP_LKP_Country CRY
	

GO

CREATE VIEW [dbo].[SHP_Customer_VIW]
AS
	SELECT
		  CST.*
		, CRY.Name AS Country
		, dbo.SHP_Customer_GetOrderCount(CST.ID) AS [OrderCount]
	FROM
		SHP_Customer CST
		LEFT JOIN SHP_LKP_Country CRY ON CRY.ID = CST.CountryID

GO

CREATE view [dbo].[SHP_Order_VIW]
AS

    SELECT 
	      SHP_Order.*

    FROM SHP_Order 


GO

CREATE view [dbo].[SHP_OrderProduct_VIW]
AS
    SELECT 
           OP.*

    FROM SHP_OrderProduct OP


GO

CREATE view [dbo].[SHP_Product_VIW]
AS

      SELECT SHP_Product.*
             ,(SELECT SHP_LKP_Category.Name FROM SHP_LKP_Category WHERE SHP_LKP_Category.ID=SHP_Product.CategoryID)AS [category]
	         ,(SELECT SHP_Supplier.Name FROM SHP_Supplier WHERE SHP_Supplier.ID=SHP_Product.SupplierID)AS [supplier]
      FROM SHP_Product

GO

CREATE view [dbo].[SHP_Supplier_VIW]
AS

     SELECT 
	       SUP.*

      FROM SHP_Supplier SUP


GO

CREATE VIEW [dbo].[SHP_LNK_ProductCategory_VIW]
AS

		SELECT	PRDCAT.*
	,	CAT.Name AS CategoryName
	,   PRD.Name AS ProductName
		FROM	SHP_LNK_ProductCategory  PRDCAT
		LEFT JOIN SHP_LKP_Category CAT ON CAT.ID = PRDCAT.CategoryID
		LEFT JOIN SHP_Product PRD ON PRD.ID = PRDCAT.ProductID

GO
		