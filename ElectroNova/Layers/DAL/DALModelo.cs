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
            double row = 0;
            SqlCommand command = new SqlCommand();

            Modelo oModelo = new Modelo();
            try
            {
                command.Parameters.AddWithValue("@Codigo_Modelo", pModelo.Codigo_Modelo);
                command.Parameters.AddWithValue("@Descripcion", pModelo.Descripcion);
                command.Parameters.AddWithValue("@Estado", pModelo.Estado);




                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_UPDATE_Motocicleta";

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    row = db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }

                // Si devuelve filas quiere decir que se salvo entonces extraerlo
                if (row > 0)
                    oModelo = this.ObtenerModeloPorId(pModelo.ID_Modelo);

                return oModelo;

            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error en Moto", ex);
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
                command.CommandText = "usp_DELETE_Motocicleta_ByID";
                command.Parameters.AddWithValue("@ID_Motocicleta", pId_Modelo);

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


            double row = 0;
            try
            {
                command.Parameters.AddWithValue("@Codigo_Modelo", pModelo.Codigo_Modelo);
                command.Parameters.AddWithValue("@Descripcion", pModelo.Descripcion);
                command.Parameters.AddWithValue("@Estado", pModelo.Estado);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_INSERT_Motocicleta";


                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    row = db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }

                if (row > 0)
                    oModelo = this.ObtenerModeloPorId(pModelo.ID_Modelo);

                return oModelo;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error en Login", ex);
                throw;
            }
        }

        public async Task<IEnumerable<Modelo>> ObtenerModelo()
        {
            IList<Modelo> lista = new List<Modelo>();

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_Motocicleta_All";

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


                                    // Usar TryParse para evitar la excepción si el valor no es un bool válido
                                    bool estado;
                                    if (reader["Estado"] != DBNull.Value && bool.TryParse(reader["Estado"].ToString(), out estado))
                                    {
                                        oModelo.Estado = estado;
                                    }
                                    else
                                    {
                                        oModelo.Estado = false;  // Valor por defecto si la conversión falla
                                    }

                                    oModelo.ID_Modelo = int.Parse(reader["ID_Cliente"].ToString());
                                }
                                catch (Exception ex)
                                {
                                    _MyLogControlEventos.Error("Error al leer datos de la motocicleta", ex);
                                    continue;  // Si hay un error al leer un registro, lo omite y continúa con el siguiente
                                }

                                lista.Add(oModelo);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _MyLogControlEventos.Error("Error en GetAllLogin", ex);
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

                command.Parameters.AddWithValue("@ID_Motocicleta", pId_Modelo);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_Motocicleta_ByID";


                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);

                    if (reader.Read())
                    {
                        oModelo = new Modelo();
                        {
                            oModelo.ID_Modelo = int.Parse(reader["ID_Modelo"].ToString());
                            oModelo.Codigo_Modelo = reader["Codigo_Modelo"].ToString();
                            oModelo.Descripcion = reader["Descripcion"].ToString();
                            oModelo.Estado = bool.Parse(reader["Estado"].ToString());
                            // Usar TryParse para evitar la excepción si el valor no es un bool válido
                            bool estado;
                            if (reader["Estado"] != DBNull.Value && bool.TryParse(reader["Estado"].ToString(), out estado))
                            {
                                oModelo.Estado = estado;
                            }
                            else
                            {
                                oModelo.Estado = false;  // Valor por defecto si la conversión falla
                            }
                            oModelo.ID_Modelo = int.Parse(reader["ID_Cliente"].ToString());


                        };
                    }
                }
                return oModelo;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error en GetAllLogin", ex);
                throw;
            }
        }
    }
}
