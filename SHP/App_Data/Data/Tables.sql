
CREATE TABLE [dbo].[SHP_Customer](
[ID] [MES_Identifier]	IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
[CreateDate] [datetime] NOT NULL,
[ModifyDate] [datetime] NULL,
[Name] nvarchar(256) NOT NULL,
[Address] nvarchar(256) NULL,
[Mobile] [nvarchar] NOT NULL,
[Phone] [nvarchar] NULL,
[Fax] [nvarchar] NULL,
[Email] [MES_Email] NOT NULL,
[Homepage] [MES_Url] NULL,
[Username] nvarchar(32) NOT NULL,
[Password] nvarchar(32) NOT NULL,
[CountryID] [MES_Lookup] NOT NULL,
[Active] [MES_Boolean] NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SHP_LKP_Category](
[ID] [MES_Identifier] IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
[Name] nvarchar(256) NOT NULL,
[SortIndex] [int] NOT NULL,
[Active] [MES_Boolean] NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SHP_LKP_Country](
[ID] [MES_Identifier] IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
[Name] nvarchar(256) NOT NULL,
[SortIndex] [int] NOT NULL,
[Active] [MES_Boolean] NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SHP_Order](
[ID] [MES_Identifier] IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
[CreateDate] [datetime] NOT NULL ,
[ModifyDate] [datetime] NULL,
[ShippingAddress] nvarchar(256) NOT NULL,
[ShippingDate] [datetime] NOT NULL,
[CustomerID] [MES_Identifier] NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SHP_OrderProduct](
[ID] [MES_Identifier] NOT NULL PRIMARY KEY CLUSTERED,
[OrderID] [MES_Identifier] NOT NULL,
[ProductID] [MES_Identifier] NOT NULL,
[UnitPrice] [float] NOT NULL,
[Quantity] [int] NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SHP_Product](
[ID] [MES_Identifier] NOT NULL PRIMARY KEY CLUSTERED,
[CreateDate] [datetime] NOT NULL,
[ModifyDate] [datetime] NULL,
[Name] nvarchar(256) NOT NULL,
[Description] nvarchar(1024) NULL,
[UnitPrice] [float] NOT NULL,
[SupplierID] [MES_Identifier] NOT NULL,
[SortIndex] [int] NOT NULL,
[Active] [MES_Boolean] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SHP_Supplier](
[ID] [MES_Identifier] NOT NULL PRIMARY KEY CLUSTERED,
[CreateDate] [datetime] NOT NULL,
[ModifyDate] [datetime] NULL,
[Name] nvarchar(256) NOT NULL,
[Description] nvarchar(1024) NULL,
[Email] [MES_Email] NULL,
[Homepage] [MES_Url] NULL,
[Active] [MES_Boolean] NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SHP_LNK_ProductCategory](
[CategoryID] [MES_Identifier] NOT NULL,
[ProductID] [MES_Identifier] NOT NULL
) ON [PRIMARY]
GO