using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.Entities
{
     public class ControlStock
    {
        public int ID_IngresoStock { set; get; }
        public int ID_Producto { set; get; }
        public string TipoMovimiento { set; get; }
        public int Cantidad { set; get; }
        public string FacturaCompra { set; get; }
        public string Observaciones { set; get; }

    }
}
