using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Interfaces
{
    interface IDALProducto
    {
        Productos ObtenerProductoPorId(int pId_Producto);
        Task<IEnumerable<Productos>> ObtenerProducto();
        Task<Productos> GuardarProducto(Productos pProducto);
        Task<Productos> ActualizarProducto(Productos pProducto);
        Task<bool> BorrarProducto(int pId_Producto);
    }
}
