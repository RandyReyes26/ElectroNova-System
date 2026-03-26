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
    class BLLProducto : IBLLProducto
    {
        public Task<bool> BorrarProducto(int pId_Producto)
        {
            IDALProducto _DALProducto = new DALProducto();

            return _DALProducto.BorrarProducto(pId_Producto);
        }

        public Task<Productos> GuardarProducto(Productos pProducto)
        {
            IDALProducto _DALProducto = new DALProducto();
            Task<Productos> oProducto = null;

            if (_DALProducto.ObtenerProductoPorId(pProducto.ID_Producto) == null)
                oProducto = _DALProducto.GuardarProducto(pProducto);
            else
                oProducto = _DALProducto.ActualizarProducto(pProducto);

            return oProducto;
        }

        public Task<IEnumerable<Productos>> ObtenerProducto()
        {
            IDALProducto _DALProducto = new DALProducto();
            return _DALProducto.ObtenerProducto();
        }

        public Productos ObtenerProductoPorId(int pId_Producto)
        {
            IDALProducto _DALProducto = new DALProducto();
            return _DALProducto.ObtenerProductoPorId(pId_Producto);
        }
    }
}
