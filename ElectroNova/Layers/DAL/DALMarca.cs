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
    class DALMarca : IDALMarca
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        public async Task<Marca> ActualizarMarca(Marca pMarca)
        {
            SqlCommand command = new SqlCommand();

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_UPDATE_Marca";

                command.Parameters.AddWithValue("@ID_Marca", pMarca.ID_Marca);
                command.Parameters.AddWithValue("@Nombre_Marca", pMarca.Nombre_Marca);
                command.Parameters.AddWithValue("@Descripcion", pMarca.Descripcion);
                command.Parameters.AddWithValue("@Estado", pMarca.Estado);

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }

                return pMarca;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error al actualizar Marca", ex);
                throw;
            }
        }

        public async Task<bool> BorrarMarca(int pId_Marca)
        {
            bool retorno = false;
            double row = 0d;
            SqlCommand command = new SqlCommand();
            try
            {


                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_DELETE_Marca_ByID";
                command.Parameters.AddWithValue("@ID_Marca", pId_Marca);

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

        public async Task<Marca> GuardarMarca(Marca pMarca)
        {
            SqlCommand command = new SqlCommand();
            Marca oMarca = null;

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_INSERT_Marca";

                command.Parameters.AddWithValue("@Nombre_Marca", pMarca.Nombre_Marca);
                command.Parameters.AddWithValue("@Descripcion", pMarca.Descripcion);
                command.Parameters.AddWithValue("@Estado", pMarca.Estado);

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (IDataReader reader = db.ExecuteReader(command))
                    {
                        if (reader.Read())
                        {
                            oMarca = new Marca
                            {
                                ID_Marca = Convert.ToInt32(reader["ID_Marca"]),
                                Nombre_Marca = reader["Nombre_Marca"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            };
                        }
                    }
                }

                return oMarca;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error al guardar Marca", ex);
                throw;
            }
        }

        public async Task<IEnumerable<Marca>> ObtenerMarca()
        {
            IList<Marca> lista = new List<Marca>();

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_Marca_All";

                try
                {
                    using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                    {
                        using (IDataReader reader = db.ExecuteReader(command))
                        {
                            while (reader.Read())
                            {
                                Marca oMarca = new Marca();

                                try
                                {
                                    oMarca.ID_Marca = Convert.ToInt32(reader["ID_Marca"]);
                                    oMarca.Nombre_Marca = reader["Nombre_Marca"].ToString();
                                    oMarca.Descripcion = reader["Descripcion"].ToString();
                                    oMarca.Estado = Convert.ToBoolean(reader["Estado"]);
                                }
                                catch (Exception ex)
                                {
                                    _MyLogControlEventos.Error("Error al leer datos de Marca", ex);
                                    continue;
                                }

                                lista.Add(oMarca);
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

        public Marca ObtenerMarcaPorId(int pId_Marca)
        {
            SqlCommand command = new SqlCommand();
            IDataReader reader = null;
            Marca oMarca = null;

            try
            {
                command.Parameters.AddWithValue("@ID_Marca", pId_Marca);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_Marca_ByID";

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);

                    if (reader.Read())
                    {
                        oMarca = new Marca
                        {
                            ID_Marca = Convert.ToInt32(reader["ID_Marca"]),
                            Nombre_Marca = reader["Nombre_Marca"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        };
                    }
                }

                return oMarca;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error en ObtenerMarcaPorId", ex);
                throw;
            }
        }
    }
}
