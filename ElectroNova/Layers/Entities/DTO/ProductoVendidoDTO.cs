using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.Entities.DTO
{
    public class ProductoVendidoDTO
    {
        public int ID_Producto { get; set; }

        public string CodigoProducto { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string TipoDispositivo { get; set; }

        public int CantidadVendida { get; set; }

        public decimal TotalVendido { get; set; }

        public byte[] Fotografia { get; set; }
    }
}
