CREATE DATABASE ProyectoElectroNova;
GO

USE ProyectoElectroNova;
GO

/* ================================
   TABLA ROLES
================================ */
CREATE TABLE Rol (
    ID_Rol INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Rol NVARCHAR(50) NOT NULL
);
GO

/* ================================
   TABLA USUARIOS
================================ */
CREATE TABLE Usuario (
    ID_Usuario INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario NVARCHAR(50) NOT NULL,
    Contrasena NVARCHAR(100) NOT NULL,
    ID_Rol INT NOT NULL,
    Nombre NVARCHAR(50) NOT NULL,
    Apellidos NVARCHAR(50),
    Email NVARCHAR(100),
    Estado BIT DEFAULT 1,

    FOREIGN KEY (ID_Rol) REFERENCES Rol(ID_Rol)
);
GO


/*no ejecutado*/
/* ================================
   TABLA CATEGORIA
================================ */
CREATE TABLE Categoria (
    ID_Categoria INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Categoria NVARCHAR(50) NOT NULL
);
GO

/* ================================
   TABLA MARCA
================================ */
CREATE TABLE Marca (
    ID_Marca INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Marca NVARCHAR(50) NOT NULL
);
GO

/* ================================
   TABLA PRODUCTOS
================================ */
CREATE TABLE Producto (
    ID_Producto INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Producto NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(200),
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL,
    ID_Categoria INT,
    ID_Marca INT,

    FOREIGN KEY (ID_Categoria) REFERENCES Categoria(ID_Categoria),
    FOREIGN KEY (ID_Marca) REFERENCES Marca(ID_Marca)
);
GO

/* ================================
   TABLA CLIENTES
================================ */
CREATE TABLE Cliente (
    ID_Cliente INT IDENTITY(1,1) PRIMARY KEY,
    Identificacion NVARCHAR(20) NOT NULL, -- Cedula o Pasaporte
    Pasaporte NVARCHAR(20), -- Nacional / Extranjero
    Nombre NVARCHAR(50) NOT NULL,
    Apellidos NVARCHAR(50),
    Sexo INT NOT NULL,
    Telefono NVARCHAR(15),
    Correo NVARCHAR(100),
    Direccion NVARCHAR(150),
    Provincia NVARCHAR(50),
    Fotografia VARBINARY(MAX),
    Estado BIT 
);
GO

ALTER TABLE Cliente
ADD Identificacion NVARCHAR(20) NULL;



/* ================================
   TABLA PEDIDO
================================ */
CREATE TABLE Pedido (
    ID_Pedido INT IDENTITY(1,1) PRIMARY KEY,
    ID_Cliente INT,
    Fecha DATETIME DEFAULT GETDATE(),
    Total DECIMAL(10,2),

    FOREIGN KEY (ID_Cliente) REFERENCES Cliente(ID_Cliente)
);
GO

/* ================================
   TABLA DETALLE PEDIDO
================================ */
CREATE TABLE DetallePedido (
    ID_Detalle INT IDENTITY(1,1) PRIMARY KEY,
    ID_Pedido INT,
    ID_Producto INT,
    Cantidad INT,
    Precio DECIMAL(10,2),

    FOREIGN KEY (ID_Pedido) REFERENCES Pedido(ID_Pedido),
    FOREIGN KEY (ID_Producto) REFERENCES Producto(ID_Producto)
);
GO