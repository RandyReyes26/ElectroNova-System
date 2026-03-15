using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Interfaces
{
    interface IDALCliente
    {
        Clientes ObtenerClientePorId(int pId_Cliente);
        Task<IEnumerable<Clientes>> ObtenerClientes();
        Task<Clientes> GuardarCliente(Clientes pCliente);
        Task<Clientes> ActualizarCliente(Clientes pCliente);
        Task<bool> BorrarCliente(int pId_Cliente);
    }
}
