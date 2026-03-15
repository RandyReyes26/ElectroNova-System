using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.Entities
{
    public class Clientes
    {
        public int ID_Cliente { set; get; }
        public string Identificacion { set; get; }
        public string Pasaporte { set; get; }
        public string Nombre { set; get; }
        public string Apellidos { set; get; }
        public int Sexo { set; get; }
        public string Telefono { set; get; }
        public string Email { set; get; }
        public string DireccionExacta { set; get; }
        public string Provincia { set; get; }
        public byte[] Fotografia { set; get; }
        public bool Estado { set; get; }
        public string NombreCompleto => $"{Nombre} {Apellidos}";

    }
}
