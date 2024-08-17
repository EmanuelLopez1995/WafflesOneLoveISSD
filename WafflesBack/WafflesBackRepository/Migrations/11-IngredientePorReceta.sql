CREATE TABLE IngredientePorReceta (
    IdIngPorReceta INT PRIMARY KEY IDENTITY (1,1),
    cantidad DECIMAL(18, 2),
    IdIngrediente INT,
    IdReceta INT,
    FOREIGN KEY (IdIngrediente) REFERENCES Ingrediente(IdIngrediente),
    FOREIGN KEY (IdReceta) REFERENCES Receta(IdReceta)
);