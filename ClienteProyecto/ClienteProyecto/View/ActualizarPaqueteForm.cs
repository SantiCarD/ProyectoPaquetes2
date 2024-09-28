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
    public partial class ActualizarPaqueteForm : Form
    {
        private PaqueteCulturalService _service;
        private PaqueteCultural _paqueteActual;

        public ActualizarPaqueteForm()
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
            if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                MessageBox.Show("Por favor, ingrese un valor para buscar.");
                return;
            }

            string criterio = cbCriterio.SelectedItem.ToString();
            string valor = txtBusqueda.Text;
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
                _paqueteActual = paquete;
                HabilitarCamposEdicion(true);
            }
            else
            {
                MessageBox.Show("Paquete no encontrado.");
                LimpiarCampos();
                HabilitarCamposEdicion(false);
            }
        }

        private void MostrarPaquete(PaqueteCultural paquete)
        {
            txtNombre.Text = paquete.Nombre;
            dtFechaInicio.Value = paquete.FechaInicio;
            dtFechaFin.Value = paquete.FechaFin;
            dtNuevaFechaInicio.Value = paquete.FechaInicio;
            dtNuevaFechaFin.Value = paquete.FechaFin;
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            dtFechaInicio.Value = DateTime.Now;
            dtFechaFin.Value = DateTime.Now;
            dtNuevaFechaInicio.Value = DateTime.Now;
            dtNuevaFechaFin.Value = DateTime.Now;
        }

        private void HabilitarCamposEdicion(bool habilitar)
        {
            dtNuevaFechaInicio.Enabled = habilitar;
            dtNuevaFechaFin.Enabled = habilitar;
            btnActualizar.Enabled = habilitar;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (_paqueteActual == null)
            {
                MessageBox.Show("Por favor, busque un paquete primero.");
                return;
            }

            if (!ValidarCampos())
            {
                return;
            }

            _paqueteActual.Nombre = txtNombre.Text;
            _paqueteActual.FechaInicio = dtNuevaFechaInicio.Value;
            _paqueteActual.FechaFin = dtNuevaFechaFin.Value;

            bool actualizado = _service.ActualizarPaquete(_paqueteActual.Id, _paqueteActual);

            if (actualizado)
            {
                MessageBox.Show("Paquete actualizado con éxito.");
                LimpiarCampos();
                HabilitarCamposEdicion(false);
                _paqueteActual = null;
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el paquete. Por favor, intente nuevamente.");
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre no puede estar vacío.");
                return false;
            }

            if (dtNuevaFechaInicio.Value >= dtNuevaFechaFin.Value)
            {
                MessageBox.Show("La fecha de inicio debe ser anterior a la fecha de fin.");
                return false;
            }

            return true;
        }
    }
}