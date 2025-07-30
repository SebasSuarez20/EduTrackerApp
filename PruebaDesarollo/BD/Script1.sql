CREATE DATABASE StudentRegDB;

USE StudentRegDB;

CREATE TABLE Teachers(
   Idcontrol INT NOT NULL AUTO_INCREMENT COMMENT 'Campo en la cual autocrimenta con insercion respectiva.',
   Identification VARCHAR(125) NOT NULL COMMENT 'Identificacion tal como la cedula del ciudadano.',
   FirstName VARCHAR(50) NOT NULL COMMENT 'Primer nombre del profesor.',
   FirstSurname VARCHAR(50) NOT NULL COMMENT 'Primer apellido del profesor.',
   Email VARCHAR(100) NOT NULL COMMENT 'Correo electronico del profesor.',
   Enabled BOOL NOT NULL DEFAULT TRUE COMMENT 'Borrado logico dentro nuestra DB',
   Username VARCHAR(50) NOT NULL COMMENT 'Campo en todas las tabla para saber que usuario fue el que hizo la insercion.',
   EntryDate DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Campo en todas las tabla para saber en que horario se hizo la insercion de la data.',
   PRIMARY KEY(Idcontrol),
   INDEX(Identification),
   UNIQUE(Identification,Enabled)
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE Students(
   Idcontrol INT NOT NULL AUTO_INCREMENT COMMENT 'Campo en la cual autocrimenta con insercion respectiva.',
   Identification VARCHAR(125) NOT NULL COMMENT 'Identificacion tal como la cedula del ciudadano.',
   FirstName VARCHAR(50) NOT NULL COMMENT 'Primer nombre del estudiante.',
   FirstSurname VARCHAR(50) NOT NULL COMMENT 'Primer apellido del estudiante.',
   Email VARCHAR(100) NOT NULL COMMENT 'Correo electronico del estudiante.',
   Enabled BOOL NOT NULL DEFAULT TRUE COMMENT 'Borrado logico dentro nuestra DB',
   Username VARCHAR(50) NOT NULL COMMENT 'Campo en todas las tabla para saber que usuario fue el que hizo la insercion.',
   EntryDate DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Campo en todas las tabla para saber en que horario se hizo la insercion de la data.',
   PRIMARY KEY(Idcontrol),
   INDEX(Identification),
   UNIQUE(Identification,Enabled)
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE Subjects(
 Idcontrol INT NOT NULL AUTO_INCREMENT COMMENT 'Campo en la cual autocrimenta con insercion respectiva.',
 Name TEXT NOT NULL COMMENT 'Nombre de la materia.',
     Enabled BOOL NOT NULL DEFAULT TRUE COMMENT 'Borrado logico dentro nuestra DB',
	Username VARCHAR(50) NOT NULL COMMENT 'Campo en todas las tabla para saber que usuario fue el que hizo la insercion.',
    EntryDate DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Campo en todas las tabla para saber en que horario se hizo la insercion de la data.',
    PRIMARY KEY(Idcontrol)
);

CREATE TABLE SubjectsAssignation (
	Idcontrol INT NOT NULL AUTO_INCREMENT COMMENT 'Campo en la cual autocrimenta con insercion respectiva.',
	Name TEXT NOT NULL COMMENT 'Nombre de la materia.',
    Day ENUM('1','2','3','4','5','6') COMMENT 'Dias de la semana',
    HoursInitial TIME NOT NULL COMMENT 'Hora inicial de la clase',
    HoursFinal TIME NOT NULL COMMENT 'Hora final de la clase',
    FkIdControlSubject INT NOT NULL COMMENT 'Idcontrol de la materia selecccionada',
    Enabled BOOL NOT NULL DEFAULT TRUE COMMENT 'Borrado logico dentro nuestra DB',
	Username VARCHAR(50) NOT NULL COMMENT 'Campo en todas las tabla para saber que usuario fue el que hizo la insercion.',
    EntryDate DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Campo en todas las tabla para saber en que horario se hizo la insercion de la data.',
    PRIMARY KEY(Idcontrol),
    FOREIGN KEY(FkIdControlSubject) REFERENCES Subjects(Idcontrol)
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE SubjectsWithTeacher(
	Idcontrol INT NOT NULL AUTO_INCREMENT COMMENT 'Campo en la cual autocrimenta con insercion respectiva.',
	IdFkTeacher INT NOT NULL COMMENT 'Se podria unir el id de la tabla profesor.',
	IdFkSubject INT NOT NULL COMMENT 'Se podria unir el id de la tabla materias.',
	Enabled BOOL NOT NULL DEFAULT TRUE COMMENT 'Borrado logico dentro nuestra DB',
   	Username VARCHAR(50) NOT NULL COMMENT 'Campo en todas las tabla para saber que usuario fue el que hizo la insercion.',
    EntryDate DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Campo en todas las tabla para saber en que horario se hizo la insercion de la data.',
    FOREIGN KEY(IdFkTeacher) REFERENCES Teachers(Idcontrol),
    FOREIGN KEY(IdFkSubject) REFERENCES Subjects(Idcontrol),
    PRIMARY KEY(Idcontrol),
    UNIQUE(IdFkTeacher,IdFkSubject,Enabled)
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE StudentWithSubjects(
	Idcontrol INT NOT NULL AUTO_INCREMENT COMMENT 'Campo en la cual autocrimenta con insercion respectiva.',
	IdFkStudent INT NOT NULL COMMENT 'Se podria unir el id de la tabla estudiantes.',
	IdFkSubject INT NOT NULL COMMENT 'Se podria unir el id de la tabla materias.',
	IdFkSubjectAssignation INT NOT NULL COMMENT 'Se podria unir el id de la tabla materias asignadas.',
    Enabled BOOL NOT NULL DEFAULT TRUE COMMENT 'Borrado logico dentro nuestra DB',
   	Username VARCHAR(50) NOT NULL COMMENT 'Campo en todas las tabla para saber que usuario fue el que hizo la insercion.',
    EntryDate DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Campo en todas las tabla para saber en que horario se hizo la insercion de la data.',
    FOREIGN KEY(IdFkStudent) REFERENCES Students(Idcontrol),
    FOREIGN KEY(IdFkSubject) REFERENCES Subjects(Idcontrol),
    PRIMARY KEY(Idcontrol)
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE Users(
	Idcontrol INT NOT NULL AUTO_INCREMENT COMMENT 'Campo en la cual autocrimenta con insercion respectiva.',
    Identification VARCHAR(100) NOT NULL COMMENT 'Correo electronico del estudiante.',
    Password VARCHAR(255) NOT NULL COMMENT 'Contrasena para ingresar al aplicativo.',
    Rol ENUM('0','1','2') NOT NULL COMMENT '(0) => Profesor. , (1) => Estudiantes. (2)=> Administativo del aplicativo.',
	Enabled BOOL NOT NULL DEFAULT TRUE COMMENT 'Borrado logico dentro nuestra DB',
	Username VARCHAR(50) NOT NULL COMMENT 'Campo en todas las tabla para saber que usuario fue el que hizo la insercion.',
    EntryDate DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Campo en todas las tabla para saber en que horario se hizo la insercion de la data.',
    UNIQUE(Identification,Enabled),
    INDEX(Identification),
    PRIMARY KEY(Idcontrol)
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

#Insercion de tabla Profesor.

INSERT INTO Teachers (Identification, FirstName, FirstSurname, Email, Username)
VALUES 
('1000123456', 'Carlos', 'Gómez', 'carlos.gomez@example.com', 'admin'),
('1000456789', 'María', 'Rodríguez', 'maria.rodriguez@example.com', 'admin'),
('1000789012', 'Luis', 'Fernández', 'luis.fernandez@example.com', 'admin'),
('1000987654', 'Ana', 'Martínez', 'ana.martinez@example.com', 'admin'),
('1000234567', 'Jorge', 'Ramírez', 'jorge.ramirez@example.com', 'admin');

#Insercion de tabla Estudiantes.

INSERT INTO Students (Identification, FirstName, FirstSurname, Email, Username)
VALUES 
('1012345670', 'Andrea', 'López', 'andrea.lopez@example.com', 'admin'),
('1012345671', 'Felipe', 'García', 'felipe.garcia@example.com', 'admin'),
('1012345672', 'Camila', 'Torres', 'camila.torres@example.com', 'admin'),
('1012345673', 'Diego', 'Morales', 'diego.morales@example.com', 'admin'),
('1012345674', 'Laura', 'Ramírez', 'laura.ramirez@example.com', 'admin'),
('1012345675', 'Santiago', 'Pérez', 'santiago.perez@example.com', 'admin'),
('1012345676', 'Valentina', 'Mendoza', 'valentina.mendoza@example.com', 'admin'),
('1012345677', 'Mateo', 'Castro', 'mateo.castro@example.com', 'admin'),
('1012345678', 'Isabella', 'Suárez', 'isabella.suarez@example.com', 'admin'),
('1012345679', 'Juan', 'Ortiz', 'juan.ortiz@example.com', 'admin');


INSERT INTO Subjects (Name, Username) VALUES
('Matemáticas', 'admin'),
('Lengua Castellana', 'admin'),
('Física', 'admin'),
('Química', 'admin'),
('Biología', 'admin'),
('Historia', 'admin'),
('Geografía', 'admin'),
('Educación Física', 'admin'),
('Inglés', 'admin'),
('Tecnología e Informática', 'admin');

#Insercion de las Materias.

INSERT INTO SubjectsAssignation (Name, Day, HoursInitial, HoursFinal, Username,FkIdControlSubject) VALUES
-- Matemáticas: Lunes, Miércoles, Viernes de 07:00 a 08:00
('Matemáticas', '1', '07:00:00', '08:00:00', 'admin',1),
('Matemáticas', '3', '07:00:00', '08:00:00', 'admin',1),
('Matemáticas', '5', '07:00:00', '08:00:00', 'admin',1),

-- Lengua Castellana: Lunes, Miércoles, Viernes de 08:00 a 09:00
('Lengua Castellana', '1', '08:00:00', '09:00:00', 'admin',2),
('Lengua Castellana', '3', '08:00:00', '09:00:00', 'admin',2),
('Lengua Castellana', '5', '08:00:00', '09:00:00', 'admin',2),

-- Física: Lunes, Miércoles, Viernes de 09:00 a 10:00
('Física', '1', '09:00:00', '10:00:00', 'admin',3),
('Física', '3', '09:00:00', '10:00:00', 'admin',3),
('Física', '5', '09:00:00', '10:00:00', 'admin',3),

-- Química: Lunes, Miércoles, Viernes de 10:00 a 11:00
('Química', '1', '10:00:00', '11:00:00', 'admin',4),
('Química', '3', '10:00:00', '11:00:00', 'admin',4),
('Química', '5', '10:00:00', '11:00:00', 'admin',4),

-- Biología: Lunes, Miércoles, Viernes de 11:00 a 12:00
('Biología', '1', '7:00:00', '08:00:00', 'admin',5),
('Biología', '3', '11:00:00', '12:00:00', 'admin',5),
('Biología', '5', '11:00:00', '12:00:00', 'admin',5),

-- Historia: Martes, Jueves, Sábado de 07:00 a 08:00
('Historia', '2', '07:00:00', '08:00:00', 'admin',6),
('Historia', '4', '07:00:00', '08:00:00', 'admin',6),
('Historia', '6', '07:00:00', '08:00:00', 'admin',6),

-- Geografía: Martes, Jueves, Sábado de 08:00 a 09:00
('Geografía', '2', '08:00:00', '09:00:00', 'admin',7),
('Geografía', '4', '08:00:00', '09:00:00', 'admin',7),
('Geografía', '6', '08:00:00', '09:00:00', 'admin',7),

-- Educación Física: Martes, Jueves, Sábado de 09:00 a 10:00
('Educación Física', '2', '09:00:00', '10:00:00', 'admin',8),
('Educación Física', '4', '09:00:00', '10:00:00', 'admin',8),
('Educación Física', '6', '09:00:00', '10:00:00', 'admin',8),

-- Inglés: Martes, Jueves, Sábado de 10:00 a 11:00
('Inglés', '2', '10:00:00', '11:00:00', 'admin',9),
('Inglés', '4', '10:00:00', '11:00:00', 'admin',9),
('Inglés', '6', '10:00:00', '11:00:00', 'admin',9),

-- Tecnología e Informática: Martes, Jueves, Sábado de 11:00 a 12:00
('Tecnología e Informática', '2', '11:00:00', '12:00:00', 'admin',10),
('Tecnología e Informática', '4', '11:00:00', '12:00:00', 'admin',10),
('Tecnología e Informática', '6', '11:00:00', '12:00:00', 'admin',10);

INSERT INTO SubjectsWithTeacher (IdFkTeacher, IdFkSubject, Enabled, Username)
VALUES
  (1, 10, 1, 'admin'),
  (2, 2, 1, 'admin'),
  (2, 4, 1, 'admin'),
  (3, 9, 1, 'admin'),
  (3, 6, 1, 'admin'),
  (4, 1, 1, 'admin'),
  (4, 3, 1, 'admin'),
  (5, 7, 1, 'admin');


#Insercion en usuarios.
INSERT INTO Users (Identification, Password, Rol, Enabled, Username)
VALUES ('1234567890', 'profesor123', '1', TRUE, 'admin');

INSERT INTO Users (Identification, Password, Rol, Enabled, Username)
VALUES ('1098765432', 'estudiante123', '2', TRUE, 'admin');



