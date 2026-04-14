CREATE DATABASE ProyectoElectroNova1;
GO

USE ProyectoElectroNova1;
GO

/* ================================
   TABLA ROLES
================================ */
   --TABLA DE ROL--
CREATE TABLE Rol (
    ID_Rol INT PRIMARY KEY,
    Nombre_Rol NVARCHAR(50) NOT NULL
);

/* ================================
   TABLA USUARIOS
================================ */
CREATE TABLE Usuario (
    ID_Usuario INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario NVARCHAR(50),
    Contrasena NVARCHAR(100) NOT NULL,
    ID_Rol INT FOREIGN KEY REFERENCES Rol(ID_Rol),
    Nombre NVARCHAR(50) NOT NULL,
    Apellidos NVARCHAR(50),
    Email NVARCHAR(100),
    Estado BIT,
);

/**************************/
select * from Usuario
/**************************/

ALTER TABLE Usuario
DROP COLUMN Nombre,
            Apellidos,
            Email;

INSERT INTO Usuario
(NombreUsuario, Contrasena, ID_Rol, Estado)
VALUES
('admin', '123', 1, 1);

DELETE FROM Usuario
WHERE ID_Usuario = 1;

INSERT INTO Rol (ID_Rol, Nombre_Rol)
VALUES (1, 'ADMINISTRADOR');

INSERT INTO Rol (ID_Rol, Nombre_Rol)
VALUES (2, 'VENDEDOR');

INSERT INTO Rol (ID_Rol, Nombre_Rol)
VALUES (3, 'REPORTES');
INSERT INTO Usuario
(NombreUsuario, Contrasena, ID_Rol, Nombre, Apellidos, Email, Estado)
VALUES
('admin', '123', 1, 'Randy', 'Reyes', 'randy@gmail.com', 1);



/* ================================
   TABLA CLIENTES
================================ */

CREATE TABLE Cliente (
    ID_Cliente INT IDENTITY(1,1) PRIMARY KEY,
    Identificacion NVARCHAR(20),
    Pasaporte NVARCHAR(20),
    Nombre NVARCHAR(50) NOT NULL,
    Apellidos NVARCHAR(50),
    Sexo INT NOT NULL,
    Telefono NVARCHAR(15),
    Email NVARCHAR(100),
    DireccionExacta NVARCHAR(200),
    Provincia NVARCHAR(50),
    Fotografia VARBINARY(MAX),
    Estado BIT
);

/**************************/
select * from Cliente
/**************************/

/* ============================
   TABLA DE MARCA
============================ */
CREATE TABLE Marca (
    ID_Marca INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Marca NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(200),
    Estado BIT
);

/**************************/
select * from Marca
/**************************/

/* ================================
   TABLA MODELO
================================ */
CREATE TABLE Modelo (
    ID_Modelo INT IDENTITY(1,1) PRIMARY KEY,
    Codigo_Modelo NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(150) NOT NULL,
    Estado BIT 
);

/**************************/
select * from Modelo
/**************************/

/* ================================
   TABLA Tipo Dispositivo
================================ */
CREATE TABLE TipoDispositivo (
    ID_TipoDispositivo INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_TipoDispositivo NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(300),
    Estado BIT 
);

select * from TipoDispositivo


/* ================================
   Datos de ejemplo
================================ */

INSERT INTO TipoDispositivo (Nombre_TipoDispositivo, Descripcion, Estado) VALUES
('Smartphone', 'Telefono inteligente de uso personal', 1),
('Tablet', 'Dispositivo tactil para multimedia y trabajo', 1),
('Laptop', 'Computadora portatil', 1),
('Smartwatch', 'Reloj inteligente con funciones avanzadas', 1),
('PC de Escritorio', 'Equipo fijo de alto rendimiento', 1),
('Smartband', 'Pulsera inteligente orientada a actividad fisica', 1),
('Audifonos Inalambricos', 'Auriculares TWS o bluetooth', 1),
('Consola de Videojuegos', 'Equipo para videojuegos', 1),
('Smart TV', 'Televisor inteligente con acceso a internet', 1),
('Asistente de Voz', 'Dispositivo inteligente para comandos por voz', 1),
('Streaming Stick', 'Dispositivo de transmision multimedia', 1),
('Camara DSLR o Mirrorless', 'Camara profesional de fotografia o video', 1),
('Camara de Accion', 'Camara compacta para deportes y aventura', 1),
('Drone', 'Dispositivo aereo con camara', 1);



/* ================================
   TABLA Producto
================================ */
CREATE TABLE Producto (
    ID_Producto INT IDENTITY(1,1) PRIMARY KEY,
    Codigo_Barras NVARCHAR(100),
    ID_Marca INT,
    ID_Modelo INT,
    ID_TipoDispositivo INT,
    Informacion_General NVARCHAR(500) ,
    Caracteristicas_Tecnicas NVARCHAR(1000),
    Extras_Accesorios NVARCHAR(500),
    Fotografia VARBINARY(MAX),
    Estado BIT,

    CONSTRAINT FK_Producto_Marca
        FOREIGN KEY (ID_Marca) REFERENCES Marca(ID_Marca),

    CONSTRAINT FK_Producto_Modelo
        FOREIGN KEY (ID_Modelo) REFERENCES Modelo(ID_Modelo),

    CONSTRAINT FK_Producto_TipoDispositivo
        FOREIGN KEY (ID_TipoDispositivo) REFERENCES TipoDispositivo(ID_TipoDispositivo)
);

ALTER TABLE Producto
ADD DocumentoEspecificaciones NVARCHAR(800),
    Precio DECIMAL(18,2);


ALTER TABLE Producto
ADD Existencia INT;

/**************************/
select * from Producto
/**************************/

/* ================================
   TABLA IVA
================================ */
CREATE TABLE IVA (
    ID_IVA INT PRIMARY KEY,
	Descripcion VARCHAR(50),
    Valor DECIMAL(5, 2) NOT NULL
);

INSERT INTO IVA (ID_IVA, Descripcion, Valor)
VALUES (1, 'IVA Costa Rica', 13.00);
/**************************/
select * from IVA
/**************************/


/* ================================
   TABLA Stock
================================ */
CREATE TABLE IngresoStock (
    ID_IngresoStock INT IDENTITY(1,1) PRIMARY KEY,
    ID_Producto INT,
    TipoMovimiento NVARCHAR(20),
    Cantidad INT,
    FacturaCompra NVARCHAR(100),
    Observaciones NVARCHAR(300),
    FOREIGN KEY (ID_Producto) REFERENCES Producto(ID_Producto)
);

/**************************/
select * from IngresoStock
/**************************/

/* ================================
   TABLA FACTURA
================================ */
CREATE TABLE Factura (
    ID_Factura VARCHAR(20) PRIMARY KEY, -- Ej: FAC-0001

    ID_Cliente INT NOT NULL,
    ID_Usuario INT NOT NULL,
    Fecha DATETIME NOT NULL,

    Subtotal DECIMAL(18,2) NOT NULL,
    IVA DECIMAL(18,2) NOT NULL,
    TotalCRC DECIMAL(18,2) NOT NULL,
    TotalUSD DECIMAL(18,2) NOT NULL,
    TipoCambio DECIMAL(18,2) NOT NULL,

    FirmaCliente VARBINARY(MAX) NULL,
    DocumentoXML XML NULL,

    TipoPago VARCHAR(50) NULL,
    Descuento DECIMAL(18,2) NULL,
    Banco VARCHAR(100) NULL,
    TipoTarjeta VARCHAR(50) NULL,

    Estado BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_Factura_Cliente FOREIGN KEY (ID_Cliente)
        REFERENCES Cliente(ID_Cliente),

    CONSTRAINT FK_Factura_Usuario FOREIGN KEY (ID_Usuario)
        REFERENCES Usuario(ID_Usuario)
);
ALTER TABLE Factura
DROP COLUMN TipoTarjeta;
ALTER TABLE Factura
ADD ID_Tarjeta INT NULL;
ALTER TABLE Factura
ADD CONSTRAINT FK_Factura_Tarjeta
FOREIGN KEY (ID_Tarjeta) REFERENCES Tarjeta(ID_Tarjeta);
select * from Factura

ALTER TABLE Factura DROP CONSTRAINT FK_Factura_Usuario;

ALTER TABLE Factura DROP COLUMN ID_Usuario;

/* ================================
   TABLA DETALLE FACTURA
================================ */

CREATE TABLE DetalleFactura (
    ID_DetalleFactura INT IDENTITY(1,1) PRIMARY KEY,
    ID_Factura VARCHAR(20) NOT NULL,
    ID_Producto INT NOT NULL,

    Cantidad INT NOT NULL,
    Precio DECIMAL(18,2) NOT NULL,
    Subtotal DECIMAL(18,2) NOT NULL,
    IVA DECIMAL(18,2) NOT NULL,
    Total DECIMAL(18,2) NOT NULL,

    CONSTRAINT FK_DetalleFactura_Factura FOREIGN KEY (ID_Factura)
        REFERENCES Factura(ID_Factura),

    CONSTRAINT FK_DetalleFactura_Producto FOREIGN KEY (ID_Producto)
        REFERENCES Producto(ID_Producto)
);

select * from DetalleFactura

CREATE TABLE Tarjeta(
    ID_tarjeta INT PRIMARY KEY,
    DescripcionTarjeta NVARCHAR(255)
);

INSERT INTO Tarjeta (ID_tarjeta, DescripcionTarjeta)
VALUES 
(1, 'Visa'),
(2, 'Mastercard'),
(3, 'American Express');

select * from Tarjeta
/*****************************************/

USE ProyectoElectroNova1;
GO

-- =========================
-- LIMPIEZA EN ORDEN CORRECTO
-- =========================

DELETE FROM DetalleFactura;
DELETE FROM Factura;
DELETE FROM IngresoStock;
DELETE FROM Producto;
DELETE FROM Cliente;
DELETE FROM Modelo;
DELETE FROM Marca;
DELETE FROM TipoDispositivo;
DELETE FROM Tarjeta;
DELETE FROM Usuario;
-- =========================
-- RESEED DE IDENTITIES
-- =========================

DBCC CHECKIDENT ('DetalleFactura', RESEED, 0);
DBCC CHECKIDENT ('IngresoStock', RESEED, 0);
DBCC CHECKIDENT ('Producto', RESEED, 0);
DBCC CHECKIDENT ('Cliente', RESEED, 0);
DBCC CHECKIDENT ('Modelo', RESEED, 0);
DBCC CHECKIDENT ('Marca', RESEED, 0);
DBCC CHECKIDENT ('TipoDispositivo', RESEED, 0);
DBCC CHECKIDENT ('Usuario', RESEED, 0);

-- Tarjeta no lleva IDENTITY en tu script, así que no ocupa reseed
-- Factura tampoco, porque usa VARCHAR tipo FAC-0001

-- =========================
-- VERIFICACIÓN
-- =========================

SELECT * FROM DetalleFactura;
SELECT * FROM Factura;
SELECT * FROM IngresoStock;
SELECT * FROM Producto;
SELECT * FROM Cliente;
SELECT * FROM Modelo;
SELECT * FROM Marca;
SELECT * FROM TipoDispositivo;
SELECT * FROM Tarjeta;
SELECT * FROM Usuario;