using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.Entities
{
    public class Rol
    {
        public int ID_Rol { set; get; }
        public string Nombre_Rol { set; get; }

        public override string ToString() => $"{ID_Rol}-{Nombre_Rol}";

    }
}
