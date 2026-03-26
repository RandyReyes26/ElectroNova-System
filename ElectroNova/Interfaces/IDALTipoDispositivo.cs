using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Interfaces
{
    interface IDALTipoDispositivo
    {
        TipoDispositivo ObtenerTipoDispositivoPorId(int pId_TipoDispositivo);
        Task<IEnumerable<TipoDispositivo>> ObtenerTipoDispositivo();
        Task<TipoDispositivo> GuardarTipoDispositivo(TipoDispositivo pTipoDispositivo);
        Task<TipoDispositivo> ActualizarTipoDispositivo(TipoDispositivo pTipoDispositivo);
        Task<bool> BorrarTipoDispositivo(int pId_TipoDispositivo);
    }
}
