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
    class DALCliente : IDALCliente
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        public Task<Clientes> ActualizarCliente(Clientes pCliente)
        {
            double row = 0;
            Clientes oCliente = null;

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_UPDATE_Cliente";

                command.Parameters.AddWithValue("@ID_Cliente", pCliente.ID_Cliente);
                command.Parameters.AddWithValue("@Identificacion", (object)pCliente.Identificacion ?? DBNull.Value);
                command.Parameters.AddWithValue("@Pasaporte", string.IsNullOrWhiteSpace(pCliente.Pasaporte) ? "" : pCliente.Pasaporte);
                command.Parameters.AddWithValue("@Nombre", (object)pCliente.Nombre ?? DBNull.Value);
                command.Parameters.AddWithValue("@Apellidos", (object)pCliente.Apellidos ?? DBNull.Value);
                command.Parameters.AddWithValue("@Sexo", pCliente.Sexo);
                command.Parameters.AddWithValue("@Telefono", (object)pCliente.Telefono ?? DBNull.Value);
                command.Parameters.AddWithValue("@Email", (object)pCliente.Email ?? DBNull.Value);
                command.Parameters.AddWithValue("@DireccionExacta", (object)pCliente.DireccionExacta ?? DBNull.Value);
                command.Parameters.AddWithValue("@Provincia", (object)pCliente.Provincia ?? DBNull.Value);
                command.Parameters.AddWithValue("@Fotografia", (object)pCliente.Fotografia ?? DBNull.Value);
                command.Parameters.AddWithValue("@Estado", pCliente.Estado);

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    row = db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }
            }

            if (row > 0)
                oCliente = this.ObtenerClientePorId(pCliente.ID_Cliente);

            return Task.FromResult(oCliente);
        }

        public async Task<bool> BorrarCliente(int pId_Cliente)
        {
            bool retorno = false;
            double row = 0d;
            SqlCommand command = new SqlCommand();


            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "usp_DELETE_Cliente_ByID";
            command.Parameters.AddWithValue("@ID_Cliente", pId_Cliente);

            using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
            {
                row = db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
            }

            if (row > 0)
                retorno = true;

            return retorno;

        }

        public  Task<Clientes> GuardarCliente(Clientes pCliente)
        {
            Clientes oCliente = null;
            double row = 0;

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_INSERT_Cliente";

                command.Parameters.AddWithValue("@Identificacion", (object)pCliente.Identificacion ?? DBNull.Value);
                command.Parameters.AddWithValue("@Pasaporte", string.IsNullOrWhiteSpace(pCliente.Pasaporte) ? "" : pCliente.Pasaporte);
                command.Parameters.AddWithValue("@Nombre", (object)pCliente.Nombre ?? DBNull.Value);
                command.Parameters.AddWithValue("@Apellidos", (object)pCliente.Apellidos ?? DBNull.Value);
                command.Parameters.AddWithValue("@Sexo", pCliente.Sexo);
                command.Parameters.AddWithValue("@Telefono", (object)pCliente.Telefono ?? DBNull.Value);
                command.Parameters.AddWithValue("@Email", (object)pCliente.Email ?? DBNull.Value);
                command.Parameters.AddWithValue("@DireccionExacta", (object)pCliente.DireccionExacta ?? DBNull.Value);
                command.Parameters.AddWithValue("@Provincia", (object)pCliente.Provincia ?? DBNull.Value);
                command.Parameters.AddWithValue("@Fotografia", (object)pCliente.Fotografia ?? DBNull.Value);
                command.Parameters.AddWithValue("@Estado", pCliente.Estado);

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    row = db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }
            }

            if (row > 0)
                oCliente = pCliente;

            return Task.FromResult(oCliente);
        }

        public Clientes ObtenerClientePorId(int pId_Cliente)
        {
            SqlCommand command = new SqlCommand();
            IDataReader reader = null;
            Clientes oCliente = null;

            //try
            //{

            command.Parameters.AddWithValue("@ID_Cliente", pId_Cliente);

            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "usp_SELECT_Cliente_ByID";


            using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
            {
                reader = db.ExecuteReader(command);

                if (reader.Read())
                {
                    oCliente = new Clientes();
                    {
                        oCliente.ID_Cliente = int.Parse(reader["ID_Cliente"].ToString());
                        oCliente.Identificacion = reader["Identificacion"].ToString();
                        oCliente.Pasaporte = reader["Pasaporte"].ToString();
                        oCliente.Nombre = reader["Nombre"].ToString();
                        oCliente.Apellidos = reader["Apellidos"].ToString();
                        oCliente.Sexo = int.Parse(reader["Sexo"].ToString());
                        oCliente.Telefono = reader["Telefono"].ToString();
                        oCliente.Email = reader["Email"].ToString();
                        oCliente.DireccionExacta = reader["DireccionExacta"].ToString();
                        oCliente.Provincia = reader["Provincia"].ToString();
                        if (reader["Fotografia"] != DBNull.Value)
                        {
                            oCliente.Fotografia = (byte[])reader["Fotografia"];
                        }
                        else
                        {
                            oCliente.Fotografia = null;
                        }
                        oCliente.Estado = bool.Parse(reader["Estado"].ToString());


                    };
                }
            }
            return oCliente;
            //}
            //catch (Exception ex)
            //{
            //    _MyLogControlEventos.Error("Error en GetAllLogin", ex);
            //    throw;
            //}
        }

        public async Task<IEnumerable<Clientes>> ObtenerClientes()
        {
            IList<Clientes> lista = new List<Clientes>();

            using (SqlCommand command = new SqlCommand())
            {

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_Cliente_All";

                try
                {
                    using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                    {
                        using (IDataReader reader = db.ExecuteReader(command))
                        {
                            while (reader.Read())
                            {
                                Clientes oCliente = new Clientes();
                                {
                                    oCliente.ID_Cliente = int.Parse(reader["ID_Cliente"].ToString());
                                    oCliente.Identificacion = reader["Identificacion"].ToString();
                                    oCliente.Pasaporte = reader["Pasaporte"].ToString();
                                    oCliente.Nombre = reader["Nombre"].ToString();
                                    oCliente.Apellidos = reader["Apellidos"].ToString();
                                    oCliente.Sexo = int.Parse(reader["Sexo"].ToString());
                                    oCliente.Telefono = reader["Telefono"].ToString();
                                    oCliente.Email = reader["Email"].ToString();
                                    oCliente.DireccionExacta = reader["DireccionExacta"].ToString();
                                    oCliente.Provincia = reader["Provincia"].ToString();
                                    if (reader["Fotografia"] != DBNull.Value)
                                    {
                                        oCliente.Fotografia = (byte[])reader["Fotografia"];
                                    }
                                    else
                                    {
                                        oCliente.Fotografia = null;
                                    }
                                    oCliente.Estado = bool.Parse(reader["Estado"].ToString());

                                };
                                lista.Add(oCliente);
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
    }
}
