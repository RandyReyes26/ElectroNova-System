using ElectroNova.Interfaces;
using ElectroNova.Layers.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ElectroNova.Layers.DAL
{
    class DALFactura : IDALFactura
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");

        public bool AnularFactura(string pId_Factura)
        {
            bool retorno = false;
            double row = 0d;
            SqlCommand command = new SqlCommand();

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_UPDATE_Factura_Anular";
                command.Parameters.AddWithValue("@ID_Factura", pId_Factura);

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    row = db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }

                if (row > 0)
                    retorno = true;

                return retorno;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error al anular Factura", ex);
                throw;
            }
        }

        public Factura GuardarFactura(Factura pFactura, List<DetalleFactura> pListaDetalle)
        {
            SqlCommand command = new SqlCommand();
            Factura oFactura = null;

            try
            {
                // =========================
                // GUARDAR ENCABEZADO FACTURA
                // =========================
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_INSERT_Factura";

                command.Parameters.AddWithValue("@ID_Factura", pFactura.ID_Factura);
                command.Parameters.AddWithValue("@ID_Cliente", pFactura.ID_Cliente);
                command.Parameters.AddWithValue("@Fecha", pFactura.Fecha);
                command.Parameters.AddWithValue("@Subtotal", pFactura.Subtotal);
                command.Parameters.AddWithValue("@IVA", pFactura.IVA);
                command.Parameters.AddWithValue("@TotalCRC", pFactura.TotalCRC);
                command.Parameters.AddWithValue("@TotalUSD", pFactura.TotalUSD);
                command.Parameters.AddWithValue("@TipoCambio", pFactura.TipoCambio);
                command.Parameters.AddWithValue("@FirmaCliente", (object)pFactura.FirmaCliente ?? DBNull.Value);
                command.Parameters.AddWithValue("@DocumentoXML", (object)pFactura.DocumentoXML ?? DBNull.Value);
                command.Parameters.AddWithValue("@TipoPago", pFactura.TipoPago);
                command.Parameters.AddWithValue("@Descuento", (object)pFactura.Descuento ?? 0);
                command.Parameters.AddWithValue("@Banco", (object)pFactura.Banco ?? DBNull.Value);
                command.Parameters.AddWithValue("@Estado", pFactura.Estado);
                command.Parameters.AddWithValue("@ID_Tarjeta", (object)pFactura.ID_Tarjeta ?? DBNull.Value);

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    db.ExecuteNonQuery(command);

                    // =========================
                    // GUARDAR DETALLE + RESTAR EXISTENCIA
                    // =========================
                    foreach (DetalleFactura item in pListaDetalle)
                    {
                        // INSERTAR DETALLE
                        SqlCommand cmdDetalle = new SqlCommand();
                        cmdDetalle.CommandType = CommandType.StoredProcedure;
                        cmdDetalle.CommandText = "usp_INSERT_DetalleFactura";

                        cmdDetalle.Parameters.AddWithValue("@ID_Factura", pFactura.ID_Factura);
                        cmdDetalle.Parameters.AddWithValue("@ID_Producto", item.ID_Producto);
                        cmdDetalle.Parameters.AddWithValue("@Cantidad", item.Cantidad);
                        cmdDetalle.Parameters.AddWithValue("@Precio", item.Precio);
                        cmdDetalle.Parameters.AddWithValue("@Subtotal", item.Subtotal);
                        cmdDetalle.Parameters.AddWithValue("@IVA", item.IVA);
                        cmdDetalle.Parameters.AddWithValue("@Total", item.Total);

                        db.ExecuteNonQuery(cmdDetalle);

                        // RESTAR EXISTENCIA DEL PRODUCTO
                        SqlCommand cmdStock = new SqlCommand();
                        cmdStock.CommandType = CommandType.StoredProcedure;
                        cmdStock.CommandText = "usp_UPDATE_Producto_RestarExistencia";

                        cmdStock.Parameters.AddWithValue("@ID_Producto", item.ID_Producto);
                        cmdStock.Parameters.AddWithValue("@Cantidad", item.Cantidad);

                        db.ExecuteNonQuery(cmdStock);
                    }
                }

                oFactura = pFactura;
                return oFactura;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error al guardar Factura", ex);
                throw;
            }
        }

        public Factura ObtenerFacturaPorId(string pId_Factura)
        {
            SqlCommand command = new SqlCommand();
            IDataReader reader = null;
            Factura oFactura = null;

            try
            {
                command.Parameters.AddWithValue("@ID_Factura", pId_Factura);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_Factura_ByID";

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);

                    if (reader.Read())
                    {
                        oFactura = new Factura
                        {
                            ID_Factura = reader["ID_Factura"].ToString(),
                            ID_Cliente = int.Parse(reader["ID_Cliente"].ToString()),
                            Fecha = DateTime.Parse(reader["Fecha"].ToString()),
                            Subtotal = decimal.Parse(reader["Subtotal"].ToString()),
                            IVA = decimal.Parse(reader["IVA"].ToString()),
                            TotalCRC = decimal.Parse(reader["TotalCRC"].ToString()),
                            TotalUSD = decimal.Parse(reader["TotalUSD"].ToString()),
                            TipoCambio = decimal.Parse(reader["TipoCambio"].ToString()),
                            FirmaCliente = (byte[])reader["FirmaCliente"],
                            DocumentoXML = reader["DocumentoXML"] == DBNull.Value ? null : reader["DocumentoXML"].ToString(),
                            TipoPago = reader["TipoPago"].ToString(),
                            Descuento = decimal.Parse(reader["Descuento"].ToString()),
                            Banco = reader["Banco"].ToString(),
                            Estado = bool.Parse(reader["Estado"].ToString()),
                            ID_Tarjeta = int.Parse(reader["ID_Tarjeta"].ToString()),
                        };
                    }
                }

                return oFactura;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error en ObtenerFacturaPorId", ex);
                throw;
            }
        }

        public List<Factura> ObtenerFacturas()
        {
            IList<Factura> lista = new List<Factura>();

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_Factura_All";

                try
                {
                    using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                    {
                        using (IDataReader reader = db.ExecuteReader(command))
                        {
                            while (reader.Read())
                            {
                                Factura oFactura = new Factura();

                                try
                                {
                                    oFactura.ID_Factura = reader["ID_Factura"].ToString();
                                    oFactura.ID_Cliente = int.Parse(reader["ID_Cliente"].ToString());
                                    oFactura.Fecha = DateTime.Parse(reader["Fecha"].ToString());
                                    oFactura.Subtotal = decimal.Parse(reader["Subtotal"].ToString());
                                    oFactura.IVA = decimal.Parse(reader["IVA"].ToString());
                                    oFactura.TotalCRC = decimal.Parse(reader["TotalCRC"].ToString());
                                    oFactura.TotalUSD = decimal.Parse(reader["TotalUSD"].ToString());
                                    oFactura.TipoCambio = decimal.Parse(reader["TipoCambio"].ToString());

                                    oFactura.FirmaCliente = reader["FirmaCliente"] == DBNull.Value
                                        ? null
                                        : (byte[])reader["FirmaCliente"];

                                    oFactura.DocumentoXML = reader["DocumentoXML"] == DBNull.Value
                                        ? null
                                        : reader["DocumentoXML"].ToString();

                                    oFactura.TipoPago = reader["TipoPago"] == DBNull.Value
                                        ? null
                                        : reader["TipoPago"].ToString();

                                    oFactura.Descuento = reader["Descuento"] == DBNull.Value
                                        ? 0
                                        : decimal.Parse(reader["Descuento"].ToString());

                                    oFactura.Banco = reader["Banco"] == DBNull.Value
                                        ? null
                                        : reader["Banco"].ToString();

                                    oFactura.Estado = reader["Estado"] == DBNull.Value
                                        ? false
                                        : bool.Parse(reader["Estado"].ToString());

                                    oFactura.ID_Tarjeta = reader["ID_Tarjeta"] == DBNull.Value
                                        ? 0
                                        : int.Parse(reader["ID_Tarjeta"].ToString());

                                }
                                catch (Exception ex)
                                {
                                    _MyLogControlEventos.Error("Error al leer datos de Marca", ex);
                                    continue;
                                }

                                lista.Add(oFactura);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _MyLogControlEventos.Error("Error en ObtenerFactura", ex);
                    throw;
                }
            }

            return lista.ToList();
        }

        public int ObtenerNumeroActualFactura()
        {
            int numero = 0;
            SqlCommand command = new SqlCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_SELECT_NumeroActualFactura";

            using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
            {
                object result = db.ExecuteScalar(command);
                if (result != null && result != DBNull.Value)
                    numero = Convert.ToInt32(result);
            }

            return numero;
        }

        public int ObtenerSiguienteNumeroFactura()
        {
            int numero = 1;
            SqlCommand command = new SqlCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_SELECT_SiguienteNumeroFactura";

            using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
            {
                object result = db.ExecuteScalar(command);
                if (result != null && result != DBNull.Value)
                    numero = Convert.ToInt32(result);
            }

            return numero;
        }
    }
}
