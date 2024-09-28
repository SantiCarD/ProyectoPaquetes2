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


        private void MostrarPaquete(PaqueteCultural paquete)
        {
            txtId.Text = paquete.Id.ToString();
            txtNombre.Text = paquete.Nombre;
            dtFechaInicio.Value = paquete.FechaInicio;
            dtFechaFin.Value = paquete.FechaFin;
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
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
                    // Llamada al servicio REST para eliminar el paquete
                    bool resultado = await _service.EliminarPaqueteAsync(paquete.Id);

                    // Mostrar el estado de la operación
                    MessageBox.Show(resultado ? "Paquete eliminado con éxito." : "Error al eliminar el paquete.");
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

    }
}

