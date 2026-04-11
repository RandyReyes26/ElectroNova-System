using ElectroNova.Interfaces;
using ElectroNova.Layers.DAL;
using ElectroNova.Layers.Entities;
using System.Collections.Generic;

namespace ElectroNova.Layers.BLL
{
    public class BLLTarjeta : IBLLTarjeta
    {
        public List<Tarjeta> ObtenerTarjeta()
        {
            IDALTarjeta _DALTarjeta = new DALTarjeta();
            return _DALTarjeta.ObtenerTarjeta();
        }
    }
}