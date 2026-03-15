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
    class BLLModelo : IBLLModelo
    {
        public Task<bool> BorrarModelo(int pId_Modelo)
        {
            IDALModelo _DALModelo = new DALModelo();

            return _DALModelo.BorrarModelo(pId_Modelo);
        }

        public Task<Modelo> GuardarModelo(Modelo pModelo)
        {
            IDALModelo _DALModelo = new DALModelo();
            Task<Modelo> oModelo = null;

            if (_DALModelo.ObtenerModeloPorId(pModelo.ID_Modelo) == null)
                oModelo = _DALModelo.GuardarModelo(pModelo);
            else
                oModelo = _DALModelo.ActualizarModelo(pModelo);

            return oModelo;
        }

        public Task<IEnumerable<Modelo>> ObtenerModelo()
        {
            IDALModelo _DALModelo = new DALModelo();
            return _DALModelo.ObtenerModelo();
        }

        public Modelo ObtenerModeloPorId(int pId_Modelo)
        {
            IDALModelo _DALModelo = new DALModelo();
            return _DALModelo.ObtenerModeloPorId(pId_Modelo);
        }
    }
}
