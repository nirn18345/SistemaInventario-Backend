USE DbInventario
GO

-- Crear tabla Usuarios
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Usuarios' AND xtype = 'U')
BEGIN
    CREATE TABLE dbo.Usuarios (
        Id NVARCHAR(450) NOT NULL,
        NombreUsuario NVARCHAR(255) NOT NULL,
        Clave NVARCHAR(255) NOT NULL,
        Rol NVARCHAR(255) NOT NULL,
        CONSTRAINT PK_Usuarios PRIMARY KEY (Id)
    )
END
GO

-- Crear tabla Productos
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Productos' AND xtype = 'U')
BEGIN
    CREATE TABLE dbo.Productos (
        Id INT IDENTITY(1,1) NOT NULL,
        Nombre NVARCHAR(255) NOT NULL,
        Descripcion NVARCHAR(255) NOT NULL,
        Stock INT NOT NULL,
        Estado NVARCHAR(50) NOT NULL,
        Imagen NVARCHAR(MAX) NOT NULL,
        FechaCreacion DATETIME NOT NULL,
        FechaModificacio DATETIME NOT NULL,
        Activo BIT NOT NULL,
        CONSTRAINT PK_Productos PRIMARY KEY (Id)
    )
END
GO

-- Crear tabla Lotes
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Lotes' AND xtype = 'U')
BEGIN
    CREATE TABLE dbo.Lotes (
        Id INT IDENTITY(1,1) NOT NULL,
        NumeroLote NVARCHAR(50) NOT NULL,
        FechaIngreso DATETIME NOT NULL,
        Descripcion NVARCHAR(255) NOT NULL,
        CONSTRAINT PK_Lotes PRIMARY KEY (Id)
    )
END
GO

ALTER TABLE dbo.Lotes ADD DEFAULT (N'') FOR Descripcion
GO

-- Crear tabla ProductoPrecioLote
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'ProductoPrecioLote' AND xtype = 'U')
BEGIN
    CREATE TABLE dbo.ProductoPrecioLote (
        Id INT IDENTITY(1,1) NOT NULL,
        ProductoID INT NOT NULL,
        LoteID INT NOT NULL,
        Precio DECIMAL(18,2) NOT NULL,
        FechaInicio DATETIME NOT NULL,
        FechaFin DATETIME NOT NULL,
        Cantidad INT NOT NULL,
        CONSTRAINT PK_ProductoPrecioLote PRIMARY KEY (Id),
        CONSTRAINT FK_ProductoPrecioLote_Lotes_LoteID FOREIGN KEY (LoteID)
            REFERENCES dbo.Lotes (Id) ON DELETE CASCADE,
        CONSTRAINT FK_ProductoPrecioLote_Productos_ProductoID FOREIGN KEY (ProductoID)
            REFERENCES dbo.Productos (Id) ON DELETE CASCADE
    )
END
GO

ALTER TABLE dbo.ProductoPrecioLote ADD DEFAULT (0) FOR Cantidad
GO

--INSERT USUARIO ADMINISTRADOR--
INSERT INTO Usuarios VALUES(1,'admin','12345','Administrador')

GO

--INSERT LOTES --
insert into Lotes values ('DC45GH',getdate(),'CompuService');
insert into Lotes values ('CP859S',getdate(),'CompuMundo');
insert into Lotes values ('DS45PY',getdate(),'DigitalSolutions');
