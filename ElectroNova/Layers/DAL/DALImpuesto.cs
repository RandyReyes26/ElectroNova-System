using ElectroNova.Interfaces;
using ElectroNova.Layers.Entities;
using log4net;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ElectroNova.Layers.DAL
{
    class DALImpuesto : IDALImpuesto
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");

        public Task<Impuesto> ObtenerImpuesto()
        {
            Impuesto oImpuesto = null;

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT ID_IVA, Descripcion, Valor FROM IVA WHERE ID_IVA = 1";

                try
                {
                    using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                    {
                        using (IDataReader reader = db.ExecuteReader(command))
                        {
                            if (reader.Read())
                            {
                                oImpuesto = new Impuesto
                                {
                                    ID_IVA = Convert.ToInt32(reader["ID_IVA"]),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    Valor = double.Parse(reader["Valor"].ToString())
                            };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _MyLogControlEventos.Error("Error en ObtenerImpuesto", ex);
                    throw;
                }
            }

            return Task.FromResult(oImpuesto);
        }
    }
}