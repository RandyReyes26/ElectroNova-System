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
        Factura ObtenerFacturaPorId(int pId_Factura);

        int GuardarFactura(Factura pFactura);
    }
}
