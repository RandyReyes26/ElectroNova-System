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
    class DALModelo : IDALModelo
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        public async Task<Modelo> ActualizarModelo(Modelo pModelo)
        {
            SqlCommand command = new SqlCommand();
            Modelo oModelo = null;

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_UPDATE_Modelo";

                command.Parameters.AddWithValue("@ID_Modelo", pModelo.ID_Modelo);
                command.Parameters.AddWithValue("@Codigo_Modelo", pModelo.Codigo_Modelo);
                command.Parameters.AddWithValue("@Descripcion", pModelo.Descripcion);
                command.Parameters.AddWithValue("@Estado", pModelo.Estado);

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (IDataReader reader = db.ExecuteReader(command))
                    {
                        if (reader.Read())
                        {
                            oModelo = new Modelo
                            {
                            ID_Modelo = Convert.ToInt32(reader["ID_Modelo"]),
                            Codigo_Modelo = reader["Codigo_Modelo"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Estado = bool.Parse(reader["Estado"].ToString())

                        };
                        }
                    }
                }

                return oModelo;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error al actualizar Marca", ex);
                throw;
            }
        }

        public async Task<bool> BorrarModelo(int pId_Modelo)
        {
            bool retorno = false;
            double row = 0d;
            SqlCommand command = new SqlCommand();
            try
            {


                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_DELETE_Modelo_ByID";
                command.Parameters.AddWithValue("@ID_Modelo", pId_Modelo);

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
                _MyLogControlEventos.Error("Error al borrar Cliente", ex);
                throw;
                //}
            }
        }

        public async Task<Modelo> GuardarModelo(Modelo pModelo)
        {
            SqlCommand command = new SqlCommand();
            Modelo oModelo = null;

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_INSERT_Modelo";

                command.Parameters.AddWithValue("@Codigo_Modelo", pModelo.Codigo_Modelo);
                command.Parameters.AddWithValue("@Descripcion", pModelo.Descripcion);
                command.Parameters.AddWithValue("@Estado", pModelo.Estado);

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (IDataReader reader = db.ExecuteReader(command))
                    {
                        if (reader.Read())
                        {
                            oModelo = new Modelo
                            {
                                ID_Modelo = Convert.ToInt32(reader["ID_Modelo"]),
                                Codigo_Modelo = reader["Codigo_Modelo"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            };
                        }
                    }
                }

                return oModelo;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error al guardar Marca", ex);
                throw;
            }
        }

        public async Task<IEnumerable<Modelo>> ObtenerModelo()
        {
            IList<Modelo> lista = new List<Modelo>();

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_Modelo_All";

                try
                {
                    using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                    {
                        using (IDataReader reader = db.ExecuteReader(command))
                        {
                            while (reader.Read())
                            {
                                Modelo oModelo = new Modelo();

                                try
                                {
                                    oModelo.ID_Modelo = int.Parse(reader["ID_Modelo"].ToString());
                                    oModelo.Codigo_Modelo = reader["Codigo_Modelo"].ToString();
                                    oModelo.Descripcion = reader["Descripcion"].ToString();
                                    oModelo.Estado = bool.Parse(reader["Estado"].ToString());
                                }
                                catch (Exception ex)
                                {
                                    _MyLogControlEventos.Error("Error al leer datos de Marca", ex);
                                    continue;
                                }

                                lista.Add(oModelo);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _MyLogControlEventos.Error("Error en ObtenerMarca", ex);
                    throw;
                }
            }

            return lista;
        }

        public Modelo ObtenerModeloPorId(int pId_Modelo)
        {
            SqlCommand command = new SqlCommand();
            IDataReader reader = null;
            Modelo oModelo = null;

            try
            {
                command.Parameters.AddWithValue("@ID_Modelo", pId_Modelo);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_Modelo_ByID";

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);

                    if (reader.Read())
                    {
                        oModelo = new Modelo
                        {
                            ID_Modelo = Convert.ToInt32(reader["ID_Modelo"]),
                            Codigo_Modelo = reader["Codigo_Modelo"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        };
                    }
                }

                return oModelo;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error en ObtenerMarcaPorId", ex);
                throw;
            }
        }
    }
}
