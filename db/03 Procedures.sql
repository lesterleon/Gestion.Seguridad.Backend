IF EXISTS (SELECT name FROM sys.databases WHERE name = N'BdSeguridad')
BEGIN
    USE BdSeguridad
END
GO

IF EXISTS (SELECT * FROM sys.objects 
           WHERE object_id = OBJECT_ID(N'dbo.usp_Listar_Usuario') 
           AND type = N'P')
BEGIN
    DROP PROCEDURE dbo.usp_Listar_Usuario
END
GO

CREATE PROCEDURE usp_Listar_Usuario
AS
BEGIN
    SELECT Id, Nombres, Apellidos, DNI, Email, Direccion
    FROM Usuario
END
GO

IF EXISTS (SELECT * FROM sys.objects 
           WHERE object_id = OBJECT_ID(N'dbo.usp_Obtener_Usuario') 
           AND type = N'P')
BEGIN
    DROP PROCEDURE dbo.usp_Obtener_Usuario
END
GO

CREATE PROCEDURE usp_Obtener_Usuario
    @Id INT
AS
BEGIN
    SELECT Id, Nombres, Apellidos, DNI, Email, Direccion
    FROM Usuario
    WHERE Id = @Id;
END
GO

IF EXISTS (SELECT * FROM sys.objects 
           WHERE object_id = OBJECT_ID(N'dbo.usp_Insertar_Usuario') 
           AND type = N'P')
BEGIN
    DROP PROCEDURE dbo.usp_Insertar_Usuario
END
GO

CREATE PROCEDURE usp_Insertar_Usuario
    @Nombres NVARCHAR(100),
    @Apellidos NVARCHAR(100),
    @DNI NVARCHAR(8),
    @Email NVARCHAR(100),
    @Clave NVARCHAR(100),
    @Direccion NVARCHAR(200) = NULL,
    @IdUsuario INT OUTPUT
AS
BEGIN
    INSERT INTO Usuario (Nombres, Apellidos, DNI, Email, Clave, Direccion, Eliminado)
    VALUES (@Nombres, @Apellidos, @DNI, @Email, @Clave, @Direccion, 0)
    SET @Id = SCOPE_IDENTITY()
END
GO

IF EXISTS (SELECT * FROM sys.objects 
           WHERE object_id = OBJECT_ID(N'dbo.usp_Actualizar_Usuario') 
           AND type = N'P')
BEGIN
    DROP PROCEDURE dbo.usp_Actualizar_Usuario
END
GO

CREATE PROCEDURE usp_Actualizar_Usuario
    @Id INT,
    @Nombres NVARCHAR(100),
    @Apellidos NVARCHAR(100),
    @DNI NVARCHAR(20),
    @Email NVARCHAR(100),
    @Direccion NVARCHAR(200) = NULL
AS
BEGIN
    UPDATE Usuario
    SET 
        Nombres = @Nombres,
        Apellidos = @Apellidos,
        DNI = @DNI,
        Email = @Email,
        Direccion = COALESCE(@Direccion, Direccion)
    WHERE Id = @Id;
END
GO

IF EXISTS (SELECT * FROM sys.objects 
           WHERE object_id = OBJECT_ID(N'dbo.usp_Eliminar_Usuario') 
           AND type = N'P')
BEGIN
    DROP PROCEDURE dbo.usp_Eliminar_Usuario
END
GO

CREATE PROCEDURE usp_Eliminar_Usuario --Eliminación Lógica
    @Id INT
AS
BEGIN
    UPDATE Usuario
    SET 
        Eliminado = 1
    WHERE Id = @Id;
END
GO