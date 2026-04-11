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
        public string NombreProducto { get; set; }
        public int Cantidad { set; get; }
        public double Precio { set; get; }
        public double Subtotal { set; get; }
        public double IVA { set; get; }
        public double Total { set; get; }

    }
}
