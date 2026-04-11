using ElectroNova.Interfaces;
using ElectroNova.Layers.BLL;
using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectroNova.Layers.UI.Filtros
{
    public partial class frmFiltroCliente : Form
    {
        public Clientes ClienteSeleccionado { get; set; }
        public frmFiltroCliente()
        {
            InitializeComponent();
        }

        private void frmFiltroCliente_Load(object sender, EventArgs e)
        {
            CargarDatos();

        }

        private async void CargarDatos()
        {
            IBLLCliente _BLLCliente = new BLLCliente();
            //try
            //{

            dgvDatos.AutoGenerateColumns = true;
            // dgvDatos.RowTemplate.Height = 100;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // Delay forzado
            await Task.Delay(500);

            // Cargar el DataGridView
            this.dgvDatos.DataSource = await _BLLCliente.ObtenerClientes();

        }

        

        private void toolStripBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripBtnBuscar_Click(object sender, EventArgs e)
        {
            string filtro = txtFiltro.Text.Trim();

            if (string.IsNullOrEmpty(filtro))
            {
                MessageBox.Show("Digite una cédula para buscar.");
                txtFiltro.Focus();
                return;
            }

            bool encontrado = false;

            foreach (DataGridViewRow row in dgvDatos.Rows)
            {
                if (row.Cells["Identificacion"]?.Value != null)
                {
                    string identificacion = row.Cells["Identificacion"].Value.ToString().Trim();

                    if (identificacion.Equals(filtro, StringComparison.OrdinalIgnoreCase))
                    {
                        row.Selected = true;
                        dgvDatos.CurrentCell = row.Cells[0];
                        dgvDatos.FirstDisplayedScrollingRowIndex = row.Index;
                        encontrado = true;
                        break;
                    }
                }
            }

            if (!encontrado)
            {
                MessageBox.Show("No se encontró ningún cliente con esa cédula.");
            }
        }

        private void toolStripBtnNuevo_Click(object sender, EventArgs e)
        {
            // Limpiar el textbox
            txtFiltro.Text = string.Empty;

            // Quitar selección del grid
            dgvDatos.ClearSelection();

            // Recargar todos los clientes
            CargarDatos();

            // Enfocar el filtro
            txtFiltro.Focus();

        }

        private void dgvDatos_DoubleClick(object sender, EventArgs e)
        {
            if (dgvDatos.CurrentRow != null)
            {
                DataGridViewRow row = dgvDatos.CurrentRow;

                ClienteSeleccionado = new Clientes
                {
                    ID_Cliente = Convert.ToInt32(row.Cells[0].Value),
                    Identificacion = row.Cells[1].Value.ToString(),
                    Nombre = row.Cells[3].Value.ToString(),
                    Apellidos = row.Cells[4].Value.ToString(),
                    Email = row.Cells["Email"].Value?.ToString()
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
