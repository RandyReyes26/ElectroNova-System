using ElectroNova.Interfaces;
using ElectroNova.Layers.DAL;
using ElectroNova.Layers.Entities;
using System.Collections.Generic;
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

        public async Task<Productos> GuardarProducto(Productos pProducto)
        {
            IDALProducto _DALProducto = new DALProducto();

            if (pProducto.ID_Producto > 0)
                return await _DALProducto.ActualizarProducto(pProducto);
            else
                return await _DALProducto.GuardarProducto(pProducto);
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