using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.Entities
{
    public class Productos
    {
        public int ID_Producto { set; get; }
        public string Codigo_Barras { set; get; }
        public int ID_Marca { set; get; }
        public int ID_Modelo { set; get; }
        public int ID_TipoDispositivo { set; get; }
        public string Informacion_General { set; get; }
        public string Caracteristicas_Tecnicas { set; get; }
        public string Extras_Accesorios { set; get; }
        public byte[] Fotografia { set; get; }
        public bool Estado { set; get; }

    }
}
