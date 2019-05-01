CREATE FUNCTION SHP_Customer_GetOrderCount(@CustomerID int)
RETURNS int
AS
BEGIN
	DECLARE @OrderNum int
	SET @OrderNum = (SELECT COUNT(*) FROM SHP_Order	WHERE CustomerID = @CustomerID)
	
	RETURN @OrderNum
END
GO

