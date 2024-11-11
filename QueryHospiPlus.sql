-- Crear la base de datos y seleccionar su uso
CREATE DATABASE HOSPIPLUS2; 
GO
USE HOSPIPLUS2
GO


-- Tabla Estados
CREATE TABLE Estados(
    EstadoID int PRIMARY KEY IDENTITY(1,1),
    Estado varchar(10) UNIQUE NOT NULL CHECK (Estado IN ('Activo', 'Inactivo'))
);

-- Insert Estados
INSERT INTO Estados (Estado) VALUES ('Activo'), ('Inactivo');

-- Nueva Tabla
CREATE TABLE RolesUsuarios(
	RolID int primary key identity(1,1),
	Roles varchar(20) unique NOT NULL CHECK (Roles IN('Administrador','Secretario','Medico'))
);

INSERT INTO RolesUsuarios(Roles) VALUES ('Administrador'),('Secretario'),('Medico');
select * from RolesUsuarios


-- Tabla Usuarios
CREATE TABLE Usuarios(
    UsuarioID int PRIMARY KEY IDENTITY(1,1),
    Correo varchar(100) UNIQUE NOT NULL,
    NombreUsuario varchar(80) NOT NULL,
    Contrasena varbinary(1000) NOT NULL,
    Rol varchar(20) CHECK (Rol IN ('Administrador', 'Secretario', 'Medico')) NOT NULL
);

select * from Usuarios

insert into Usuarios(NombreUsuario,Correo,Contrasena,Rol)
values('Ruth Vaquerano','ruthv@hospiplus.com',
ENCRYPTBYPASSPHRASE('hospiplus24','hospitca'),'Administrador')

insert into Usuarios(NombreUsuario,Correo,Contrasena,Rol)
values('Carlos Jimenez','carlosj@hospiplus.com',
ENCRYPTBYPASSPHRASE('hospiplus24','2'),'Secretario')

insert into Usuarios(NombreUsuario,Correo,Contrasena,Rol)
values('Diego Mejia','diegom@hospiplus.com',
ENCRYPTBYPASSPHRASE('hospiplus24','hospitca'),'Medico')



-- Tabla Sexos
CREATE TABLE Sexos(
    SexoID int PRIMARY KEY IDENTITY(1,1),
    DescripcionSexo varchar(10) UNIQUE NOT NULL CHECK (DescripcionSexo IN ('Masculino', 'Femenino'))
);

-- Insert Sexos
INSERT INTO Sexos (DescripcionSexo) VALUES ('Masculino'), ('Femenino');

SELECT * FROM Sexos


-- Tabla EstadosCiviles
CREATE TABLE EstadosCiviles(
    EstadoCivilID int PRIMARY KEY IDENTITY(1,1),
    DescripcionEstadoCivil varchar(10) UNIQUE NOT NULL CHECK (DescripcionEstadoCivil IN ('Soltero', 'Casado', 'Viudo', 'Divorciado'))
);

-- Insert EstadosCiviles
INSERT INTO EstadosCiviles (DescripcionEstadoCivil) VALUES ('Soltero'), ('Casado'), ('Viudo'), ('Divorciado');



-- Tabla Departamentos
CREATE TABLE Departamentos(
    DepartamentosID int PRIMARY KEY IDENTITY(1,1),
    Depar varchar(20) UNIQUE NOT NULL CHECK(Depar IN 
        ('Ahuachapán','Sonsonate','Santa Ana',
         'La Libertad','Chalatenango','San Salvador',
         'Cuscatlán','La Paz','San Vicente','Cabañas',
         'Usulután','San Miguel','Morazán','La Unión'))
);

-- Insert Departamentos
INSERT INTO Departamentos (Depar) VALUES 
('Ahuachapán'), ('Sonsonate'), ('Santa Ana'), 
('La Libertad'), ('Chalatenango'), ('San Salvador'), 
('Cuscatlán'), ('La Paz'), ('San Vicente'), 
('Cabañas'), ('Usulután'), ('San Miguel'), 
('Morazán'), ('La Unión');

SELECT * FROM Departamentos

-- Tabla Municipios
CREATE TABLE Municipios(
    MunicipioID int PRIMARY KEY IDENTITY(1,1),
    NombreMunicipio varchar(60) NOT NULL,
    DepartamentosID int NOT NULL,
    FOREIGN KEY (DepartamentosID) REFERENCES Departamentos(DepartamentosID)
);

-- Insert Municipios (Ejemplo: municipios de Ahuachapan)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Ahuachapán', 1), ('Apaneca', 1), ('Atiquizaya', 1), 
('Concepción de Ataco', 1), ('El Refugio', 1), ('Guaymango', 1), ('Jujutla', 1), ('San Francisco Menéndez', 1),
('San Lorenzo', 1), ('San Pedro Puxtla', 1), ('Tacuba', 1), ('Turín', 1);

-- Insert Municipios (Ejemplo: municipios de Sonsonate)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Sonsonate', 2), ('Acajutla', 2), ('Armenia', 2), 
('Caluco', 2), ('Cuisnahuat', 2), ('Izalco', 2), ('Juayúa', 2), ('Nahuizalco', 2),
('Nahulingo', 2), ('Salcoatitán', 2), ('San Antonio del Monte', 2), ('San Julián', 2),
('Santa Catarina Masahuat', 2), ('Santa Isabel Ishuatán', 2), ('Santo Domingo de Guzmán', 2), ('Sonzacate', 2);

-- Insert Municipios (Ejemplo: municipios de Santa Ana)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Santa Ana', 3), ('Candelaria de la Frontera', 3), ('Chalchuapa', 3), 
('Coatepeque', 3), ('El Congo', 3), ('El Porvenir', 3),
('Masahuat', 3), ('Metapán', 3),('San Antonio Pajonal', 3),
('San Sebastián Salitrillo', 3), ('Santa Rosa Guachipilín', 3), ('Santiago de la Frontera', 3),
('Texistepeque', 3);

-- Insert Municipios (Ejemplo: municipios de La Libertad)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Santa Tecla', 4), ('Antiguo Cuscatlán', 4), ('Chiltiupán', 4), 
('Ciudad Arce', 4), ('Colón', 4), ('Comasagua', 4), ('Huizúcar', 4), ('Jayaque', 4),
('Jicalapa', 4), ('La Libertad', 4), ('Nuevo Cuscatlán', 4), ('Quezaltepeque', 4), ('Sacacoyo', 4),
('San José Villanueva', 4), ('San Juan Opico', 4), ('San Matías', 4),
('San Pablo Tacachico', 4), ('Talnique', 4), ('Tamanique', 4), ('Teotepeque', 4), ('Tepecoyo', 4), ('Zaragoza', 4);

-- Insert Municipios (Ejemplo: municipios de La Chalatenango)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Agua Caliente', 5), ('Arcatao', 5), ('Azacualpa', 5), ('Cancasque', 5), 
('Chalatenango', 5), ('Citalá', 5), ('Comalapa', 5), ('Concepción Quezaltepeque', 5), 
('Dulce Nombre de María', 5), ('El Carrizal', 5), ('El Paraíso', 5), ('La Laguna', 5), 
('La Palma', 5), ('La Reina', 5), ('Las Vueltas', 5), ('Nueva Concepción', 5), 
('Nueva Trinidad', 5), ('Nombre de Jesús', 5), ('Ojos de Agua', 5), ('Potonico', 5), 
('San Antonio de la Cruz', 5), ('San Antonio Los Ranchos', 5), ('San Fernando', 5), 
('San Francisco Lempa', 5), ('San Francisco Morazán', 5), ('San Ignacio', 5), 
('San Isidro Labrador', 5), ('San José Cancasque', 5), ('San José Las Flores', 5), 
('San Luis del Carmen', 5), ('San Miguel de Mercedes', 5), ('San Rafael', 5), 
('Santa Rita', 5), ('Tejutla', 5);

-- Insert Municipios (Ejemplo: municipios de San Salvador)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('San Salvador', 6),('Aguilares', 6),('Apopa', 6),('Ayutuxtepeque', 6),
('Ciudad Delgado', 6),('Cuscatancingo', 6),('El Paisnal', 6),('Guazapa', 6),
('Ilopango', 6),('Mejicanos', 6),('Nejapa', 6),('Panchimalco', 6),
('Rosario de Mora', 6),('San Marcos', 6),('San Martín', 6),('Santiago Texacuangos', 6),
('Santo Tomás', 6),('Soyapango', 6),('Tonacatepeque', 6);

-- Insert Municipios (Ejemplo: municipios de Cuscatlán)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Candelaria', 7), ('Cojutepeque', 7), ('El Carmen', 7), ('El Rosario', 7),
('Monte San Juan', 7), ('Oratorio de Concepción', 7), ('San Bartolomé Perulapía', 7),
('San Cristóbal', 7), ('San José Guayabal', 7), ('San Pedro Perulapán', 7),
('San Rafael Cedros', 7), ('San Ramón', 7), ('Santa Cruz Analquito', 7),
('Santa Cruz Michapa', 7), ('Suchitoto', 7), ('Tenancingo', 7);

-- Insert Municipios (Ejemplo: municipios de La Paz)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Cuyultitán', 8), ('El Rosario', 8), ('Jerusalén', 8), ('Mercedes La Ceiba', 8),
('Olocuilta', 8), ('Paraíso de Osorio', 8), ('San Antonio Masahuat', 8),
('San Emigdio', 8), ('San Francisco Chinameca', 8), ('San Juan Nonualco', 8),
('San Juan Talpa', 8), ('San Juan Tepezontes', 8), ('San Luis La Herradura', 8),
('San Luis Talpa', 8), ('San Miguel Tepezontes', 8), ('San Pedro Masahuat', 8),
('San Pedro Nonualco', 8), ('San Rafael Obrajuelo', 8), ('Santa María Ostuma', 8),
('Santiago Nonualco', 8), ('Tapalhuaca', 8), ('Zacatecoluca', 8);

-- Insert Municipios (Ejemplo: municipios de San Vicente)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Apastepeque', 9), ('Guadalupe', 9), ('San Cayetano Istepeque', 9),
('San Esteban Catarina', 9), ('San Ildefonso', 9), ('San Lorenzo', 9),
('San Sebastián', 9), ('San Vicente', 9), ('Santa Clara', 9),
('Santo Domingo', 9), ('Tecoluca', 9), ('Tepetitán', 9), ('Verapaz', 9);

-- Insert Municipios (Ejemplo: municipios de Cabañas)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Cinquera', 10), ('Dolores', 10), ('Guacotecti', 10), ('Ilobasco', 10),
('Jutiapa', 10), ('San Isidro', 10), ('Sensuntepeque', 10), 
('Tejutepeque', 10), ('Victoria', 10);

-- Insert Municipios (Ejemplo: municipios de Usulután)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Alegría', 11), ('Berlín', 11), ('California', 11), ('Concepción Batres', 11),
('El Triunfo', 11), ('Ereguayquín', 11), ('Estanzuelas', 11), ('Jiquilisco', 11),
('Jucuapa', 11), ('Jucuarán', 11), ('Mercedes Umaña', 11), ('Nueva Granada', 11),
('Ozatlán', 11), ('Puerto El Triunfo', 11), ('San Agustín', 11), ('San Buenaventura', 11),
('San Dionisio', 11), ('San Francisco Javier', 11), ('Santa Elena', 11), ('Santa María', 11),
('Santiago de María', 11), ('Tecapán', 11), ('Usulután', 11);

-- Insert Municipios (Ejemplo: municipios de San Miguel)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Carolina', 12), ('Chapeltique', 12), ('Chinameca', 12), ('Chirilagua', 12),
('Ciudad Barrios', 12), ('Comacarán', 12), ('El Tránsito', 12), ('Lolotique', 12),
('Moncagua', 12), ('Nueva Guadalupe', 12), ('Nuevo Edén de San Juan', 12),
('Quelepa', 12), ('San Antonio', 12), ('San Gerardo', 12), ('San Jorge', 12),
('San Luis de la Reina', 12), ('San Miguel', 12), ('San Rafael Oriente', 12),
('Sesori', 12), ('Uluazapa', 12);

-- Insert Municipios (Ejemplo: municipios de Morazán)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Arambala', 13), ('Cacaopera', 13), ('Chilanga', 13), ('Corinto', 13),
('Delicias de Concepción', 13), ('El Divisadero', 13), ('El Rosario', 13),
('Gualococti', 13), ('Guatajiagua', 13), ('Joateca', 13), ('Jocoaitique', 13),
('Jocoro', 13), ('Lolotiquillo', 13), ('Meanguera', 13), ('Osicala', 13),
('Perquín', 13), ('San Carlos', 13), ('San Fernando', 13), ('San Francisco Gotera', 13),
('San Isidro', 13), ('San Simón', 13), ('Sensembra', 13), ('Sociedad', 13),
('Torola', 13), ('Yamabal', 13), ('Yoloaiquín', 13);

-- Insert Municipios (Ejemplo: municipios de La Unión)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Anamorós', 14), ('Bolívar', 14), ('Concepción de Oriente', 14),
('Conchagua', 14), ('El Carmen', 14), ('El Sauce', 14), ('Intipucá', 14),
('La Unión', 14), ('Lislique', 14), ('Meanguera del Golfo', 14),
('Nueva Esparta', 14), ('Pasaquina', 14), ('Polorós', 14), ('San Alejo', 14),
('San José', 14), ('Santa Rosa de Lima', 14), ('Yayantique', 14), ('Yucuaiquín', 14);


-- Tabla Pacientes
CREATE TABLE Pacientes(
    PacienteID int PRIMARY KEY IDENTITY(1,1),
    NombrePaciente varchar(50) NOT NULL,
    ApellidoPaciente varchar(60) NOT NULL,
    ApellidoDeCasada varchar(60) NULL,
    FechaNacimientoPaciente date NOT NULL,
    SexoID int NOT NULL,
    EstadoCivilID int NOT NULL,
    DUIPaciente varchar(10) UNIQUE NOT NULL,
    TelefonoPaciente varchar(15) NOT NULL,
    CorreoPaciente varchar(100) UNIQUE NOT NULL,
    DepartamentosID int NOT NULL,
    MunicipioID int NOT NULL,
    EstadoID int NOT NULL,
    FOREIGN KEY (SexoID) REFERENCES Sexos(SexoID),
    FOREIGN KEY (EstadoCivilID) REFERENCES EstadosCiviles(EstadoCivilID),
    FOREIGN KEY (DepartamentosID) REFERENCES Departamentos(DepartamentosID),
    FOREIGN KEY (MunicipioID) REFERENCES Municipios(MunicipioID),
    FOREIGN KEY (EstadoID) REFERENCES Estados(EstadoID)
);

-- Insert Pacientes (Ejemplo de datos de prueba)
INSERT INTO Pacientes 
(NombrePaciente, ApellidoPaciente, ApellidoDeCasada, FechaNacimientoPaciente, SexoID, EstadoCivilID, DUIPaciente, TelefonoPaciente, CorreoPaciente, DepartamentosID, MunicipioID, EstadoID) 
VALUES 
('Jonathan Vladimir', 'Ascencio Ramos', '-', '2003-09-24', 1, 1, '69874512-3', '6107-8146', 'ascencio3.1417@gmail.com', 6, 1, 1);


INSERT INTO Pacientes 
(NombrePaciente, ApellidoPaciente, ApellidoDeCasada, FechaNacimientoPaciente, SexoID, EstadoCivilID, DUIPaciente, TelefonoPaciente, CorreoPaciente, DepartamentosID, MunicipioID, EstadoID) 
VALUES 
('Alexander Eduardo', 'De Leon', '', '2004-06-12', 1, 1, '69874500-0', '6074-0646', 'alexdeleon@gmail.com', 7, 120, 1);


INSERT INTO Pacientes 
(NombrePaciente, ApellidoPaciente, ApellidoDeCasada, FechaNacimientoPaciente, SexoID, EstadoCivilID, DUIPaciente, TelefonoPaciente, CorreoPaciente, DepartamentosID, MunicipioID, EstadoID) 
VALUES 
('Armando Jose', 'Palacios Vivas', '', '2003-10-17', 1, 1, '45004589-7', '7741-6078', 'armandojose@gmail.com', 4, 45, 1);


-- Tabla Especialidades
CREATE TABLE Especialidades (
    EspecialidadID int PRIMARY KEY IDENTITY(1,1),
    NombreEspecialidad varchar(50) NOT NULL
);


-- Insert Especialidades
INSERT INTO Especialidades (NombreEspecialidad) VALUES
('Cardiologo'), ('Pediatra'), ('General');


-- Tabla de Horario Médico
CREATE TABLE HorarioMedico (
    HorarioID INT PRIMARY KEY IDENTITY(1,1),
    HoraInicio TIME NOT NULL,
    HoraFin TIME NOT NULL
);


INSERT INTO HorarioMedico (HoraInicio, HoraFin) VALUES ('8:00','12:35');


	-- Tabla de Días de la Semana
CREATE TABLE DiasSemana (
    DiaID INT PRIMARY KEY IDENTITY(1,1),
    NombreDia VARCHAR(10) NOT NULL
);

-- Insertar los días de la semana
INSERT INTO DiasSemana (NombreDia) VALUES 
('Lunes'), ('Martes'), ('Miércoles'), ('Jueves'), ('Viernes'), ('Sábado'), ('Domingo');

select * from DiasSemana


-- Tabla de Relación entre Horario y Días
CREATE TABLE HorarioDias (
    HorarioID INT NOT NULL,
    DiaID INT NOT NULL,
    FOREIGN KEY (HorarioID) REFERENCES HorarioMedico(HorarioID),
    FOREIGN KEY (DiaID) REFERENCES DiasSemana(DiaID),
    PRIMARY KEY (HorarioID, DiaID)
);

-- Tabla Medicos
CREATE TABLE Medicos (
    MedicoID int PRIMARY KEY IDENTITY(1,1),
    NombreMedico varchar(50) NOT NULL,
    ApellidoMedico varchar(50) NOT NULL,
    ApellidoMedicoCasada varchar(60) NULL,
    FechaNacimientoMedico date NOT NULL,
    TelefonoMedico varchar(15) NOT NULL,
    DepartamentosID int NOT NULL,
    MunicipioID int NOT NULL,
    CorreoMedico varchar(100) UNIQUE NOT NULL,
    DUIMedico varchar(10) UNIQUE NOT NULL,
    SexoID int NOT NULL,
    EstadoCivilID int NOT NULL,
    EspecialidadID int NOT NULL,
    HorarioID int NOT NULL,
	DiaID int NOT NULL,
    EstadoID int NOT NULL,
    FOREIGN KEY (SexoID) REFERENCES Sexos(SexoID),
    FOREIGN KEY (EstadoCivilID) REFERENCES EstadosCiviles(EstadoCivilID),
    FOREIGN KEY (EspecialidadID) REFERENCES Especialidades(EspecialidadID),
    FOREIGN KEY (DepartamentosID) REFERENCES Departamentos(DepartamentosID),
    FOREIGN KEY (HorarioID) REFERENCES HorarioMedico(HorarioID),
    FOREIGN KEY (EstadoID) REFERENCES Estados(EstadoID),
	FOREIGN KEY (MunicipioID) REFERENCES Municipios(MunicipioID),
	FOREIGN KEY (DiaID) REFERENCES DiasSemana(DiaID),
);

select * from Medicos


select * from Especialidades

-- Insert Medicos
INSERT INTO Medicos (NombreMedico, ApellidoMedico, ApellidoMedicoCasada, FechaNacimientoMedico, TelefonoMedico, DepartamentosID, MunicipioID,CorreoMedico, DUIMedico, SexoID, EstadoCivilID, EspecialidadID, HorarioID, DiaID ,EstadoID)
VALUES
('Jonathan Vladimir', 'Ascencio Ramos', '', '1981-05-14', '6107-8146', 6, 45,'jonathan@hospital.com', '06582560-1', 1, 1, 1, 1, 1, 1);

INSERT INTO Medicos (NombreMedico, ApellidoMedico, ApellidoMedicoCasada, FechaNacimientoMedico, TelefonoMedico, DepartamentosID, MunicipioID,CorreoMedico, DUIMedico, SexoID, EstadoCivilID, EspecialidadID, HorarioID, DiaID ,EstadoID)
VALUES
('Ruth Abigail', 'Vaquerano Melara', '', '2000-05-12', '7012-8146', 12, 212,'ruthabi@hospital.com', '78945127-2', 2, 1, 2, 1, 2, 1);


SELECT * FROM Medicos


-- Tabla para los Estados de las Citas
CREATE TABLE EstadoCita(
	EstadoCitaID int primary key identity(1,1),
	CitaEstado varchar(10) unique not null check (CitaEstado in ('Agendada', 'Cancelada'))
);

INSERT INTO EstadoCita (CitaEstado) VALUES ('Agendada'), ('Cancelada');

SELECT * FROM EstadoCita

-- Tabla de Citas
CREATE TABLE Citas(
	CitaID int primary key identity(1,1),
	PacienteID int not null,
	MedicoID int not null,
	EstadoCitaID int not null,
	FechaCita date not null,
	HoraCita time not null,
	EspecialidadID int not null,
	Foreign key (PacienteID) references Pacientes(PacienteID),
	Foreign key (MedicoID) references Medicos(MedicoID),
	Foreign key (EspecialidadID) references Especialidades(EspecialidadID),
	foreign key (EstadoCitaID) references EstadoCita(EstadoCitaID)
);

INSERT INTO Citas (PacienteID, MedicoID, EstadoCitaID, FechaCita, HoraCita, EspecialidadID)
VALUES 
(1, 1, 1, '2024-10-01', '09:00', 1);

-- Tabla de Consultas Medicas
CREATE TABLE ConsultasMedicas(
	ConsultaID int primary key identity(1,1),
	CitaID int not null,
	PacienteID int not null,
	MedicoID int not null,
	Altura decimal(5,2) not null,
	Peso decimal(5,2) not null,
	Alergia varchar(255) not null,
	Sintomas varchar(255) not null,
	Diagnostico text not null,
	Observaciones text not null,
	FechaConsulta date not null,
	Foreign key (CitaID) references Citas(CitaID),
	Foreign key (PacienteID) references Pacientes(PacienteID),
	foreign key (MedicoID) references Medicos(MedicoID)
);

INSERT INTO ConsultasMedicas (CitaID, PacienteID, MedicoID, Altura, Peso, Alergia, Sintomas, Diagnostico, Observaciones, FechaConsulta)
VALUES 
(1, 1, 1, 1.75, 70.5, 'Ninguna', 'Dolor de cabeza, fiebre', 'Gripe común', 'Recomendar reposo y mucha hidratación', '2024-10-01');

INSERT INTO ConsultasMedicas (CitaID, PacienteID, MedicoID, Altura, Peso, Alergia, Sintomas, Diagnostico, Observaciones, FechaConsulta)
VALUES 
(3, 4, 2, 1.75, 65.6, 'A las put*s', 'Dolor de cabeza, fiebre y estar hot las 24/7', 'Necesita ser deslechado', 'Recomendar reposo, mucha hidratación, un hielerazo del bunnnys',
'2024-11-08');

SELECT * FROM Citas
SELECT * FROM ConsultasMedicas

-- Tabla de Recetas Médicas
CREATE TABLE Recetas(
	RecetaID int primary key identity(1,1),
	PacienteID int not null,
	MedicoID int not null,
	FechaEmision datetime not null,
	ConsultaID int not null,
	Medicamento varchar(100) not null,
	Dosis varchar(50) not null, 
	Frecuencia text not null,
	Duracion text not null,
	Instrucciones text not null,
	foreign key (ConsultaID) references ConsultasMedicas(ConsultaID),
	foreign key (PacienteID) references Pacientes(PacienteID),
	foreign key (MedicoID) references Medicos(MedicoID)
);

INSERT INTO Recetas (PacienteID, MedicoID, FechaEmision, ConsultaID, Medicamento, Dosis, Frecuencia, Duracion, Instrucciones)
VALUES 
(1, 1, '2024-11-02', 1, 'Inyeccion', '50 mg', 'Cada 2 dias', '3 meses', 'Aplicar despues de almuerzo.');

SELECT * FROM ConsultasMedicas
SELECT * FROM Medicos

-- Tabla de Detalle de Recetas
CREATE TABLE DetalleReceta (
    IdDetalleReceta INT PRIMARY KEY IDENTITY(1,1),
    RecetaID INT NOT NULL,
    FOREIGN KEY (RecetaID) REFERENCES Recetas(RecetaID)
);

-- Tabla de Exámenes Médicos
CREATE TABLE ExamenesMedicos(
	ExamenID int primary key identity(1,1),
	PacienteID int not null,
	ConsultaID int null,
	TipoExamen varchar(100) not null,
	FechaExamen date not null,
	Resultado varchar(255) not null,
	Observaciones varchar(255) not null,
	Foreign key (PacienteID) references Pacientes(PacienteID),
	Foreign key (ConsultaID) references ConsultasMedicas(ConsultaID) on delete set null
);



GO
---PROCEDIMIENTOS ALMACENADOS CRUD PACIENTE---

---MOSTRAR PACIENTE
CREATE PROCEDURE MostrarPacientes
AS
BEGIN
    SELECT 
        PacienteID,
        NombrePaciente,
        ApellidoPaciente,
        ApellidoDeCasada,
        FechaNacimientoPaciente,
        S.DescripcionSexo AS Sexos, 
        EC.DescripcionEstadoCivil AS EstadoCivil, 
        DUIPaciente,
        TelefonoPaciente,
        CorreoPaciente,
        D.Depar AS Departamentos,
        M.NombreMunicipio AS Municipio,
        E.Estado AS Estados 
    FROM 
        Pacientes P
        LEFT JOIN Sexos S ON P.SexoID = S.SexoID
        LEFT JOIN Departamentos D ON P.DepartamentosID = D.DepartamentosID
        LEFT JOIN Municipios M ON P.MunicipioID = M.MunicipioID
        LEFT JOIN EstadosCiviles EC ON P.EstadoCivilID = EC.EstadoCivilID
        LEFT JOIN Estados E ON P.EstadoID = E.EstadoID;
END;


GO
---INSERTAR PACIENTE
CREATE PROCEDURE InsertarPaciente
    @NombrePaciente VARCHAR(50),
    @ApellidoPaciente VARCHAR(60),
    @ApellidoDeCasada VARCHAR(60),
    @FechaNacimiento DATE,
    @SexoID INT,
    @EstadoCivilID INT,
    @DUIPaciente VARCHAR(10),
    @TelefonoPaciente VARCHAR(15),
    @CorreoPaciente VARCHAR(100),
    @DepartamentosID INT,
    @MunicipioID INT,
    @EstadoID INT
AS
BEGIN
    INSERT INTO Pacientes 
    (NombrePaciente, ApellidoPaciente, ApellidoDeCasada, FechaNacimientoPaciente, SexoID, EstadoCivilID, DUIPaciente, TelefonoPaciente, CorreoPaciente, DepartamentosID, MunicipioID, EstadoID)
    VALUES 
    (@NombrePaciente, @ApellidoPaciente, @ApellidoDeCasada, @FechaNacimiento, @SexoID, @EstadoCivilID, @DUIPaciente, @TelefonoPaciente, @CorreoPaciente, @DepartamentosID, @MunicipioID, @EstadoID);
    SELECT SCOPE_IDENTITY() AS NuevoPacienteID;
END;

GO
---EDITAR PACIENTE
CREATE PROCEDURE EditarPaciente
    @PacienteID INT,
    @NombrePaciente VARCHAR(50),
    @ApellidoPaciente VARCHAR(60),
    @ApellidoDeCasada VARCHAR(60),
    @FechaNacimientoPaciente DATE,
    @SexoID INT,
    @EstadoCivilID INT,
    @DUIPaciente VARCHAR(10),
    @TelefonoPaciente VARCHAR(15),
    @CorreoPaciente VARCHAR(100),
    @DepartamentosID INT,
    @MunicipioID INT,
    @EstadoID INT
AS
BEGIN
    UPDATE Pacientes
    SET 
        NombrePaciente = @NombrePaciente,
        ApellidoPaciente = @ApellidoPaciente,
        ApellidoDeCasada = @ApellidoDeCasada,
        FechaNacimientoPaciente = @FechaNacimientoPaciente,
        SexoID = @SexoID,
        EstadoCivilID = @EstadoCivilID,
        DUIPaciente = @DUIPaciente,
        TelefonoPaciente = @TelefonoPaciente,
        CorreoPaciente = @CorreoPaciente,
        DepartamentosID = @DepartamentosID,
        MunicipioID = @MunicipioID,
        EstadoID = @EstadoID 
    WHERE 
        PacienteID = @PacienteID;
    SELECT 'Paciente actualizado exitosamente.' AS Mensaje;
END;

GO
---ELIMINAR PACIENTE (OCULTARLO)
CREATE PROCEDURE EliminarPaciente
    @PacienteID INT
AS
BEGIN
    UPDATE Pacientes
    SET EstadoID = 2
    WHERE PacienteID = @PacienteID;
    SELECT 'Paciente desactivado exitosamente.' AS Mensaje;
END;

GO
---BUSCAR PACIENTE POR DUI
CREATE PROCEDURE BuscarPacientePorDUI
    @DUI VARCHAR(10)
AS
BEGIN
    SELECT 
        PacienteID,
        NombrePaciente,
        ApellidoPaciente,
        ApellidoDeCasada,
        FechaNacimientoPaciente,
        S.DescripcionSexo AS Sexos, 
        EC.DescripcionEstadoCivil AS EstadoCivil, 
        DUIPaciente,
        TelefonoPaciente,
        CorreoPaciente,
        D.Depar AS Departamentos, 
        M.NombreMunicipio AS Municipio,
        E.Estado AS Estados 
    FROM 
        Pacientes P
        LEFT JOIN Sexos S ON P.SexoID = S.SexoID 
        LEFT JOIN Departamentos D ON P.DepartamentosID = D.DepartamentosID 
        LEFT JOIN Municipios M ON P.MunicipioID = M.MunicipioID
        LEFT JOIN EstadosCiviles EC ON P.EstadoCivilID = EC.EstadoCivilID 
        LEFT JOIN Estados E ON P.EstadoID = E.EstadoID 
    WHERE 
        DUIPaciente = @DUI;
END;



---PROCEDIMIENTOS ALMACENADOS CRUD MEDICO---

GO
---MOSTRAR MEDICO
CREATE PROCEDURE MostrarMedicos
AS
BEGIN
    SELECT 
        M.MedicoID, 
        M.NombreMedico, 
        M.ApellidoMedico, 
        M.ApellidoMedicoCasada, 
        M.FechaNacimientoMedico, 
        M.TelefonoMedico, 
        D.Depar AS Departamento,
        MN.NombreMunicipio AS Municipio,
        M.CorreoMedico, 
        M.DUIMedico, 
        S.DescripcionSexo AS Sexo,  
        EC.DescripcionEstadoCivil AS EstadoCivil, 
        ES.NombreEspecialidad AS Especialidad, 
        E.Estado AS Estado,
		DS.NombreDia AS Dias,
        HM.HoraInicio, 
        HM.HoraFin
        
    FROM
        Medicos M
        LEFT JOIN Sexos S ON M.SexoID = S.SexoID
        LEFT JOIN Departamentos D ON M.DepartamentosID = D.DepartamentosID
        LEFT JOIN Municipios MN ON M.MunicipioID = MN.MunicipioID
        LEFT JOIN EstadosCiviles EC ON M.EstadoCivilID = EC.EstadoCivilID
        LEFT JOIN HorarioMedico HM ON M.HorarioID = HM.HorarioID
        LEFT JOIN Estados E ON M.EstadoID = E.EstadoID
		LEFT JOIN DiasSemana DS ON M.DiaID = DS.DiaID
        LEFT JOIN Especialidades ES ON M.EspecialidadID = ES.EspecialidadID;
END;
EXEC MostrarMedicos


GO

GO
---INSERTAR MEDICO
CREATE PROCEDURE InsertarMedico
    @NombreMedico VARCHAR(50),
    @ApellidoMedico VARCHAR(50),
    @ApellidoMedicoCasada VARCHAR(60) = NULL,
    @FechaNacimientoMedico DATE,
    @TelefonoMedico VARCHAR(15),
    @DepartamentosID INT,
    @MunicipioID INT,
    @CorreoMedico VARCHAR(100),
    @DUIMedico VARCHAR(10),
    @SexoID INT,
    @EstadoCivilID INT,
    @HoraInicio TIME,
    @HoraFin TIME,
    @DiaID INT, -- El día ahora puede ser NULL
    @EspecialidadID INT,
    @EstadoID INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @HorarioID INT;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Insertar el horario médico
        INSERT INTO HorarioMedico (HoraInicio, HoraFin)
        VALUES (@HoraInicio, @HoraFin);
        
        -- Obtener el ID del horario insertado
        SET @HorarioID = SCOPE_IDENTITY();

        -- Insertar el día de la semana en la tabla HorarioDias solo si @DiaID no es NULL
        IF @DiaID IS NOT NULL
        BEGIN
            INSERT INTO HorarioDias (HorarioID, DiaID)
            VALUES (@HorarioID, @DiaID);
        END

        -- Insertar el médico con el HorarioID generado
        INSERT INTO Medicos 
        (NombreMedico, ApellidoMedico, ApellidoMedicoCasada, FechaNacimientoMedico, 
         TelefonoMedico, DepartamentosID, MunicipioID, CorreoMedico, DUIMedico, 
         SexoID, EstadoCivilID, EspecialidadID,EstadoID,DiaID,HorarioID)
        VALUES 
        (@NombreMedico, @ApellidoMedico, @ApellidoMedicoCasada, @FechaNacimientoMedico, 
         @TelefonoMedico, @DepartamentosID, @MunicipioID, @CorreoMedico, @DUIMedico, 
         @SexoID, @EstadoCivilID, @EspecialidadID,@EstadoID,@DiaID,@HorarioID)

        -- Confirmar la transacción
        COMMIT TRANSACTION;
        
        -- Devolver el ID del médico recién insertado
        SELECT SCOPE_IDENTITY() AS MedicoID;
    END TRY
    BEGIN CATCH
        -- Revertir transacción en caso de error
        ROLLBACK TRANSACTION;
        THROW; -- Lanza el error para verlo en la aplicación
    END CATCH;
END;


-- Datos de Prueba
EXEC InsertarMedico
    @NombreMedico = 'Gabriela Ines',
    @ApellidoMedico = 'Pineda',
    @ApellidoMedicoCasada = '',
    @FechaNacimientoMedico = '2008-11-21',
    @TelefonoMedico = '555-6770',
    @DepartamentosID = 5,
    @MunicipioID = 12,
    @CorreoMedico = 'gaby@hospiplus.com',
    @DUIMedico = '87894777-5',
    @SexoID = 2,
    @EstadoCivilID = 1,
    @HoraInicio = '05:00:00',
    @HoraFin = '15:00:00',
    @DiaID = 7,  
    @EspecialidadID = 1,
    @EstadoID = 1;


select * from Medicos


GO
---EDITAR MEDICO
CREATE PROCEDURE EditarMedico
    @MedicoID INT,
    @NombreMedico VARCHAR(50),
    @ApellidoMedico VARCHAR(50),
    @ApellidoMedicoCasada VARCHAR(60) = NULL,
    @FechaNacimientoMedico DATE,
    @TelefonoMedico VARCHAR(15),
    @DepartamentosID INT,
    @MunicipioID INT,
    @CorreoMedico VARCHAR(100),
    @DUIMedico VARCHAR(10),
    @SexoID INT,
    @EstadoCivilID INT,
    @HoraInicio TIME,
    @HoraFin TIME,
    @DiaID INT, -- ID del día a agregar
    @EspecialidadID INT,
    @EstadoID INT
AS
BEGIN
    DECLARE @HorarioID INT;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Obtener el ID de Horario actual del médico
        SELECT @HorarioID = HorarioID FROM Medicos WHERE MedicoID = @MedicoID;

        -- Actualizar horario médico
        UPDATE HorarioMedico
        SET HoraInicio = @HoraInicio, HoraFin = @HoraFin
        WHERE HorarioID = @HorarioID;

        -- Borrar días de la semana existentes en el horario
        DELETE FROM HorarioDias WHERE HorarioID = @HorarioID;

        -- Insertar el nuevo día de la semana si @DiaID no es NULL
        IF @DiaID IS NOT NULL
        BEGIN
            INSERT INTO HorarioDias (HorarioID, DiaID)
            VALUES (@HorarioID, @DiaID);
        END

        -- Actualizar los demás datos del médico
        UPDATE Medicos 
        SET 
            NombreMedico = @NombreMedico, 
            ApellidoMedico = @ApellidoMedico, 
            ApellidoMedicoCasada = @ApellidoMedicoCasada, 
            FechaNacimientoMedico = @FechaNacimientoMedico, 
            TelefonoMedico = @TelefonoMedico, 
            DepartamentosID = @DepartamentosID, 
            MunicipioID = @MunicipioID, 
            CorreoMedico = @CorreoMedico, 
            DUIMedico = @DUIMedico, 
            SexoID = @SexoID, 
            EstadoCivilID = @EstadoCivilID, 
			DiaID = @DiaID,
            EspecialidadID = @EspecialidadID, 
            EstadoID = @EstadoID
        WHERE MedicoID = @MedicoID;

        COMMIT TRANSACTION;
        SELECT @MedicoID AS MedicoID;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT 'Error al actualizar el médico: ' + ERROR_MESSAGE();
    END CATCH;
END;



GO

GO
---ELIMINAR MEDICO(OCULTARLO)
CREATE PROCEDURE EliminarMedico
    @MedicoID INT
AS
BEGIN
    UPDATE Medicos
    SET EstadoID = 2
    WHERE MedicoID = @MedicoID;
    PRINT 'El médico ha sido eliminado correctamente.';
END;

GO

GO
---PROCEDIMIENTOS PARA MOSTRAR MEDICOS CON SUS HORARIOS
CREATE PROCEDURE MostrarMedicosConHorarios
AS
BEGIN
    SELECT 
        M.MedicoID,
        M.NombreMedico,
        M.ApellidoMedico,
        M.ApellidoMedicoCasada,
        M.FechaNacimientoMedico,
        M.TelefonoMedico,
        D.Depar AS Departamento,
        MN.NombreMunicipio AS Municipio,
        M.CorreoMedico,
        M.DUIMedico,
        S.DescripcionSexo AS Sexo,
        EC.DescripcionEstadoCivil AS EstadoCivil,
        ES.NombreEspecialidad AS Especialidad,
        E.Estado AS Estado,
        HM.HoraInicio,
        HM.HoraFin,
        STUFF(
            (SELECT ', ' + DS.NombreDia
             FROM HorarioDias HD
             JOIN DiasSemana DS ON HD.DiaID = DS.DiaID
             WHERE HD.HorarioID = M.HorarioID
             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ''
        ) AS DiasSemana
    FROM
        Medicos M
    LEFT JOIN Sexos S ON M.SexoID = S.SexoID
    LEFT JOIN Departamentos D ON M.DepartamentosID = D.DepartamentosID
    LEFT JOIN Municipios MN ON M.MunicipioID = MN.MunicipioID
    LEFT JOIN EstadosCiviles EC ON M.EstadoCivilID = EC.EstadoCivilID
    LEFT JOIN HorarioMedico HM ON M.HorarioID = HM.HorarioID
    LEFT JOIN Estados E ON M.EstadoID = E.EstadoID
    LEFT JOIN Especialidades ES ON M.EspecialidadID = ES.EspecialidadID;
END;

GO
---alterando el procedimiento
ALTER PROCEDURE MostrarMedicosConHorarios
AS
BEGIN
    SELECT 
        M.MedicoID,
        M.NombreMedico,
        M.ApellidoMedico,
        M.ApellidoMedicoCasada,
        E.NombreEspecialidad AS Especialidad,
        HM.HoraInicio,
        HM.HoraFin,
        DS.NombreDia AS Dia,
        ES.Estado AS Estado
    FROM
        Medicos M
        LEFT JOIN Especialidades E ON M.EspecialidadID = E.EspecialidadID
        LEFT JOIN HorarioMedico HM ON M.HorarioID = HM.HorarioID
        LEFT JOIN DiasSemana DS ON M.DiaID = DS.DiaID
        LEFT JOIN Estados ES ON M.EstadoID = ES.EstadoID;
END;




EXEC MostrarMedicosConHorarios;
GO
---PROCEDIMIENTO PARA MOSTRAR MEDICOS POR ESPECIALIDAD
CREATE PROCEDURE MostrarMedicosPorEspecialidad
    @EspecialidadID INT
AS
BEGIN
    SELECT 
        M.MedicoID,
        M.NombreMedico,
        M.ApellidoMedico,
        M.ApellidoMedicoCasada,
        M.TelefonoMedico,
        M.CorreoMedico,
        E.NombreEspecialidad AS Especialidad,
        EST.Estado AS Estado
    FROM 
        Medicos M
    JOIN 
        Especialidades E ON M.EspecialidadID = E.EspecialidadID
    JOIN 
        Estados EST ON M.EstadoID = EST.EstadoID
    WHERE 
        M.EspecialidadID = @EspecialidadID;
END;

GO
---ALTERANDO EL PROCEDIMIENTO DE MOSTRAR MEDICOS POR ESPECIALIDAD
ALTER PROCEDURE MostrarMedicosPorEspecialidad
    @EspecialidadID INT
AS
BEGIN
    SELECT 
        M.MedicoID,
        M.NombreMedico,
        M.ApellidoMedico,
        M.ApellidoMedicoCasada,
        ES.NombreEspecialidad AS Especialidad,
        HM.HoraInicio,
        HM.HoraFin,
        DS.NombreDia AS Dia,
        E.Estado AS Estado
    FROM 
        Medicos M
    LEFT JOIN Especialidades ES ON M.EspecialidadID = ES.EspecialidadID
    LEFT JOIN HorarioMedico HM ON M.HorarioID = HM.HorarioID
    LEFT JOIN DiasSemana DS ON M.DiaID = DS.DiaID
    LEFT JOIN Estados E ON M.EstadoID = E.EstadoID
    WHERE 
        M.EspecialidadID = @EspecialidadID;
END;


EXEC MostrarMedicosPorEspecialidad @EspecialidadID = 2;

---PROCEDIMIENTOS ALMACENADOS CRUD CITAS---

GO
---MOSTRAR CITAS
CREATE PROCEDURE MostrarCitas
    @PacienteID INT = NULL,
    @MedicoID INT = NULL,
    @EspecialidadID INT = NULL,
    @EstadoCitaID INT = NULL,
    @FechaCita DATE = NULL
AS
BEGIN
    SELECT 
        C.CitaID,
        P.NombrePaciente + ' ' + P.ApellidoPaciente AS Paciente,
        P.DuiPaciente AS DUIPaciente,
        M.NombreMedico + ' ' + M.ApellidoMedico AS Medico,
        E.NombreEspecialidad AS Especialidad,
        C.FechaCita,
        C.HoraCita,
        EC.CitaEstado AS EstadoCita
    FROM 
        Citas C
        JOIN Pacientes P ON C.PacienteID = P.PacienteID
        JOIN Medicos M ON C.MedicoID = M.MedicoID
        JOIN Especialidades E ON C.EspecialidadID = E.EspecialidadID
        JOIN EstadoCita EC ON C.EstadoCitaID = EC.EstadoCitaID
    WHERE 
        (@PacienteID IS NULL OR C.PacienteID = @PacienteID) AND
        (@MedicoID IS NULL OR C.MedicoID = @MedicoID) AND
        (@EspecialidadID IS NULL OR C.EspecialidadID = @EspecialidadID) AND
        (@EstadoCitaID IS NULL OR C.EstadoCitaID = @EstadoCitaID) AND
        (@FechaCita IS NULL OR C.FechaCita = @FechaCita);
END;

GO
---BUSCAR CITA POR DUI
CREATE PROCEDURE BuscarCitasPorDUI
    @DuiPaciente NVARCHAR(10)
AS
BEGIN
    SELECT 
        C.CitaID,
        P.NombrePaciente + ' ' + P.ApellidoPaciente AS Paciente,
        P.DUIPaciente,
        M.NombreMedico + ' ' + M.ApellidoMedico AS Medico,
        E.NombreEspecialidad AS Especialidad,
        C.FechaCita,
        C.HoraCita,
        EC.CitaEstado AS EstadoCita
    FROM 
        Citas C
        JOIN Pacientes P ON C.PacienteID = P.PacienteID
        JOIN Medicos M ON C.MedicoID = M.MedicoID
        JOIN Especialidades E ON C.EspecialidadID = E.EspecialidadID
        JOIN EstadoCita EC ON C.EstadoCitaID = EC.EstadoCitaID
    WHERE 
        P.DUIPaciente = @DuiPaciente;
END;

select * from Citas	
SELECT * FROM EstadoCita
SELECT * FROM Especialidades

GO
--AGENDAR NUEVA CITA
CREATE PROCEDURE AgendarNuevaCita
    @DuiPaciente NVARCHAR(10),
    @MedicoID INT,
    @EstadoCitaID INT,
    @FechaCita DATE,
    @HoraCita TIME,
    @EspecialidadID INT
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Obtener el ID del paciente basado en el DUI
        DECLARE @PacienteID INT;
        SELECT @PacienteID = PacienteID 
        FROM Pacientes 
        WHERE DUIPaciente = @DuiPaciente;

        -- Verificar si el PacienteID es válido
        IF @PacienteID IS NULL
        BEGIN
            RAISERROR('El paciente con el DUI especificado no existe.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Insertar la nueva cita
        INSERT INTO Citas (PacienteID, MedicoID, EstadoCitaID, FechaCita, HoraCita, EspecialidadID)
        VALUES (@PacienteID, @MedicoID, @EstadoCitaID, @FechaCita, @HoraCita, @EspecialidadID);

        -- Confirmar la transacción
        COMMIT TRANSACTION;

        PRINT 'La cita ha sido agendada correctamente.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT 'Ocurrió un error al agendar la cita: ' + ERROR_MESSAGE();
    END CATCH;
END;


GO
GO
---AGENDAR CITA
CREATE PROCEDURE AgendarCita
    @PacienteID INT,
    @MedicoID INT,
    @EspecialidadID INT,
    @FechaCita DATE,
    @HoraCita TIME,
    @EstadoCitaID INT
AS
BEGIN
    DECLARE @CitasPorDia INT;

    SELECT @CitasPorDia = COUNT(*)
    FROM Citas
    WHERE MedicoID = @MedicoID AND CAST(FechaCita AS DATE) = CAST(@FechaCita AS DATE);

    IF @CitasPorDia >= 5
    BEGIN
        RAISERROR('El médico ya tiene el límite de citas para el día.', 16, 1);
        RETURN;
    END

    INSERT INTO Citas (PacienteID, MedicoID, EspecialidadID, FechaCita, HoraCita, EstadoCitaID)
    VALUES (@PacienteID, @MedicoID, @EspecialidadID, @FechaCita, @HoraCita, @EstadoCitaID);
    PRINT 'La cita ha sido agendada correctamente.';
END;

GO
---EDITAR CITA
CREATE PROCEDURE EditarCita
    @CitaID INT,
    @MedicoID INT,
    @FechaCita DATE,
    @HoraCita TIME
AS
BEGIN
    UPDATE Citas
    SET MedicoID = @MedicoID, FechaCita = @FechaCita, HoraCita = @HoraCita
    WHERE CitaID = @CitaID;
    PRINT 'La cita ha sido actualizada correctamente.';
END;

GO
---MODIFICAR CITA
CREATE PROCEDURE ModificarCita
    @CitaID INT,
    @FechaCita DATE = NULL,
    @HoraCita TIME = NULL,
    @MedicoID INT = NULL,
    @EspecialidadID INT = NULL
AS
BEGIN
    UPDATE Citas
    SET 
        FechaCita = ISNULL(@FechaCita, FechaCita),
        HoraCita = ISNULL(@HoraCita, HoraCita),
        MedicoID = ISNULL(@MedicoID, MedicoID),
        EspecialidadID = ISNULL(@EspecialidadID, EspecialidadID)
    WHERE CitaID = @CitaID;
    PRINT 'La cita ha sido actualizada correctamente.';
END;

GO
---ELIMINAR CITA (CANCELAR)
CREATE PROCEDURE EliminarCita
    @CitaID INT
AS
BEGIN
    UPDATE Citas
    SET EstadoCitaID = 2
    WHERE CitaID = @CitaID;
    PRINT 'La cita ha sido cancelada correctamente.';
END;

---PROCEDIMIENTOS ALMACENADOS CRUD CONSULTAS MEDICAS---

GO
---MOSTRAR CONSULTA MEDICA
CREATE PROCEDURE MostrarConsultas
AS
BEGIN
    SELECT 
        c.ConsultaID,
        c.CitaID,
        CONCAT(p.NombrePaciente, ' ', p.ApellidoPaciente) AS NombreCompletoPaciente,
        CONCAT(m.NombreMedico, ' ', m.ApellidoMedico) AS NombreCompletoMedico,
        e.NombreEspecialidad AS EspecialidadMedico,
        c.Altura,
        c.Peso,
        c.Alergia,
        c.Sintomas,
        c.Diagnostico,
        c.Observaciones,
        c.FechaConsulta,
        cit.FechaCita,
        est.CitaEstado AS EstadoCita
    FROM 
        ConsultasMedicas c
        JOIN Pacientes p ON c.PacienteID = p.PacienteID
        JOIN Medicos m ON c.MedicoID = m.MedicoID
        JOIN Especialidades e ON m.EspecialidadID = e.EspecialidadID
        JOIN Citas cit ON c.CitaID = cit.CitaID
        JOIN EstadoCita est ON cit.EstadoCitaID = est.EstadoCitaID;
END;

GO
--------------------ALTERANDO MOSTRAR CONSULTAS--------------------------------------------------------------------------
ALTER PROCEDURE MostrarConsultas
AS
BEGIN
    SELECT 
        c.ConsultaID,
        c.CitaID,
		p.PacienteID, -- Añadido el ID del paciente
        CONCAT(p.NombrePaciente, ' ', p.ApellidoPaciente) AS NombreCompletoPaciente,
        CONCAT(m.NombreMedico, ' ', m.ApellidoMedico) AS NombreCompletoMedico,
        e.NombreEspecialidad AS EspecialidadMedico,
        c.Altura,
        c.Peso,
        c.Alergia,
        c.Sintomas,
        c.Diagnostico,
        c.Observaciones,
        c.FechaConsulta,
        cit.FechaCita,
        est.CitaEstado AS EstadoCita
    FROM 
        ConsultasMedicas c
        JOIN Pacientes p ON c.PacienteID = p.PacienteID
        JOIN Medicos m ON c.MedicoID = m.MedicoID
        JOIN Especialidades e ON m.EspecialidadID = e.EspecialidadID
        JOIN Citas cit ON c.CitaID = cit.CitaID
        JOIN EstadoCita est ON cit.EstadoCitaID = est.EstadoCitaID;
END;

EXEC MostrarConsultas;
select * from Citas
select * from ConsultasMedicas
GO
---BUSCAR CONSULTA POR FECHA
CREATE PROCEDURE BuscarConsultasPorFecha
    @FechaConsulta DATE
AS
BEGIN
    SELECT 
        c.ConsultaID,
        c.CitaID,
        CONCAT(p.NombrePaciente, ' ', p.ApellidoPaciente) AS NombreCompletoPaciente,
        CONCAT(m.NombreMedico, ' ', m.ApellidoMedico) AS NombreCompletoMedico,
        e.NombreEspecialidad AS EspecialidadMedico,
        c.Altura,
        c.Peso,
        c.Alergia,
        c.Sintomas,
        c.Diagnostico,
        c.Observaciones,
        c.FechaConsulta,
        cit.FechaCita,
        est.CitaEstado AS EstadoCita
    FROM 
        ConsultasMedicas c
        JOIN Pacientes p ON c.PacienteID = p.PacienteID
        JOIN Medicos m ON c.MedicoID = m.MedicoID
        JOIN Especialidades e ON m.EspecialidadID = e.EspecialidadID
        JOIN Citas cit ON c.CitaID = cit.CitaID
        JOIN EstadoCita est ON cit.EstadoCitaID = est.EstadoCitaID
    WHERE c.FechaConsulta = @FechaConsulta;
END;

GO
--------------------ALTERANDO BUSCAR CONSULTAS POR FECHA--------------------------------------------------------------------------
ALTER PROCEDURE BuscarConsultasPorFecha
    @FechaConsulta DATE
AS
BEGIN
    SELECT 
        c.ConsultaID,
        c.CitaID,
        p.PacienteID,
        CONCAT(p.NombrePaciente, ' ', p.ApellidoPaciente) AS NombreCompletoPaciente,
        CONCAT(m.NombreMedico, ' ', m.ApellidoMedico) AS NombreCompletoMedico,
        e.NombreEspecialidad AS EspecialidadMedico,
        c.Altura,
        c.Peso,
        c.Alergia,
        c.Sintomas,
        c.Diagnostico,
        c.Observaciones,
        c.FechaConsulta,
        cit.FechaCita,
        est.CitaEstado AS EstadoCita
    FROM 
        ConsultasMedicas c
        JOIN Pacientes p ON c.PacienteID = p.PacienteID
        JOIN Medicos m ON c.MedicoID = m.MedicoID
        JOIN Especialidades e ON m.EspecialidadID = e.EspecialidadID
        JOIN Citas cit ON c.CitaID = cit.CitaID
        JOIN EstadoCita est ON cit.EstadoCitaID = est.EstadoCitaID
    WHERE c.FechaConsulta = @FechaConsulta;
END;

SELECT * From Medicos
GO
---INGRESAR CONSULTA MEDICA
CREATE PROCEDURE Ingresar_consulta
    @CitaID INT,
    @PacienteID INT,
    @MedicoID INT,
    @Altura DECIMAL(5, 2),
    @Peso DECIMAL(5, 2),
    @Alergia VARCHAR(255),
    @Sintomas VARCHAR(255),
    @Diagnostico TEXT,
    @Observaciones TEXT,
    @FechaConsulta DATE
AS
BEGIN
    INSERT INTO ConsultasMedicas 
    (CitaID, PacienteID, MedicoID, Altura, Peso, Alergia, Sintomas, Diagnostico, Observaciones, FechaConsulta)
    VALUES 
    (@CitaID, @PacienteID, @MedicoID, @Altura, @Peso, @Alergia, @Sintomas, @Diagnostico, @Observaciones, @FechaConsulta);
    SELECT SCOPE_IDENTITY() AS ConsultaID;
END;

---PROCEDIMIENTOS ALMACENADOS CRUD RECETAS---

GO
---MOSTRAR RECETAS
CREATE PROCEDURE MostrarRecetas
AS
BEGIN
    SELECT 
        r.RecetaID,
        p.PacienteID,
        CONCAT(p.NombrePaciente, ' ', p.ApellidoPaciente) AS NombreCompletoPaciente,
        p.DUIPaciente AS DuiPaciente,
        r.MedicoID,
        CONCAT(m.NombreMedico, ' ', m.ApellidoMedico) AS NombreCompletoMedico,
        r.FechaEmision,
        r.ConsultaID,
        r.Medicamento,
        r.Dosis,
        r.Frecuencia,
        r.Duracion,
        r.Instrucciones
    FROM 
        Recetas r
        JOIN Pacientes p ON r.PacienteID = p.PacienteID
        JOIN Medicos m ON r.MedicoID = m.MedicoID;
END;

GO

GO
---BUSCAR RECETAS POR DUI
CREATE PROCEDURE BuscarRecetasPorDUI
    @DuiPaciente VARCHAR(10)
AS
BEGIN
    SELECT 
        r.RecetaID,
        p.PacienteID,
        CONCAT(p.NombrePaciente, ' ', p.ApellidoPaciente) AS NombreCompletoPaciente,
        p.DUIPaciente AS DuiPaciente,
        r.MedicoID,
        CONCAT(m.NombreMedico, ' ', m.ApellidoMedico) AS NombreCompletoMedico,
        r.FechaEmision,
        r.ConsultaID,
        r.Medicamento,
        r.Dosis,
        r.Frecuencia,
        r.Duracion,
        r.Instrucciones
    FROM 
        Recetas r
        JOIN Pacientes p ON r.PacienteID = p.PacienteID
        JOIN Medicos m ON r.MedicoID = m.MedicoID
    WHERE p.DUIPaciente = @DuiPaciente;
END;


---PROCEDIMIENTOS DE REPORTES---

GO
-----PROCEDIMIENTO ALMACENADO PARA CONSULTAS MEDICAS-------------------

CREATE PROCEDURE GenerarReporteConsultasMedicas
    @FechaInicio DATE = NULL,
    @FechaFin DATE = NULL
AS
BEGIN
    -- Si no se proporcionan fechas de inicio o fin, se establece la fecha actual como predeterminada
    IF @FechaInicio IS NULL AND @FechaFin IS NULL
    BEGIN
        SET @FechaInicio = CAST(GETDATE() AS DATE);
        SET @FechaFin = CAST(GETDATE() AS DATE);
    END

    SELECT 
        CONCAT(m.NombreMedico, ' ', m.ApellidoMedico) AS NombreCompletoMedico,
        CONCAT(p.NombrePaciente, ' ', p.ApellidoPaciente) AS NombreCompletoPaciente,
        p.DUIPaciente,
        cm.FechaConsulta,
        c.FechaCita
    FROM 
        ConsultasMedicas cm
        JOIN Medicos m ON cm.MedicoID = m.MedicoID
        JOIN Pacientes p ON cm.PacienteID = p.PacienteID
        JOIN Citas c ON cm.CitaID = c.CitaID
    WHERE 
        cm.FechaConsulta BETWEEN @FechaInicio AND @FechaFin
    ORDER BY 
        cm.FechaConsulta, m.NombreMedico, p.NombrePaciente;
END;

EXEC GenerarReporteConsultasMedicas '2024-11-08', '2024-11-08';
EXEC GenerarReporteConsultasMedicas '2024-10-13', '2024-11-05';
EXEC GenerarReporteConsultasMedicas;

GO

GO
---OBTENER EXPEDIENTE DEL PACIENTE
CREATE PROCEDURE ObtenerExpedientePaciente 
    @PacienteID INT
AS 
BEGIN 
    -- Datos Personales del Paciente
    SELECT 
        P.NombrePaciente,
        P.ApellidoPaciente,
        P.ApellidoDeCasada,
        P.FechaNacimientoPaciente,
        S.DescripcionSexo AS Sexo,
        EC.DescripcionEstadoCivil AS EstadoCivil,
        P.DUIPaciente,
        P.TelefonoPaciente,
        P.CorreoPaciente,
        D.Depar AS Departamento,
        M.NombreMunicipio AS Municipio
    FROM 
        Pacientes P
    LEFT JOIN 
        Sexos S ON P.SexoID = S.SexoID
    LEFT JOIN 
        Departamentos D ON P.DepartamentosID = D.DepartamentosID
    LEFT JOIN 
        Municipios M ON P.MunicipioID = M.MunicipioID
    LEFT JOIN 
        EstadosCiviles EC ON P.EstadoCivilID = EC.EstadoCivilID
    WHERE 
        P.PacienteID = @PacienteID;

    -- Historial de Consultas Médicas del Paciente
    SELECT
        CM.Altura,
        CM.Peso,
        CM.Alergia,
        CM.Sintomas,
        CM.Diagnostico,
        CM.Observaciones,
        C.FechaCita,
        C.HoraCita
    FROM 
        ConsultasMedicas CM
    JOIN 
        Citas C ON CM.CitaID = C.CitaID
    WHERE 
        C.PacienteID = @PacienteID;

    -- Exámenes Médicos del Paciente
    SELECT 
        EM.TipoExamen,
        EM.FechaExamen,
        EM.Resultado,
        EM.Observaciones
    FROM 
        ExamenesMedicos EM
    WHERE 
        EM.PacienteID = @PacienteID;
END;

GO
---OBTENER RECETAS POR PACIENTE Y FECHA
CREATE PROCEDURE ObtenerRecetasPorPacienteYFecha
    @PacienteID INT,
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    SELECT 
        R.Medicamento,
        R.Dosis,
        R.Frecuencia,
        R.Duracion,
        R.Instrucciones,
        C.FechaCita
    FROM Recetas R 
    JOIN ConsultasMedicas CM ON R.ConsultaID = CM.ConsultaID
    JOIN Citas C ON CM.CitaID = C.CitaID
    WHERE CM.PacienteID = @PacienteID
      AND C.FechaCita BETWEEN @FechaInicio AND @FechaFin;
END;

GO
---PROCEDIMIENTO PARA OBTENER LOS DEPARTAMENTOS Y MUNICIPIOS---
CREATE PROCEDURE ObtenerDepartamentos
AS
BEGIN
    SELECT 
        DepartamentosID, 
        Depar AS NombreDepartamento 
    FROM 
        Departamentos;
END;

GO
---OBTENER MUNICIPIO SEGUN EL DEPARTAMENTO SELECCIONADO
CREATE PROCEDURE ObtenerMunicipiosPorDepartamento
    @DepartamentosID INT
AS
BEGIN
    SELECT 
        MunicipioID, 
        NombreMunicipio 
    FROM 
        Municipios 
    WHERE 
        DepartamentosID = @DepartamentosID;
END;




--------PROCEDIMIENTO ALMACENADO PARA USUARIOS / LOGIN / RUTH
---PROCEDIMIENTO PARA VERIFICAR CORREO Y CONTRA---
GO
CREATE PROCEDURE spLogin1
@correo VARCHAR(80),
@pass VARCHAR(60)  -- Contraseña en texto
AS
BEGIN
    SELECT TOP 1 UsuarioID,Rol  -- Devuelve el ID del usuario
    FROM Usuarios
    WHERE Correo = @correo 
    AND CONVERT(VARCHAR(1000), DECRYPTBYPASSPHRASE('hospiplus24', Contrasena)) = @pass;
END
GO


-- Mostrar los usuarios
CREATE PROCEDURE MostrarUsuarios
AS
BEGIN
    SELECT 
        u.UsuarioID,
        u.NombreUsuario,
        u.Correo,
        u.Rol,
        u.Contrasena
    FROM Usuarios u
END;



exec MostrarUsuarios
select * from Usuarios


GO
-- Insertar Usuario
CREATE PROCEDURE InsertarUsuario
    @NombreUsuario VARCHAR(80),
    @Correo VARCHAR(100),
    @ContrasenaTexto VARCHAR(100),
    @Rol NVARCHAR(20)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;  -- Iniciar transacción

        -- Verificar si el correo ya existe
        IF EXISTS (SELECT 1 FROM Usuarios WHERE Correo = @Correo)
        BEGIN
            -- Si el correo existe, devolver 2 (correo duplicado)
            ROLLBACK TRANSACTION;  -- Deshacer la transacción
            RETURN 2;
        END

        -- Convertir la contraseña de texto plano a varbinary (si la columna Contraseña está como varbinary)
        DECLARE @Contrasena VARBINARY(1000);
        SET @Contrasena = ENCRYPTBYPASSPHRASE('hospiplus24', @ContrasenaTexto);  -- Encriptar la contraseña

        -- Insertar el nuevo usuario si el correo no existe
        INSERT INTO Usuarios (NombreUsuario, Correo, Contrasena, Rol)
        VALUES (@NombreUsuario, @Correo, @Contrasena, @Rol);

        -- Confirmar la transacción si todo fue exitoso
        COMMIT TRANSACTION;

        -- Retornar 1 si la inserción fue exitosa
        RETURN 1;

    END TRY
    BEGIN CATCH
        -- Capturar el error y retornar 0
        IF XACT_STATE() <> 0
        BEGIN
            ROLLBACK TRANSACTION;  -- Deshacer la transacción si hubo un error
        END

        -- Mostrar el mensaje de error para depuración
        DECLARE @ErrorMessage NVARCHAR(4000);
        SET @ErrorMessage = ERROR_MESSAGE();
        PRINT @ErrorMessage;  -- Esto imprime el error en el log para depuración
        RETURN 0;
    END CATCH
END;



GO
-- Editar los Usuarios
CREATE PROCEDURE EditarUsuario
    @UsuarioID INT,
    @NombreUsuario VARCHAR(80),
    @Correo VARCHAR(100),
    @ContrasenaTexto VARCHAR(100),
    @Rol VARCHAR(20)
AS
BEGIN
    BEGIN TRY
        -- Verificar si el correo ya existe para otro usuario
        IF EXISTS (SELECT 1 FROM Usuarios WHERE Correo = @Correo AND UsuarioID != @UsuarioID)
        BEGIN
            RETURN 2; -- Retorna 2 si el correo ya está registrado
        END

        -- Cifrar la nueva contraseña
        DECLARE @Contrasena VARBINARY(1000);
        SET @Contrasena = ENCRYPTBYPASSPHRASE('hospiplus24', @ContrasenaTexto);

        -- Editar el usuario
        UPDATE Usuarios
        SET 
            NombreUsuario = @NombreUsuario,
            Correo = @Correo,
            Contrasena = @Contrasena,
            Rol = @Rol
        WHERE UsuarioID = @UsuarioID;

        RETURN 1; -- Retorna 1 si todo es exitoso
    END TRY
    BEGIN CATCH
        RETURN 0; -- Retorna 0 si ocurre un error
    END CATCH
END;


-- Ejemplo

EXEC EditarUsuario
    @UsuarioID = 1,          
    @NombreUsuario = 'Ruth Abigail',  
    @Correo = 'rutha@hospiplus.com',    
    @ContrasenaTexto = 'hospitca', 
    @Rol = 'Administrador';         



select * from Usuarios


GO
--Método para obtener la contra del usuario
CREATE PROCEDURE spObtenerUsuarioContrasenaDesencriptada
    @UsuarioID INT
AS
BEGIN
    SELECT 
        UsuarioID,
        NombreUsuario,
        Correo,
        CONVERT(VARCHAR(1000), DECRYPTBYPASSPHRASE('hospiplus24', Contrasena)) AS ContrasenaDesencriptada
    FROM Usuarios
    WHERE UsuarioID = @UsuarioID;
END;
GO


---Nuevos metodos 
CREATE PROCEDURE MostrarExamenesMedicos
AS
BEGIN
    SELECT 
        e.ExamenID,
        p.PacienteID,
        CONCAT(p.NombrePaciente, ' ', p.ApellidoPaciente) AS NombreCompletoPaciente,
        e.ConsultaID,
        c.FechaConsulta,
        e.TipoExamen,
        e.FechaExamen,
        e.Resultado,
        e.Observaciones
    FROM 
        ExamenesMedicos e
    INNER JOIN 
        Pacientes p ON e.PacienteID = p.PacienteID
    LEFT JOIN 
        ConsultasMedicas c ON e.ConsultaID = c.ConsultaID
    ORDER BY 
        e.ExamenID ASC; -- Ordenar por el ID del examen en orden ascendente
END;


exec MostrarExamenesMedicos
select * from Usuarios

GO
CREATE PROCEDURE InsertarExamenMedico
    @PacienteID INT,
    @ConsultaID INT = NULL, -- Puede ser NULL
    @TipoExamen VARCHAR(100),
    @FechaExamen DATE,
    @Resultado VARCHAR(255),
    @Observaciones VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Insertar el examen médico
        INSERT INTO ExamenesMedicos 
        (PacienteID, ConsultaID, TipoExamen, FechaExamen, Resultado, Observaciones)
        VALUES 
        (@PacienteID, @ConsultaID, @TipoExamen, @FechaExamen, @Resultado, @Observaciones);
        
        -- Confirmar la transacción
        COMMIT TRANSACTION;
        
        -- Devolver el ID del examen recién insertado
        SELECT SCOPE_IDENTITY() AS ExamenID;
    END TRY
    BEGIN CATCH
        -- Revertir transacción en caso de error
        ROLLBACK TRANSACTION;
        THROW; -- Lanza el error para manejarlo en la aplicación
    END CATCH;
END;



GO

CREATE PROCEDURE EditarExamenMedico
    @ExamenID INT,
    @PacienteID INT,
    @ConsultaID INT = NULL, -- Puede ser NULL
    @TipoExamen VARCHAR(100),
    @FechaExamen DATE,
    @Resultado VARCHAR(255),
    @Observaciones VARCHAR(255)
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Actualizar los datos del examen médico
        UPDATE ExamenesMedicos
        SET 
            PacienteID = @PacienteID,
            ConsultaID = @ConsultaID, -- Si es NULL, se establecerá en NULL
            TipoExamen = @TipoExamen,
            FechaExamen = @FechaExamen,
            Resultado = @Resultado,
            Observaciones = @Observaciones
        WHERE ExamenID = @ExamenID;

        -- Confirmar la transacción
        COMMIT TRANSACTION;

        -- Retornar el ID del examen actualizado
        SELECT @ExamenID AS ExamenID;
    END TRY
    BEGIN CATCH
        -- Revertir la transacción en caso de error
        ROLLBACK TRANSACTION;

        -- Manejo de errores
        PRINT 'Error al actualizar el examen médico: ' + ERROR_MESSAGE();
    END CATCH;
END;

GO

CREATE PROCEDURE EliminarExamenMedico
    @ExamenID INT
AS
BEGIN
    -- Actualizar el estado del examen para ocultarlo en lugar de eliminarlo permanentemente
    UPDATE ExamenesMedicos
    SET Observaciones = 'Este examen ha sido eliminado' -- Marcamos con una observación de eliminación
    WHERE ExamenID = @ExamenID;

    PRINT 'El examen médico ha sido eliminado correctamente.';
END;




EXEC InsertarExamenMedico
    @PacienteID = 1,          -- ID del paciente
    @ConsultaID = 2,          -- ID de la consulta (puede ser NULL si no se tiene)
    @TipoExamen = 'Radiografía',
    @FechaExamen = '2024-11-09',
    @Resultado = 'Normal',
    @Observaciones = 'Sin observaciones adicionales';


select * from ExamenesMedicos

	EXEC EditarExamenMedico
    @ExamenID = 1,           -- ID del examen a editar
    @PacienteID = 1,         -- Nuevo ID de paciente
    @ConsultaID = 2,         -- Nuevo ID de consulta (puede ser NULL)
    @TipoExamen = 'Tomografía',
    @FechaExamen = '2024-11-10',
    @Resultado = 'Anómalo',
    @Observaciones = 'Requiere análisis adicional';



    GO
-- EXPEDIENTE DEL PACIENTE
CREATE PROCEDURE ObtenerExpedientePaciente 
    @PacienteID INT
AS 
BEGIN 
    SELECT 
        -- Datos Personales del Paciente
        P.NombrePaciente,
        P.ApellidoPaciente,
        P.ApellidoDeCasada,
        P.FechaNacimientoPaciente,
        S.DescripcionSexo AS Sexo,
        EC.DescripcionEstadoCivil AS EstadoCivil,
        P.DUIPaciente,
        P.TelefonoPaciente,
        P.CorreoPaciente,
        D.Depar AS Departamento,
        M.NombreMunicipio AS Municipio,
        
        -- Historial de Consultas Médicas del Paciente (Subconsulta con salto de línea)
        (SELECT TOP 1 
            CONCAT('Fecha Consulta: ', CM.FechaConsulta, CHAR(13) + CHAR(10), 
                   'Altura: ', CM.Altura, CHAR(13) + CHAR(10), 
                   'Peso: ', CM.Peso, CHAR(13) + CHAR(10),
                   'Alergia: ', CM.Alergia, CHAR(13) + CHAR(10), 
                   'Síntomas: ', CM.Sintomas, CHAR(13) + CHAR(10), 
                   'Diagnóstico: ', CM.Diagnostico, CHAR(13) + CHAR(10), 
                   'Observaciones: ', CM.Observaciones)
         FROM ConsultasMedicas CM
         WHERE CM.PacienteID = P.PacienteID
         ORDER BY CM.FechaConsulta DESC) AS UltimaConsulta,

        -- Exámenes Médicos del Paciente (Subconsulta con salto de línea)
        (SELECT TOP 1 
            CONCAT('Tipo de Examen: ', EM.TipoExamen, CHAR(13) + CHAR(10),
                   'Fecha Examen: ', EM.FechaExamen, CHAR(13) + CHAR(10),
                   'Resultado: ', EM.Resultado, CHAR(13) + CHAR(10),
                   'Observaciones: ', EM.Observaciones)
         FROM ExamenesMedicos EM
         WHERE EM.PacienteID = P.PacienteID
         ORDER BY EM.FechaExamen DESC) AS UltimoExamen,

        -- Recetas Médicas del Paciente (Subconsulta con salto de línea)
        (SELECT TOP 1 
            CONCAT('Fecha Emisión: ', CONVERT(VARCHAR(10), R.FechaEmision, 103), CHAR(13) + CHAR(10),
                   'Medicamento: ', R.Medicamento, CHAR(13) + CHAR(10),
                   'Dosis: ', R.Dosis, CHAR(13) + CHAR(10),
                   'Frecuencia: ', R.Frecuencia, CHAR(13) + CHAR(10),
                   'Duración: ', R.Duracion, CHAR(13) + CHAR(10),
                   'Instrucciones: ', R.Instrucciones)
         FROM Recetas R
         WHERE R.PacienteID = P.PacienteID
         ORDER BY R.FechaEmision DESC) AS UltimaReceta

    FROM 
        Pacientes P
    INNER JOIN 
        Sexos S ON P.SexoID = S.SexoID
    INNER JOIN 
        Departamentos D ON P.DepartamentosID = D.DepartamentosID
    INNER JOIN 
        Municipios M ON P.MunicipioID = M.MunicipioID
    INNER JOIN 
        EstadosCiviles EC ON P.EstadoCivilID = EC.EstadoCivilID
    LEFT JOIN 
        Citas C ON P.PacienteID = C.PacienteID -- Ajustado a LEFT JOIN en caso de no tener citas
    LEFT JOIN 
        Medicos Med ON C.MedicoID = Med.MedicoID
    LEFT JOIN 
        Especialidades E ON Med.EspecialidadID = E.EspecialidadID
    WHERE 
        P.PacienteID = @PacienteID;
END;


exec ObtenerExpedientePaciente @PacienteID = 1;



-- Procedimiento para el segundo reporte
GO
CREATE PROCEDURE ObtenerReporteRecetasPorPacienteYFecha
    @PacienteID INT,
    @FechaInicio DATE,
    @FechaFin DATE
AS 
BEGIN 
    SELECT 
        -- Datos del paciente
        P.NombrePaciente,
        P.ApellidoPaciente,
        P.ApellidoDeCasada,
        P.DUIPaciente,
        P.TelefonoPaciente,
        P.CorreoPaciente,
        D.Depar AS Departamento,
        M.NombreMunicipio AS Municipio,
        
        -- Recetas Médicas del Paciente
        CONVERT(VARCHAR(10), R.FechaEmision, 103) AS FechaEmision, -- Formatear solo la fecha (sin hora)
        R.Medicamento,
        R.Dosis,
        R.Frecuencia,
        R.Duracion,
        R.Instrucciones,
        CONCAT(MedRec.NombreMedico, ' ', MedRec.ApellidoMedico) AS Doctor
    FROM 
        Pacientes P
    INNER JOIN 
        Departamentos D ON P.DepartamentosID = D.DepartamentosID
    INNER JOIN 
        Municipios M ON P.MunicipioID = M.MunicipioID
    LEFT JOIN 
        Recetas R ON P.PacienteID = R.PacienteID
    LEFT JOIN 
        Medicos MedRec ON R.MedicoID = MedRec.MedicoID
    WHERE 
        P.PacienteID = @PacienteID
        AND (R.FechaEmision BETWEEN @FechaInicio AND @FechaFin OR R.FechaEmision IS NULL)
    ORDER BY 
        R.FechaEmision DESC;
END;





EXEC ObtenerReporteRecetasPorPacienteYFecha 
    @PacienteID = 1, 
    @FechaInicio = '2024-01-01', 
    @FechaFin = '2024-12-31';


    GO
    --Ultimo procedimiento agregado
    CREATE PROCEDURE BuscarConsultasPorDUI
    @DUI nvarchar(15)
AS
BEGIN
    SELECT 
        c.ConsultaID,
        p.PacienteID,
        m.MedicoID,
        e.NombreEspecialidad AS EspecialidaMedico,
        c.Altura,
        c.Peso,
        c.Alergia,
        c.Sintomas,
        c.Diagnostico,
        c.Observaciones,
        c.FechaConsulta
    FROM 
        ConsultasMedicas c
    INNER JOIN 
        Pacientes p ON c.PacienteID = p.PacienteID
    INNER JOIN 
        Medicos m ON c.MedicoID = m.MedicoID
    INNER JOIN 
        Especialidades e ON m.EspecialidadID = e.EspecialidadID
    WHERE 
        p.DUIPaciente LIKE @DUI;
END