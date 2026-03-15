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
            double row = 0;
            SqlCommand command = new SqlCommand();

            Marca oMarca = new Marca();
            try
            {
                command.Parameters.AddWithValue("@Nombre_Marca", pMarca.Nombre_Marca);
                command.Parameters.AddWithValue("@Descripcion", pMarca.Descripcion);
                command.Parameters.AddWithValue("@Estado", pMarca.Estado);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_UPDATE_Marca";

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    row = db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }

                // Si devuelve filas quiere decir que se salvo entonces extraerlo
                if (row > 0)
                    oMarca = this.ObtenerMarcaPorId(pMarca.ID_Marca);

                return oMarca;

            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error en Moto", ex);
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


            double row = 0;
            try
            {
                command.Parameters.AddWithValue("@Nombre_Marca", pMarca.Nombre_Marca);
                command.Parameters.AddWithValue("@Descripcion", pMarca.Descripcion);
                command.Parameters.AddWithValue("@Estado", pMarca.Estado);


                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_INSERT_Marca";


                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    row = db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }

                if (row > 0)
                    oMarca = this.ObtenerMarcaPorId(pMarca.ID_Marca);

                return oMarca;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error en Login", ex);
                throw;
            }
        }

        public async Task<IEnumerable<Marca>> ObtenerMarca()
        {
            IList<Marca> lista = new List<Marca>();

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
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
                                    oMarca.ID_Marca = int.Parse(reader["ID_Marca"].ToString());
                                    oMarca.Nombre_Marca = reader["Nombre_Marca"].ToString();
                                    oMarca.Descripcion = reader["Descripcion"].ToString();
                                    oMarca.Estado = bool.Parse(reader["Estado"].ToString());


                                    // Usar TryParse para evitar la excepción si el valor no es un bool válido
                                    bool estado;
                                    if (reader["Estado"] != DBNull.Value && bool.TryParse(reader["Estado"].ToString(), out estado))
                                    {
                                        oMarca.Estado = estado;
                                    }
                                    else
                                    {
                                        oMarca.Estado = false;  // Valor por defecto si la conversión falla
                                    }

                                    oMarca.ID_Marca = int.Parse(reader["ID_Marca"].ToString());
                                }
                                catch (Exception ex)
                                {
                                    _MyLogControlEventos.Error("Error al leer datos de la motocicleta", ex);
                                    continue;  // Si hay un error al leer un registro, lo omite y continúa con el siguiente
                                }

                                lista.Add(oMarca);
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

        public Marca ObtenerMarcaPorId(int pId_Marca)
        {
            SqlCommand command = new SqlCommand();
            IDataReader reader = null;
            Marca oMarca = null;

            try
            {

                command.Parameters.AddWithValue("@ID_Marca", pId_Marca);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_Marca_ByID";


                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);

                    if (reader.Read())
                    {
                        oMarca = new Marca();
                        {
                            oMarca.ID_Marca = int.Parse(reader["ID_Marca"].ToString());
                            oMarca.Nombre_Marca = reader["Nombre_Marca"].ToString();
                            oMarca.Descripcion = reader["Descripcion"].ToString();
                            oMarca.Estado = bool.Parse(reader["Estado"].ToString());
                            // Usar TryParse para evitar la excepción si el valor no es un bool válido
                            bool estado;
                            if (reader["Estado"] != DBNull.Value && bool.TryParse(reader["Estado"].ToString(), out estado))
                            {
                                oMarca.Estado = estado;
                            }
                            else
                            {
                                oMarca.Estado = false;  // Valor por defecto si la conversión falla
                            }
                            oMarca.ID_Marca = int.Parse(reader["ID_Marca"].ToString());


                        };
                    }
                }
                return oMarca;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error en GetAllLogin", ex);
                throw;
            }
        }
    }
}
