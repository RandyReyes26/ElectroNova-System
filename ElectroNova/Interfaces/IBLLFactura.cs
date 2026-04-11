using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Interfaces
{
    interface IBLLFactura
    {
        Factura GuardarFactura(Factura pFactura, List<DetalleFactura> pListaDetalle);
        int ObtenerSiguienteNumeroFactura();
        int ObtenerNumeroActualFactura();
        Factura ObtenerFacturaPorId(string pId_Factura);
        List<Factura> ObtenerFacturas();
        bool AnularFactura(string pId_Factura);
    }
}
