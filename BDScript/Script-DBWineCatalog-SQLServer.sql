-- Eliminando la DBWineCatalog
-- USE master;
-- GO
-- DROP DATABASE DBWineCatalog;

-- Creando la DataBase y usandola
CREATE DATABASE DBTiendaVinos;
GO
USE DBTiendaVinos;
GO

-- Creando tablas
CREATE TABLE [TipoVino] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NOT NULL UNIQUE,
	CONSTRAINT [PK_TipoVino_Id] PRIMARY KEY ([Id])
);
GO
CREATE TABLE [Marca] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NOT NULL UNIQUE,
	CONSTRAINT [PK_Marca_Id] PRIMARY KEY ([Id])
);
GO
CREATE TABLE [Cepa] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NOT NULL UNIQUE,
	CONSTRAINT [PK_Cepa_Id] PRIMARY KEY ([Id])
);
GO
CREATE TABLE [Vino] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImagenUri] [varchar](1024) NOT NULL,	
	[IdTipoVino] [int]NOT NULL,	
	[IdMarca] [int] NOT NULL,	
	[IdCepa] [int] NOT NULL,	
	[Pais] [varchar](25) NOT NULL,	
	[Cosecha] [Date] NOT NULL,	
	[Precio] [decimal](5,2) NULL,
	[Stock] [int] NOT NULL,	
	CONSTRAINT [PK_Vino_Id] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Marca_Id] FOREIGN KEY ([IdMarca]) REFERENCES Marca ([Id]),
	CONSTRAINT [FK_TipoVino_Id] FOREIGN KEY ([IdTipoVino]) REFERENCES TipoVino ([Id]),
	CONSTRAINT [FK_Cepa_Id] FOREIGN KEY ([IdCepa]) REFERENCES Cepa ([Id])
);
GO


--- INSERTs
INSERT INTO [Marca] ([Nombre])
	VALUES 
	('Concha y Toro'),
	('Santiago Queirolo'),
	('Tabernero');
GO
---

INSERT INTO [TipoVino] ([Nombre])
	VALUES 
	('Vino Tinto'),
	('Vino Blanco'),
	('Vino Rose');
GO
---
	
INSERT INTO [Cepa] ([Nombre])
	VALUES 
	('Ancellota'),
	('Blend'),
	('Bonarda'),
	('Cabernet Franc'),
	('Viura');
GO
---

CREATE TABLE [Role] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Tipo] [varchar](20) NOT NULL UNIQUE,
	CONSTRAINT [PK_Role_Id] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Usuario] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NOT NULL UNIQUE,
	[Contrasenia] [varchar](20) NOT NULL,
	[Estado] bit NOT NULL,
	[IdRole] [int] NOT NULL,
	CONSTRAINT [PK_Usuario_Id] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Role_Id] FOREIGN KEY ([IdRole]) REFERENCES [Role] ([Id])
);
GO

INSERT INTO [Role] ([Tipo])
	VALUES 
	('SUPERADM'),
	('ADMIN');
GO

INSERT INTO [Usuario] ([Nombre], [Contrasenia], [Estado], [IdRole])
	VALUES 
	('carlos', '123', 1, 1),
	('usuario', '123', 0, 2);
GO

-- SELECTs
--  SELECT * FROM [Marca];
--  SELECT * FROM [TipoVino];
--  SELECT * FROM [Cepa];
--  SELECT * FROM [Vino];
--  SELECT * FROM [Usuario];
--  SELECT * FROM [Role];