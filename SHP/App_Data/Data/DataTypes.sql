EXEC sp_addtype N'MES_Boolean',N'int',N'null'
GO

EXEC sp_addtype N'MES_Email',N'nvarchar(128)',N'null'
GO

EXEC sp_addtype N'MES_File',N'nvarchar(128)',N'null'
GO

EXEC sp_addtype N'MES_Identifier',N'int',N'not null'
GO

EXEC sp_addtype N'MES_Lookup',N'int',N'null'
GO

EXEC sp_addtype N'MES_Url',N'nvarchar(256)',N'null'
GO
