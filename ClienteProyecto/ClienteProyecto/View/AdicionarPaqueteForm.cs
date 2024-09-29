using System;
using System.Dynamic;
using System.Net;
using System.Text.Json;
using System.Windows.Forms;
using ClienteApp.Model;
using ClienteApp.Service;
using RestSharp; // Para trabajar con RestSharp
using System; // Para tipos básicos como int, string, etc.
using System.Net; // Para el manejo de códigos de estado HTTP
using System.Text.Json; // Para la deserialización de JSON
using System.Windows.Forms;
using System.Net.Http; // Para usar los formularios y MessageBox


namespace ClienteProyecto.View
{
    public partial class AdicionarPaqueteForm : Form
    {
        private PaqueteCulturalService _service;

        public AdicionarPaqueteForm()
        {
            InitializeComponent();
            _service = new PaqueteCulturalService();
        }

        private async void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre no puede estar vacío.");
                    return;
                }

                if (!double.TryParse(txtPrecio.Text, out double precio) || precio <= 0)
                {
                    MessageBox.Show("El precio debe ser un número mayor que 0.");
                    return;
                }

                DateTime fechaInicio = dtFechaInicio.Value;
                DateTime fechaFin = dtFechaFin.Value;

                if (fechaInicio > fechaFin)
                {
                    MessageBox.Show("La fecha de inicio debe ser anterior a la fecha de fin.");
                    return;
                }


                _service.AdicionarPaquete(int.Parse(txtId.Text), txtNombre.Text, double.Parse(txtPrecio.Text), fechaInicio, fechaFin);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error de formato: " + ex.Message);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errorr: {ex.Message}");
            }
        }

    }
}
