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
    public partial class EliminarPaqueteForm : Form
    {
        private PaqueteCulturalService _service;

        public EliminarPaqueteForm()
        {
            InitializeComponent();
            _service = new PaqueteCulturalService();
            InicializarComboBox();
        }

        private void InicializarComboBox()
        {
            cbCriterio.Items.Clear();
            cbCriterio.Items.Add("Id");
            cbCriterio.Items.Add("Nombre");

            if (cbCriterio.Items.Count > 0)
            {
                cbCriterio.SelectedIndex = 0;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cbCriterio.SelectedItem == null || string.IsNullOrWhiteSpace(txtValor.Text))
            {
                MessageBox.Show("Por favor, seleccione un criterio y ingrese un valor para buscar.");
                return;
            }

            string criterio = cbCriterio.SelectedItem.ToString();
            string valor = txtValor.Text;
            PaqueteCultural paquete = null;

            if (criterio == "Id")
            {
                if (int.TryParse(valor, out int id))
                {
                    paquete = _service.BuscarPaquetePorId(id);
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un número válido para el Id.");
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
                btnEliminar.Enabled = true;
            }
            else
            {
                MessageBox.Show("Paquete no encontrado.");
                LimpiarCampos();
                btnEliminar.Enabled = false;
            }
        }

        private void MostrarPaquete(PaqueteCultural paquete)
        {
            txtId.Text = paquete.Id.ToString();
            txtNombre.Text = paquete.Nombre;
            txtPrecio.Text = paquete.Precio.ToString("C");
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar este paquete?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    bool eliminado = _service.EliminarPaquete(id);
                    if (eliminado)
                    {
                        MessageBox.Show("Paquete eliminado con éxito.");
                        LimpiarCampos();
                        btnEliminar.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el paquete. Por favor, intente nuevamente.");
                    }
                }
            }
            else
            {
                MessageBox.Show("ID de paquete inválido.");
            }
        }
    }
}