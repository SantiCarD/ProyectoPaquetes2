using System;
using System.Windows.Forms;
using ClienteApp.Model;
using ClienteApp.Service;

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
                // Crear el paquete cultural utilizando los valores de los campos
                PaqueteCultural nuevoPaquete = new PaqueteCultural
                {
                    Id = int.Parse(txtId.Text),
                    Nombre = txtNombre.Text,
                    Precio = double.Parse(txtPrecio.Text),
                    FechaInicio = dtFechaInicio.Value,
                    FechaFin = dtFechaFin.Value
                };

                // Llamada al servicio REST para agregar el paquete
                bool resultado = await _service.AdicionarPaqueteAsync(nuevoPaquete);

                // Mostrar el estado de la operación
                MessageBox.Show(resultado ? "Paquete añadido con éxito." : "Error al añadir el paquete.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
