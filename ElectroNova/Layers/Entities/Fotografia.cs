using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.Entities
{
    class Fotografia
    {
        public int ID_Imagen { get; set; }
        public int ID_OrdenTrabajo { get; set; }
        public byte[] Imagen { get; set; }
    }
}
