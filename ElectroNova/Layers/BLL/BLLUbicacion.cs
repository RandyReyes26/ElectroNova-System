using ElectroNova.Interfaces;
using ElectroNova.Layers.Entities.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.BLL
{
    class BLLUbicacion : IBLLUbicacion
    {
        private readonly string _urlProvincias;

        public BLLUbicacion()
        {
            // Leer del App.Config las URLs para las APIs
            _urlProvincias = ConfigurationManager.AppSettings["URLProvincias"];

        }
        public async Task<List<UbicacionDTO.Provincia>> ObtenerProvinciasAsync()
        {
            return await ObtenerDatosDeApi<List<UbicacionDTO.Provincia>>(_urlProvincias);
        }

        private async Task<T> ObtenerDatosDeApi<T>(string url)
        {
            string json = "";

            // Crear una solicitud GET para obtener los datos
            WebRequest solicitud = WebRequest.Create(url);
            // Método GET
            solicitud.Method = "GET";

            using (WebResponse respuestaWeb = await solicitud.GetResponseAsync())
            {
                // Leer datos
                using (StreamReader lector = new StreamReader(respuestaWeb.GetResponseStream()))
                {
                    json = await lector.ReadToEndAsync();
                }
            }

            // Deserializar JSON usando Newtonsoft.Json
            T resultado = JsonConvert.DeserializeObject<T>(json);

            return resultado;
        }
    }
}
