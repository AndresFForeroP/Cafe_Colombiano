using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces
{
    public interface IMenusfiltros
    {
        string MenuNombre();
        public string MenuPorte();
        public string Menutamano();
        public string MenuAltitud();
        public string MenuRendimiento();
        public string MenuCalidad();
        public string Menuresistencias();
        public string MenuNivelResistencias();
        public string MenuTiempoCosecha();
        public string MenuMaduracion();
        public string MenuNutricion();
        public string MenuDensidad();
        public string MenuGrupo();
        public int ImprimirMenuFiltros(List<string> filtros);
    }
}