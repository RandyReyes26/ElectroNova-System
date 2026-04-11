using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Interfaces
{
    interface IDALTarjeta
    {
        List<Tarjeta> ObtenerTarjeta();
    }
}
