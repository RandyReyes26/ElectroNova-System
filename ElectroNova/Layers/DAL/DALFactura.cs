//using ElectroNova.Interfaces;
//using ElectroNova.Layers.Entities;
//using log4net;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ElectroNova.Layers.DAL
//{
//    class DALFactura : IDALFactura
//    {
//        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");

//        public int GuardarFactura(Factura pFactura)
//        {
//            using (IDataBase db = FactoryDatabase.CreateDefaultDataBase())
//            {
//                SqlCommand command = new SqlCommand();
//                command.CommandType = System.Data.CommandType.StoredProcedure;
//                command.CommandText = "usp_INSERT_Facturas";

//                command.Parameters.AddWithValue("@ID_Factura", pFactura.ID_Factura);
//                command.Parameters.AddWithValue("@ID_Cliente", pFactura.ID_Cliente);
//                command.Parameters.AddWithValue("@Fecha", pFactura.Fecha);
//                command.Parameters.AddWithValue("@SubtotalCRC", pFactura.SubtotalCRC);
//                command.Parameters.AddWithValue("@ImpuestoCRC", pFactura.ImpuestoCRC);
//                command.Parameters.AddWithValue("@TotalCRC", pFactura.TotalCRC);
//                command.Parameters.AddWithValue("@TipoCambio", pFactura.TipoCambio);
//                command.Parameters.AddWithValue("@SubtotalUSD", pFactura.SubtotalUSD);
//                command.Parameters.AddWithValue("@ImpuestoUSD", pFactura.ImpuestoUSD);
//                command.Parameters.AddWithValue("@TotalUSD", pFactura.TotalUSD);
//                command.Parameters.AddWithValue("@MetodoPago", pFactura.MetodoPago);
//                command.Parameters.AddWithValue("@XMLFactura", pFactura.XMLFactura);
//                command.Parameters.AddWithValue("@FirmaCliente", pFactura.FirmaCliente);


//                db.ExecuteNonQuery(command);
//            }
//            return pFactura.ID_Factura;
//        }

//        public Factura ObtenerFacturaPorId(int pId_Factura)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
