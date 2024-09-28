using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ClienteApp.Model;
using ClienteApp.Service;

namespace ClienteProyecto.View
{
    public partial class EliminarPaqueteForm : Form
    {
        private readonly PaqueteCulturalService _service;

        public EliminarPaqueteForm()
        {
            InitializeComponent();
            _service = new PaqueteCulturalService();
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            cbCriterio.Items.AddRange(new object[] { "Id", "Nombre" });
            cbCriterio.SelectedIndex = 0;
            ConfigurarCamposSoloLectura();
        }

        private void ConfigurarCamposSoloLectura()
        {
            txtId.ReadOnly = true;
            txtNombre.ReadOnly = true;
            txtPrecio.ReadOnly = true;
            dtFechaInicio.Enabled = false;
            dtFechaFin.Enabled = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string criterio = cbCriterio.SelectedItem.ToString();
                string valor = txtValor.Text;

                if (string.IsNullOrWhiteSpace(valor))
                {
                    MessageBox.Show("Por favor, ingrese un valor para buscar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                PaqueteCultural paquete = null;

                if (criterio == "Id")
                {
                    // Intenta buscar por ID
                    if (int.TryParse(valor, out int id))
                    {
                        paquete = _service.BuscarPaquetePorId(id);
                    }
                    else
                    {
                        MessageBox.Show("El valor del ID debe ser numérico.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (criterio == "Nombre")
                {
                    var paquetes = _service.BuscarPaquetesPorNombre(valor);

                    if (paquetes != null && paquetes.Count > 0)
                    {
                        // Si hay más de un resultado, toma el primero o implementa lógica para seleccionar uno
                        paquete = paquetes.First();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron paquetes con ese nombre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        return;
                    }
                }

                if (paquete != null)
                {
                    // Rellenar los campos del formulario con los datos del paquete encontrado
                    txtId.Text = paquete.Id.ToString();
                    txtNombre.Text = paquete.Nombre;
                    txtPrecio.Text = paquete.Precio.ToString("F2");
                    dtFechaInicio.Value = paquete.FechaInicio;
                    dtFechaFin.Value = paquete.FechaFin;
                }
                else
                {
                    MessageBox.Show("No se encontró ningún paquete con ese criterio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al realizar la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            dtFechaInicio.Value = DateTime.Now;
            dtFechaFin.Value = DateTime.Now;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(txtId.Text, out int id))
                {
                    PaqueteCultural paquete = _service.BuscarPaquetePorId(id);

                    if (paquete == null)
                    {
                        MessageBox.Show("No se encontró ningún paquete con ese ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Confirmación antes de eliminar
                    var result = MessageBox.Show($"¿Está seguro de que desea eliminar el paquete '{paquete.Nombre}'?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        _service.EliminarPaquete(id);
                        MessageBox.Show("Paquete eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                    }
                }
                else
                {
                    MessageBox.Show("El ID es inválido. Por favor, busque un paquete antes de intentar eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar eliminar el paquete: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
