using ElectroNova.Interfaces;
using ElectroNova.Layers.Entities.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectroNova.Layers.BLL
{
    class BLLFoto : IBLLFoto
    {
        private string apiUrl;

        public BLLFoto()
        {
            apiUrl = ConfigurationManager.AppSettings["URLFoto"];
        }
        public async Task<FotoDTO> ObtenerFotoUsuarioAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Analiza la respuesta JSON para extraer la URL de la imagen
                    dynamic jsonResponse = JsonConvert.DeserializeObject(responseBody);
                    string imageUrl = jsonResponse.results[0].picture.large;

                    return new FotoDTO { ImagenUrl = imageUrl };
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"Error en la solicitud: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
