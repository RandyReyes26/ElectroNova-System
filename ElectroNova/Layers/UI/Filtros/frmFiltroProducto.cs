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
    public partial class frmFiltroProducto : Form
    {
        public Productos ProductoSeleccionado { get; set; }
        public frmFiltroProducto()
        {
            InitializeComponent();
        }

        private void frmFiltroProducto_Load(object sender, EventArgs e)
        {
            CargarDatos();

        }

        private void toolStripBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripBtnBuscar_Click(object sender, EventArgs e)
        {
            string filtro = txtFiltro.Text.ToLower().Trim();

            if (string.IsNullOrEmpty(filtro))
                return;

            foreach (DataGridViewRow row in dgvDatos.Rows)
            {
                if (
                    row.Cells["Codigo_Barras"].Value.ToString().ToLower().Contains(filtro) ||
                    row.Cells["Informacion_General"].Value.ToString().ToLower().Contains(filtro)
                   )
                {
                    row.Selected = true;
                    dgvDatos.CurrentCell = row.Cells["Codigo_Barras"];
                    return;
                }
            }

            MessageBox.Show("Producto no encontrado");

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

        private async void CargarDatos()
        {
            IBLLProducto _BLLProducto = new BLLProducto();

            dgvDatos.AutoGenerateColumns = true;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            await Task.Delay(500);

            dgvDatos.DataSource = await _BLLProducto.ObtenerProducto();

            // Ocultar todo primero
            foreach (DataGridViewColumn col in dgvDatos.Columns)
            {
                col.Visible = false;
            }

            // Mostrar solo lo necesario
            dgvDatos.Columns["Codigo_Barras"].Visible = true;
            dgvDatos.Columns["Informacion_General"].Visible = true;

            // Encabezados bonitos
            dgvDatos.Columns["Codigo_Barras"].HeaderText = "Código de Barras";
            dgvDatos.Columns["Informacion_General"].HeaderText = "Información General";

            // Opcional: dejar el grid más limpio
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatos.MultiSelect = false;
            dgvDatos.ReadOnly = true;
            dgvDatos.RowHeadersVisible = false;
        }

        private void dgvDatos_DoubleClick(object sender, EventArgs e)
        {
            if (dgvDatos.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvDatos.CurrentRow.Cells["ID_Producto"].Value);

                IBLLProducto _BLLProducto = new BLLProducto();

                // 🔥 TRAE EL PRODUCTO COMPLETO DESDE BD
                ProductoSeleccionado = _BLLProducto.ObtenerProductoPorId(id);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }
    }
}
