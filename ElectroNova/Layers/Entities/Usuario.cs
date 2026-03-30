using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.Entities
{
    public class Usuario
    {
        public int ID_Usuario { set; get; }
        public string NombreUsuario { set; get; }
        public string Contrasena { set; get; }
        public int ID_Rol { set; get; }
        public bool Estado { set; get; }

    }
}
