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
    public partial class frmTipoDispositivo : Form
    {
        private int _idTipoDispositivo = 0;
        public frmTipoDispositivo()
        {
            InitializeComponent();
        }

        private void frmTipoDispositivo_Load(object sender, EventArgs e)
        {
            CargarDatos();
            //txtID_TipoDispositivo.ReadOnly = true;
            EstiloDataGrid();
        }

        private void ToolStripMenuNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        private void Limpiar()
        {
            _idTipoDispositivo = 0;
            txtNombre_TipoDispositivo.Clear();
            txtDescripcion.Clear();
            chkActivo.Checked = false;
            chkInactivo.Checked = false;
        }

        private async void CargarDatos()
        {
            IBLLTipoDispositivo _BLLTipoDispositivo = new BLLTipoDispositivo();
            //try
            //{

            dgvDatos.AutoGenerateColumns = true;
            // dgvDatos.RowTemplate.Height = 100;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // Delay forzado
            await Task.Delay(500);

            // Cargar el DataGridView
            this.dgvDatos.DataSource = await _BLLTipoDispositivo.ObtenerTipoDispositivo();
        }

        private async void GuardartoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                IBLLTipoDispositivo _BLLTipoDispositivo = new BLLTipoDispositivo();
                TipoDispositivo oTipoDispositivo = new TipoDispositivo();

                errorProvider1.Clear();

                if (string.IsNullOrWhiteSpace(txtNombre_TipoDispositivo.Text))
                {
                    errorProvider1.SetError(txtNombre_TipoDispositivo, "El nombre del dispositivo es requerido");
                    txtNombre_TipoDispositivo.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    errorProvider1.SetError(txtDescripcion, "La descripción es requerida");
                    txtDescripcion.Focus();
                    return;
                }

                oTipoDispositivo.ID_TipoDispositivo = _idTipoDispositivo;
                oTipoDispositivo.Nombre_TipoDispositivo = txtNombre_TipoDispositivo.Text.Trim();
                oTipoDispositivo.Descripcion = txtDescripcion.Text.Trim();
                oTipoDispositivo.Estado = chkActivo.Checked;

                await _BLLTipoDispositivo.GuardarTipoDispositivo(oTipoDispositivo);

                bool eraEdicion = _idTipoDispositivo > 0;

                CargarDatos();
                Limpiar();

                MessageBox.Show(
                    eraEdicion ? "Tipo de dispositivo actualizado correctamente." : "Tipo de dispositivo guardado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el tipo de dispositivo: " + ex.Message,
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

            TipoDispositivo oTipoDispositivo = this.dgvDatos.SelectedRows[0].DataBoundItem as TipoDispositivo;

            if (oTipoDispositivo != null)
            {
                _idTipoDispositivo = oTipoDispositivo.ID_TipoDispositivo;

                txtNombre_TipoDispositivo.Text = oTipoDispositivo.Nombre_TipoDispositivo;
                txtDescripcion.Text = oTipoDispositivo.Descripcion;

                chkActivo.Checked = oTipoDispositivo.Estado;
                chkInactivo.Checked = !oTipoDispositivo.Estado;
            }

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IBLLTipoDispositivo _IBLLTipoDispositivo = new BLLTipoDispositivo();

            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {
                    TipoDispositivo oTipoDispositivo = this.dgvDatos.SelectedRows[0].DataBoundItem as TipoDispositivo;

                    if (oTipoDispositivo != null)
                    {
                        if (MessageBox.Show($"¿Seguro que desea borrar el registro de {oTipoDispositivo.ID_TipoDispositivo}?",
                            "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            _IBLLTipoDispositivo.BorrarTipoDispositivo(oTipoDispositivo.ID_TipoDispositivo);
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
