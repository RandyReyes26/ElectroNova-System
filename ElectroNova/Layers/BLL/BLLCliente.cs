using ElectroNova.Interfaces;
using ElectroNova.Layers.DAL;
using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.BLL
{
    class BLLCliente : IBLLCliente
    {
        public Task<bool> BorrarCliente(int pId_Cliente)
        {
            IDALCliente _DALCliente = new DALCliente();

            return _DALCliente.BorrarCliente(pId_Cliente);
        }

        public Task<Clientes> GuardarCliente(Clientes pCliente)
        {
            IDALCliente _DALCliente = new DALCliente();
            Task<Clientes> oCliente = null;

            if (_DALCliente.ObtenerClientePorId(pCliente.ID_Cliente) == null)
                oCliente = _DALCliente.GuardarCliente(pCliente);
            else
                oCliente = _DALCliente.ActualizarCliente(pCliente);

            return oCliente;
        }

        public Clientes ObtenerClientePorId(int pId_Cliente)
        {
            IDALCliente _DALCliente = new DALCliente();
            return _DALCliente.ObtenerClientePorId(pId_Cliente);
        }

        public Task<IEnumerable<Clientes>> ObtenerClientes()
        {
            IDALCliente _DALCliente = new DALCliente();
            return _DALCliente.ObtenerClientes();
        }
    }
}
