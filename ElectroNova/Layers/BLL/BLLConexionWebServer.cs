using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.BLL
{
    public class BLLConexionWebServer
    {
        private readonly string TOKEN = "356E1MEM15";
        private readonly string NOMBRE = "Reyes Romero";
        private readonly string CORREO = "randy022315@gmail.com";


        private double ObtenerTipoCambio(DateTime fecha)
        {
            try
            {
                ServiceReference1.wsindicadoreseconomicosSoapClient client = new ServiceReference1.wsindicadoreseconomicosSoapClient("wsindicadoreseconomicosSoap12");
                string fechaString = fecha.ToString("dd/MM/yyyy");
                DataSet dataset = client.ObtenerIndicadoresEconomicos("317", fechaString, fechaString, NOMBRE, "N", CORREO, TOKEN);

                if (dataset == null || dataset.Tables.Count == 0 || dataset.Tables[0].Rows.Count == 0)
                {
                    throw new Exception("No se encontraron datos para la fecha proporcionada.");
                }

                DataTable table = dataset.Tables[0];
                double tipoCambio = Convert.ToDouble(table.Rows[0][2].ToString());
                return tipoCambio;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al obtener el tipo de cambio: " + ex.Message);
            }
        }
        /// <summary>
        /// Obtiene una lista de datos de dólares para un rango de fechas y un tipo de operación (compra o venta).
        /// </summary>
        /// <param name="pFechaInicial">La fecha inicial del rango de fechas.</param>
        /// <param name="pFechaFinal">La fecha final del rango de fechas.</param>
        /// <param name="pCompraoVenta">El tipo de operación: "c" para compra y "v" para venta.</param>
        /// <returns>Una lista de objetos <see cref="Dolar"/> con los datos de dólares para el rango de fechas y tipo de operación proporcionado.</returns>
        /// <exception cref="Exception">Lanza una excepción si no se encuentran datos o si ocurre un error durante la solicitud.</exception>
        public IEnumerable<Dolar> GetDolar(DateTime pFechaInicial, DateTime pFechaFinal, string pCompraoVenta)
        {
            try
            {
                List<Dolar> lista = new List<Dolar>();
                string fecha_inicial = pFechaInicial.ToString("dd/MM/yyyy");
                string fecha_final = pFechaFinal.ToString("dd/MM/yyyy");
                string tipoCompraoVenta = pCompraoVenta.Equals("c", StringComparison.InvariantCulture) ? "317" : "318";

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServiceReference1.wsindicadoreseconomicosSoapClient client = new ServiceReference1.wsindicadoreseconomicosSoapClient("wsindicadoreseconomicosSoap12");
                DataSet dataset = client.ObtenerIndicadoresEconomicos(tipoCompraoVenta, fecha_inicial, fecha_final, NOMBRE, "N", CORREO, TOKEN);

                if (dataset == null || dataset.Tables.Count == 0 || dataset.Tables[0].Rows.Count == 0)
                {
                    throw new Exception("No se encontraron datos para el rango de fechas proporcionado.");
                }

                DataTable table = dataset.Tables[0];

                foreach (DataRow row in table.Rows)
                {
                    if (row[0].ToString().Contains("error"))
                    {
                        throw new Exception(row[0].ToString());
                    }

                    Dolar dolar = new Dolar
                    {
                        Codigo = row[0].ToString(),
                        Fecha = DateTime.Parse(row[1].ToString()),
                        Monto = Convert.ToDouble(row[2].ToString())
                    };
                    lista.Add(dolar);
                }

                return lista;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al obtener la lista de dólares: " + ex.Message);
            }
        }
        /// <summary>
        /// Calcula el precio de venta a partir del precio base en dólares y la fecha, aplicando el tipo de cambio y un margen de ganancia.
        /// </summary>
        /// <param name="precioBaseDolares">El precio base en dólares.</param>
        /// <param name="fecha">La fecha para obtener el tipo de cambio.</param>
        /// <returns>El precio de venta calculado.</returns>
        /// <exception cref="Exception">Lanza una excepción si ocurre un error durante el cálculo.</exception>
    }
}
