using ElectroNova.Interfaces;
using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace ElectroNova.Layers.DAL
{
    class DALDetalleFactura : IDALDetalleFactura
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        public DetalleFactura GuardarDetalleFactura(DetalleFactura pDetalleFactura)
        {
            SqlCommand command = new SqlCommand();
            DetalleFactura oDetalleFactura = null;

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_INSERT_DetalleFactura";


                command.Parameters.AddWithValue("@ID_Factura", pDetalleFactura.ID_Factura);
                command.Parameters.AddWithValue("@ID_Producto", pDetalleFactura.ID_Producto);
                command.Parameters.AddWithValue("@Cantidad", pDetalleFactura.Cantidad);
                command.Parameters.AddWithValue("@Precio", pDetalleFactura.Precio);
                command.Parameters.AddWithValue("@Subtotal", pDetalleFactura.Subtotal);
                command.Parameters.AddWithValue("@IVA", pDetalleFactura.IVA);
                command.Parameters.AddWithValue("@Total", pDetalleFactura.Total);



                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (IDataReader reader = db.ExecuteReader(command))
                    {
                        if (reader.Read())
                        {
                            oDetalleFactura = new DetalleFactura
                            {
                            ID_DetalleFactura = int.Parse(reader["ID_DetalleFactura"].ToString()),
                            ID_Factura = reader["ID_Factura"].ToString(),
                            ID_Producto = int.Parse(reader["ID_Producto"].ToString()),
                            Cantidad = int.Parse(reader["Cantidad"].ToString()),
                            Precio = double.Parse(reader["Precio"].ToString()),
                            Subtotal = double.Parse(reader["Subtotal"].ToString()),
                            IVA = double.Parse(reader["IVA"].ToString()),
                            Total = double.Parse(reader["Total"].ToString()),


                        };
                        }
                    }
                }

                return oDetalleFactura;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error al guardar Factura", ex);
                throw;
            }
        }

        public List<DetalleFactura> ObtenerDetalleFacturaPorIdFactura(string pId_Factura)
        {
            IList<DetalleFactura> lista = new List<DetalleFactura>();

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_DetalleFactura_ByFactura";
                command.Parameters.AddWithValue("@ID_Factura", pId_Factura);

                try
                {
                    using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                    {
                        using (IDataReader reader = db.ExecuteReader(command))
                        {
                            while (reader.Read())
                            {
                                DetalleFactura oDetalleFactura = new DetalleFactura();

                                try
                                {
                                    oDetalleFactura.ID_DetalleFactura = int.Parse(reader["ID_DetalleFactura"].ToString());
                                    oDetalleFactura.ID_Factura = reader["ID_Factura"].ToString();
                                    oDetalleFactura.ID_Producto = int.Parse(reader["ID_Producto"].ToString());
                                    oDetalleFactura.Cantidad = int.Parse(reader["Cantidad"].ToString());
                                    oDetalleFactura.Precio = double.Parse(reader["Precio"].ToString());
                                    oDetalleFactura.Subtotal = double.Parse(reader["Subtotal"].ToString());
                                    oDetalleFactura.IVA = double.Parse(reader["IVA"].ToString());
                                    oDetalleFactura.Total = double.Parse(reader["Total"].ToString());

                                }
                                catch (Exception ex)
                                {
                                    _MyLogControlEventos.Error("Error al leer detalle de factura", ex);
                                    continue;
                                }

                                lista.Add(oDetalleFactura);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _MyLogControlEventos.Error("Error en ObtenerDetalleFacturaPorIdFactura", ex);
                    throw;
                }
            }

            return lista.ToList();
        }
    }
}
