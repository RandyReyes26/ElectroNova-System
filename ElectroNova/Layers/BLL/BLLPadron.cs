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
    class BLLPadron : IBLLPadron
    {
        public PadronDTO ObtenerPersonaPorId(string pIdCliente)
        {
            string json = "";

            // Leer del App.Config el URL con el Key URLPadron
            string URLPadron = ConfigurationManager.AppSettings["URLPadron"];

            // Concatenar el Id al URL
            string url = URLPadron + pIdCliente;


            // Creates a GET request to fetch
            WebRequest request = WebRequest.Create(url);
            // Verb GET
            request.Method = "GET";

            using (WebResponse response = request.GetResponse())
            {
                // GetResponse returns a web response containing the response to the request
                using (WebResponse webResponse = request.GetResponse())
                {
                    // Reading data
                    StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                    json = reader.ReadToEnd();
                }
            }

            PadronDTO oPadronDTO = JsonConvert.DeserializeObject<PadronDTO>(json);

            return oPadronDTO;
        }
    }
}
