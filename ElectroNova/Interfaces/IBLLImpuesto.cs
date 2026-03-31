using ElectroNova.Layers.Entities;
using System.Threading.Tasks;

namespace ElectroNova.Interfaces
{
    public interface IBLLImpuesto
    {
        Task<Impuesto> ObtenerImpuesto();
    }
}