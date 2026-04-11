using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ElectroNova.Layers.Entities
{
    public class Factura
    {
        public string ID_Factura { get; set; }
        public int ID_Cliente { get; set; }
        public DateTime Fecha { get; set; }

        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public decimal TotalCRC { get; set; }
        public decimal TotalUSD { get; set; }
        public decimal TipoCambio { get; set; }

        public byte[] FirmaCliente { get; set; }
        public string DocumentoXML { get; set; }
        public string TipoPago { get; set; }

        public decimal? Descuento { get; set; }
        public string Banco { get; set; }
        public bool Estado { get; set; }

        public int? ID_Tarjeta { get; set; }

    }
}
