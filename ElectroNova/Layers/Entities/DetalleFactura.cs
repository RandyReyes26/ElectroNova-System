using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.Entities
{
    public class DetalleFactura
    {
        public int ID_DetalleFactura { set; get; }
        public string ID_Factura { set; get; }
        public int ID_Producto { set; get; }
        public int Cantidad { set; get; }
        public double PrecioUnitarioCRC { set; get; }
        public double SubtotalLineaCRC { set; get; }
        public double PrecioUnitarioUSD { set; get; }
        public double SubtotalLineaUSD { set; get; }

    }
}
