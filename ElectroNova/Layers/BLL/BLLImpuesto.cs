using ElectroNova.Interfaces;
using ElectroNova.Layers.DAL;
using ElectroNova.Layers.Entities;
using log4net;
using System.Threading.Tasks;

namespace ElectroNova.Layers.BLL
{
    class BLLImpuesto : IBLLImpuesto
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");

        public async Task<Impuesto> ObtenerImpuesto()
        {
            IDALImpuesto oDALImpuesto = new DALImpuesto();
            return await oDALImpuesto.ObtenerImpuesto();
        }
    }
}