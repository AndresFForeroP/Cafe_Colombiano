-- Grupos genéticos
INSERT INTO GrupoGenetico (nombre_grupo, origen) VALUES
('Arábigo', 'Etiopía'),
('Guinea', 'África central-occidental'),
('Congo', 'África central'),
('Uganda', 'Uganda'),
('Guinea x Congo', 'Híbrido'),
('Guinea x Coffea congensis', 'Híbrido');

-- Porte
INSERT INTO Porte (nombre_porte) VALUES
('Alto'),
('Bajo'),
('Dwarf/Compact'),
('Tall'),
('Desconocido');

-- Tamaño de grano
INSERT INTO TamanoGrano (nombre_tamano) VALUES
('Pequeño (tamaño de pantalla 14 o menos)'),
('Mediano (tamaño de pantalla 15-16)'),
('Grande (tamaño de pantalla >17)'),
('Muy grande'),
('Desconocido');

-- Altitud óptima
INSERT INTO AltitudOptima (rango_altitud, descripcion) VALUES
('500-1000 msnm', 'Zona cafetera colombiana'),
('400-900 msnm', 'Zonas bajas para Robusta'),
('500-800 msnm', 'Zonas medias para Robusta'),
('700 msnm', 'Zonas específicas como Chiapas, México'),
('1200-1800 msnm', 'Zonas altas para Arábigo');

-- Potencial de rendimiento
INSERT INTO PotencialRendimiento (nivel_rendimiento) VALUES
('Bajo (menos de 1500 kg/ha)'),
('Medio (1500-3000 kg/ha)'),
('Alto (3000-5000 kg/ha)'),
('Muy alto (más de 5000 kg/ha)'),
('Desconocido');

-- Calidad de grano
INSERT INTO CalidadGrano (nivel_calidad, descripcion) VALUES
('Excelente', 'Puntaje SCA >85'),
('Muy buena', 'Puntaje SCA 80-85'),
('Buena', 'Puntaje SCA 75-80'),
('Regular', 'Puntaje SCA 70-75'),
('Básica', 'Puntaje SCA <70'),
('Desconocida', 'No especificada');

-- Tipos de resistencia
INSERT INTO TipoResistencia (nombre_tipo) VALUES
('Roya del cafeto'),
('Enfermedad de la cereza del café (CBD)'),
('Nematodos'),
('Broca del café'),
('Barrenador del tallo (Xylosandus compactus)'),
('Marchitez del café (CWD)'),
('Antracnosis'),
('Enfermedad de la ampolla roja');

-- Niveles de resistencia
INSERT INTO NivelResistencia (nombre_nivel) VALUES
('Resistente'),
('Tolerante'),
('Susceptible'),
('Desconocido'); 
-- Insertar variedades colombianas
INSERT INTO Variedad (nombre_comun, nombre_cientifico, descripcion_general, imagen_referencia_url, historia_linaje, id_grupo_genetico, id_porte, id_tamano_grano, id_altitud_optima, id_potencial_rendimiento, id_calidad_grano) VALUES
('Típica', 'Coffea arabica var. typica', 'Variedad tradicional con hojas nuevas bronceadas o rojizas, de forma alargada. También llamada arábigo, pajarito o nacional.', "https://i.pinimg.com/1200x/69/2e/f5/692ef5aba77ebab9979c73584369c232.jpg", 'Una de las variedades más antiguas de café arábigo, originaria de Etiopía.', 1, 1, 2, 5, 2, 3),
('Borbón', 'Coffea arabica var. bourbon', 'Variedad con cogollos de color verde claro, hojas redondeadas y mayor número de ramas que Típica.', "https://i.pinimg.com/1200x/e5/29/3d/e5293d7c859011b511092d0ebbe4aa2a.jpg", 'Originaria de la Isla de Borbón (ahora Reunión), derivada de Típica.', 1, 1, 2, 5, 2, 3),
('Maragogipe', 'Coffea arabica var. maragogype', 'Variedad de grano muy grande, mutación natural de Típica encontrada en Brasil.', "https://i.pinimg.com/1200x/01/46/af/0146af31dc2e15fc5f1eb1b659ebc153.jpg", 'Descubierta en Maragogipe, Bahía, Brasil, en 1870.', 1, 1, 4, 5, 1, 3),
('Tabi', 'Coffea arabica var. tabi', 'Variedad derivada de cruzamientos del Híbrido de Timor con Típica y Borbón, de grano grande.', "https://i.pinimg.com/1200x/c7/34/b2/c734b2318ee09d6a16c3d4c8e7fb161f.jpg", 'Desarrollada por Cenicafé para combinar resistencia a roya con calidad.', 1, 1, 3, 5, 2, 2),
('Caturra', 'Coffea arabica var. caturra', 'Variedad de porte bajo con cogollos verde claro, hojas redondeadas y buena adaptación.', "https://i.pinimg.com/736x/d7/22/06/d72206ea78e373e71b1b4bbd087bf1a1.jpg", 'Mutación natural de Borbón descubierta en Brasil en 1937.', 1, 2, 2, 5, 2, 3),
('Variedad Colombia', 'Coffea arabica var. colombia', 'Variedad resistente a roya, similar a Caturra en tamaño pero con cogollos bronceados.', "https://i.pinimg.com/1200x/8b/dc/2a/8bdc2a3d14e697f5a9ef19374e5c1ba3.jpg", 'Desarrollada por Cenicafé cruzando Híbrido de Timor con Caturra.', 1, 2, 3, 5, 3, 3);

-- Información agronómica para variedades colombianas
INSERT INTO InformacionAgronomica (id_variedad, tiempo_cosecha, maduracion, nutricion, densidad_siembra) VALUES
(1, 'Principal: 6-8 meses después de floración', 'Tardía', 'Media', '2,500 árboles/ha'),
(2, 'Principal: 6-8 meses después de floración', 'Tardía', 'Media', '2,500 árboles/ha'),
(3, 'Principal: 6-8 meses después de floración', 'Tardía', 'Media', '2,500 árboles/ha'),
(4, 'Principal: 6-8 meses después de floración', 'Tardía', 'Media', 'hasta 3,000 plantas/ha'),
(5, 'Principal: 6-8 meses después de floración', 'Tardía', 'Media', 'hasta 10,000 cafetos/ha'),
(6, 'Principal: 6-8 meses después de floración', 'Tardía', 'Media', 'hasta 10,000 cafetos/ha');

-- Resistencia para variedades colombianas
INSERT INTO VariedadResistencia (id_variedad, id_tipo_resistencia, id_nivel_resistencia) VALUES
(1, 1, 3), -- Típica susceptible a roya
(2, 1, 3), -- Borbón susceptible a roya
(3, 1, 3), -- Maragogipe susceptible a roya
(4, 1, 1), -- Tabi resistente a roya
(5, 1, 3), -- Caturra susceptible a roya
(6, 1, 1); -- Colombia resistente a roya 
-- Insertar variedades Robusta (ejemplos destacados)
INSERT INTO Variedad (nombre_comun, nombre_cientifico, descripcion_general, imagen_referencia_url, historia_linaje, id_grupo_genetico, id_porte, id_tamano_grano, id_altitud_optima, id_potencial_rendimiento, id_calidad_grano) VALUES
('BP 534', 'Coffea canephora var. BP534', 'Clon más comúnmente cultivado por agricultores en Indonesia; adecuado para cultivo bajo sistemas agroforestriales.', "https://i.pinimg.com/1200x/bc/5a/4c/bc5a4c91e130df3093f4bdc494493499.jpg", 'Selección individual etiquetada 6 de una población Congolensis.', 3, 4, 3, 2, 2, 4),
('BRS 2314', 'Coffea canephora var. BRS2314', 'Alto puntaje en catación; ha sido clasificado como "robusta fino".', "https://i.pinimg.com/1200x/c3/e4/f6/c3e4f626e897064f9b97cc58cb705177.jpg", 'Robusta 640 X Encapa 03', 5, 3, 1, 2, 3, 2),
('TR11', 'Coffea canephora var. TR11', 'Muy alto rendimiento y calidad. Crecimiento fuerte.', "https://i.pinimg.com/1200x/bd/31/2c/bd312c6379ec9ce2067f97dccea66732.jpg", 'Selección de árbol madre de población de polinización abierta en cultivo, multiplicación vegetativa por injerto.', 3, 4, 2, 3, 4, 3),
('Sin.1R', 'Coffea canephora var. Sin1R', 'Plantas muy vigorosas que crecen en árboles moderadamente grandes.', "https://i.pinimg.com/1200x/b3/9a/f3/b39af34aad3087c778cd783bfaacdf0d.jpg", 'Coffea congensis x Coffea canephora y retrocruza recurrente a Robusta. Selección de BC2.', 6, 4, 2, 1, 2, 3),
('NARO-Kituza Robusta 1', 'Coffea canephora var. NARO1', 'Resistente a la enfermedad de la marchitez del café (CWD).', "https://i.pinimg.com/1200x/d6/63/e3/d663e3a77d53df7fba2c757f1086d1a2.jpg", 'Clon híbrido de polinización cruzada natural.', 4, 4, 2, 2, 2, 2),
('Perdenia', 'Coffea canephora var. perdenia', 'Vigoroso, de amplia extensión, crece en árboles moderadamente grandes. Alto rendimiento, granos relativamente pequeños.', "https://i.pinimg.com/1200x/db/f4/01/dbf4014ed3efe50de0ab42ea3c4cee4a.jpg", 'Origen desconocido', 3, 4, 1, 1, 2, 3);

-- Información agronómica para variedades Robusta
INSERT INTO InformacionAgronomica (id_variedad, tiempo_cosecha, maduracion, nutricion, densidad_siembra) VALUES
(7, 'Año 2', 'Promedio', 'Media', '1000-2000 plantas/ha (usando poda de un solo tallo)'),
(8, 'Año 2', 'Tardía', 'Alta', '2000-3000 plantas/ha (usando poda de múltiples tallos)'),
(9, 'Año 2', 'Tardía', 'Alta', '1000-2000 plantas/ha (usando poda de un solo tallo)'),
(10, 'Desconocido', 'Tardía', 'Desconocida', '1000-2000 plantas/ha (usando poda de un solo tallo)'),
(11, 'Desconocido', 'Tardía', 'Alta', '1000-2000 plantas/ha (usando poda de un solo tallo)'),
(12, 'Año 4', 'Tardía', 'Media', '1000-2000 plantas/ha (usando poda de un solo tallo)');

-- Resistencia para variedades Robusta
INSERT INTO VariedadResistencia (id_variedad, id_tipo_resistencia, id_nivel_resistencia) VALUES
(7, 1, 1),  -- BP 534 resistente a roya
(7, 3, 1),  -- BP 534 resistente a nematodos
(7, 4, 3),  -- BP 534 susceptible a broca
(8, 1, 1),  -- BRS 2314 resistente a roya
(8, 3, 1),  -- BRS 2314 resistente a nematodos
(8, 4, 3),  -- BRS 2314 susceptible a broca
(9, 1, 2),  -- TR11 tolerante a roya
(10, 3, 2), -- Sin.1R tolerante a nematodos
(11, 6, 1), -- NARO-Kituza 1 resistente a CWD
(11, 1, 1), -- NARO-Kituza 1 resistente a roya
(12, 1, 2), -- Perdenia tolerante a roya
(12, 3, 2); -- Perdenia tolerante a nematodos

-- Usuarios de ejemplo
INSERT INTO Usuario (nombre_usuario, contrasena) VALUES
('admin', SHA2('admin', 256));