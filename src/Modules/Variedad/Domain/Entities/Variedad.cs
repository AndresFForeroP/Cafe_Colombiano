using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.Variedad.Domain.Entities
{
    public class Variedad
    {
        public int id { get; set; }
        public string? nombre_comun { get; set; }
        public string? nombre_cientifico { get; set; }
        public string? descripcion_general { get; set; }
        public string? imagen_referencia_url { get; set; }
        public string? historia_linaje { get; set; }
        public int id_grupo_genetico { get; set; }
        public Cafe_Colombiano.src.Modules.GrupoGenetico.Domain.Entities.GrupoGenetico? GrupoGenetico { get; set; }
        public int id_porte { get; set; }
        public Cafe_Colombiano.src.Modules.Porte.Domain.Entities.Porte? Porte { get; set; }
        public int id_tamano_grano { get; set; }
        public Cafe_Colombiano.src.Modules.TamanoGrano.Domain.Entities.TamanoGrano? TamanoGrano { get; set; }
        public int id_altitud_optima { get; set; }
        public Cafe_Colombiano.src.Modules.AltitudOptima.Domain.Entities.AltitudOptima? AltitudOptima { get; set; }
        public int id_potencial_rendimiento { get; set; }
        public Cafe_Colombiano.src.Modules.PotencialRendimiento.Domain.Entities.PotencialRendimiento? PotencialRendimiento { get; set; }
        public int id_calidad_grano { get; set; }
        public Cafe_Colombiano.src.Modules.CalidadGrano.Domain.Entities.CalidadGrano? CalidadGrano { get; set; }
        public ICollection<Cafe_Colombiano.src.Modules.VariedadResistencia.Domain.Entities.VariedadResistencia>? VariedadesResistencia { get; set; }
        public Cafe_Colombiano.src.Modules.InformacionAgronomica.Domain.Entities.InformacionAgronomica? InformacionAgronomica { set; get; }
    }
}