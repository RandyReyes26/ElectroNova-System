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
    class BLLMarca : IBLLMarca
    {
        public Task<bool> BorrarMarca(int pId_Marca)
        {
            IDALMarca _DALMarca = new DALMarca();

            return _DALMarca.BorrarMarca(pId_Marca);
        }

        public Task<Marca> GuardarMarca(Marca pMarca)
        {
            IDALMarca _DALMarca = new DALMarca();
            Task<Marca> oMarca = null;

            if (_DALMarca.ObtenerMarcaPorId(pMarca.ID_Marca) == null)
                oMarca = _DALMarca.GuardarMarca(pMarca);
            else
                oMarca = _DALMarca.ActualizarMarca(pMarca);

            return oMarca;
        }

        public Task<IEnumerable<Marca>> ObtenerMarca()
        {
            IDALMarca _DALMarca = new DALMarca();
            return _DALMarca.ObtenerMarca();
        }

        public Marca ObtenerMarcaPorId(int pId_Marca)
        {
            IDALMarca _DALMarca = new DALMarca();
            return _DALMarca.ObtenerMarcaPorId(pId_Marca);
        }
    }
}
