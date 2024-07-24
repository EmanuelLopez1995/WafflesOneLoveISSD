CREATE TABLE Usuario (
    idUsuario INT PRIMARY KEY IDENTITY(1,1),
    nombreUsuario NVARCHAR(50),
    claveUsuario NVARCHAR(255)
);

CREATE TABLE Seccion (
    idSeccion INT PRIMARY KEY IDENTITY(1,1),
    nombreSeccion NVARCHAR(50)
);

CREATE TABLE UsuarioSecciones (
    idUsuario INT,
    idSeccion INT,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario),
    FOREIGN KEY (idSeccion) REFERENCES Seccion(idSeccion),
    PRIMARY KEY (idUsuario, idSeccion)
);

INSERT INTO Seccion (nombreSeccion) VALUES ('compras'), ('ventas'), ('stock'), ('turno'), ('admin'), ('proveedores'), ('empleados'), ('finanzas'), ('promociones'), ('produccion');

INSERT INTO Usuario (nombreUsuario, claveUsuario) VALUES ('admin', '123456');

INSERT INTO UsuarioSecciones (idUsuario, idSeccion) VALUES (1,1),(1,2),(1,3),(1,4),(1,5),(1,6),(1,7),(1,8),(1,9),(1,10);


--Capaz hace falta instalar estas librerias: 
--using Microsoft.AspNetCore.Authentication.JwtBearer; version 6.0.16
--using Microsoft.IdentityModel.Tokens;
--using System.Text;


