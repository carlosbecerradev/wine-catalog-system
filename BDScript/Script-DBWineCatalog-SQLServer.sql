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

-- SELECTs
--  SELECT * FROM [Marca];
--  SELECT * FROM [TipoVino];
--  SELECT * FROM [Cepa];
--  SELECT * FROM [Vino];

--- INSERTs
INSERT INTO [Marca] ([Nombre])
	VALUES 
	('CONCHA Y TORO'),
	('SANTIAGO QUEIROLO'),
	('TABERNERO'),
	('TRAPICHE'),
	('TACAMA');
GO
---

INSERT INTO [TipoVino] ([Nombre])
	VALUES 
	('VINO TINTO'),
	('VINO BLANCO'),
	('VINO ROSE');
GO
---
	
INSERT INTO [Cepa] ([Nombre])
	VALUES 
	('ANCELLOTA'),
	('BLEND'),
	('BONARDA'),
	('CABERNET FRANC'),
	('VIURA');
GO
---

INSERT INTO [Vino] ([ImagenUri], [IdTipoVino], [IdMarca],[IdCepa], [Pais], [Cosecha], [Precio], [Stock])
	VALUES 
	('img/img1.jpg', 1, 1, 1, 'Italia', '2018-10-15', 55, 100),
	('img/img1.jpg', 1, 2, 1, 'Francia', '2018-10-15', 45, 100),
	('img/img1.jpg', 1, 1, 2, 'Italia', '2018-10-15', 155, 100),
	('img/img1.jpg', 2, 3, 1, 'Peru', '2018-10-15', 85, 100),
	('img/img1.jpg', 2, 1, 3, 'Francia', '2018-10-15', 255, 100),
	('img/img1.jpg', 2, 2, 3, 'Peru', '2018-10-15', 200, 100);
GO

-- select * from Vino where Precio between 40 and 100;
-- use DBWineCatalog
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
	('CLIENTE'),
	('ADMIN');
GO

INSERT INTO [Usuario] ([Nombre], [Contrasenia], [Estado], [IdRole])
	VALUES 
	('cliente', '123', 1, 1),
	('administrador', '123', 0, 2);
GO

-- select * from Usuario
-- select * from Role