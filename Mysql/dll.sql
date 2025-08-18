DROP DATABASE CafeColombiano;
CREATE DATABASE IF NOT EXISTS CafeColombiano;
USE CafeColombiano;
CREATE TABLE IF NOT EXISTS GrupoGenetico (
  id INT NOT NULL AUTO_INCREMENT,
  nombre_grupo VARCHAR(100) NOT NULL UNIQUE,
  origen VARCHAR(255),
  PRIMARY KEY (id)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS Porte (
  id INT NOT NULL AUTO_INCREMENT,
  nombre_porte VARCHAR(50) NOT NULL UNIQUE,
  PRIMARY KEY (id)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS TamanoGrano (
  id INT NOT NULL AUTO_INCREMENT,
  nombre_tamano VARCHAR(50) NOT NULL ,
  PRIMARY KEY (id)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS AltitudOptima (
  id INT NOT NULL AUTO_INCREMENT,
  rango_altitud VARCHAR(100) NOT NULL UNIQUE,
  descripcion TEXT,
  PRIMARY KEY (id)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS PotencialRendimiento (
  id INT NOT NULL AUTO_INCREMENT,
  nivel_rendimiento VARCHAR(50) NOT NULL UNIQUE,
  PRIMARY KEY (id)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS CalidadGrano (
  id INT NOT NULL AUTO_INCREMENT,
  nivel_calidad VARCHAR(50) NOT NULL ,
  descripcion TEXT,
  PRIMARY KEY (id)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS Variedad (
  id INT NOT NULL AUTO_INCREMENT,
  nombre_comun VARCHAR(255) NOT NULL UNIQUE,
  nombre_cientifico VARCHAR(255) NOT NULL UNIQUE,
  descripcion_general TEXT,
  imagen_referencia_url VARCHAR(512),
  historia_linaje TEXT,
  id_grupo_genetico INT NOT NULL,
  id_porte INT NOT NULL,
  id_tamano_grano INT NOT NULL,
  id_altitud_optima INT NOT NULL,
  id_potencial_rendimiento INT NOT NULL,
  id_calidad_grano INT NOT NULL,
  PRIMARY KEY (id),
  CONSTRAINT fk_variedad_grupo_genetico
    FOREIGN KEY (id_grupo_genetico)
    REFERENCES GrupoGenetico (id)
    ON DELETE NO ACTION,
  CONSTRAINT fk_variedad_porte
    FOREIGN KEY (id_porte)
    REFERENCES Porte (id)
    ON DELETE NO ACTION,
  CONSTRAINT fk_variedad_tamano_grano
    FOREIGN KEY (id_tamano_grano)
    REFERENCES TamanoGrano (id)
    ON DELETE NO ACTION,
  CONSTRAINT fk_variedad_altitud_optima
    FOREIGN KEY (id_altitud_optima)
    REFERENCES AltitudOptima (id)
    ON DELETE NO ACTION,
  CONSTRAINT fk_variedad_potencial_rendimiento
    FOREIGN KEY (id_potencial_rendimiento)
    REFERENCES PotencialRendimiento (id)
    ON DELETE NO ACTION,
  CONSTRAINT fk_variedad_calidad_grano
    FOREIGN KEY (id_calidad_grano)
    REFERENCES CalidadGrano (id)
    ON DELETE NO ACTION
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS InformacionAgronomica (
  id_variedad INT NOT NULL,
  tiempo_cosecha VARCHAR(255),
  maduracion VARCHAR(255),
  nutricion TEXT,
  densidad_siembra VARCHAR(255),
  PRIMARY KEY (id_variedad),
  CONSTRAINT fk_info_agronomica_variedad
    FOREIGN KEY (id_variedad)
    REFERENCES Variedad (id)
    ON DELETE CASCADE
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS TipoResistencia (
  id INT NOT NULL AUTO_INCREMENT,
  nombre_tipo VARCHAR(100) NOT NULL UNIQUE,
  PRIMARY KEY (id)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS NivelResistencia (
  id INT NOT NULL AUTO_INCREMENT,
  nombre_nivel VARCHAR(50) NOT NULL UNIQUE,
  PRIMARY KEY (id)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS VariedadResistencia (
  id_variedad INT NOT NULL,
  id_tipo_resistencia INT NOT NULL,
  id_nivel_resistencia INT NOT NULL,
  PRIMARY KEY (id_variedad, id_tipo_resistencia),
  CONSTRAINT fk_variedad_resistencia_variedad
    FOREIGN KEY (id_variedad)
    REFERENCES Variedad (id)
    ON DELETE CASCADE,
  CONSTRAINT fk_variedad_resistencia_tipo_resistencia
    FOREIGN KEY (id_tipo_resistencia)
    REFERENCES TipoResistencia (id)
    ON DELETE NO ACTION,
  CONSTRAINT fk_variedad_resistencia_nivel_resistencia
    FOREIGN KEY (id_nivel_resistencia)
    REFERENCES NivelResistencia (id)
    ON DELETE NO ACTION
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS Usuario (
  id INT NOT NULL AUTO_INCREMENT,
  nombre_usuario VARCHAR(100) NOT NULL UNIQUE,
  contrasena VARCHAR(255) NOT NULL,
  PRIMARY KEY (id)
) ENGINE = InnoDB;