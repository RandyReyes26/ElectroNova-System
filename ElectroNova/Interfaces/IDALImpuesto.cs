using ElectroNova.Layers.Entities;
using System.Threading.Tasks;

namespace ElectroNova.Interfaces
{
    public interface IDALImpuesto
    {
        Task<Impuesto> ObtenerImpuesto();
    }
}