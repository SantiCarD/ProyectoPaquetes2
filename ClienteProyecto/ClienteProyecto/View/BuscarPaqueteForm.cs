using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClienteProyecto.View
{
    public partial class BuscarPaqueteForm : Form
    {
        private PaqueteCulturalService _service;
        public BuscarPaqueteForm()
        {
            InitializeComponent();
            _service = new PaqueteCulturalService();
            inicializar();

        }

        private void inicializar()
        {
            cbCriterio.Items.Clear();
            cbCriterio.Items.Add("Id");
            cbCriterio.Items.Add("Nombre");
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = cbCriterio.SelectedItem.ToString();
            string valor = txtValor.Text;
            PaqueteCultural paquete = null;

            // Buscar por Id o Nombre
            if (criterio == "Id")
            {
                int id = int.Parse(valor);
                paquete = _service.BuscarPaquetePorId(id);
            }
            else if (criterio == "Nombre")
            {
                var paquetes = _service.BuscarPaquetesPorNombre(valor);
                if (paquetes.Count > 0)
                {
                    paquete = paquetes[0]; // Si hay varios, tomar el primero
                }
            }

            if (paquete != null)
            {
                txtId.Text = paquete.Id.ToString();
                txtNombre.Text = paquete.Nombre;
                txtPrecio.Text = paquete.Precio.ToString();
                dtFechaInicio.Value = paquete.FechaInicio;
                dtFechaFin.Value = paquete.FechaFin;
            }
            else
            {
                MessageBox.Show("Paquete no encontrado.");
            }
        }
    }
}