using ElectroNova.Layers.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Interfaces
{
    interface IBLLReporteProducto
    {
        List<ProductoVendidoDTO> ObtenerProductosVendidos(int? idMarca, int? idModelo, int? idTipo);
    }
}
