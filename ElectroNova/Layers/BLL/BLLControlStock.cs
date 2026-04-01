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
    class BLLControlStock : IBLLControlStock
    {
        public Task<bool> BorrarStock(int pId_Stock)
        {
            IDALControlStock _DALStock = new DALControlStock();

            return _DALStock.BorrarStock(pId_Stock);
        }

        public Task<ControlStock> GuardarStock(ControlStock pStock)
        {
            IDALControlStock _DALStock = new DALControlStock();
            Task<ControlStock> oStock = null;

            if (_DALStock.ObtenerStockPorId(pStock.ID_IngresoStock) == null)
                oStock = _DALStock.GuardarStock(pStock);
            else
                oStock = _DALStock.ActualizarStock(pStock);

            return oStock;
        }

        public Task<IEnumerable<ControlStock>> ObtenerStock()
        {
            IDALControlStock _DALStock = new DALControlStock();
            return _DALStock.ObtenerStock();
        }

        public ControlStock ObtenerStockPorId(int pId_Stock)
        {
            IDALControlStock _DALStock = new DALControlStock();
            return _DALStock.ObtenerStockPorId(pId_Stock);
        }
    }
}
