using ElectroNova.Layers.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Interfaces
{
    interface IBLLUbicacion
    {
        Task<List<UbicacionDTO.Provincia>> ObtenerProvinciasAsync();
    }
}
