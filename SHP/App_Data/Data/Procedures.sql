CREATE PROCEDURE [dbo].[SHP_Category_DEL]
 @ID MES_Identifier 
	
AS
BEGIN
	
    DELETE FROM SHP_LKP_Category
	WHERE ID  =  @ID;	

   print 'Category ID  =  ' + CAST(@ID AS NVARCHAR) + ' Linked to some products'
END

GO

CREATE procedure [dbo].[SHP_Category_INS]
	@ID MES_Identifier output     
  , @Name nvarchar(256)
  , @SortIndex int
  , @Active MES_Boolean
AS
    
BEGIN
    INSERT INTO SHP_LKP_Category([Name],[SortIndex],[Active])
    values( @Name,@SortIndex,@Active )
END
GO

CREATE PROCEDURE [dbo].[SHP_Category_UPD]
   @ID MES_Identifier
  , @Name nvarchar(256)
  , @SortIndex int
  , @Active MES_Boolean
AS
BEGIN
	 UPDATE SHP_LKP_Category
	 SET Name = @Name, SortIndex = @SortIndex, Active = @Active
	 WHERE ID = @ID;
END 
GO

CREATE PROCEDURE [dbo].[SHP_Country_DEL]
  @ID MES_Identifier
  
AS
BEGIN
	DELETE FROM SHP_LKP_Country
	WHERE ID = @ID
END
GO

CREATE PROCEDURE [dbo].[SHP_Country_INS]
  @ID MES_Identifier output
 ,@Name nvarchar(255)
 ,@SortIndex int
 ,@Active MES_Boolean
AS
BEGIN
	INSERT INTO SHP_LKP_Country([Name],[SortIndex],[Active])
	VALUES(@Name,@SortIndex,@Active);
END
GO

CREATE PROCEDURE [dbo].[SHP_Country_UPD]
  @ID MES_Identifier
 ,@Name nvarchar(255)
 ,@SortIndex int
 ,@Active MES_Boolean
AS
BEGIN
	UPDATE SHP_LKP_Country
	SET Name = @Name,SortIndex = @SortIndex,Active = @Active
	WHERE ID = @ID;
END

GO

CREATE PROCEDURE [dbo].[SHP_Customer_DEL]
@ID MES_Identifier
AS
BEGIN
	DELETE FROM SHP_Customer
	WHERE ID = @ID;
END
GO

CREATE PROCEDURE [dbo].[SHP_Customer_INS]
  @ID MES_Identifier output	
 ,@CreateDate datetime
 ,@ModifyDate datetime = NULL
 ,@Name nvarchar(256)
 ,@Address nvarchar(256) = NULL
 ,@Mobile nvarchar(16)
 ,@Phone nvarchar(16) = NULL
 ,@Fax nvarchar(16) = NULL
 ,@Email nvarchar (128)
 ,@Homepage MES_Url = NULL
 ,@UserName nvarchar(32)
 ,@Password nvarchar(32)
 ,@CountryID MES_Lookup
 ,@Active MES_Boolean
 
AS
BEGIN
	INSERT INTO SHP_Customer([CreateDate],[ModifyDate],[Name],[Address],[Mobile],[Phone],[Fax],[Email],[Homepage],[Username],[Password],[CountryID],[Active])
	VALUES(@CreateDate,@ModifyDate,@Name,@Address,@Mobile,@Phone,@Fax,@Email,@Homepage,@UserName,@Password,@CountryID,@Active);
END
GO

CREATE PROCEDURE [dbo].[SHP_Customer_UPD]
  @ID MES_Identifier
 ,@CreateDate datetime
 ,@ModifyDate datetime = NULL
 ,@Name nvarchar(256)
 ,@Address nvarchar(256) = NULL
 ,@Mobile nvarchar(16)
 ,@Phone nvarchar(16) = NULL
 ,@Fax nvarchar(16) = NULL
 ,@Email nvarchar (128)
 ,@Homepage MES_Url = NULL
 ,@UserName nvarchar(32)
 ,@Password nvarchar(32)
 ,@CountryID MES_Lookup
 ,@Active MES_Boolean
AS
BEGIN
	
UPDATE SHP_Customer 
SET CreateDate = @CreateDate,ModifyDate = @ModifyDate,Name = @Name,[Address] = @Address,Mobile = @Mobile,Phone = @Phone,fax = @Fax,Email = @Email,Homepage = @Homepage,Username = @UserName,[Password] = @Password,CountryID = @CountryID,Active = @Active
WHERE ID = @ID

  
END
GO


CREATE PROCEDURE [dbo].[SHP_Order_DEL]
   @ID MES_Identifier
AS
BEGIN
	DELETE FROM SHP_Order
	WHERE ID = @ID
END
GO

CREATE PROCEDURE [dbo].[SHP_Order_INS]
  @ID MES_Identifier output
 ,@CreateDate datetime
 ,@ModifyDate datetime = NULL
 ,@ShippingAddress nvarchar(256)
 ,@ShippingDate datetime 
 ,@CustomerID MES_Identifier
AS
BEGIN
	INSERT INTO SHP_Order([CreateDate],[ModifyDate],[ShippingAddress],[ShippingDate],[CustomerID])
	VALUES(@CreateDate,@ModifyDate,@ShippingAddress,@ShippingDate,@CustomerID)
END
GO

CREATE PROCEDURE [dbo].[SHP_Order_UPD]
  @ID MES_Identifier
 ,@CreateDate datetime
 ,@ModifyDate datetime = NULL
 ,@ShippingAddress nvarchar(256)
 ,@ShippingDate datetime 
 ,@CustomerID MES_Identifier
AS
BEGIN
	UPDATE SHP_Order
	SET CreateDate = @CreateDate,ModifyDate = @ModifyDate,ShippingAddress = @ShippingAddress,ShippingDate = @ShippingDate,CustomerID = @CustomerID
	WHERE ID = @ID;

END
GO

CREATE PROCEDURE [dbo].[SHP_OrderProduct_DEL]
  @ID MES_Identifier
AS
BEGIN
	
	DELETE FROM SHP_OrderProduct
	WHERE ID = @ID
END
GO

CREATE PROCEDURE [dbo].[SHP_OrderProduct_INS]
  @ID MES_Identifier output 
 ,@OrderID MES_Identifier
 ,@ProductID MES_Identifier = NULL
 ,@UnitePrice float
 ,@Quantity int
AS
BEGIN
	INSERT INTO SHP_OrderProduct([OrderID],[ProductID],[UnitPrice],[Quantity])
	VALUES(@OrderID,@ProductID,@UnitePrice,@Quantity)
END
GO

CREATE PROCEDURE [dbo].[SHP_OrderProduct_UPD]
  @ID MES_Identifier
 ,@OrderID MES_Identifier
 ,@ProductID MES_Identifier = NULL
 ,@UnitePrice float
 ,@Quantity int
AS
BEGIN
     UPDATE SHP_OrderProduct
	 SET OrderID =  @OrderID,ProductID = @ProductID,UnitPrice = @UnitePrice,Quantity = @Quantity
	 WHERE ID = @ID
END
GO

CREATE PROCEDURE [dbo].[SHP_Product_DEL]
 @ID MES_Identifier
AS
BEGIN
	
	DELETE FROM SHP_Product
	WHERE ID = @ID
END
GO

CREATE PROCEDURE [dbo].[SHP_Product_INS]
  @ID MES_Identifier output
 ,@CreateDate datetime
 ,@ModifyDate datetime = NULL
 ,@Name nvarchar(256)
 ,@Description nvarchar(1024) = NULL
 ,@UnitPrice float
 ,@SupplierID MES_Lookup
 ,@SortIndex int
 ,@Active MES_Boolean
AS
BEGIN
	INSERT INTO SHP_Product([CreateDate],[ModifyDate],[Name],[Description],[UnitPrice],[SupplierID],[SortIndex],[Active])
	VALUES( @CreateDate,@ModifyDate,@Name,@Description,@UnitPrice, @SupplierID,@SortIndex,@Active)
END
GO

CREATE PROCEDURE [dbo].[SHP_Product_UPD]
  @ID MES_Identifier
 ,@CreateDate datetime
 ,@Name nvarchar(256)
 ,@UnitPrice float
 ,@SupplierID MES_Lookup
 ,@SortIndex int
 ,@Active MES_Boolean
 ,@ModifyDate datetime = NULL
 ,@Description nvarchar(1024) = NULL
AS
BEGIN
	
    UPDATE SHP_Product
	SET CreateDate = @CreateDate,Name = @Name,UnitPrice = @UnitPrice, SupplierID = @SupplierID,SortIndex = @SortIndex,Active = @Active
    WHERE ID = @ID
END
GO

CREATE PROCEDURE [dbo].[SHP_Supplier_DEL]
 @ID MES_Identifier
AS
BEGIN
	
	DELETE FROM SHP_Supplier
	WHERE ID = @ID
END
GO

CREATE PROCEDURE [dbo].[SHP_Supplier_INS]
  @ID MES_Identifier output
 ,@CreateDate datetime
 ,@ModifyDate datetime = NULL
 ,@Name nvarchar(256)
 ,@Description nvarchar(1024) = NULL
 ,@Email MES_Email = NULL
 ,@Homepage MES_Url = NULL
 ,@Active MES_Boolean
AS
BEGIN
	INSERT INTO SHP_Supplier([CreateDate],[ModifyDate],[Name],[Description],[Email],[Homepage],[Active])
	VALUES(@CreateDate,@ModifyDate,@Name,@Description,@Email,@Homepage,@Active)
END
GO

CREATE PROCEDURE [dbo].[SHP_Supplier_UPD]
  @ID MES_Identifier
 ,@CreateDate datetime
 ,@Name nvarchar(256)
 ,@Active MES_Boolean
 ,@Description nvarchar(1024) = NULL
 ,@Email MES_Email = NULL
 ,@Homepage MES_Url = NULL
 ,@ModifyDate datetime = NULL
AS
BEGIN
    UPDATE SHP_Supplier
	SET CreateDate = @CreateDate,Name = @Name,Active = @Active
	WHERE ID = @ID

END
GO

CREATE PROCEDURE [dbo].[SHP_LNK_Product_Category_DEL]
	@CategoryID MES_Identifier
   ,@ProductID MES_Identifier
AS
BEGIN
	 DELETE FROM SHP_LNK_ProductCategory
	 WHERE  CategoryID = @CategoryID
	 ANd	ProductID = @ProductID
END
GO

CREATE PROCEDURE [dbo].[SHP_LNK_Product_Category_INS]
	@CategoryID MES_Identifier
   ,@ProductID MES_Identifier
AS
BEGIN
	 	 INSERT INTO SHP_LNK_ProductCategory([CategoryID], [ProductID])
		 VALUES(@CategoryID, @ProductID)
END
GO



