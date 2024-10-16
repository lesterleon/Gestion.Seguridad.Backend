IF EXISTS (SELECT name FROM sys.databases WHERE name = N'BdSeguridad')
BEGIN
    USE BdSeguridad
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'Usuario')
BEGIN
    CREATE TABLE Usuario (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Nombres NVARCHAR(100) NOT NULL,
        Apellidos NVARCHAR(100) NOT NULL,
        DNI NVARCHAR(20) NOT NULL,
        Email NVARCHAR(100) NOT NULL,
        Clave NVARCHAR(100) NOT NULL,
        Direccion NVARCHAR(200) NULL,
        Eliminado BIT NOT NULL DEFAULT 0
    )
END
GO