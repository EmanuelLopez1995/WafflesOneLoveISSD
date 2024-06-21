CREATE TABLE ArticulosPorIngrediente (
    IdArtPorIngrediente INT IDENTITY(1,1) PRIMARY KEY,
    IdIngrediente INT NOT NULL,
    IdArticulo INT NOT NULL,
    FOREIGN KEY (IdIngrediente) REFERENCES Ingrediente(IdIngrediente),
    FOREIGN KEY (IdArticulo) REFERENCES Articulo(IdArticulo)
);