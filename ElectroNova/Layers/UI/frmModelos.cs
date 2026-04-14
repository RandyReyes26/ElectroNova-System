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

namespace ElectroNova.Layers.UI
{
    public partial class frmModelos : Form
    {
        private int _idModelo = 0;
        public frmModelos()
        {
            InitializeComponent();
        }

        private void frmModelos_Load(object sender, EventArgs e)
        {
            CargarDatos();
            //txtID_Modelo.ReadOnly = true;
            txtCodigoModelo.ForeColor = Color.Gray;
            txtCodigoModelo.Text = "Ej: SM-A54 o MOD-001";
            EstiloDataGrid();

        }

        private void ToolStripMenuNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private async void GuardartoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                IBLLModelo _BLLModelo = new BLLModelo();
                Modelo oModelo = new Modelo();

                errorProvider1.Clear();

                if (string.IsNullOrWhiteSpace(txtCodigoModelo.Text) ||
                    txtCodigoModelo.Text == "EJ: SM-A54 O MOD-001")
                {
                    errorProvider1.SetError(txtCodigoModelo, "El código del modelo es requerido");
                    txtCodigoModelo.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    errorProvider1.SetError(txtDescripcion, "La descripción es requerida");
                    txtDescripcion.Focus();
                    return;
                }

                oModelo.ID_Modelo = _idModelo;
                oModelo.Codigo_Modelo = txtCodigoModelo.Text.Trim();
                oModelo.Descripcion = txtDescripcion.Text.Trim();
                oModelo.Estado = chkActivo.Checked;

                await _BLLModelo.GuardarModelo(oModelo);

                CargarDatos();
                Limpiar();

                if (_idModelo > 0)
                    MessageBox.Show("Modelo actualizado correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Modelo guardado correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el modelo: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void toolStripEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila para editar.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Modelo oModelo = this.dgvDatos.SelectedRows[0].DataBoundItem as Modelo;

            if (oModelo != null)
            {
                _idModelo = oModelo.ID_Modelo;

                txtCodigoModelo.Text = oModelo.Codigo_Modelo;
                txtCodigoModelo.ForeColor = Color.Black;
                txtDescripcion.Text = oModelo.Descripcion;

                chkActivo.Checked = oModelo.Estado;
                chkInactivo.Checked = !oModelo.Estado;
            }

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IBLLModelo _IBLLModelo = new BLLModelo();

            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {
                    Modelo oModelo = this.dgvDatos.SelectedRows[0].DataBoundItem as Modelo;

                    if (oModelo != null)
                    {
                        if (MessageBox.Show($"¿Seguro que desea borrar el registro de {oModelo.ID_Modelo}?",
                            "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            _IBLLModelo.BorrarModelo(oModelo.ID_Modelo);
                            this.CargarDatos();
                            this.Limpiar();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione el registro.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show($"Ocurrió un error: {er.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Limpiar()
        {
            _idModelo = 0;

            txtCodigoModelo.Text = "EJ: SM-A54 O MOD-001";
            txtCodigoModelo.ForeColor = Color.Gray;
            txtDescripcion.Clear();

            chkActivo.Checked = false;
            chkInactivo.Checked = true;
        }

        private async void CargarDatos()
        {
            IBLLModelo _BLLModelo = new BLLModelo();
            //try
            //{

            dgvDatos.AutoGenerateColumns = true;
            // dgvDatos.RowTemplate.Height = 100;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // Delay forzado
            await Task.Delay(500);

            // Cargar el DataGridView
            this.dgvDatos.DataSource = await _BLLModelo.ObtenerModelo();
        }

        private void txtCodigoModelo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) &&
               e.KeyChar != '-' &&
               e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtCodigoModelo_Enter(object sender, EventArgs e)
        {
            if (txtCodigoModelo.Text == "EJ: SM-A54 O MOD-001")
            {
                txtCodigoModelo.Text = "";
                txtCodigoModelo.ForeColor = Color.Black;
            }
        }

        private void txtCodigoModelo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigoModelo.Text))
            {
                txtCodigoModelo.Text = "EJ: SM-A54 O MOD-001";
                txtCodigoModelo.ForeColor = Color.Gray;
            }
        }

        private void EstiloDataGrid()
        {
            dgvDatos.BorderStyle = BorderStyle.None;
            dgvDatos.BackgroundColor = Color.White;
            dgvDatos.EnableHeadersVisualStyles = false;

            dgvDatos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(47, 128, 237);
            dgvDatos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDatos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvDatos.ColumnHeadersHeight = 35;

            dgvDatos.DefaultCellStyle.BackColor = Color.White;
            dgvDatos.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);

            dgvDatos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(209, 250, 229);
            dgvDatos.DefaultCellStyle.SelectionForeColor = Color.FromArgb(6, 78, 59);

     
            dgvDatos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            dgvDatos.RowHeadersVisible = false;
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatos.MultiSelect = false;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDatos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }
    }
}
