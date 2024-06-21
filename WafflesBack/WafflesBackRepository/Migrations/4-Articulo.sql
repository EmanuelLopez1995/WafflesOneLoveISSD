CREATE TABLE Articulo (
    IdArticulo INT PRIMARY KEY IDENTITY(1,1),
    nombreArticulo VARCHAR(255),
    marcaArticulo VARCHAR(255),
    stockMinimo DECIMAL(10, 2),
    stockActual DECIMAL(10, 2),
    esMateriaPrima BIT, 
    pesoArticulo DECIMAL(10, 2),
    detalleArticulo VARCHAR(255),
    idUMD INT,
    FOREIGN KEY (idUMD) REFERENCES UMD(IdUMD)
);

INSERT INTO Articulo (  nombreArticulo, marcaArticulo, stockMinimo,
                        stockActual, esMateriaPrima, pesoArticulo, detalleArticulo, idUMD) 
VALUES  ('Leche', 'La Vaquita', 10.00, 50.00, 0, 0.00, 'Leche entera pasteurizada',5),
        ('Manteca x 200gr', 'La Cabaña', 5.00, 20.00, 0, 0.00, 'Manteca sin sal', 1),
        ('Manteca x 150gr', 'La Quesera', 5.00, 20.00, 0, 0.00, 'Manteca sin sal', 1),
        ('Harina', 'Molinos del Sol', 15.00, 60.00, 0, 0.00, 'Harina de trigo 000', 2),
        ('Huevo', 'Granja Feliz', 30.00, 100.00, 0, 0.00, 'Huevo de gallina', 3);
