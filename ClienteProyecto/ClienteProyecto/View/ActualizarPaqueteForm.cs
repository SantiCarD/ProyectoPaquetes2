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
            LlenarComboCriterio();
        }

        private void LlenarComboCriterio()
        {
            // Limpia el ComboBox para evitar duplicados
            cbCriterio.Items.Clear();

            // Agrega las opciones "Id" y "Nombre" al ComboBox
            cbCriterio.Items.Add("Id");
            cbCriterio.Items.Add("Nombre");

            // Selecciona el primer elemento por defecto
            cbCriterio.SelectedIndex = 0;
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string criterio = cbCriterio.SelectedItem.ToString();
                PaqueteCultural paquete = null;

                if (criterio == "Id") // Si el criterio es buscar por Id
                {
                    int resultado;
                    if (int.TryParse(txtBusqueda.Text, out resultado))
                    {
                        // Realizamos la búsqueda por Id utilizando el servicio REST
                        paquete = _service.BuscarPaquetePorId(resultado);
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese un Id válido.");
                        return;
                    }
                }
                else if (criterio == "Nombre") // Si el criterio es buscar por Nombre
                {
                    string nombre = txtBusqueda .Text;
                    // Realizamos la búsqueda por Nombre utilizando el servicio REST
                    paquete = _service.BuscarPaquetePorNombre(nombre);
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


                // Llamada al servicio REST para actualizar el paquete
                bool resultado =  _service.ActualizarPaquete(int.Parse(textBox1.Text), textBox5.Text, double.Parse(textBox3.Text), dtNuevaFechaInicio.Value, dtNuevaFechaFin.Value);

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
            txtNombre.Text = paquete.nombre;
            textBox1.Text = paquete.id.ToString();
            textBox2.Text = paquete.precio.ToString();
            textBox6.Text = paquete.fechaInicio.ToShortDateString();
            textBox7.Text = paquete.fechaFin.ToShortDateString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
