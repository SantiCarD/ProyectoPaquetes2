using ClienteApp.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteApp.Service
{
    public class PaqueteCulturalService
    {
        private readonly HttpClient _httpClient;

        public PaqueteCulturalService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8080/api/packages/");
        }

        // Obtener todos los paquetes culturales
        public async Task<List<PaqueteCultural>> ListarPaquetesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("");
            MessageBox.Show(response.StatusCode.ToString());

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<PaqueteCultural>>(jsonResponse);
            }

            return new List<PaqueteCultural>();
        }

        // Buscar paquete cultural por Id
        public async Task<PaqueteCultural> BuscarPaquetePorIdAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(id.ToString());
            MessageBox.Show(response.StatusCode.ToString());

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<PaqueteCultural>(jsonResponse);
            }

            return null;
        }

        // Buscar paquete cultural por Nombre
        public async Task<PaqueteCultural> BuscarPaquetePorNombreAsync(string nombre)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"?nombre={nombre}");
            MessageBox.Show(response.StatusCode.ToString());

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<PaqueteCultural>(jsonResponse);
            }

            return null;
        }

        // Adicionar paquete cultural
        public async Task<bool> AdicionarPaqueteAsync(PaqueteCultural nuevoPaquete)
        {
            string jsonContent = JsonSerializer.Serialize(nuevoPaquete);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("", content);
            MessageBox.Show(response.StatusCode.ToString());

            return response.IsSuccessStatusCode;
        }

        // Actualizar paquete cultural
        public async Task<bool> ActualizarPaqueteAsync(int id, PaqueteCultural paqueteActualizado)
        {
            string jsonContent = JsonSerializer.Serialize(paqueteActualizado);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(id.ToString(), content);
            MessageBox.Show(response.StatusCode.ToString());

            return response.IsSuccessStatusCode;
        }

        // Eliminar paquete cultural
        public async Task<bool> EliminarPaqueteAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(id.ToString());
            MessageBox.Show(response.StatusCode.ToString());

            return response.IsSuccessStatusCode;
        }
    }
}
