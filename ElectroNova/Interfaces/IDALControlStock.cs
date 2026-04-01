using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Interfaces
{
    interface IDALControlStock
    {
        ControlStock ObtenerStockPorId(int pId_Stock);
        Task<IEnumerable<ControlStock>> ObtenerStock();
        Task<ControlStock> GuardarStock(ControlStock pStock);
        Task<ControlStock> ActualizarStock(ControlStock pStock);
        Task<bool> BorrarStock(int pId_Stock);
    }
}
