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
    class DALProducto : IDALProducto
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");

        public async Task<Productos> ActualizarProducto(Productos pProducto)
        {
            SqlCommand command = new SqlCommand();

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_UPDATE_Producto";

                command.Parameters.AddWithValue("@ID_Producto", pProducto.ID_Producto);
                command.Parameters.AddWithValue("@Codigo_Barras", pProducto.Codigo_Barras);
                command.Parameters.AddWithValue("@ID_Marca", pProducto.ID_Marca);
                command.Parameters.AddWithValue("@ID_Modelo", pProducto.ID_Modelo);
                command.Parameters.AddWithValue("@ID_TipoDispositivo", pProducto.ID_TipoDispositivo);
                command.Parameters.AddWithValue("@Informacion_General", pProducto.Informacion_General);
                command.Parameters.AddWithValue("@Caracteristicas_Tecnicas", pProducto.Caracteristicas_Tecnicas);
                command.Parameters.AddWithValue("@DocumentoEspecificaciones", pProducto.DocumentoEspecificaciones);
                command.Parameters.AddWithValue("@Precio", pProducto.Precio);
                command.Parameters.AddWithValue("@Existencia", pProducto.Existencia);
                command.Parameters.AddWithValue("@Extras_Accesorios", pProducto.Extras_Accesorios);
                command.Parameters.AddWithValue("@Fotografia", pProducto.Fotografia);
                command.Parameters.AddWithValue("@Estado", pProducto.Estado);

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }

                return pProducto;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error al actualizar Producto", ex);
                throw;
            }
        }

        public async Task<bool> BorrarProducto(int pId_Producto)
        {
            bool retorno = false;
            double row = 0d;
            SqlCommand command = new SqlCommand();
            try
            {


                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_DELETE_Producto_ByID";
                command.Parameters.AddWithValue("@ID_Producto", pId_Producto);

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
                _MyLogControlEventos.Error("Error al borrar Producto", ex);
                throw;

            }
        }

        public async Task<Productos> GuardarProducto(Productos pProducto)
        {
            SqlCommand command = new SqlCommand();
            Productos oProducto = null;

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_INSERT_Producto";

                //command.Parameters.AddWithValue("@ID_Producto", pProducto.ID_Producto);
                command.Parameters.AddWithValue("@Codigo_Barras", pProducto.Codigo_Barras);
                command.Parameters.AddWithValue("@ID_Marca", pProducto.ID_Marca);
                command.Parameters.AddWithValue("@ID_Modelo", pProducto.ID_Modelo);
                command.Parameters.AddWithValue("@ID_TipoDispositivo", pProducto.ID_TipoDispositivo);
                command.Parameters.AddWithValue("@Informacion_General", pProducto.Informacion_General);
                command.Parameters.AddWithValue("@Caracteristicas_Tecnicas", pProducto.Caracteristicas_Tecnicas);
                command.Parameters.AddWithValue("@DocumentoEspecificaciones", pProducto.DocumentoEspecificaciones);
                command.Parameters.AddWithValue("@Precio", pProducto.Precio);
                command.Parameters.AddWithValue("@Existencia", pProducto.Existencia);
                command.Parameters.AddWithValue("@Extras_Accesorios", pProducto.Extras_Accesorios);
                command.Parameters.AddWithValue("@Fotografia", pProducto.Fotografia);
                command.Parameters.AddWithValue("@Estado", pProducto.Estado);

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (IDataReader reader = db.ExecuteReader(command))
                    {
                        if (reader.Read())
                        {
                            oProducto = new Productos
                            {
                                ID_Producto = int.Parse(reader["ID_Producto"].ToString()),
                                Codigo_Barras = reader["Codigo_Barras"].ToString(),
                                ID_Marca = int.Parse(reader["ID_Marca"].ToString()),
                                ID_Modelo = int.Parse(reader["ID_Modelo"].ToString()),
                                ID_TipoDispositivo = int.Parse(reader["ID_TipoDispositivo"].ToString()),
                                Informacion_General = reader["Informacion_General"].ToString(),
                                Caracteristicas_Tecnicas = reader["Caracteristicas_Tecnicas"].ToString(),
                                DocumentoEspecificaciones = reader["DocumentoEspecificaciones"].ToString(),
                                Precio = reader["Precio"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Precio"]),
                                Existencia = reader["Existencia"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Existencia"]),
                                Extras_Accesorios = reader["Extras_Accesorios"].ToString(),
                                Fotografia = (byte[])reader["Fotografia"],
                                Estado = bool.Parse(reader["Estado"].ToString())
                            };
                        }
                    }
                }

                return oProducto;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error al guardar Producto", ex);
                throw;
            }
        }

        public async Task<IEnumerable<Productos>> ObtenerProducto()
        {
            IList<Productos> lista = new List<Productos>();

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_Producto_All";

                try
                {
                    using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                    {
                        using (IDataReader reader = db.ExecuteReader(command))
                        {
                            while (reader.Read())
                            {
                                Productos oProducto = new Productos();

                                try
                                {
                                    oProducto.ID_Producto = int.Parse(reader["ID_Producto"].ToString());
                                    oProducto.Codigo_Barras = reader["Codigo_Barras"].ToString();
                                    oProducto.ID_Marca = int.Parse(reader["ID_Marca"].ToString());
                                    oProducto.ID_Modelo = int.Parse(reader["ID_Modelo"].ToString());
                                    oProducto.ID_TipoDispositivo = int.Parse(reader["ID_TipoDispositivo"].ToString());
                                    oProducto.Informacion_General = reader["Informacion_General"].ToString();
                                    oProducto.Caracteristicas_Tecnicas = reader["Caracteristicas_Tecnicas"].ToString();
                                    oProducto.DocumentoEspecificaciones = reader["DocumentoEspecificaciones"].ToString();
                                    oProducto.Precio = reader["Precio"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Precio"]);
                                    oProducto.Existencia = reader["Existencia"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Existencia"]);
                                    oProducto.Extras_Accesorios = reader["Extras_Accesorios"].ToString();
                                    oProducto.Fotografia = (byte[])reader["Fotografia"];
                                    oProducto.Estado = bool.Parse(reader["Estado"].ToString());

                                }
                                catch (Exception ex)
                                {
                                    _MyLogControlEventos.Error("Error al leer datos de Producto", ex);
                                    continue;
                                }

                                lista.Add(oProducto);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _MyLogControlEventos.Error("Error en Obtener Producto", ex);
                    throw;
                }
            }

            return lista;
        }

        public Productos ObtenerProductoPorId(int pId_Producto)
        {
            SqlCommand command = new SqlCommand();
            IDataReader reader = null;
            Productos oProducto = null;

            try
            {
                command.Parameters.AddWithValue("@ID_Producto", pId_Producto);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_Producto_ByID";

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);

                    if (reader.Read())
                    {
                        oProducto = new Productos
                        {
                            ID_Producto = int.Parse(reader["ID_Producto"].ToString()),
                            Codigo_Barras = reader["Codigo_Barras"].ToString(),
                            ID_Marca = int.Parse(reader["ID_Marca"].ToString()),
                            ID_Modelo = int.Parse(reader["ID_Modelo"].ToString()),
                            ID_TipoDispositivo = int.Parse(reader["ID_TipoDispositivo"].ToString()),
                            Informacion_General = reader["Informacion_General"].ToString(),
                            Caracteristicas_Tecnicas = reader["Caracteristicas_Tecnicas"].ToString(),
                            DocumentoEspecificaciones = reader["DocumentoEspecificaciones"].ToString(),
                            Precio = reader["Precio"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Precio"]),
                            Existencia = reader["Existencia"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Existencia"]),
                            Extras_Accesorios = reader["Extras_Accesorios"].ToString(),
                            Fotografia = (byte[])reader["Fotografia"],
                            Estado = bool.Parse(reader["Estado"].ToString())
                        };
                    }
                }

                return oProducto;
            }
            catch (Exception ex)
            {
                _MyLogControlEventos.Error("Error en ObtenerMarcaPorId", ex);
                throw;
            }
        }
    }
}
