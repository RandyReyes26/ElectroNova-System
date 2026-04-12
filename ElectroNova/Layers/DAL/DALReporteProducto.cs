using ElectroNova.Interfaces;
using ElectroNova.Layers.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.DAL
{
    class DALReporteProducto : IDALReporteProducto
    {
        public List<ProductoVendidoDTO> ObtenerProductosVendidos(int? idMarca, int? idModelo, int? idTipo)
        {
            List<ProductoVendidoDTO> lista = new List<ProductoVendidoDTO>();

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_Reporte_ProductosVendidos";

                command.Parameters.AddWithValue("@ID_Marca", (object)idMarca ?? DBNull.Value);
                command.Parameters.AddWithValue("@ID_Modelo", (object)idModelo ?? DBNull.Value);
                command.Parameters.AddWithValue("@ID_Tipo", (object)idTipo ?? DBNull.Value);

                try
                {
                    using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                    {
                        using (IDataReader reader = db.ExecuteReader(command))
                        {
                            while (reader.Read())
                            {
                                ProductoVendidoDTO obj = new ProductoVendidoDTO();

                                obj.ID_Producto = Convert.ToInt32(reader["ID_Producto"]);
                                obj.CodigoProducto = reader["Codigo_Barras"].ToString();
                                obj.Marca = reader["Nombre_Marca"].ToString();
                                obj.Modelo = reader["Codigo_Modelo"].ToString();
                                obj.TipoDispositivo = reader["Nombre_TipoDispositivo"].ToString();
                                obj.CantidadVendida = Convert.ToInt32(reader["CantidadVendida"]);
                                obj.TotalVendido = Convert.ToDecimal(reader["TotalVendido"]);

                                obj.Fotografia = reader["Fotografia"] == DBNull.Value
                                    ? null
                                    : (byte[])reader["Fotografia"];

                                lista.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener productos vendidos: " + ex.Message);
                }
            }

            return lista;
        }
    }
}
