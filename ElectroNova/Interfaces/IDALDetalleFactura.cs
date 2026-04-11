using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Interfaces
{
    interface IDALDetalleFactura
    {
        DetalleFactura GuardarDetalleFactura(DetalleFactura pDetalleFactura);
        List<DetalleFactura> ObtenerDetalleFacturaPorIdFactura(string pId_Factura);
    }
}
