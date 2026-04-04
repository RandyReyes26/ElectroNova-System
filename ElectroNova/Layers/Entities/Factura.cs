using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ElectroNova.Layers.Entities
{
    public class Factura
    {
        public string ID_Factura { set; get; }
        public int ID_Cliente { set; get; }
        public DateTime Fecha { set; get; }
        public double SubtotalCRC { set; get; }
        public double ImpuestoCRC { set; get; }
        public double TotalCRC { set; get; }
        public double TipoCambio { set; get; }
        public double SubtotalUSD { set; get; }
        public double ImpuestoUSD { set; get; }
        public double TotalUSD { set; get; }
        public string MetodoPago { set; get; }
        public string XMLFactura { set; get; }
        public byte[] FirmaCliente { set; get; }

    }
}
