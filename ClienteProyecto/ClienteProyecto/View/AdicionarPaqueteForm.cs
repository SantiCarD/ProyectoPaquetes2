using System;
using System.Windows.Forms;
using ClienteApp.Model;
using ClienteApp.Service;

namespace ClienteProyecto.View
{
    public partial class AdicionarPaqueteForm : Form
    {
        private readonly PaqueteCulturalService _service;

        public AdicionarPaqueteForm()
        {
            InitializeComponent();
            _service = new PaqueteCulturalService();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                int id = int.Parse(txtId.Text);
                string nombre = txtNombre.Text;
                double precio = double.Parse(txtPrecio.Text);
                DateTime fechaInicio = dtFechaInicio.Value;
                DateTime fechaFin = dtFechaFin.Value;

                PaqueteCultural nuevoPaquete = new PaqueteCultural(id, nombre, precio, fechaInicio, fechaFin);
                _service.AdicionarPaquete(nuevoPaquete);
                MessageBox.Show("Paquete cultural añadido exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingrese valores válidos en los campos.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al adicionar el paquete: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtFechaInicio.Value >= dtFechaFin.Value)
            {
                MessageBox.Show("La fecha de inicio debe ser anterior a la fecha de fin.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            dtFechaInicio.Value = DateTime.Now;
            dtFechaFin.Value = DateTime.Now.AddDays(1);
        }
    }
}