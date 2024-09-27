using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteProyecto.View
{
    public partial class AdicionarPaqueteForm : Form
    {
        private PaqueteCulturalService _service;
        public AdicionarPaqueteForm()
        {
            InitializeComponent();
            _service = new PaqueteCulturalService();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            // Capturar datos del formulario
            int id = int.Parse(txtId.Text);
            string nombre = txtNombre.Text;
            double precio = double.Parse(txtPrecio.Text);
            DateTime fechaInicio = dtFechaInicio.Value;
            DateTime fechaFin = dtFechaFin.Value;

            // Crear nuevo paquete
            PaqueteCultural nuevoPaquete = new PaqueteCultural(id, nombre, precio, fechaInicio, fechaFin);
            _service.AdicionarPaquete(nuevoPaquete);

            // Mensaje de éxito
            MessageBox.Show("Paquete cultural añadido exitosamente.");
        }
    }
}
