-- Crear las tablas iniciales
CREATE TABLE SueldosBasicos (
    idSueldosBasicos INT PRIMARY KEY IDENTITY(1,1),
    descripcion VARCHAR(100),
    valorHoraNormal INT,
    valorHoraFerDom INT,
    basicoDueno DECIMAL(10, 2),
    plusEncargado DECIMAL(10, 2)
);

CREATE TABLE PuestoEmpleado (
    idPuestoEmpleado INT PRIMARY KEY IDENTITY(1,1),
    DescripcionPuesto VARCHAR(50),
    idSueldosBasicos INT,
    FOREIGN KEY (idSueldosBasicos) REFERENCES SueldosBasicos(idSueldosBasicos)
);

CREATE TABLE Empleado (
    idEmpleado INT PRIMARY KEY IDENTITY(1,1),
    nombreEmpleado VARCHAR(50),
    apellidoEmpleado VARCHAR(50),
    direccionEmpleado VARCHAR(100),
    telefonoEmpleado VARCHAR(20),
    mailEmpleado VARCHAR(50),
    DNIEmpleado VARCHAR(20),
    idPuestoEmpleado INT,
    FOREIGN KEY (idPuestoEmpleado) REFERENCES PuestoEmpleado(idPuestoEmpleado)
);

CREATE TABLE Caja (
    idCaja INT PRIMARY KEY IDENTITY(1,1),
    activoInicial DECIMAL(10, 2),
    retiroCaja DECIMAL(10, 2),
    importeInicial DECIMAL(10, 2),
    importeFinal DECIMAL(10, 2)
);

CREATE TABLE Turno (
    idTurno INT PRIMARY KEY IDENTITY(1,1),
    tipoTurno INT,
    fechaTurno DATE,
    horaDelInicio TIME,
    horaCierre TIME,
    notasInicio VARCHAR(255),
    notasCierre VARCHAR(255),
    esFeriado BIT,
    idCaja INT,
    idEncargadoTurno INT,
    FOREIGN KEY (idCaja) REFERENCES Caja(idCaja),
    FOREIGN KEY (idEncargadoTurno) REFERENCES Empleado(idEmpleado)
);

CREATE TABLE Gasto (
    idGasto INT PRIMARY KEY IDENTITY(1,1),
    fechaGasto DATETIME,
    idCaja INT,
    FOREIGN KEY (idCaja) REFERENCES Caja(idCaja)
);

CREATE TABLE DetalleGasto (
    idDetalleGasto INT PRIMARY KEY IDENTITY(1,1),
    importeTotalGasto DECIMAL(10, 2),
    descripcionGasto VARCHAR(60),
    idGasto INT,
    FOREIGN KEY (idGasto) REFERENCES Gasto(idGasto)
);

CREATE TABLE ActivoInicial (
    idActivoInicial INT PRIMARY KEY IDENTITY(1,1),
    montoActivoInicial DECIMAL(10, 2)
);

CREATE TABLE TurnoEmpleado (
    idEmpleado INT,
    horaIngresoEmpleado TIME,
    descripcionIngreso VARCHAR(255),
    horaEgresoEmpleado TIME,
    descripcionEgreso VARCHAR(255),
    idTurno INT,
    esRespDeApertCaja BIT,
    esRespDeCierreCaja BIT,
    sueldoTotalDelDia DECIMAL(10, 2),
    FOREIGN KEY (idEmpleado) REFERENCES Empleado(idEmpleado),
    FOREIGN KEY (idTurno) REFERENCES Turno(idTurno)
);

CREATE TABLE Pago (
    idPago INT PRIMARY KEY IDENTITY(1,1),
    idEmpleado INT,
    FechaPago DATE,
    MontoPago DECIMAL(10, 2),
    FOREIGN KEY (idEmpleado) REFERENCES Empleado(idEmpleado)
);

CREATE TABLE AdelantoSueldo (
    idAdelantoSueldo INT PRIMARY KEY IDENTITY(1,1),
    montoTotalAdelantoSueldo DECIMAL(10, 2),
    idEmpleado INT,
    FOREIGN KEY (idEmpleado) REFERENCES Empleado(idEmpleado)
);

CREATE TABLE Proveedor (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    RazonSocial NVARCHAR(100),
    Direccion NVARCHAR(100),
    Numero NVARCHAR(20),
    Cuit NVARCHAR(20),
    Email NVARCHAR(100),
    Detalle NVARCHAR(255)
);