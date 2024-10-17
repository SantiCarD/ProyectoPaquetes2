using System;
using System.Collections.Generic;
using System.Linq;
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
                var paquetes = _service.ListarPaquetes();

                // Limpia las filas anteriores para evitar duplicados
                dgvPaquetes.Rows.Clear();
                dgvPaquetes.Columns.Add("id", "ID");
                dgvPaquetes.Columns.Add("nombre", "Nombre");
                dgvPaquetes.Columns.Add("precio", "Precio");
                dgvPaquetes.Columns.Add("fechaInicio", "Fecha Inicio");
                dgvPaquetes.Columns.Add("fechaFin", "Fecha Fin");
                dgvPaquetes.Columns.Add("guias", "Guías"); // Columna para guías

                // Recorre cada paquete y agrega una fila al DataGridView
                foreach (PaqueteCultural paquete in paquetes)
                {
                    // Concatenar los nombres de los guías
                    string nombresGuias = paquete.guias != null && paquete.guias.Count > 0
                        ? string.Join(", ", paquete.guias.Select(g => g.nombre))
                        : "No hay guías"; // Mensaje alternativo si no hay guías

                    // Agregar la fila con los datos del paquete
                    dgvPaquetes.Rows.Add(
                        paquete.id,
                        paquete.nombre,
                        paquete.precio,
                        paquete.fechaInicio,
                        paquete.fechaFin,
                        nombresGuias // Asegúrate de que esta columna exista
                    );
                }
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

                foreach (PaqueteCultural paquete in paquetesFiltrados)
                {
                    // Concatenar los nombres de los guías
                    string nombresGuias = paquete.guias != null && paquete.guias.Count > 0
                        ? string.Join(", ", paquete.guias.Select(g => g.nombre))
                        : "No hay guías"; // Mensaje alternativo si no hay guías
                    dgvPaquetes.Rows.Clear();
                    dgvPaquetes.Columns.Add("id", "ID");
                    dgvPaquetes.Columns.Add("nombre", "Nombre");
                    dgvPaquetes.Columns.Add("precio", "Precio");
                    dgvPaquetes.Columns.Add("fechaInicio", "Fecha Inicio");
                    dgvPaquetes.Columns.Add("fechaFin", "Fecha Fin");
                    dgvPaquetes.Columns.Add("guias", "Guías"); // Columna para guías
                    // Agregar la fila con los datos del paquete
                    dgvPaquetes.Rows.Add(
                        paquete.id,
                        paquete.nombre,
                        paquete.precio,
                        paquete.fechaInicio,
                        paquete.fechaFin,
                        nombresGuias // Asegúrate de que esta columna exista
                    );
                }
                


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


    }
}
