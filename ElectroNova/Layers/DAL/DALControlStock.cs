using ElectroNova.Interfaces;
using ElectroNova.Layers.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.DAL
{
    class DALControlStock : IDALControlStock
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");

        public async Task<ControlStock> ActualizarStock(ControlStock pStock)
        {
            SqlCommand command = new SqlCommand();
            ControlStock oIngresoStock = null;

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_UPDATE_IngresoStock";

                command.Parameters.AddWithValue("@ID_Producto", pStock.ID_Producto);
                command.Parameters.AddWithValue("@TipoMovimiento", pStock.TipoMovimiento);
                command.Parameters.AddWithValue("@Cantidad", pStock.Cantidad);
                command.Parameters.AddWithValue("@FacturaCompra", pStock.FacturaCompra);
                command.Parameters.AddWithValue("@Observaciones", pStock.Observaciones);


                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (IDataReader reader = db.ExecuteReader(command))
                    {
                        if (reader.Read())
                        {
                            oIngresoStock = new ControlStock
                            {
                            ID_IngresoStock = int.Parse(reader["ID_IngresoStock"].ToString()),
                            ID_Producto = int.Parse(reader["ID_Producto"].ToString()),
                            TipoMovimiento = reader["TipoMovimiento"].ToString(),
                            Cantidad = int.Parse(reader["Cantidad"].ToString()),
                            FacturaCompra = reader["FacturaCompra"].ToString(),
                            Observaciones = reader["Observaciones"].ToString()

                        };
                        }
                    }
                }

                return oIngresoStock;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error al actualizar Marca", ex);
                throw;
            }
        }

        public async Task<bool> BorrarStock(int pId_Stock)
        {
            bool retorno = false;
            double row = 0d;
            SqlCommand command = new SqlCommand();
            try
            {


                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_DELETE_IngresoStock_ByID";
                command.Parameters.AddWithValue("@ID_IngresoStock", pId_Stock);

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
                _MyLogControlEventos.Error("Error al borrar el Stock", ex);
                throw;
                //}
            }
        }

        public async Task<ControlStock> GuardarStock(ControlStock pStock)
        {
            SqlCommand command = new SqlCommand();
            ControlStock oIngresoStock = null;

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_INSERT_IngresoStock";

                command.Parameters.AddWithValue("@ID_Producto", pStock.ID_Producto);
                command.Parameters.AddWithValue("@TipoMovimiento", pStock.TipoMovimiento);
                command.Parameters.AddWithValue("@Cantidad", pStock.Cantidad);
                command.Parameters.AddWithValue("@FacturaCompra", pStock.FacturaCompra);
                command.Parameters.AddWithValue("@Observaciones", pStock.Observaciones);

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (IDataReader reader = db.ExecuteReader(command))
                    {
                        if (reader.Read())
                        {
                            oIngresoStock = new ControlStock
                            {
                                ID_IngresoStock = int.Parse(reader["ID_IngresoStock"].ToString()),
                                ID_Producto = int.Parse(reader["ID_Producto"].ToString()),
                                TipoMovimiento = reader["TipoMovimiento"].ToString(),
                                Cantidad = int.Parse(reader["Cantidad"].ToString()),
                                FacturaCompra = reader["FacturaCompra"].ToString(),
                                Observaciones = reader["Observaciones"].ToString()
                            };
                        }
                    }
                }

                return oIngresoStock;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error al guardar el Stock", ex);
                throw;
            }
        }

        public async Task<IEnumerable<ControlStock>> ObtenerStock()
        {
            IList<ControlStock> lista = new List<ControlStock>();

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_IngresoStock_All";

                try
                {
                    using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                    {
                        using (IDataReader reader = db.ExecuteReader(command))
                        {
                            while (reader.Read())
                            {
                                ControlStock oIngresoStock = new ControlStock();

                                try
                                {
                                    oIngresoStock.ID_IngresoStock = int.Parse(reader["ID_IngresoStock"].ToString());
                                    oIngresoStock.ID_Producto = int.Parse(reader["ID_Producto"].ToString());
                                    oIngresoStock.TipoMovimiento = reader["TipoMovimiento"].ToString();
                                    oIngresoStock.Cantidad = int.Parse(reader["Cantidad"].ToString());
                                    oIngresoStock.FacturaCompra = reader["FacturaCompra"].ToString();
                                    oIngresoStock.Observaciones = reader["Observaciones"].ToString();

                                }
                                catch (Exception ex)
                                {
                                    _MyLogControlEventos.Error("Error al leer datos de Marca", ex);
                                    continue;
                                }

                                lista.Add(oIngresoStock);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _MyLogControlEventos.Error("Error en Obtener Ingreso del Stock", ex);
                    throw;
                }
            }

            return lista;
        }

        public ControlStock ObtenerStockPorId(int pId_Stock)
        {
            SqlCommand command = new SqlCommand();
            IDataReader reader = null;
            ControlStock oIngresoStock = null;

            try
            {
                command.Parameters.AddWithValue("@ID_IngresoStock", pId_Stock);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_IngresoStock_ByID";

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);

                    if (reader.Read())
                    {
                        oIngresoStock = new ControlStock
                        {
                            ID_IngresoStock = int.Parse(reader["ID_IngresoStock"].ToString()),
                            ID_Producto = int.Parse(reader["ID_Producto"].ToString()),
                            TipoMovimiento = reader["TipoMovimiento"].ToString(),
                            Cantidad = int.Parse(reader["Cantidad"].ToString()),
                            FacturaCompra = reader["FacturaCompra"].ToString(),
                            Observaciones = reader["Observaciones"].ToString()
                        };
                    }
                }

                return oIngresoStock;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error en Obtener Stock por ID", ex);
                throw;
            }
        }
    }
}
