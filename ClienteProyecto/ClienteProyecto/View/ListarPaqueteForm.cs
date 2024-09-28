using System;
using System.Windows.Forms;
using ClienteApp.Model;
using ClienteApp.Service;

namespace ClienteProyecto.View
{
    public partial class ListarPaquetesForm : Form
    {
        private PaqueteCulturalService _service;

        public ListarPaquetesForm()
        {
            InitializeComponent();
            _service = new PaqueteCulturalService();
        }

        private async void ListarPaquetesForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Llamada al servicio REST para listar todos los paquetes
                var paquetes = await _service.ListarPaquetesAsync();

                // Mostrar los paquetes en el DataGridView (suponiendo que tienes uno llamado dgvPaquetes)
                dgvPaquetes.DataSource = paquetes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
