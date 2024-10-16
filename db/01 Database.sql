IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'BdSeguridad')
BEGIN
    CREATE DATABASE BdSeguridad
END
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'BdSeguridad')
BEGIN
    USE BdSeguridad
END
GO