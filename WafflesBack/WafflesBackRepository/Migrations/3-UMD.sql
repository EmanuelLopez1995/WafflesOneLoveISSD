-- creacion tabla
CREATE TABLE UMD (
    idUMD INTEGER PRIMARY KEY IDENTITY(1,1),
    nombreUMD VARCHAR(255),
    nombreCortoUMD VARCHAR(50)
);



-- insert
INSERT INTO UMD (nombreUMD, nombreCortoUMD)
VALUES  ('gramos','GR'),
		('kilogramos','KG'),
		('mililitros','ML'),
		('litros','L'),
		('unidad','UN')