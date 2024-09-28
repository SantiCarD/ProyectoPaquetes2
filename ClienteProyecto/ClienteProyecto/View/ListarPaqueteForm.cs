using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ClienteApp.Model;
using ClienteApp.Service;

namespace ClienteProyecto.View
{
    public partial class ListarPaquetesForm : Form
    {
        private readonly PaqueteCulturalService _service;

        public ListarPaquetesForm()
        {
            InitializeComponent();
            _service = new PaqueteCulturalService();
            ConfigurarDataGridView();
            CargarPaquetes();
        }

        private void ConfigurarDataGridView()
        {
            dgvPaquetes.AutoGenerateColumns = false;

            dgvPaquetes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 50
            });

            dgvPaquetes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nombre",
                HeaderText = "Nombre",
                Width = 200
            });

            dgvPaquetes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Precio",
                HeaderText = "Precio",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            dgvPaquetes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FechaInicio",
                HeaderText = "Fecha Inicio",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "d" }
            });

            dgvPaquetes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FechaFin",
                HeaderText = "Fecha Fin",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "d" }
            });
        }

        private void CargarPaquetes()
        {
            try
            {
                var paquetes = _service.ListarPaquetes();
                dgvPaquetes.DataSource = paquetes;

                if (paquetes.Count == 0)
                {
                    MessageBox.Show("No hay paquetes culturales disponibles.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los paquetes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarPaquetes();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            FiltrarPaquetes();
        }

        private void FiltrarPaquetes()
        {
            string filtro = txtBuscar.Text.ToLower();
            var paquetes = _service.ListarPaquetes();

            var paquetesFiltrados = paquetes.FindAll(p =>
                p.Id.ToString().Contains(filtro) ||
                p.Nombre.ToLower().Contains(filtro) ||
                p.Precio.ToString().Contains(filtro) ||
                p.FechaInicio.ToString("dd/MM/yyyy").Contains(filtro) ||
                p.FechaFin.ToString("dd/MM/yyyy").Contains(filtro)
            );

            dgvPaquetes.DataSource = paquetesFiltrados;
        }
    }
}