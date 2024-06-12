INSERT INTO SueldosBasicos ( descripcion, valorHoraNormal, valorHoraFerDom, basicoDueno, plusEncargado)
VALUES 
('valorColaborador', 300, 700, NULL, NULL),
('valorEncargado', 300, 700, NULL, 25000),
('valorDueño', NULL, NULL, 350000, NULL);

INSERT INTO PuestoEmpleado ( DescripcionPuesto, idSueldosBasicos)
VALUES 
('Colaborador', 1),
('Encargado', 2),
('Dueño', 3); 


INSERT INTO Empleado (nombreEmpleado, apellidoEmpleado, direccionEmpleado, telefonoEmpleado, mailEmpleado, DNIEmpleado, idPuestoEmpleado)
VALUES
    ('Juan', 'Pérez', 'Calle 123', '123456789', 'juan.perez@example.com', '12345678', 1),
    ('María', 'Gómez', 'Avenida 456', '987654321', 'maria.gomez@example.com', '87654321', 2),
    ('Pedro', 'López', 'Plaza Principal', '456123789', 'pedro.lopez@example.com', '45678912', 1)


INSERT INTO ActivoInicial (montoActivoInicial)
VALUES
    (0)