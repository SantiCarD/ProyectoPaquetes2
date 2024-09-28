using ClienteProyecto.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteProyecto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void adicionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdicionarPaqueteForm adicionarForm = new AdicionarPaqueteForm();
            adicionarForm.Show();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarPaqueteForm eliminarForm = new EliminarPaqueteForm();
            eliminarForm.Show();
        }

        private void listarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListarPaquetesForm listarForm = new ListarPaquetesForm();
            listarForm.Show();
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuscarPaqueteForm buscarForm = new BuscarPaqueteForm();
            buscarForm.Show();
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActualizarPaqueteForm actualizarForm = new ActualizarPaqueteForm();
            actualizarForm.Show();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AcercaDe acercaDeForm = new AcercaDe();
            acercaDeForm.Show();
        }
    }
}
