using System;
using System.Windows.Forms;
using ClienteApp.Model;
using ClienteApp.Service;

namespace ClienteProyecto.View
{
    public partial class EliminarPaqueteForm : Form
    {
        private PaqueteCulturalService _service;

        public EliminarPaqueteForm()
        {
            InitializeComponent();
            _service = new PaqueteCulturalService();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(txtId.Text, out int id))
                {
                    // Llamada al servicio REST para buscar el paquete por Id
                    PaqueteCultural paquete = await _service.BuscarPaquetePorIdAsync(id);

                    if (paquete != null)
                    {
                        // Mostrar los detalles del paquete en los campos
                        txtNombre.Text = paquete.Nombre;
                        txtPrecio.Text = paquete.Precio.ToString();
                        dtFechaInicio.Value = paquete.FechaInicio;
                        dtFechaFin.Value = paquete.FechaFin;
                        btnEliminar.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Paquete no encontrado.");
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un Id válido.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(txtId.Text, out int id))
                {
                    // Llamada al servicio REST para eliminar el paquete
                    bool resultado = await _service.EliminarPaqueteAsync(id);

                    // Mostrar el estado de la operación
                    MessageBox.Show(resultado ? "Paquete eliminado con éxito." : "Error al eliminar el paquete.");
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un Id válido.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
