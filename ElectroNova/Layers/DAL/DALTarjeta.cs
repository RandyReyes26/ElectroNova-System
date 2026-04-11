using ElectroNova.Interfaces;
using ElectroNova.Layers.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ElectroNova.Layers.DAL
{
    public class DALTarjeta : IDALTarjeta
    {
        private static readonly ILog _MyLogControlEventos = LogManager.GetLogger("MyControlEventos");

        public List<Tarjeta> ObtenerTarjeta()
        {
            IList<Tarjeta> lista = new List<Tarjeta>();

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_SELECT_Tarjeta_All";

                try
                {
                    using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                    {
                        using (IDataReader reader = db.ExecuteReader(command))
                        {
                            while (reader.Read())
                            {
                                Tarjeta oTarjeta = new Tarjeta();

                                try
                                {
                                    oTarjeta.ID_tarjeta = int.Parse(reader["ID_tarjeta"].ToString());
                                    oTarjeta.DescripcionTarjeta = reader["DescripcionTarjeta"].ToString();
                                }
                                catch (Exception ex)
                                {
                                    _MyLogControlEventos.Error("Error al leer datos de Tarjeta", ex);
                                    continue;
                                }

                                lista.Add(oTarjeta);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _MyLogControlEventos.Error("Error en ObtenerTarjeta", ex);
                    throw;
                }
            }

            return lista.ToList();
        }
    }
}