using ElectroNova.Interfaces;
using ElectroNova.Layers.DAL;
using ElectroNova.Layers.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.BLL
{
    class BLLReporteProducto : IBLLReporteProducto
    {
        private readonly IDALReporteProducto _dal;
        public BLLReporteProducto()
        {
            _dal = new DALReporteProducto();
        }
        public List<ProductoVendidoDTO> ObtenerProductosVendidos(int? idMarca, int? idModelo, int? idTipo)
        {
            return _dal.ObtenerProductosVendidos(idMarca, idModelo, idTipo);
        }
    }
}
