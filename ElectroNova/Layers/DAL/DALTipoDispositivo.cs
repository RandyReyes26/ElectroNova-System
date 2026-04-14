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
    class DALTipoDispositivo : IDALTipoDispositivo
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        public async Task<TipoDispositivo> ActualizarTipoDispositivo(TipoDispositivo pTipoDispositivo)
        {
            SqlCommand command = new SqlCommand();

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_UPDATE_TipoDispositivo";

                command.Parameters.AddWithValue("@ID_TipoDispositivo", pTipoDispositivo.ID_TipoDispositivo);
                command.Parameters.AddWithValue("@Nombre_TipoDispositivo", pTipoDispositivo.Nombre_TipoDispositivo);
                command.Parameters.AddWithValue("@Descripcion", pTipoDispositivo.Descripcion);
                command.Parameters.AddWithValue("@Estado", pTipoDispositivo.Estado);

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }

                return pTipoDispositivo;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error al actualizar TipoDispositivo", ex);
                throw;
            }
        }

        public async Task<bool> BorrarTipoDispositivo(int pId_TipoDispositivo)
        {
            bool retorno = false;
            double row = 0d;
            SqlCommand command = new SqlCommand();
            try
            {


                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_DELETE_TipoDispositivo_ByID";
                command.Parameters.AddWithValue("@ID_TipoDispositivo", pId_TipoDispositivo);

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

        public async Task<TipoDispositivo> GuardarTipoDispositivo(TipoDispositivo pTipoDispositivo)
        {
            SqlCommand command = new SqlCommand();
            TipoDispositivo oTipoDispositivo = null;

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_INSERT_TipoDispositivo";

                command.Parameters.AddWithValue("@Nombre_TipoDispositivo", pTipoDispositivo.Nombre_TipoDispositivo);
                command.Parameters.AddWithValue("@Descripcion", pTipoDispositivo.Descripcion);
                command.Parameters.AddWithValue("@Estado", pTipoDispositivo.Estado);

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (IDataReader reader = db.ExecuteReader(command))
                    {
                        if (reader.Read())
                        {
                            oTipoDispositivo = new TipoDispositivo
                            {
                                ID_TipoDispositivo = int.Parse(reader["ID_TipoDispositivo"].ToString()),
                                Nombre_TipoDispositivo = reader["Nombre_TipoDispositivo"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Estado = bool.Parse(reader["Estado"].ToString())
                            };
                        }
                    }
                }

                return oTipoDispositivo;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error al guardar TipoDispositivo", ex);
                throw;
            }
        }

        public async Task<IEnumerable<TipoDispositivo>> ObtenerTipoDispositivo()
        {
            IList<TipoDispositivo> lista = new List<TipoDispositivo>();

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_TipoDispositivo_All";

                try
                {
                    using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                    {
                        using (IDataReader reader = db.ExecuteReader(command))
                        {
                            while (reader.Read())
                            {
                                TipoDispositivo oTipoDispositivo = new TipoDispositivo();

                                try
                                {
                                    oTipoDispositivo.ID_TipoDispositivo = int.Parse(reader["ID_TipoDispositivo"].ToString());
                                    oTipoDispositivo.Nombre_TipoDispositivo = reader["Nombre_TipoDispositivo"].ToString();
                                    oTipoDispositivo.Descripcion = reader["Descripcion"].ToString();
                                    oTipoDispositivo.Estado = bool.Parse(reader["Estado"].ToString());

                                }
                                catch (Exception ex)
                                {
                                    _MyLogControlEventos.Error("Error al leer datos de Marca", ex);
                                    continue;
                                }

                                lista.Add(oTipoDispositivo);
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

        public TipoDispositivo ObtenerTipoDispositivoPorId(int pId_TipoDispositivo)
        {
            SqlCommand command = new SqlCommand();
            IDataReader reader = null;
            TipoDispositivo oTipoDispositivo = null;

            try
            {
                command.Parameters.AddWithValue("@ID_TipoDispositivo", pId_TipoDispositivo);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_TipoDispositivo_ByID";

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);

                    if (reader.Read())
                    {
                        oTipoDispositivo = new TipoDispositivo
                        {
                            ID_TipoDispositivo = int.Parse(reader["ID_TipoDispositivo"].ToString()),
                            Nombre_TipoDispositivo = reader["Nombre_TipoDispositivo"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Estado = bool.Parse(reader["Estado"].ToString())
                        };
                    }
                }

                return oTipoDispositivo;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error en ObtenerMarcaPorId", ex);
                throw;
            }
        }
    }
}
