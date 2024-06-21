  CREATE TABLE Ingrediente (
    IdIngrediente INT IDENTITY(1,1) PRIMARY KEY,
    nombreIngrediente VARCHAR(255),
    stockMinimo DECIMAL(10, 2),
    stockActual DECIMAL(10, 2),
    detalleIngrediente VARCHAR(255),
    idUMD INT,
    FOREIGN KEY (idUMD) REFERENCES UMD(IdUMD)
);

