using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        private void ListarPaquetesForm_Load(object sender, EventArgs e)
        {
            FiltrarPaquetes(txtFilter.Text); // Cargar paquetes al inicio
        }

        private void btnFiltrar_Click(object sender, EventArgs e) // Cambia el nombre según tu botón
        {
            string filterText = txtFilter.Text.Trim(); // Obtener texto del filtro
            FiltrarPaquetes(filterText); // Filtrar paquetes
        }

        private async void btnRefrescar_Click(object sender, EventArgs e)
        {
             CargarPaquetes(); // Refrescar paquetes
        }

        private void CargarPaquetes()
        {
            try
            {
                // Llamada al servicio REST para listar todos los paquetes
                var paquetes =  _service.ListarPaquetes(); // Asegúrate de que el método sea async

                // Mostrar los paquetes en el DataGridView
                dgvPaquetes.DataSource = paquetes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        public void FiltrarPaquetes(string filterText)
        {
            List<PaqueteCultural> paquetesFiltrados = new List<PaqueteCultural>();
            try
            {
                // Llamada al servicio para filtrar los paquetes
                foreach (PaqueteCultural paq in _service.ListarPaquetes())
                {
                    if (paq.nombre.Contains(filterText))
                    {
                        paquetesFiltrados.Add(paq);
                    }
                }
                dgvPaquetes.DataSource = paquetesFiltrados;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


    }
}
