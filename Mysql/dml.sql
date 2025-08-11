-- Inserts para la tabla GrupoGenetico
INSERT INTO GrupoGenetico (nombre_grupo, origen) VALUES
('Typica', 'Yemen, Etiopía'),
('Bourbon', 'Reunión (Isla Borbón)'),
('SL (Scott Labs)', 'Kenia'),
('Catimor', 'Portugal (Timor)');

-- Inserts para la tabla Porte
INSERT INTO Porte (nombre_porte) VALUES
('Alto'),
('Mediano'),
('Bajo');

-- Inserts para la tabla TamanoGrano
INSERT INTO TamanoGrano (nombre_tamano) VALUES
('Grande'),
('Mediano'),
('Pequeno');

-- Inserts para la tabla AltitudOptima
INSERT INTO AltitudOptima (rango_altitud, descripcion) VALUES
('1200 - 1600 msnm', 'Ideal para variedades que se adaptan a altitudes intermedias.'),
('1600 - 2000 msnm', 'Excelente para cafés de alta calidad, con desarrollo de acidez y complejidad.'),
('800 - 1200 msnm', 'Apropiada para variedades de bajo porte y alta productividad.');

-- Inserts para la tabla PotencialRendimiento
INSERT INTO PotencialRendimiento (nivel_rendimiento) VALUES
('Alto'),
('Medio'),
('Bajo');

-- Inserts para la tabla CalidadGrano
INSERT INTO CalidadGrano (nivel_calidad, descripcion) VALUES
('Excelente', 'Cualidades sensoriales sobresalientes, aroma y sabor complejos.'),
('Bueno', 'Sabor y aroma agradables y equilibrados.'),
('Estándar', 'Calidad promedio, apta para consumo masivo.');

-- Inserts para la tabla TipoResistencia
INSERT INTO TipoResistencia (nombre_tipo) VALUES
('Roya del cafe'),
('Oidio'),
('Broca del cafe');

-- Inserts para la tabla NivelResistencia
INSERT INTO NivelResistencia (nombre_nivel) VALUES
('Alta'),
('Media'),
('Baja');

-- Inserts para la tabla Variedad
-- Se asume que los IDs de las tablas anteriores se insertan en orden secuencial (1, 2, 3...)
INSERT INTO Variedad (
    nombre_comun,
    nombre_cientifico,
    descripcion_general,
    imagen_referencia_url,
    historia_linaje,
    id_grupo_genetico,
    id_porte,
    id_tamano_grano,
    id_altitud_optima,
    id_potencial_rendimiento,
    id_calidad_grano
) VALUES
('Caturra', 'Coffea arabica var. Caturra', 'Una mutación natural del café Bourbon, conocida por su alta productividad.', 'https://placehold.co/512x512/000000/FFFFFF?text=Caturra', 'Descubierta en Brasil en la década de 1930.', 2, 3, 2, 2, 1, 2),
('Castillo', 'Coffea arabica var. Castillo', 'Una variedad híbrida desarrollada en Colombia, con alta resistencia a la roya.', 'https://placehold.co/512x512/000000/FFFFFF?text=Castillo', 'Desarrollada por el Cenicafé, lanzada en 2005.', 4, 2, 1, 1, 1, 2),
('Colombia', 'Coffea arabica var. Colombia', 'Primera variedad de café resistente a la roya desarrollada en Colombia.', 'https://placehold.co/512x512/000000/FFFFFF?text=Colombia', 'Desarrollada por el Cenicafé en la década de 1980.', 4, 2, 2, 2, 1, 2),
('Typica', 'Coffea arabica var. Typica', 'Una de las variedades más antiguas y genéticamente puras, con excelente calidad en taza.', 'https://placehold.co/512x512/000000/FFFFFF?text=Typica', 'Expandida globalmente a partir de Yemen.', 1, 1, 2, 2, 3, 1),
('Geisha', 'Coffea arabica var. Geisha', 'Famosa por su perfil de taza excepcional, con notas florales y frutales.', 'https://placehold.co/512x512/000000/FFFFFF?text=Geisha', 'Originaria de Etiopía, redescubierta y popularizada en Panamá.', 1, 1, 1, 2, 2, 1);

-- Inserts para la tabla InformacionAgronomica
-- Se asume que los IDs de las variedades se insertan en orden secuencial (1, 2, 3...)
INSERT INTO InformacionAgronomica (id_variedad, tiempo_cosecha, maduracion, nutricion, densidad_siembra) VALUES
(1, '8 a 10 meses después de floración', 'Uniforme', 'Nitrógeno, Fósforo, Potasio', '5000 a 7000 plantas/ha'),
(2, '9 a 11 meses después de floración', 'Ligeramente escalonada', 'Requiere fertilización balanceada', '5000 a 7000 plantas/ha'),
(3, '9 a 11 meses después de floración', 'Ligeramente escalonada', 'Buena respuesta a la fertilización foliar', '5000 a 7000 plantas/ha'),
(4, '9 a 11 meses después de floración', 'Tardía y escalonada', 'Requiere suelos ricos en materia orgánica', '3000 a 4000 plantas/ha'),
(5, '10 a 12 meses después de floración', 'Muy escalonada', 'Sensible a deficiencias nutricionales', '3500 a 5000 plantas/ha');

-- Inserts para la tabla VariedadResistencia
-- Variedad 1 (Caturra): Baja resistencia a la roya.
-- Variedad 2 (Castillo): Alta resistencia a la roya.
-- Variedad 3 (Colombia): Alta resistencia a la roya.
-- Se asume que los IDs de variedades, tipos y niveles se insertan en orden secuencial.
INSERT INTO VariedadResistencia (id_variedad, id_tipo_resistencia, id_nivel_resistencia) VALUES
(1, 1, 3), -- Caturra: Baja resistencia a la roya
(2, 1, 1), -- Castillo: Alta resistencia a la roya
(3, 1, 1), -- Colombia: Alta resistencia a la roya
(4, 1, 3), -- Typica: Baja resistencia a la roya
(5, 1, 3), -- Geisha: Baja resistencia a la roya
(2, 2, 2); -- Castillo: Media resistencia al oídio

-- Inserts para la tabla Usuario
INSERT INTO Usuario (nombre_usuario, contrasena) VALUES
('admin_cafe', 'password123'),
('invitado', 'guest_pass');