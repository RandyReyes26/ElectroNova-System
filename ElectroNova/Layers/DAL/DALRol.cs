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
    class DALRol : IDALRol
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        public List<Rol> ObtenerTodosRol()
        {
            IDataReader reader = null;

            List<Rol> lista = new List<Rol>();
            SqlCommand command = new SqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "usp_SELECT_Rol_All";



            using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
            {
                reader = db.ExecuteReader(command);

                while (reader.Read())
                {
                    Rol oRol = new Rol();
                    oRol.ID_Rol = int.Parse(reader["ID_Rol"].ToString());
                    oRol.Nombre_Rol = reader["Nombre_Rol"].ToString();
                    lista.Add(oRol);
                }
            }
            return lista;
        }
    }
}
