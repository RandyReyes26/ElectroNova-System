using ElectroNova.Interfaces;
using ElectroNova.Layers.DAL;
using ElectroNova.Layers.Entities;
using System.Collections.Generic;

namespace ElectroNova.Layers.BLL
{
    public class BLLFactura : IBLLFactura
    {
        private readonly IDALFactura _dalFactura;

        public BLLFactura()
        {
            _dalFactura = new DALFactura();
        }

        public bool AnularFactura(string pId_Factura)
        {
            return _dalFactura.AnularFactura(pId_Factura);
        }

        public Factura GuardarFactura(Factura pFactura, List<DetalleFactura> pListaDetalle)
        {
            return _dalFactura.GuardarFactura(pFactura, pListaDetalle);
        }

        public Factura ObtenerFacturaPorId(string pId_Factura)
        {
            return _dalFactura.ObtenerFacturaPorId(pId_Factura);
        }

        public List<Factura> ObtenerFacturas()
        {
            return _dalFactura.ObtenerFacturas();
        }

        public int ObtenerNumeroActualFactura()
        {
            return _dalFactura.ObtenerNumeroActualFactura();
        }

        public int ObtenerSiguienteNumeroFactura()
        {
            return _dalFactura.ObtenerSiguienteNumeroFactura();
        }
    }
}