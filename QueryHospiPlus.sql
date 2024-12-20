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


CREATE TABLE RolesUsuarios(
	RolID int primary key identity(1,1),
	Roles varchar(20) unique NOT NULL CHECK (Roles IN('Administrador','Secretario','Medico'))
);

-- Insertar datos
INSERT INTO RolesUsuarios(Roles) VALUES ('Administrador'),('Secretario'),('Medico');

-- Consulta
SELECT * FROM RolesUsuarios


-- Tabla Usuarios
CREATE TABLE Usuarios(
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    Correo VARCHAR(100) UNIQUE NOT NULL,
    NombreUsuario VARCHAR(80) NOT NULL,
    Contrasena VARBINARY(1000) NOT NULL,
    Rol VARCHAR(20) CHECK (Rol IN ('Administrador', 'Secretario', 'Medico')) NOT NULL
);



-- Se insertan datos de prueba
insert into Usuarios(NombreUsuario,Correo,Contrasena,Rol)
values('Ruth Vaquerano','ruthv@hospiplus.com',
ENCRYPTBYPASSPHRASE('hospiplus24','hospitca'),'Administrador')

insert into Usuarios(NombreUsuario,Correo,Contrasena,Rol)
values('Bryan Campos','carlosj@hospiplus.com',
ENCRYPTBYPASSPHRASE('hospiplus24','2'),'Medico')

insert into Usuarios(NombreUsuario,Correo,Contrasena,Rol)
values('Diego Mejia','diegom@hospiplus.com',
ENCRYPTBYPASSPHRASE('hospiplus24','hospitca'),'Secretario')

insert into Usuarios(NombreUsuario,Correo,Contrasena,Rol)
values('Vladimir Ascencio','vladi@hospiplus.com',
ENCRYPTBYPASSPHRASE('hospiplus24','hospitca'),'Administrador')



-- Consulta
SELECT * FROM Usuarios


-- Tabla Sexos
CREATE TABLE Sexos(
    SexoID INT PRIMARY KEY IDENTITY(1,1),
    DescripcionSexo VARCHAR(10) UNIQUE NOT NULL CHECK (DescripcionSexo IN ('Masculino', 'Femenino'))
);

-- Insert Sexos
INSERT INTO Sexos (DescripcionSexo) VALUES ('Masculino'), ('Femenino');

-- Consulta
SELECT * FROM Sexos


-- Tabla EstadosCiviles
CREATE TABLE EstadosCiviles(
    EstadoCivilID int PRIMARY KEY IDENTITY(1,1),
    DescripcionEstadoCivil varchar(10) UNIQUE NOT NULL CHECK (DescripcionEstadoCivil IN ('Soltero', 'Casado', 'Viudo', 'Divorciado'))
);

-- Insert EstadosCiviles
INSERT INTO EstadosCiviles (DescripcionEstadoCivil) VALUES ('Soltero'), ('Casado'), ('Viudo'), ('Divorciado');

-- Consulta
SELECT * FROM EstadosCiviles


-- Tabla Departamentos
CREATE TABLE Departamentos(
    DepartamentosID INT PRIMARY KEY IDENTITY(1,1),
    Depar VARCHAR(20) UNIQUE NOT NULL CHECK(Depar IN 
        ('Ahuachap�n','Sonsonate','Santa Ana',
         'La Libertad','Chalatenango','San Salvador',
         'Cuscatl�n','La Paz','San Vicente','Caba�as',
         'Usulut�n','San Miguel','Moraz�n','La Uni�n'))
);

-- Insert Departamentos
INSERT INTO Departamentos (Depar) VALUES 
('Ahuachap�n'), ('Sonsonate'), ('Santa Ana'), 
('La Libertad'), ('Chalatenango'), ('San Salvador'), 
('Cuscatl�n'), ('La Paz'), ('San Vicente'), 
('Caba�as'), ('Usulut�n'), ('San Miguel'), 
('Moraz�n'), ('La Uni�n');


-- Consulta
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
('Ahuachap�n', 1), ('Apaneca', 1), ('Atiquizaya', 1), 
('Concepci�n de Ataco', 1), ('El Refugio', 1), ('Guaymango', 1), ('Jujutla', 1), ('San Francisco Men�ndez', 1),
('San Lorenzo', 1), ('San Pedro Puxtla', 1), ('Tacuba', 1), ('Tur�n', 1);

-- Insert Municipios (Ejemplo: municipios de Sonsonate)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Sonsonate', 2), ('Acajutla', 2), ('Armenia', 2), 
('Caluco', 2), ('Cuisnahuat', 2), ('Izalco', 2), ('Juay�a', 2), ('Nahuizalco', 2),
('Nahulingo', 2), ('Salcoatit�n', 2), ('San Antonio del Monte', 2), ('San Juli�n', 2),
('Santa Catarina Masahuat', 2), ('Santa Isabel Ishuat�n', 2), ('Santo Domingo de Guzm�n', 2), ('Sonzacate', 2);

-- Insert Municipios (Ejemplo: municipios de Santa Ana)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Santa Ana', 3), ('Candelaria de la Frontera', 3), ('Chalchuapa', 3), 
('Coatepeque', 3), ('El Congo', 3), ('El Porvenir', 3),
('Masahuat', 3), ('Metap�n', 3),('San Antonio Pajonal', 3),
('San Sebasti�n Salitrillo', 3), ('Santa Rosa Guachipil�n', 3), ('Santiago de la Frontera', 3),
('Texistepeque', 3);

-- Insert Municipios (Ejemplo: municipios de La Libertad)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Santa Tecla', 4), ('Antiguo Cuscatl�n', 4), ('Chiltiup�n', 4), 
('Ciudad Arce', 4), ('Col�n', 4), ('Comasagua', 4), ('Huiz�car', 4), ('Jayaque', 4),
('Jicalapa', 4), ('La Libertad', 4), ('Nuevo Cuscatl�n', 4), ('Quezaltepeque', 4), ('Sacacoyo', 4),
('San Jos� Villanueva', 4), ('San Juan Opico', 4), ('San Mat�as', 4),
('San Pablo Tacachico', 4), ('Talnique', 4), ('Tamanique', 4), ('Teotepeque', 4), ('Tepecoyo', 4), ('Zaragoza', 4);

-- Insert Municipios (Ejemplo: municipios de La Chalatenango)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Agua Caliente', 5), ('Arcatao', 5), ('Azacualpa', 5), ('Cancasque', 5), 
('Chalatenango', 5), ('Cital�', 5), ('Comalapa', 5), ('Concepci�n Quezaltepeque', 5), 
('Dulce Nombre de Mar�a', 5), ('El Carrizal', 5), ('El Para�so', 5), ('La Laguna', 5), 
('La Palma', 5), ('La Reina', 5), ('Las Vueltas', 5), ('Nueva Concepci�n', 5), 
('Nueva Trinidad', 5), ('Nombre de Jes�s', 5), ('Ojos de Agua', 5), ('Potonico', 5), 
('San Antonio de la Cruz', 5), ('San Antonio Los Ranchos', 5), ('San Fernando', 5), 
('San Francisco Lempa', 5), ('San Francisco Moraz�n', 5), ('San Ignacio', 5), 
('San Isidro Labrador', 5), ('San Jos� Cancasque', 5), ('San Jos� Las Flores', 5), 
('San Luis del Carmen', 5), ('San Miguel de Mercedes', 5), ('San Rafael', 5), 
('Santa Rita', 5), ('Tejutla', 5);

-- Insert Municipios (Ejemplo: municipios de San Salvador)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('San Salvador', 6),('Aguilares', 6),('Apopa', 6),('Ayutuxtepeque', 6),
('Ciudad Delgado', 6),('Cuscatancingo', 6),('El Paisnal', 6),('Guazapa', 6),
('Ilopango', 6),('Mejicanos', 6),('Nejapa', 6),('Panchimalco', 6),
('Rosario de Mora', 6),('San Marcos', 6),('San Mart�n', 6),('Santiago Texacuangos', 6),
('Santo Tom�s', 6),('Soyapango', 6),('Tonacatepeque', 6);

-- Insert Municipios (Ejemplo: municipios de Cuscatl�n)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Candelaria', 7), ('Cojutepeque', 7), ('El Carmen', 7), ('El Rosario', 7),
('Monte San Juan', 7), ('Oratorio de Concepci�n', 7), ('San Bartolom� Perulap�a', 7),
('San Crist�bal', 7), ('San Jos� Guayabal', 7), ('San Pedro Perulap�n', 7),
('San Rafael Cedros', 7), ('San Ram�n', 7), ('Santa Cruz Analquito', 7),
('Santa Cruz Michapa', 7), ('Suchitoto', 7), ('Tenancingo', 7);

-- Insert Municipios (Ejemplo: municipios de La Paz)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Cuyultit�n', 8), ('El Rosario', 8), ('Jerusal�n', 8), ('Mercedes La Ceiba', 8),
('Olocuilta', 8), ('Para�so de Osorio', 8), ('San Antonio Masahuat', 8),
('San Emigdio', 8), ('San Francisco Chinameca', 8), ('San Juan Nonualco', 8),
('San Juan Talpa', 8), ('San Juan Tepezontes', 8), ('San Luis La Herradura', 8),
('San Luis Talpa', 8), ('San Miguel Tepezontes', 8), ('San Pedro Masahuat', 8),
('San Pedro Nonualco', 8), ('San Rafael Obrajuelo', 8), ('Santa Mar�a Ostuma', 8),
('Santiago Nonualco', 8), ('Tapalhuaca', 8), ('Zacatecoluca', 8);

-- Insert Municipios (Ejemplo: municipios de San Vicente)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Apastepeque', 9), ('Guadalupe', 9), ('San Cayetano Istepeque', 9),
('San Esteban Catarina', 9), ('San Ildefonso', 9), ('San Lorenzo', 9),
('San Sebasti�n', 9), ('San Vicente', 9), ('Santa Clara', 9),
('Santo Domingo', 9), ('Tecoluca', 9), ('Tepetit�n', 9), ('Verapaz', 9);

-- Insert Municipios (Ejemplo: municipios de Caba�as)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Cinquera', 10), ('Dolores', 10), ('Guacotecti', 10), ('Ilobasco', 10),
('Jutiapa', 10), ('San Isidro', 10), ('Sensuntepeque', 10), 
('Tejutepeque', 10), ('Victoria', 10);

-- Insert Municipios (Ejemplo: municipios de Usulut�n)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Alegr�a', 11), ('Berl�n', 11), ('California', 11), ('Concepci�n Batres', 11),
('El Triunfo', 11), ('Ereguayqu�n', 11), ('Estanzuelas', 11), ('Jiquilisco', 11),
('Jucuapa', 11), ('Jucuar�n', 11), ('Mercedes Uma�a', 11), ('Nueva Granada', 11),
('Ozatl�n', 11), ('Puerto El Triunfo', 11), ('San Agust�n', 11), ('San Buenaventura', 11),
('San Dionisio', 11), ('San Francisco Javier', 11), ('Santa Elena', 11), ('Santa Mar�a', 11),
('Santiago de Mar�a', 11), ('Tecap�n', 11), ('Usulut�n', 11);

-- Insert Municipios (Ejemplo: municipios de San Miguel)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Carolina', 12), ('Chapeltique', 12), ('Chinameca', 12), ('Chirilagua', 12),
('Ciudad Barrios', 12), ('Comacar�n', 12), ('El Tr�nsito', 12), ('Lolotique', 12),
('Moncagua', 12), ('Nueva Guadalupe', 12), ('Nuevo Ed�n de San Juan', 12),
('Quelepa', 12), ('San Antonio', 12), ('San Gerardo', 12), ('San Jorge', 12),
('San Luis de la Reina', 12), ('San Miguel', 12), ('San Rafael Oriente', 12),
('Sesori', 12), ('Uluazapa', 12);

-- Insert Municipios (Ejemplo: municipios de Moraz�n)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Arambala', 13), ('Cacaopera', 13), ('Chilanga', 13), ('Corinto', 13),
('Delicias de Concepci�n', 13), ('El Divisadero', 13), ('El Rosario', 13),
('Gualococti', 13), ('Guatajiagua', 13), ('Joateca', 13), ('Jocoaitique', 13),
('Jocoro', 13), ('Lolotiquillo', 13), ('Meanguera', 13), ('Osicala', 13),
('Perqu�n', 13), ('San Carlos', 13), ('San Fernando', 13), ('San Francisco Gotera', 13),
('San Isidro', 13), ('San Sim�n', 13), ('Sensembra', 13), ('Sociedad', 13),
('Torola', 13), ('Yamabal', 13), ('Yoloaiqu�n', 13);

-- Insert Municipios (Ejemplo: municipios de La Uni�n)
INSERT INTO Municipios (NombreMunicipio, DepartamentosID) VALUES
('Anamor�s', 14), ('Bol�var', 14), ('Concepci�n de Oriente', 14),
('Conchagua', 14), ('El Carmen', 14), ('El Sauce', 14), ('Intipuc�', 14),
('La Uni�n', 14), ('Lislique', 14), ('Meanguera del Golfo', 14),
('Nueva Esparta', 14), ('Pasaquina', 14), ('Polor�s', 14), ('San Alejo', 14),
('San Jos�', 14), ('Santa Rosa de Lima', 14), ('Yayantique', 14), ('Yucuaiqu�n', 14);



-- Tabla Pacientes
CREATE TABLE Pacientes(
    PacienteID INT PRIMARY KEY IDENTITY(1,1),
    NombrePaciente VARCHAR(50) NOT NULL,
    ApellidoPaciente VARCHAR(60) NOT NULL,
    ApellidoDeCasada VARCHAR(60) NULL,
    FechaNacimientoPaciente DATE NOT NULL,
    SexoID INT NOT NULL,
    EstadoCivilID INT NOT NULL,
    DUIPaciente VARCHAR(10) UNIQUE NOT NULL,
    TelefonoPaciente VARCHAR(15) NOT NULL,
    CorreoPaciente VARCHAR(100) UNIQUE NOT NULL,
    DepartamentosID INT NOT NULL,
    MunicipioID INT NOT NULL,
    EstadoID INT NOT NULL,
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


-- Consulta
SELECT * FROM Pacientes


-- Tabla Especialidades
CREATE TABLE Especialidades (
    EspecialidadID INT PRIMARY KEY IDENTITY(1,1),
    NombreEspecialidad VARCHAR(50) NOT NULL
);


-- Insert Especialidades
INSERT INTO Especialidades (NombreEspecialidad) VALUES
('Cardiologo'), ('Pediatra'), ('General');


SELECT * FROM Especialidades


-- Tabla de Horario M�dico
CREATE TABLE HorarioMedico (
    HorarioID INT PRIMARY KEY IDENTITY(1,1),
    HoraInicio TIME NOT NULL,
    HoraFin TIME NOT NULL
);


INSERT INTO HorarioMedico (HoraInicio, HoraFin) VALUES ('8:00','12:35');
INSERT INTO HorarioMedico (HoraInicio, HoraFin) VALUES ('10:00','15:45');
INSERT INTO HorarioMedico (HoraInicio, HoraFin) VALUES ('11:00','21:00');

-- Consulta
SELECT * FROM HorarioMedico


-- Tabla de D�as de la Semana
CREATE TABLE DiasSemana (
    DiaID INT PRIMARY KEY IDENTITY(1,1),
    NombreDia VARCHAR(10) NOT NULL
);


-- Insertar los d�as de la semana
INSERT INTO DiasSemana (NombreDia) VALUES 
('Lunes'), ('Martes'), ('Mi�rcoles'), ('Jueves'), ('Viernes'), ('S�bado'), ('Domingo');

-- Consulta
SELECT * FROM DiasSemana


-- Tabla de Relaci�n entre Horario y D�as
CREATE TABLE HorarioDias (
    HorarioID INT NOT NULL,
    DiaID INT NOT NULL,
    FOREIGN KEY (HorarioID) REFERENCES HorarioMedico(HorarioID),
    FOREIGN KEY (DiaID) REFERENCES DiasSemana(DiaID),
    PRIMARY KEY (HorarioID, DiaID)
);

-- Consulta
SELECT * FROM HorarioDias


-- Tabla Medicos
CREATE TABLE Medicos (
    MedicoID INT PRIMARY KEY IDENTITY(1,1),
    NombreMedico VARCHAR(50) NOT NULL,
    ApellidoMedico VARCHAR(50) NOT NULL,
    ApellidoMedicoCasada VARCHAR(60) NULL,
    FechaNacimientoMedico DATE NOT NULL,
    TelefonoMedico VARCHAR(15) NOT NULL,
    DepartamentosID INT NOT NULL,
    MunicipioID INT NOT NULL,
    CorreoMedico VARCHAR(100) UNIQUE NOT NULL,
    DUIMedico VARCHAR(10) UNIQUE NOT NULL,
    SexoID INT NOT NULL,
    EstadoCivilID INT NOT NULL,
    EspecialidadID INT NOT NULL,
    HorarioID INT NOT NULL,
	DiaID INT NOT NULL,
    EstadoID INT NOT NULL,
    FOREIGN KEY (SexoID) REFERENCES Sexos(SexoID),
    FOREIGN KEY (EstadoCivilID) REFERENCES EstadosCiviles(EstadoCivilID),
    FOREIGN KEY (EspecialidadID) REFERENCES Especialidades(EspecialidadID),
    FOREIGN KEY (DepartamentosID) REFERENCES Departamentos(DepartamentosID),
    FOREIGN KEY (HorarioID) REFERENCES HorarioMedico(HorarioID),
    FOREIGN KEY (EstadoID) REFERENCES Estados(EstadoID),
	FOREIGN KEY (MunicipioID) REFERENCES Municipios(MunicipioID),
	FOREIGN KEY (DiaID) REFERENCES DiasSemana(DiaID),
);


-- Insert Medicos
INSERT INTO Medicos (NombreMedico, ApellidoMedico, ApellidoMedicoCasada, FechaNacimientoMedico, TelefonoMedico, DepartamentosID, MunicipioID,CorreoMedico, DUIMedico, SexoID, EstadoCivilID, EspecialidadID, HorarioID, DiaID ,EstadoID)
VALUES
('Bryan Gustavo', 'Campos Gutierrez', '', '1981-05-14', '6107-8146', 6, 45,'bryan@hospital.com', '06582560-1', 1, 1, 1, 1, 1, 1);

INSERT INTO Medicos (NombreMedico, ApellidoMedico, ApellidoMedicoCasada, FechaNacimientoMedico, TelefonoMedico, DepartamentosID, MunicipioID,CorreoMedico, DUIMedico, SexoID, EstadoCivilID, EspecialidadID, HorarioID, DiaID ,EstadoID)
VALUES
('Ruth Abigail', 'Vaquerano Melara', '', '2000-05-12', '7012-8146', 12, 212,'ruthabi@hospital.com', '78945127-2', 2, 1, 2, 2, 2, 1);

INSERT INTO Medicos (NombreMedico, ApellidoMedico, ApellidoMedicoCasada, FechaNacimientoMedico, TelefonoMedico, DepartamentosID, MunicipioID,CorreoMedico, DUIMedico, SexoID, EstadoCivilID, EspecialidadID, HorarioID, DiaID ,EstadoID)
VALUES
('Allison Andrea', 'Servano Pacheco', '', '2005-04-12', '7217-8706', 12, 212,'alli@hospital.com', '78945787-2', 2, 1, 3, 2, 7, 1);

-- Consulta
SELECT * FROM Medicos


-- Tabla para los Estados de las Citas
CREATE TABLE EstadoCita(
	EstadoCitaID INT PRIMARY KEY IDENTITY(1,1),
	CitaEstado VARCHAR(10) UNIQUE NOT NULL CHECK(CitaEstado IN ('Agendada', 'Cancelada'))
);

-- Insertar los estados de la cita
INSERT INTO EstadoCita (CitaEstado) VALUES ('Agendada'), ('Cancelada');

-- Consulta
SELECT * FROM EstadoCita


-- Tabla de Citas
CREATE TABLE Citas(
	CitaID INT PRIMARY KEY IDENTITY(1,1),
	PacienteID INT NOT NULL,
	MedicoID INT NOT NULL,
	EstadoCitaID INT NOT NULL,
	FechaCita DATE NOT NULL,
	HoraCita TIME NOT NULL,
	EspecialidadID INT NOT NULL,
	FOREIGN KEY (PacienteID) REFERENCES Pacientes(PacienteID),
	FOREIGN KEY (MedicoID) REFERENCES Medicos(MedicoID),
	FOREIGN KEY (EspecialidadID) REFERENCES Especialidades(EspecialidadID),
	FOREIGN KEY (EstadoCitaID) REFERENCES EstadoCita(EstadoCitaID)
);

-- Insertar las citas con los pacientes
INSERT INTO Citas (PacienteID, MedicoID, EstadoCitaID, FechaCita, HoraCita, EspecialidadID)
VALUES 
(1, 1, 1, '2024-10-01', '09:00', 1);

INSERT INTO Citas (PacienteID, MedicoID, EstadoCitaID, FechaCita, HoraCita, EspecialidadID)
VALUES 
(2, 2, 1, '2024-10-15', '10:40', 2);

INSERT INTO Citas (PacienteID, MedicoID, EstadoCitaID, FechaCita, HoraCita, EspecialidadID)
VALUES 
(3, 3, 1, '2024-10-15', '10:40', 3);

-- Consultar
SELECT * FROM Citas


-- Tabla de Consultas Medicas
CREATE TABLE ConsultasMedicas(
	ConsultaID INT PRIMARY KEY IDENTITY(1,1),
	CitaID INT NOT NULL,
	PacienteID INT NOT NULL,
	MedicoID INT NOT NULL,
	Altura DECIMAL(5,2) NOT NULL,
	Peso DECIMAL(5,2) NOT NULL,
	Alergia VARCHAR(255) NOT NULL,
	Sintomas VARCHAR(255) NOT NULL,
	Diagnostico TEXT NOT NULL,
	Observaciones TEXT NOT NULL,
	FechaConsulta DATE NOT NULL,
	FOREIGN KEY (CitaID) REFERENCES Citas(CitaID),
	FOREIGN KEY (PacienteID) REFERENCES Pacientes(PacienteID),
	FOREIGN KEY (MedicoID) REFERENCES Medicos(MedicoID)
);

-- Insertar las consultas
INSERT INTO ConsultasMedicas (CitaID, PacienteID, MedicoID, Altura, Peso, Alergia, Sintomas, Diagnostico, Observaciones, FechaConsulta)
VALUES 
(1, 1, 1, 1.75, 70.5, 'Ninguna', 'Dolor de cabeza, fiebre', 'Gripe com�n', 'Recomendar reposo y mucha hidrataci�n', '2024-10-01');

INSERT INTO ConsultasMedicas (CitaID, PacienteID, MedicoID, Altura, Peso, Alergia, Sintomas, Diagnostico, Observaciones, FechaConsulta)
VALUES 
(2, 1, 2, 1.75, 65.6, 'Ninguna', 'Dolor de cabeza y fiebre alta', 'Necesita reposo', 'Recomendar reposo por 30 dias, mucha hidrataci�n',
'2024-11-08');

INSERT INTO ConsultasMedicas (CitaID, PacienteID, MedicoID, Altura, Peso, Alergia, Sintomas, Diagnostico, Observaciones, FechaConsulta)
VALUES 
(3, 3, 3, 1.75, 65.6, 'Ninguna', 'Dolor de cabeza y fiebre alta', 'Necesita reposo', 'Recomendar reposo por 30 dias, mucha hidrataci�n',
'2024-12-24');

-- Consultar
SELECT * FROM Citas
SELECT * FROM ConsultasMedicas


-- Tabla de Recetas M�dicas
CREATE TABLE Recetas(
	RecetaID INT PRIMARY KEY IDENTITY(1,1),
	PacienteID INT NOT NULL,
	MedicoID INT NOT NULL,
	FechaEmision DATETIME NOT NULL,
	ConsultaID INT NULL,
	Medicamento VARCHAR(100) NOT NULL,
	Dosis VARCHAR(50) NOT NULL, 
	Frecuencia TEXT NOT NULL,
	Duracion TEXT NOT NULL,
	Instrucciones TEXT NOT NULL,
	FOREIGN KEY (ConsultaID) REFERENCES ConsultasMedicas(ConsultaID),
	FOREIGN KEY (PacienteID) REFERENCES Pacientes(PacienteID),
	FOREIGN KEY (MedicoID) REFERENCES Medicos(MedicoID)
);

-- Insertar las recetas al paciente
INSERT INTO Recetas (PacienteID, MedicoID, FechaEmision, ConsultaID, Medicamento, Dosis, Frecuencia, Duracion, Instrucciones)
VALUES 
(1, 1, '2024-11-02', 1, 'Inyeccion', '50 mg', 'Cada 2 dias', '3 meses', 'Aplicar despues de almuerzo.');

INSERT INTO Recetas (PacienteID, MedicoID, FechaEmision, ConsultaID, Medicamento, Dosis, Frecuencia, Duracion, Instrucciones)
VALUES 
(2, 2, '2024-11-08', 2, 'Inyeccion', '50 mg', 'Cada 2 dias', '3 meses', 'Aplicar despues de almuerzo.');

INSERT INTO Recetas (PacienteID, MedicoID, FechaEmision, ConsultaID, Medicamento, Dosis, Frecuencia, Duracion, Instrucciones)
VALUES 
(3, 2, '2024-12-24', 3, 'Inyeccion', '50 mg', 'Cada 2 dias', '3 meses', 'Aplicar despues de almuerzo.');


-- Consultar
SELECT * FROM Recetas
SELECT * FROM ConsultasMedicas
SELECT * FROM Medicos
SELECT * FROM Pacientes



-- Tabla de Detalle de Recetas
CREATE TABLE DetalleReceta (
    IdDetalleReceta INT PRIMARY KEY IDENTITY(1,1),
    RecetaID INT NOT NULL,
    FOREIGN KEY (RecetaID) REFERENCES Recetas(RecetaID)
);

-- Insertar los detalles de la receta
INSERT INTO DetalleReceta (RecetaID) VALUES (1);
INSERT INTO DetalleReceta (RecetaID) VALUES (2);
INSERT INTO DetalleReceta (RecetaID) VALUES (3);

SELECT * FROM DetalleReceta
SELECT * FROM Usuarios


-- Tabla de Ex�menes M�dicos
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

-- Insertar los examenes m�dicos
INSERT INTO ExamenesMedicos (PacienteID, ConsultaID, TipoExamen, FechaExamen, Resultado, Observaciones)
VALUES (1, 1, 'Examen de Sangre', '2024-10-01','Sin anomalias en los flujos sanguineos','Se observo falta de vitamina C');

INSERT INTO ExamenesMedicos (PacienteID, ConsultaID, TipoExamen, FechaExamen, Resultado, Observaciones)
VALUES (2,2,'Examen de Orina','2024-11-08','Infeccion de vias orinarias','Se necesita tratamiento urgente');

INSERT INTO ExamenesMedicos (PacienteID, ConsultaID, TipoExamen, FechaExamen, Resultado, Observaciones)
VALUES (3,3,'Examen dental','2024-12-24','Caries avanzado','Se necesita cirujia dental');

-- Consulta
SELECT * FROM ExamenesMedicos







-------------------------------------------------------- PROCEDIMIENTO ALMACENADO PARA USUARIOS / LOGIN ----------------------------------------------------------------
GO
-- 1. PROCEDIMIENTO PARA VERIFICAR CORREO Y CONTRA
CREATE PROCEDURE spLogin1
@correo VARCHAR(80),
@pass VARCHAR(60)  -- Contrase�a en texto
AS
BEGIN
    SELECT TOP 1 UsuarioID,Rol  -- Devuelve el ID del usuario
    FROM Usuarios
    WHERE Correo = @correo 
    AND CONVERT(VARCHAR(1000), DECRYPTBYPASSPHRASE('hospiplus24', Contrasena))�=�@pass;
END
GO



-- 2. MOSTRAR LOS USUARIOS
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



-- 3. INSERTAR UN NUEVO USUARIO
GO
CREATE PROCEDURE InsertarUsuario
    @NombreUsuario VARCHAR(80),
    @Correo VARCHAR(100),
    @ContrasenaTexto VARCHAR(100),
    @Rol NVARCHAR(20)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;  -- Iniciar transacci�n

        -- Verificar si el correo ya existe
        IF EXISTS (SELECT 1 FROM Usuarios WHERE Correo = @Correo)
        BEGIN
            -- Si el correo existe, devolver 2 (correo duplicado)
            ROLLBACK TRANSACTION;  -- Deshacer la transacci�n
            RETURN 2;
        END

        -- Convertir la contrase�a de texto plano a varbinary (si la columna Contrase�a est� como varbinary)
        DECLARE @Contrasena VARBINARY(1000);
        SET @Contrasena = ENCRYPTBYPASSPHRASE('hospiplus24', @ContrasenaTexto);  -- Encriptar la contrase�a

        -- Insertar el nuevo usuario si el correo no existe
        INSERT INTO Usuarios (NombreUsuario, Correo, Contrasena, Rol)
        VALUES (@NombreUsuario, @Correo, @Contrasena, @Rol);

        -- Confirmar la transacci�n si todo fue exitoso
        COMMIT TRANSACTION;

        -- Retornar 1 si la inserci�n fue exitosa
        RETURN 1;

    END TRY
    BEGIN CATCH
        -- Capturar el error y retornar 0
        IF XACT_STATE() <> 0
        BEGIN
            ROLLBACK TRANSACTION;  -- Deshacer la transacci�n si hubo un error
        END

        -- Mostrar el mensaje de error para depuraci�n
        DECLARE @ErrorMessage NVARCHAR(4000);
        SET @ErrorMessage = ERROR_MESSAGE();
        PRINT @ErrorMessage;  -- Esto imprime el error en el log para depuraci�n
        RETURN 0;
    END CATCH
END;




-- 4. EDITAR USUARIOS
GO
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
            RETURN 2; -- Retorna 2 si el correo ya est� registrado
        END

        -- Cifrar la nueva contrase�a
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


SELECT * FROM Usuarios


-- 5. M�TODO PARA OBTENER LA CONTRASE�A DEL USUARIO
GO
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





-------------------------------------------------- PROCEDIMIENTOS ALMACENADOS DEL PACIENTE ------------------------------------------------

--- 1. MOSTRAR PACIENTE
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



--- 2. INSERTAR PACIENTE
GO
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



--- 3. EDITAR PACIENTE
GO
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



---4. ELIMINAR PACIENTE (OCULTARLO)
GO
CREATE PROCEDURE EliminarPaciente
    @PacienteID INT
AS
BEGIN
    UPDATE Pacientes
    SET EstadoID = 2
    WHERE PacienteID = @PacienteID;
    SELECT 'Paciente desactivado exitosamente.' AS Mensaje;
END;



--- 5. BUSCAR PACIENTE POR DUI
GO
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






---------------------------------------------------- PROCEDIMIENTOS ALMACENADOS DEL M�DICO -------------------------------------------

--- 1. MOSTRAR M�DICO
GO
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


---2. INSERTAR M�DICO
GO
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
    @DiaID INT, 
    @EspecialidadID INT,
    @EstadoID INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @HorarioID INT;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Insertar el horario m�dico
        INSERT INTO HorarioMedico (HoraInicio, HoraFin)
        VALUES (@HoraInicio, @HoraFin);
        
        -- Obtener el ID del horario insertado
        SET @HorarioID = SCOPE_IDENTITY();

        -- Insertar el d�a de la semana en la tabla HorarioDias solo si @DiaID no es NULL
        IF @DiaID IS NOT NULL
        BEGIN
            INSERT INTO HorarioDias (HorarioID, DiaID)
            VALUES (@HorarioID, @DiaID);
        END

        -- Insertar el m�dico con el HorarioID generado
        INSERT INTO Medicos 
        (NombreMedico, ApellidoMedico, ApellidoMedicoCasada, FechaNacimientoMedico, 
         TelefonoMedico, DepartamentosID, MunicipioID, CorreoMedico, DUIMedico, 
         SexoID, EstadoCivilID, EspecialidadID,EstadoID,DiaID,HorarioID)
        VALUES 
        (@NombreMedico, @ApellidoMedico, @ApellidoMedicoCasada, @FechaNacimientoMedico, 
         @TelefonoMedico, @DepartamentosID, @MunicipioID, @CorreoMedico, @DUIMedico, 
         @SexoID, @EstadoCivilID, @EspecialidadID,@EstadoID,@DiaID,@HorarioID)

        -- Confirmar la transacci�n
        COMMIT TRANSACTION;
        
        -- Devolver el ID del m�dico reci�n insertado
        SELECT SCOPE_IDENTITY() AS MedicoID;
    END TRY
    BEGIN CATCH
        -- Revertir transacci�n en caso de error
        ROLLBACK TRANSACTION;
        THROW; -- Lanza el error para verlo en la aplicaci�n
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


SELECT * FROM Medicos


---3. EDITAR M�DICO
GO
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
    @DiaID INT, 
    @EspecialidadID INT,
    @EstadoID INT
AS
BEGIN
    DECLARE @HorarioID INT;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Obtener el ID de Horario actual del m�dico
        SELECT @HorarioID = HorarioID FROM Medicos WHERE MedicoID = @MedicoID;

        -- Actualizar horario m�dico
        UPDATE HorarioMedico
        SET HoraInicio = @HoraInicio, HoraFin = @HoraFin
        WHERE HorarioID = @HorarioID;

        -- Borrar d�as de la semana existentes en el horario
        DELETE FROM HorarioDias WHERE HorarioID = @HorarioID;

        -- Insertar el nuevo d�a de la semana si @DiaID no es NULL
        IF @DiaID IS NOT NULL
        BEGIN
            INSERT INTO HorarioDias (HorarioID, DiaID)
            VALUES (@HorarioID, @DiaID);
        END

        -- Actualizar los dem�s datos del m�dico
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
        PRINT 'Error al actualizar el m�dico: ' + ERROR_MESSAGE();
    END CATCH;
END;



---4. ELIMINAR M�DICO(OCULTARLO)
GO
CREATE PROCEDURE EliminarMedico
    @MedicoID INT
AS
BEGIN
    UPDATE Medicos
    SET EstadoID = 2
    WHERE MedicoID = @MedicoID;
    PRINT 'El m�dico ha sido eliminado correctamente.';
END;



--- 5. PROCEDIMIENTO PARA MOSTRAR M�DICOS CON SUS HORARIOS
GO
CREATE PROCEDURE MostrarMedicosConHorarios
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


--- 6. PROCEDIMIENTO PARA MOSTRAR M�DICOS POR ESPECIALIDAD
GO
CREATE PROCEDURE MostrarMedicosPorEspecialidad
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




-------------------------------------------------------- PROCEDIMIENTOS ALMACENADOS DE CITAS ------------------------------------------------

---1. MOSTRAR CITAS
GO
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



---2. BUSCAR CITA POR DUI
GO
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


--2. AGENDAR NUEVA CITA
GO
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

        -- Verificar si el PacienteID es v�lido
        IF @PacienteID IS NULL
        BEGIN
            RAISERROR('El paciente con el DUI especificado no existe.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Insertar la nueva cita
        INSERT INTO Citas (PacienteID, MedicoID, EstadoCitaID, FechaCita, HoraCita, EspecialidadID)
        VALUES (@PacienteID, @MedicoID, @EstadoCitaID, @FechaCita, @HoraCita, @EspecialidadID);

        -- Confirmar la transacci�n
        COMMIT TRANSACTION;

        PRINT 'La cita ha sido agendada correctamente.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT 'Ocurri� un error al agendar la cita: ' + ERROR_MESSAGE();
    END CATCH;
END;



---3. AGENDAR CITA
GO
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
        RAISERROR('El m�dico ya tiene el l�mite de citas para el d�a.', 16, 1);
        RETURN;
    END

    INSERT INTO Citas (PacienteID, MedicoID, EspecialidadID, FechaCita, HoraCita, EstadoCitaID)
    VALUES (@PacienteID, @MedicoID, @EspecialidadID, @FechaCita, @HoraCita, @EstadoCitaID);
    PRINT 'La cita ha sido agendada correctamente.';
END;


---4. EDITAR CITA
GO
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



---5. MODIFICAR CITA
GO
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



---6. ELIMINAR CITA (CANCELAR)
GO
CREATE PROCEDURE EliminarCita
    @CitaID INT
AS
BEGIN
    UPDATE Citas
    SET EstadoCitaID = 2
    WHERE CitaID = @CitaID;
    PRINT 'La cita ha sido cancelada correctamente.';
END;




--------------------------------------------------------- PROCEDIMIENTOS ALMACENADOS DE CONSULTAS M�DICAS ----------------------------------------------------------

--- 1. MOSTRAR CONSULTA M�DICA
GO
CREATE PROCEDURE MostrarConsultas
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
        JOIN EstadoCita est ON cit.EstadoCitaID = est.EstadoCitaID;
END;


EXEC MostrarConsultas;


--- 2. BUSCAR CONSULTA POR FECHA
GO
CREATE PROCEDURE BuscarConsultasPorFecha
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



--- 3. INGRESAR CONSULTA M�DICA
GO
CREATE PROCEDURE InsertarConsulta
    @CitaID INT,
    @PacienteID INT,
    @MedicoID INT,
    @Altura DECIMAL(5,2),
    @Peso DECIMAL(5,2),
    @Alergia VARCHAR(255),
    @Sintomas VARCHAR(255),
    @Diagnostico VARCHAR(255),
    @Observaciones VARCHAR(255),
    @FechaConsulta DATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO ConsultasMedicas (CitaID, PacienteID, MedicoID, Altura, Peso, Alergia, Sintomas, Diagnostico, Observaciones, FechaConsulta)
    VALUES (@CitaID, @PacienteID, @MedicoID, @Altura, @Peso, @Alergia, @Sintomas, @Diagnostico, @Observaciones, @FechaConsulta);

    SELECT SCOPE_IDENTITY() AS NewConsultaID; -- Devuelve el ID de la nueva consulta insertada
END;

--SP para editar la consulta
Go
CREATE PROCEDURE EditarConsulta
    @ConsultaID INT,
    @CitaID INT,
    @PacienteID INT,
    @MedicoID INT,
    @Altura DECIMAL(5,2),
    @Peso DECIMAL(5,2),
    @Alergia VARCHAR(255),
    @Sintomas VARCHAR(255),
    @Diagnostico VARCHAR(255),
    @Observaciones VARCHAR(255),
    @FechaConsulta DATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE ConsultasMedicas
    SET CitaID = @CitaID,
        PacienteID = @PacienteID,
        MedicoID = @MedicoID,
        Altura = @Altura,
        Peso = @Peso,
        Alergia = @Alergia,
        Sintomas = @Sintomas,
        Diagnostico = @Diagnostico,
        Observaciones = @Observaciones,
        FechaConsulta = @FechaConsulta
    WHERE ConsultaID = @ConsultaID;

    SELECT @ConsultaID AS UpdatedConsultaID; -- Devuelve el ID de la consulta actualizada
END;



--SP CancelarConsulta
GO
ALTER PROCEDURE [dbo].[CancelarConsulta]
    @ConsultaID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
            
        DECLARE @CitaID INT;
        DECLARE @EstadoCanceladaID INT;

        -- Obtener el CitaID asociado a la consulta
        SELECT @CitaID = CitaID 
        FROM ConsultasMedicas 
        WHERE ConsultaID = @ConsultaID;

        -- Obtener el EstadoCitaID para "Cancelada"
        SELECT @EstadoCanceladaID = EstadoCitaID 
        FROM EstadoCita 
        WHERE CitaEstado = 'Cancelada';

        IF @CitaID IS NULL
        BEGIN
            THROW 51000, 'No se encontr� la consulta especificada.', 1;
        END

        IF @EstadoCanceladaID IS NULL
        BEGIN
            THROW 51001, 'No se encontr� el estado "Cancelada" en la tabla EstadoCita.', 1;
        END

        -- Actualizar el estado de la cita
        UPDATE Citas
        SET EstadoCitaID = @EstadoCanceladaID
        WHERE CitaID = @CitaID;

        -- Retornar el n�mero de filas afectadas
        SELECT @@ROWCOUNT AS AffectedRows;
            
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        THROW;
    END CATCH
END;




------------------------------------------------       PROCEDIMIENTOS ALMACENADOS DE RECETAS       ------------------------------------------------------------

-- 1. MOSTRAR RECETAS
GO
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

exec MostrarRecetas


-- 2. PROCEDIMIENTO PARA INSERTAR UNA NUEVA RECETA
GO
CREATE PROCEDURE InsertarReceta
    @PacienteID INT,
    @MedicoID INT,
    @FechaEmision DATETIME,
    @ConsultaID INT,
    @Medicamento VARCHAR(100),
    @Dosis VARCHAR(50),
    @Frecuencia TEXT,
    @Duracion TEXT,
    @Instrucciones TEXT
AS
BEGIN
    INSERT INTO Recetas (PacienteID, MedicoID, FechaEmision, ConsultaID, Medicamento, Dosis, Frecuencia, Duracion, Instrucciones)
    VALUES (@PacienteID, @MedicoID, @FechaEmision, @ConsultaID, @Medicamento, @Dosis, @Frecuencia, @Duracion, @Instrucciones);

    -- Devuelve el ID de la receta reci�n insertada
    SELECT SCOPE_IDENTITY() AS RecetaID;
END;


-- Ejemplo de ejecuci�n del procedimiento
EXEC InsertarReceta 
    @PacienteID = 1,
    @MedicoID = 1,
    @FechaEmision = '2024-11-10',
    @ConsultaID = 1,
    @Medicamento = 'Antibi�tico',
    @Dosis = '100 mg',
    @Frecuencia = 'Cada 8 horas',
    @Duracion = '7 d�as',
    @Instrucciones = 'Tomar despu�s de las comidas.';



-- 3. PROCEDIMIENTO PARA EDITAR LA RECETA
GO
CREATE PROCEDURE EditarReceta
    @RecetaID INT,
    @PacienteID INT,
    @MedicoID INT,
    @FechaEmision DATETIME,
    @ConsultaID INT,
    @Medicamento VARCHAR(100),
    @Dosis VARCHAR(50),
    @Frecuencia TEXT,
    @Duracion TEXT,
    @Instrucciones TEXT
AS
BEGIN
    UPDATE Recetas
    SET 
        PacienteID = @PacienteID,
        MedicoID = @MedicoID,
        FechaEmision = @FechaEmision,
        ConsultaID = @ConsultaID,
        Medicamento = @Medicamento,
        Dosis = @Dosis,
        Frecuencia = @Frecuencia,
        Duracion = @Duracion,
        Instrucciones = @Instrucciones
    WHERE RecetaID = @RecetaID;

    -- Verificar si se ha actualizado alg�n registro
    IF @@ROWCOUNT > 0
    BEGIN
        SELECT 'Receta actualizada correctamente' AS Resultado;
    END
    ELSE
    BEGIN
        SELECT 'No se encontr� la receta con el ID especificado' AS Resultado;
    END
END;



-- Ejemplo de ejecuci�n del procedimiento
EXEC EditarReceta
    @RecetaID = 1,
    @PacienteID = 1,
    @MedicoID = 1,
    @FechaEmision = '2024-11-15',
    @ConsultaID = 1,
    @Medicamento = 'Antiinflamatorio',
    @Dosis = '75 mg',
    @Frecuencia = 'Cada 12 horas',
    @Duracion = '5 d�as',
    @Instrucciones = 'Tomar con agua despu�s de las comidas.';




---4. BUSCAR RECETAS POR DUI
GO
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





------------------------------------------- PROCEDIMIENTO ALMACENADO CRUD DE LOS EXAMENES M�DICOS ---------------------------------------------
-- SP para insertar ExamenMedico
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
        -- Insertar el examen m�dico
        INSERT INTO ExamenesMedicos 
        (PacienteID, ConsultaID, TipoExamen, FechaExamen, Resultado, Observaciones)
        VALUES 
        (@PacienteID, @ConsultaID, @TipoExamen, @FechaExamen, @Resultado, @Observaciones);
        
        -- Confirmar la transacci�n
        COMMIT TRANSACTION;
        
        -- Devolver el ID del examen reci�n insertado
        SELECT SCOPE_IDENTITY() AS ExamenID;
    END TRY
    BEGIN CATCH
        -- Revertir transacci�n en caso de error
        ROLLBACK TRANSACTION;
        THROW; -- Lanza el error para manejarlo en la aplicaci�n
    END CATCH;
END;

--SP para Editar ExamenMedico
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
        -- Actualizar los datos del examen m�dico
        UPDATE ExamenesMedicos
        SET 
            PacienteID = @PacienteID,
            ConsultaID = @ConsultaID, -- Si es NULL, se establecer� en NULL
            TipoExamen = @TipoExamen,
            FechaExamen = @FechaExamen,
            Resultado = @Resultado,
            Observaciones = @Observaciones
        WHERE ExamenID = @ExamenID;

        -- Confirmar la transacci�n
        COMMIT TRANSACTION;

        -- Retornar el ID del examen actualizado
        SELECT @ExamenID AS ExamenID;
    END TRY
    BEGIN CATCH
        -- Revertir la transacci�n en caso de error
        ROLLBACK TRANSACTION;

        -- Manejo de errores
        PRINT 'Error al actualizar el examen m�dico: ' + ERROR_MESSAGE();
    END CATCH;
END;

--SP para eliminar ExamenMedico
GO

CREATE PROCEDURE EliminarExamenMedico
    @ExamenID INT
AS
BEGIN
    -- Actualizar el estado del examen para ocultarlo en lugar de eliminarlo permanentemente
    UPDATE ExamenesMedicos
    SET Observaciones = 'Este examen ha sido eliminado' -- Marcamos con una observaci�n de eliminaci�n
    WHERE ExamenID = @ExamenID;

    PRINT 'El examen m�dico ha sido eliminado correctamente.';
END;
GO


--Datos de prueba
EXEC InsertarExamenMedico
    @PacienteID = 1,          -- ID del paciente
    @ConsultaID = 2,          -- ID de la consulta (puede ser NULL si no se tiene)
    @TipoExamen = 'Radiograf�a',
    @FechaExamen = '2024-11-09',
    @Resultado = 'Normal',
    @Observaciones = 'Sin observaciones adicionales';


select * from ExamenesMedicos

	EXEC EditarExamenMedico
    @ExamenID = 1,           -- ID del examen a editar
    @PacienteID = 1,         -- Nuevo ID de paciente
    @ConsultaID = 2,         -- Nuevo ID de consulta (puede ser NULL)
    @TipoExamen = 'Tomograf�a',
    @FechaExamen = '2024-11-10',
    @Resultado = 'An�malo',
    @Observaciones = 'Requiere an�lisis�adicional';
-- FIN datos prueba
GO

--SP para mostrar ExamenesMedicos

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

-- Datos de prueba
exec MostrarExamenesMedicos
select * from Usuarios
--FIN datos prueba
-----------------------------------------------



-- 5. BUSCAR EL EX�MEN POR FECHA DE EXAMEN
GO
CREATE PROCEDURE BuscarExamenPorFecha
    @FechaExamen DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        e.ExamenID AS ID,
        CONCAT(p.NombrePaciente, ' ', p.ApellidoPaciente) AS Pacientes,
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
    WHERE 
        e.FechaExamen = @FechaExamen
    ORDER BY 
        e.ExamenID ASC; -- Ordenar por el ID del examen en orden ascendente
END;




----------------------------------------------------            PROCEDIMIENTOS PARA REALIZAR LOS REPORTES            ---------------------------------------------

-- 1. Obtener el expediente del paciente
GO
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
        
        -- Historial de Consultas M�dicas del Paciente (Subconsulta con salto de l�nea)
        (SELECT TOP 1 
            CONCAT('Fecha Consulta: ', CM.FechaConsulta, CHAR(13) + CHAR(10), 
                   'Altura: ', CM.Altura, CHAR(13) + CHAR(10), 
                   'Peso: ', CM.Peso, CHAR(13) + CHAR(10),
                   'Alergia: ', CM.Alergia, CHAR(13) + CHAR(10), 
                   'S�ntomas: ', CM.Sintomas, CHAR(13) + CHAR(10), 
                   'Diagn�stico: ', CM.Diagnostico, CHAR(13) + CHAR(10), 
                   'Observaciones: ', CM.Observaciones)
         FROM ConsultasMedicas CM
         WHERE CM.PacienteID = P.PacienteID
         ORDER BY CM.FechaConsulta DESC) AS UltimaConsulta,

        -- Ex�menes M�dicos del Paciente (Subconsulta con salto de l�nea)
        (SELECT TOP 1 
            CONCAT('Tipo de Examen: ', EM.TipoExamen, CHAR(13) + CHAR(10),
                   'Fecha Examen: ', EM.FechaExamen, CHAR(13) + CHAR(10),
                   'Resultado: ', EM.Resultado, CHAR(13) + CHAR(10),
                   'Observaciones: ', EM.Observaciones)
         FROM ExamenesMedicos EM
         WHERE EM.PacienteID = P.PacienteID
         ORDER BY EM.FechaExamen DESC) AS UltimoExamen,

        -- Recetas M�dicas del Paciente (Subconsulta con salto de l�nea)
        (SELECT TOP 1 
            CONCAT('Fecha Emisi�n: ', CONVERT(VARCHAR(10), R.FechaEmision, 103), CHAR(13) + CHAR(10),
                   'Medicamento: ', R.Medicamento, CHAR(13) + CHAR(10),
                   'Dosis: ', R.Dosis, CHAR(13) + CHAR(10),
                   'Frecuencia: ', R.Frecuencia, CHAR(13) + CHAR(10),
                   'Duraci�n: ', R.Duracion, CHAR(13) + CHAR(10),
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



-- 2. OBTENER EL REPORTE DE RECETAS POR PACIENTE Y FECHA
GO
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



-- 3. OBTENER EL REPORTE DE LAS CONSULTAS POR M�DICO CON LA ESPECIALIDAD Y DUI
GO
CREATE PROCEDURE ObtenerReporteConsultasPorMedicoODUIEspecialidad
    @DUIMedico VARCHAR(10) = NULL,
    @EspecialidadID INT = NULL
AS 
BEGIN 
    SET NOCOUNT ON;

    SELECT 
        -- Datos del m�dico
        Med.MedicoID,
        CONCAT(Med.NombreMedico, ' ', Med.ApellidoMedico) AS NombreCompletoMedico,
        Med.DUIMedico,
        E.NombreEspecialidad AS Especialidad,
        
        -- Datos del paciente
        P.PacienteID,
        CONCAT(P.NombrePaciente, ' ', P.ApellidoPaciente) AS NombreCompletoPaciente,
        P.DUIPaciente,
        
        -- Detalles de la consulta
        C.FechaConsulta,
        C.Altura,
        C.Peso,
        C.Alergia,
        C.Sintomas,
        C.Diagnostico,
        C.Observaciones
    FROM 
        ConsultasMedicas C
    INNER JOIN 
        Medicos Med ON C.MedicoID = Med.MedicoID
    INNER JOIN 
        Especialidades E ON Med.EspecialidadID = E.EspecialidadID
    INNER JOIN 
        Pacientes P ON C.PacienteID = P.PacienteID
    WHERE 
        (@DUIMedico IS NULL OR Med.DUIMedico = @DUIMedico)
        AND (@EspecialidadID IS NULL OR E.EspecialidadID = @EspecialidadID)
    ORDER BY 
        C.FechaConsulta DESC;
END;




-- 4. OBTENER EL REPORTE DE LAS RECETAS POR PACIENTE Y FECHA
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
        
        -- Recetas M�dicas del Paciente
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

select * from Usuarios




--------------------------------       PROCEDIMIENTOS PARA OBTENER EL DEPARTAMENTO Y LOS MUNICIPIOS CORRESPONDIENTES A CADA UNO DE ELLOS         -------------------------------------
-- 1. PROCEDIMIENTO PARA OBTENER LOS DEPARTAMENTOS
GO
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
---2. PROCEDIMIENTO PARA OBTENER MUNICIPIO SEGUN EL DEPARTAMENTO SELECCIONADO
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