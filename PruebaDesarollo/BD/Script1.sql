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



