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

INSERT INTO Articulo (  IdArticulo, nombreArticulo, marcaArticulo, stockMinimo,
                        stockActual, esMateriaPrima, pesoArticulo, detalleArticulo, idUMD) 
VALUES  ('Leche', 'La Vaquita', 10.00, 50.00, false, NULL, 'Leche entera pasteurizada',5),
        ('Manteca x 200gr', 'La Cabaña', 5.00, 20.00, false, NULL, 'Manteca sin sal', 1),
        ('Manteca x 150gr', 'La Quesera', 5.00, 20.00, false, NULL, 'Manteca sin sal', 1),
        ('Harina', 'Molinos del Sol', 15.00, 60.00, false, NULL, 'Harina de trigo 000', 2),
        ('Huevo', 'Granja Feliz', 30.00, 100.00, false, NULL, 'Huevo de gallina', 3);
