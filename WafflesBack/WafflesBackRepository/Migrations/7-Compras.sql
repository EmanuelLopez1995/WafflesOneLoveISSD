CREATE TABLE Compra (
    idCompra INT PRIMARY KEY IDENTITY(1,1),
    fechaCompra DATE,
    archivo VARBINARY(MAX),
    idProveedor INT,
    Total DECIMAL(10, 2),
    FOREIGN KEY (idProveedor) REFERENCES Proveedor(id)
);

CREATE TABLE DetalleCompra (
    idDetalleCompra INT PRIMARY KEY IDENTITY(1,1),
    cantidad INT,
    precioUnitario DECIMAL(10, 2),
    subtotal DECIMAL(10, 2),
    idCompra INT,
    idArticulo INT,
    FOREIGN KEY (idCompra) REFERENCES Compra(idCompra),
    FOREIGN KEY (idArticulo) REFERENCES Articulo(idArticulo)
);