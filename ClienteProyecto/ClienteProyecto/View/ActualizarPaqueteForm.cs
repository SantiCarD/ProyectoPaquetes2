using System;
using System.Windows.Forms;
using ClienteApp.Model;
using ClienteApp.Service;

namespace ClienteProyecto.View
{
    public partial class ActualizarPaqueteForm : Form
    {
        private PaqueteCulturalService _service;
        private PaqueteCultural _paqueteActual;

        public ActualizarPaqueteForm()
        {
            InitializeComponent();
            _service = new PaqueteCulturalService();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string criterio = cbCriterio.SelectedItem.ToString();
                PaqueteCultural paquete = null;

                if (criterio == "Id") // Si el criterio es buscar por Id
                {
                    if (int.TryParse(cbCriterio.Text, out int id))
                    {
                        // Realizamos la búsqueda por Id utilizando el servicio REST
                        paquete = await _service.BuscarPaquetePorIdAsync(id);
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese un Id válido.");
                        return;
                    }
                }
                else if (criterio == "Nombre") // Si el criterio es buscar por Nombre
                {
                    string nombre = cbCriterio.Text;
                    // Realizamos la búsqueda por Nombre utilizando el servicio REST
                    paquete = await _service.BuscarPaquetePorNombreAsync(nombre);
                }

                if (paquete != null)
                {
                    MostrarPaquete(paquete);
                    _paqueteActual = paquete; // Guardar el paquete encontrado
                }
                else
                {
                    MessageBox.Show("Paquete no encontrado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_paqueteActual == null)
                {
                    MessageBox.Show("Por favor, busque un paquete primero.");
                    return;
                }

                // Mantiene la lógica de actualización de nombre y fechas
                _paqueteActual.Nombre = txtNombre.Text;
                _paqueteActual.FechaInicio = dtFechaInicio.Value;
                _paqueteActual.FechaFin = dtFechaFin.Value;

                // Llamada al servicio REST para actualizar el paquete
                bool resultado = await _service.ActualizarPaqueteAsync(_paqueteActual.Id, _paqueteActual);

                // Mostrar el estado de la operación
                MessageBox.Show(resultado ? "Paquete actualizado con éxito." : "Error al actualizar el paquete.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void MostrarPaquete(PaqueteCultural paquete)
        {
            txtNombre.Text = paquete.Nombre;
            dtFechaInicio.Value = paquete.FechaInicio;
            dtFechaFin.Value = paquete.FechaFin;
        }
    }
}
