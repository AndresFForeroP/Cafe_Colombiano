using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Variedad.Ui;

namespace Cafe_Colombiano.src.Modules.Variedad.Application.Services
{
    public class FiltroServices : IFiltrarservices
    {
        private readonly IVariedadRepository _repo;
        private VariedadServices serviciosvariedad = new VariedadServices();
        private DIbujoMenusFiltrar dibujoMenusFiltrar = new DIbujoMenusFiltrar();
        public FiltroServices(IVariedadRepository _repo)
        {
            this._repo = _repo;
        }
        public async Task Filtrar()
        {
            List<string> FiltrosAplicados = [];
            Console.Clear();
            string Respuesta = "";
            int opmenufiltro = 0;
            var variedades = await _repo.GetAllVariedadesAsync();
            var Variedad = variedades;
            do
            {
                opmenufiltro = dibujoMenusFiltrar.ImprimirMenuFiltros(FiltrosAplicados);
                switch (opmenufiltro)
                {
                    case 1:
                        FiltrosAplicados.Add("nombre");
                        Variedad = _repo.FiltrarPorNombre(Variedad);
                        break;
                    case 2:
                        FiltrosAplicados.Add("porte");
                        Variedad = _repo.FiltrarPorPorte(Variedad);
                        break;
                    case 3:
                        FiltrosAplicados.Add("tamano");
                        Variedad = _repo.FiltrarPorTamano(Variedad);
                        break;
                    case 4:
                        FiltrosAplicados.Add("altitud");
                        Variedad = _repo.FiltrarPorAltitud(Variedad);
                        break;
                    case 5:
                        FiltrosAplicados.Add("rendimiento");
                        Variedad = _repo.FiltrarPorRendimiento(Variedad);
                        break;
                    case 6:
                        FiltrosAplicados.Add("calidad");
                        Variedad = _repo.FiltrarPorCalidad(Variedad);
                        break;
                    case 7:
                        FiltrosAplicados.Add("resistencia");
                        Variedad = _repo.FiltrarPorResistencia(Variedad);
                        break;
                    case 8:
                        FiltrosAplicados.Add("tiempocosecha");
                        Variedad = _repo.FiltrarPorTiempoCoshecha(Variedad);
                        break;
                    case 9:
                        FiltrosAplicados.Add("maduracion");
                        Variedad = _repo.FiltrarPorMaduracion(Variedad);
                        break;
                    case 10:
                        FiltrosAplicados.Add("nutricion");
                        Variedad = _repo.FiltrarPorNutricion(Variedad);
                        break;
                    case 11:
                        FiltrosAplicados.Add("densidad");
                        Variedad = _repo.FiltrarPorDensidad(Variedad);
                        break;
                    case 12:
                        FiltrosAplicados.Add("grupo");
                        Variedad = _repo.FiltrarPorGrupo(Variedad);
                        break;
                }
                Respuesta = serviciosvariedad.Validarfiltros();
                Console.Clear();
            } while (Respuesta != "no");
            serviciosvariedad.MostrarVariedades(Variedad);
        }
    }
}