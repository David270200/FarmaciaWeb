-- Crear base de datos
CREATE DATABASE BDBiofarma;
GO

USE BDBiofarma;
GO

------------------------------------------------------------
-- TABLA: Categorias
------------------------------------------------------------
CREATE TABLE Categorias (
    CategoriaID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL UNIQUE
);
GO

INSERT INTO Categorias (Nombre) VALUES
('Analgésico'),
('Antiinflamatorio'),
('Gastrointestinal'),
('Antihistamínico');
GO

------------------------------------------------------------
-- TABLA: Clientes
------------------------------------------------------------
CREATE TABLE Clientes (
    ClienteID INT IDENTITY(1,1) PRIMARY KEY,
    NombreCliente NVARCHAR(255) NOT NULL,
    Telefono NVARCHAR(15),
    CorreoElectronico NVARCHAR(100),
    Contrasena NVARCHAR(255)
);
GO

INSERT INTO Clientes (NombreCliente, Telefono, CorreoElectronico, Contrasena) VALUES
('Tiara Rodriguez','67985102','tiara.rodriguez@hotmail.com','1234');
GO

------------------------------------------------------------
-- TABLA: Empleado
------------------------------------------------------------
CREATE TABLE Empleado (
    EmpleadoID INT IDENTITY(1,1) PRIMARY KEY,
    NombreEmpleado NVARCHAR(50) NOT NULL UNIQUE,
    Contrasena NVARCHAR(255) NOT NULL,
    CorreoElectronico NVARCHAR(100)
);
GO

INSERT INTO Empleado (NombreEmpleado, Contrasena, CorreoElectronico) VALUES
('admin','1234','adminbiofarma@gmail.com');
GO

------------------------------------------------------------
-- TABLA: Productos
------------------------------------------------------------
CREATE TABLE Productos (
    ProductoID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(255) NOT NULL,
    Descripcion NVARCHAR(MAX),
    Precio DECIMAL(10,2) CHECK (Precio >= 0),
    CantidadEnStock INT CHECK (CantidadEnStock >= 0),
    FechaVencimiento DATE,
    CategoriaID INT,
    ImagenUrl NVARCHAR(255),
    FOREIGN KEY (CategoriaID) REFERENCES Categorias(CategoriaID)
);
GO

INSERT INTO Productos (Nombre, Descripcion, Precio, CantidadEnStock, FechaVencimiento, CategoriaID, ImagenUrl) VALUES
('Paracetamol 500mg','Caja con 10 tabletas',2.50,30,'2026-12-31',1,'imagenes/producto24.jpg'),
('Ibuprofeno 400mg','Caja con 20 tabletas',3.00,80,'2025-08-15',2,'imagenes/producto23.jpg'),
('Omeprazol 20mg','Caja con 14 cápsulas',4.25,50,'2027-03-20',3,'imagenes/producto22.jpg'),
('Loratadina 10mg','Caja con 10 tabletas',1.80,120,'2026-06-10',4,'imagenes/producto21.jpg'),
('Hidroxicina 25 mg','Caja con 10 tabletas',0.30,100,'2026-12-31',1,'imagenes/producto1.jpeg'),
('Prednisona 20 mg','Caja con 10 tabletas',0.30,100,'2026-12-31',1,'imagenes/producto2.jpeg'),
('Nistatina suspensión oral','Frasco de 60 ml',5.00,50,'2025-11-30',2,'imagenes/producto3.jpeg'),
('Dextrometorfano jarabe','Frasco de 120 ml',3.00,50,'2026-01-15',2,'imagenes/producto4.jpeg'),
('Loperamida 150 mg','Caja con 10 tabletas',3.00,80,'2026-08-10',1,'imagenes/producto5.jpeg'),
('Ciprofloxacino 500 mg','Caja con 10 tabletas',0.40,100,'2027-03-01',1,'imagenes/producto6.jpeg'),
('Diclofenaco sódico 50 mg','Caja con 10 tabletas',0.20,120,'2025-10-01',1,'imagenes/producto7.jpeg'),
('Metoclopramida 10 mg','Caja con 10 tabletas',0.20,100,'2026-07-07',1,'imagenes/producto8.jpeg'),
('Clonazepam 0.5 mg','Caja con 10 tabletas',0.50,90,'2027-05-20',1,'imagenes/producto9.jpeg'),
('Ácido fólico 5 mg','Caja con 30 tabletas',1.00,150,'2026-12-12',3,'imagenes/producto10.jpeg'),
('Simvastatina 20 mg','Caja con 30 tabletas',2.50,60,'2026-03-03',3,'imagenes/producto11.jpeg'),
('Losartán 50 mg','Caja con 30 tabletas',0.35,100,'2026-04-10',3,'imagenes/producto12.jpeg'),
('Enalapril 10 mg','Caja con 30 tabletas',0.45,100,'2027-01-01',3,'imagenes/producto13.jpeg'),
('Ranitidina 150 mg','Caja con 20 tabletas',1.00,100,'2025-09-15',1,'imagenes/producto14.jpeg'),
('Salbutamol 100 mcg (inhalador)','Inhalador de 200 dosis',3.50,40,'2026-02-14',2,'imagenes/producto15.jpeg'),
('Metformina 850 mg','Caja con 30 tabletas',8.85,100,'2027-06-06',3,'imagenes/producto16.jpeg'),
('Amoxicilina 500 mg','Caja con 21 cápsulas',5.00,80,'2026-08-08',1,'imagenes/producto18.jpeg'),
('Multivitamínico diario','Frasco con 30 cápsulas',2.00,70,'2026-05-05',3,'imagenes/producto17.jpeg'),
('Acetaminofén 500 mg','Caja con 20 tabletas',0.15,150,'2027-03-10',1,'imagenes/producto19.jpeg'),
('Fentanilo 100mcg','Ampolla o parche',3.00,20,'2025-12-31',4,'imagenes/producto20.jpeg'),
('Etofenamato 30mg','Tópica (aplicación cutánea)',1.10,25,'2026-05-15',3,NULL),
('Epiduo','Gel para el acne',5.10,10,'2026-08-27',3,NULL);
GO

------------------------------------------------------------
-- TABLA: Ventas
------------------------------------------------------------
CREATE TABLE Ventas (
    VentaID INT IDENTITY(1,1) PRIMARY KEY,
    FechaVenta DATETIME NOT NULL,
    ClienteID INT NOT NULL,
    TotalVenta DECIMAL(10,2) CHECK (TotalVenta >= 0),
    DireccionEntrega NVARCHAR(255),
    FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID)
);
GO

INSERT INTO Ventas (FechaVenta, ClienteID, TotalVenta, DireccionEntrega) VALUES
('2025-07-10 14:35:00',2,8.00,'Villa Zaita, Panamá');
GO

------------------------------------------------------------
-- TABLA: DetalleVenta
------------------------------------------------------------
CREATE TABLE DetalleVenta (
    DetalleID INT IDENTITY(1,1) PRIMARY KEY,
    VentaID INT NOT NULL,
    ProductoID INT NOT NULL,
    Cantidad INT CHECK (Cantidad > 0),
    PrecioUnitario DECIMAL(10,2) CHECK (PrecioUnitario >= 0),
    FOREIGN KEY (VentaID) REFERENCES Ventas(VentaID) ON DELETE CASCADE,
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID)
);
GO