using ElectroNova.Interfaces;
using ElectroNova.Layers.DAL;
using ElectroNova.Layers.Entities;
using System.Collections.Generic;

namespace ElectroNova.Layers.BLL
{
    public class BLLDetalleFactura : IBLLDetalleFactura
    {
        private IDALDetalleFactura _dalDetalleFactura;

        public BLLDetalleFactura()
        {
            _dalDetalleFactura = new DALDetalleFactura();
        }

        public DetalleFactura GuardarDetalleFactura(DetalleFactura pDetalleFactura)
        {
            return _dalDetalleFactura.GuardarDetalleFactura(pDetalleFactura);
        }

        public List<DetalleFactura> ObtenerDetalleFacturaPorIdFactura(string pId_Factura)
        {
            return _dalDetalleFactura.ObtenerDetalleFacturaPorIdFactura(pId_Factura);
        }
    }
}