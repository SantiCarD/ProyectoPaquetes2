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
            InicializarComboBox();
            ConfigurarCampos(false); // Deshabilitar campos de nueva fecha al inicio
        }

        private void InicializarComboBox()
        {
            cbCriterio.Items.Clear();
            cbCriterio.Items.Add("Id");
            cbCriterio.Items.Add("Nombre");
            cbCriterio.SelectedIndex = 0; // Seleccionar "Id" por defecto
        }

        // Método que habilita o deshabilita los campos
        private void ConfigurarCampos(bool habilitarNuevasFechas)
        {
            // Los campos dtFechaInicio y dtFechaFin siempre están deshabilitados
            dtFechaInicio.Enabled = false;
            dtFechaFin.Enabled = false;

            // Los campos dtNuevaFechaInicio y dtNuevaFechaFin se habilitan/deshabilitan según el valor de habilitarNuevasFechas
            dtNuevaFechaInicio.Enabled = habilitarNuevasFechas;
            dtNuevaFechaFin.Enabled = habilitarNuevasFechas;

            btnActualizar.Enabled = habilitarNuevasFechas; // Habilitar o deshabilitar el botón de actualizar según el estado
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string criterio = cbCriterio.SelectedItem.ToString();
                string valor = txtBusqueda.Text;
                PaqueteCultural paquete = null;

                // Buscar por Id o Nombre
                if (criterio == "Id")
                {
                    if (int.TryParse(valor, out int id))
                    {
                        paquete = _service.BuscarPaquetePorId(id);
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese un Id válido.");
                        return;
                    }
                }
                else if (criterio == "Nombre")
                {
                    var paquetes = _service.BuscarPaquetesPorNombre(valor);
                    if (paquetes.Count > 0)
                    {
                        paquete = paquetes[0]; // Si hay varios, toma el primero
                    }
                }

                if (paquete != null)
                {
                    MostrarPaquete(paquete);
                    _paqueteActual = paquete;  // Guardar el paquete encontrado
                    ConfigurarCampos(true);    // Habilitar los campos de nuevas fechas
                }
                else
                {
                    MessageBox.Show("Paquete no encontrado.");
                    LimpiarCampos();
                    ConfigurarCampos(false); // Deshabilitar los campos si no se encuentra el paquete
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_paqueteActual == null)
                {
                    MessageBox.Show("Por favor, busque un paquete primero.");
                    return;
                }

                // Validar campos antes de actualizar
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre no puede estar vacío.");
                    return;
                }

                if (dtNuevaFechaInicio.Value >= dtNuevaFechaFin.Value)
                {
                    MessageBox.Show("La fecha de inicio debe ser anterior a la fecha de fin.");
                    return;
                }

                // Actualizar el paquete
                _paqueteActual.Nombre = txtNombre.Text;
                _paqueteActual.FechaInicio = dtNuevaFechaInicio.Value;
                _paqueteActual.FechaFin = dtNuevaFechaFin.Value;

                bool actualizado = _service.ActualizarPaquete(_paqueteActual.Id, _paqueteActual);
                if (actualizado)
                {
                    MessageBox.Show("Paquete actualizado con éxito.");
                    LimpiarCampos();
                    ConfigurarCampos(false); // Deshabilitar campos después de actualizar
                    _paqueteActual = null;   // Limpiar la referencia al paquete actual
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el paquete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}");
            }
        }

        private void MostrarPaquete(PaqueteCultural paquete)
        {
            txtNombre.Text = paquete.Nombre;
            dtFechaInicio.Value = paquete.FechaInicio;
            dtFechaFin.Value = paquete.FechaFin;
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            dtFechaInicio.Value = DateTime.Now;
            dtFechaFin.Value = DateTime.Now;
            dtNuevaFechaInicio.Value = DateTime.Now;
            dtNuevaFechaFin.Value = DateTime.Now;
        }
    }
}
