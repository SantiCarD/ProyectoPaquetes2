using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ClienteApp.Model;
using ClienteApp.Service;

namespace ClienteProyecto.View
{
    public partial class BuscarPaqueteForm : Form
    {
        private readonly PaqueteCulturalService _service;

        public BuscarPaqueteForm()
        {
            InitializeComponent();
            _service = new PaqueteCulturalService();
            ConfigurarControles();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
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
                    if (int.TryParse(valor, out int id))
                    {
                        paquete = _service.BuscarPaquetePorId(id);
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese un Id válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (criterio == "Nombre")
                {
                    var paquetes = _service.BuscarPaquetesPorNombre(valor);
                    if (paquetes.Count > 0)
                    {
                        paquete = paquetes[0];
                    }
                }

                if (paquete != null)
                {
                    MostrarPaquete(paquete);
                }
                else
                {
                    MessageBox.Show("Paquete no encontrado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar el paquete: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarPaquete(PaqueteCultural paquete)
        {
            txtId.Text = paquete.Id.ToString();
            txtNombre.Text = paquete.Nombre;
            txtPrecio.Text = paquete.Precio.ToString("C2");
            dtFechaInicio.Value = paquete.FechaInicio;
            dtFechaFin.Value = paquete.FechaFin;
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            dtFechaInicio.Value = DateTime.Now;
            dtFechaFin.Value = DateTime.Now;
        }
    }

}

