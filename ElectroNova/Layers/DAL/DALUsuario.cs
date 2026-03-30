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
    class DALUsuario : IDALUsuario
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        public void ActualizarUsuario(Usuario pUsuario)
        {
            if (pUsuario == null)
            {
                throw new ArgumentNullException(nameof(pUsuario), "El usuario no puede ser nulo.");
            }

            using (IDataBase db = FactoryDatabase.CreateDefaultDataBase())
            {
                SqlCommand command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "usp_UPDATE_Usuario"
                };

                // Añadir parámetros de entrada
                command.Parameters.AddWithValue("@NombreUsuario", pUsuario.NombreUsuario);
                command.Parameters.AddWithValue("@Contrasena", pUsuario.Contrasena);
                command.Parameters.AddWithValue("@ID_Rol", pUsuario.ID_Rol);
                command.Parameters.AddWithValue("@Estado", pUsuario.Estado);

                db.ExecuteNonQuery(command);
            }
        }

        public bool EliminarUsuario(string pLogin)
        {
            SqlCommand command = new SqlCommand();
            string sql = @"Delete from  Usuario   Where (ID_Usuario = @ID_Usuario) ";
            double row = 0;
            try
            {
                command.Parameters.AddWithValue("@ID_Rol", "");
                command.CommandType = CommandType.Text;
                command.CommandText = sql;


                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    row = db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }


                return row > 0 ? true : false;
            }
            catch (SqlException er)
            {
                _MyLogControlEventos.Error("Error en GetAllLogin", er);
                throw;
            }
        }

        public void GuardarUsuario(Usuario pUsuario)
        {
            try
            {
                using (IDataBase db = FactoryDatabase.CreateDefaultDataBase())
                {
                    SqlCommand command = new SqlCommand
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "usp_INSERT_Usuario"
                    };

                    // Añadir parámetros de entrada
                    command.Parameters.AddWithValue("@NombreUsuario", pUsuario.NombreUsuario);
                    command.Parameters.AddWithValue("@Contrasena", pUsuario.Contrasena);
                    command.Parameters.AddWithValue("@ID_Rol", pUsuario.ID_Rol);
                    command.Parameters.AddWithValue("@Estado", pUsuario.Estado);


                    db.ExecuteNonQuery(command);
                }
            }


            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error en GetAllLogin", ex);
                throw;

            }
        }

        public Usuario Login(string pLogin, string pPassword)
        {
            Usuario oUsuario = null;
            using (SqlCommand command = new SqlCommand())
            {
                command.CommandText = @"SELECT ID_Usuario,NombreUsuario,Contrasena,ID_Rol,Estado  
                                FROM Usuario  
                                WHERE NombreUsuario = @NombreUsuario 
                                AND Contrasena = @Contrasena";

                command.Parameters.AddWithValue("@NombreUsuario", pLogin);
                command.Parameters.AddWithValue("@Contrasena", pPassword);
                command.CommandType = CommandType.Text;

                try
                {
                    using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                    {
                        using (IDataReader reader = db.ExecuteReader(command))
                        {
                            if (reader.Read())
                            {
                                oUsuario = new Usuario
                                {
                                    ID_Usuario = Convert.ToInt32(reader["ID_Usuario"]),
                                    NombreUsuario = reader["NombreUsuario"].ToString(),
                                    Contrasena = reader["Contrasena"].ToString(),
                                    ID_Rol = int.Parse(reader["ID_Rol"].ToString()),
                                    Estado = bool.Parse(reader["Estado"].ToString()),
                                };
                            }
                        }
                    }

                    return oUsuario;
                }
                catch (Exception ex)
                {
                    _MyLogControlEventos.Error("Error en Login", ex);
                    throw;
                }
            }
        }

        public IEnumerable<Usuario> ObtenerTodoLogin()
        {
            IList<Usuario> lista = new List<Usuario>();
            using (SqlCommand command = new SqlCommand())
            {
                command.CommandText = @"SELECT * FROM Usuario WITH (ROWLOCK)";
                command.CommandType = CommandType.Text;

                try
                {
                    using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                    {
                        using (IDataReader reader = db.ExecuteReader(command))
                        {
                            while (reader.Read())
                            {
                                Usuario oUsuario = new Usuario
                                {
                                    ID_Usuario = int.Parse(reader["ID_Usuario"].ToString()),
                                    NombreUsuario = reader["NombreUsuario"].ToString(),
                                    Contrasena = reader["Contrasena"].ToString(),
                                    ID_Rol = int.Parse(reader["ID_Rol"].ToString()),
                                    Estado = bool.Parse(reader["Estado"].ToString()),

                                };
                                lista.Add(oUsuario);
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

        public Usuario ObtenerUsuarioPorId(int pLogin)
        {
            SqlCommand command = new SqlCommand();
            IDataReader reader = null;
            Usuario oUsuario = null;

            try
            {
                command.CommandText = @"Select  ID_Usuario,NombreUsuario,Contrasena,ID_Rol,Estado  from  Usuario   Where (ID_Usuario = @ID_Usuario) ";
                command.Parameters.AddWithValue("@ID_Usuario", pLogin);

                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);

                    if (reader.Read())
                    {
                        oUsuario = new Usuario
                        {
                            ID_Usuario = int.Parse(reader["ID_Usuario"].ToString()),
                            NombreUsuario = reader["NombreUsuario"].ToString(),
                            Contrasena = reader["Contrasena"].ToString(),
                            ID_Rol = int.Parse(reader["ID_Rol"].ToString()),
                            Estado = bool.Parse(reader["Estado"].ToString()),

                        };

                    }
                    return oUsuario;
                }
            }

            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error en GetAllLogin", ex);
                throw;
            }
        }
       

        public List<Usuario> SeleccionarTodos()
        {
            List<Usuario> lista = new List<Usuario>();
            using (IDataBase db = FactoryDatabase.CreateDefaultDataBase())
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_Usuario_All";

                IDataReader dr = db.ExecuteReader(command);
                while (dr.Read())
                {
                    Usuario oUsuario = new Usuario();
                    oUsuario.ID_Usuario = int.Parse(dr["ID_Usuario"].ToString());
                    oUsuario.NombreUsuario = dr["NombreUsuario"].ToString();
                    oUsuario.Contrasena = dr["Contrasena"].ToString();
                    oUsuario.ID_Rol = int.Parse(dr["ID_Rol"].ToString());
                    oUsuario.Estado = bool.Parse(dr["Estado"].ToString());
                    lista.Add(oUsuario);


                }
            }
            return lista;
        }
    }
}
