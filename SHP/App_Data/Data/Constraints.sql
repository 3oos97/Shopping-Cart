ALTER TABLE SHP_Customer ADD
	CONSTRAINT [DF_SHP_Customer_Active] DEFAULT (1) FOR [Active],
	CONSTRAINT [DF_SHP_Customer_CreateDate] DEFAULT (GETDATE()) FOR [CreateDate],
	CONSTRAINT [FK_SHP_Customer_CountryID] FOREIGN KEY ([CountryID]) REFERENCES [SHP_LKP_Country] ([ID])
GO

ALTER TABLE SHP_LKP_Category ADD
	CONSTRAINT [DF_SHP_LKP_Category_Active] DEFAULT (1) FOR [Active],
	CONSTRAINT [DF_SHP_LKP_Category_SortIndex] DEFAULT (0) FOR [SortIndex],
	CONSTRAINT [UQ_SHP_LKP_Category_Name] UNIQUE NONCLUSTERED ([Name])
Go

ALTER TABLE SHP_LKP_Country ADD
	CONSTRAINT [DF_SHP_LKP_Country_Active] DEFAULT (1) FOR [Active],
	CONSTRAINT [DF_SHP_LKP_Country_SortIndex] DEFAULT (0) FOR [SortIndex],
	CONSTRAINT [UQ_SHP_LKP_COUNTRY_Name] UNIQUE NONCLUSTERED ([Name])
GO

ALTER TABLE SHP_Order ADD
	CONSTRAINT [DF_SHP_Order_CreateDate] DEFAULT (GETDATE()) FOR [CreateDate],
	CONSTRAINT [FK_SHP_Order_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [SHP_Customer] ([ID])
Go

ALTER TABLE SHP_OrderProduct ADD
	CONSTRAINT [DF_SHP_OrderProduct_Quantity] DEFAULT (1) FOR [Quantity],
	CONSTRAINT [DF_SHP_OrderProduct_UnitPrice] DEFAULT (0) FOR [UnitPrice],
	CONSTRAINT [FK_SHP_OrderProduct_OrderID] FOREIGN KEY ([OrderID]) REFERENCES [SHP_Order] ([ID]),
	CONSTRAINT [FK_SHP_OrderProduct_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [SHP_Product] ([ID]),
	CONSTRAINT [UQ_SHP_OrderProduct] UNIQUE NONCLUSTERED ([OrderID], [ProductID])

GO

ALTER TABLE SHP_Product ADD
	CONSTRAINT [DF_SHP_Product_Active] DEFAULT (1) FOR [Active],
	CONSTRAINT [DF_SHP_Product_CreateDate] DEFAULT (GETDATE()) FOR [CreateDate],
	CONSTRAINT [DF_SHP_Product_SortIndex] DEFAULT (0) FOR [SortIndex],
	CONSTRAINT [DF_SHP_Product_UnitPrice] DEFAULT (0) FOR [UnitPrice],
	CONSTRAINT [FK_SHP_Product_SupplierID] FOREIGN KEY ([SupplierID]) REFERENCES [SHP_Supplier] ([ID])
 GO

 ALTER TABLE SHP_Supplier ADD
	CONSTRAINT [DF_SHP_Supplier_Active] DEFAULT (1) FOR [Active],
	CONSTRAINT [DF_SHP_Supplier_CreateDate] DEFAULT (GETDATE()) FOR [CreateDate]
 GO

 ALTER TABLE SHP_LNK_ProductCategory ADD
	CONSTRAINT [PK_SHP_LNK_ProductCategory] PRIMARY KEY ([CategoryID], [ProductID]),
	CONsTRAINT [FK_SHP_LNK_ProductCategory_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [SHP_LKP_Category] ([ID]),
	CONsTRAINT [FK_SHP_LNK_ProductCategory_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [SHP_Product] ([ID])
GO