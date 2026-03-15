using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Interfaces
{
    interface IBLLModelo
    {
        Modelo ObtenerModeloPorId(int pId_Modelo);
        Task<IEnumerable<Modelo>> ObtenerModelo();
        Task<Modelo> GuardarModelo(Modelo pModelo);
        Task<bool> BorrarModelo(int pId_Modelo);
    }
}
