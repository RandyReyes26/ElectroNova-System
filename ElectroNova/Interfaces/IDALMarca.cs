using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Interfaces
{
    interface IDALMarca
    {
        Marca ObtenerMarcaPorId(int pId_Marca);
        Task<IEnumerable<Marca>> ObtenerMarca();
        Task<Marca> GuardarMarca(Marca pMarca);
        Task<Marca> ActualizarMarca(Marca pMarca);
        Task<bool> BorrarMarca(int pId_Marca);
    }
}
