using ElectroNova.Interfaces;
using ElectroNova.Layers.DAL;
using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.BLL
{
    class BLLTipoDispositivo : IBLLTipoDispositivo
    {
        public Task<bool> BorrarTipoDispositivo(int pId_TipoDispositivo)
        {
            IDALTipoDispositivo _DALTipoDispositivo = new DALTipoDispositivo();

            return _DALTipoDispositivo.BorrarTipoDispositivo(pId_TipoDispositivo);
        }

        public Task<TipoDispositivo> GuardarTipoDispositivo(TipoDispositivo pTipoDispositivo)
        {
            IDALTipoDispositivo _DALTipoDispositivo = new DALTipoDispositivo();
            Task<TipoDispositivo> oTipoDispositivo = null;

            if (_DALTipoDispositivo.ObtenerTipoDispositivoPorId(pTipoDispositivo.ID_TipoDispositivo) == null)
                oTipoDispositivo = _DALTipoDispositivo.GuardarTipoDispositivo(pTipoDispositivo);
            else
                oTipoDispositivo = _DALTipoDispositivo.ActualizarTipoDispositivo(pTipoDispositivo);

            return oTipoDispositivo;
        }

        public Task<IEnumerable<TipoDispositivo>> ObtenerTipoDispositivo()
        {
            IDALTipoDispositivo _DALTipoDispositivo = new DALTipoDispositivo();
            return _DALTipoDispositivo.ObtenerTipoDispositivo();
        }

        public TipoDispositivo ObtenerTipoDispositivoPorId(int pId_TipoDispositivo)
        {
            IDALTipoDispositivo _DALTipoDispositivo = new DALTipoDispositivo();
            return _DALTipoDispositivo.ObtenerTipoDispositivoPorId(pId_TipoDispositivo);
        }
    }
}
