using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.Entities
{
    class Usuario
    {
        public int ID_Usuario { set; get; }
        public string NombreUsuario { set; get; }
        public string Contrasena { set; get; }
        public int ID_Rol { set; get; }
        public string Nombre { set; get; }
        public string Apellidos { set; get; }
        public string Email { set; get; }
        public bool Estado { set; get; }

    }
}
