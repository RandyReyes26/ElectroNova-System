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
select * from Cliente

/* ============================
   TABLA DE MARCA
============================ */
CREATE TABLE Marca (
    ID_Marca INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Marca NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(200),
    Estado BIT
);

select * from Marca



/* ================================
   TABLA MODELO
================================ */
CREATE TABLE Modelo (
    ID_Modelo INT IDENTITY(1,1) PRIMARY KEY,
    Codigo_Modelo NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(150) NOT NULL,
    Estado BIT 
);
