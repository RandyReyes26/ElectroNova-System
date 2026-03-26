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
        public frmTipoDispositivo()
        {
            InitializeComponent();
        }

        private void frmTipoDispositivo_Load(object sender, EventArgs e)
        {
            CargarDatos();
            txtID_TipoDispositivo.ReadOnly = true;
        }

        private void ToolStripMenuNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        private void Limpiar()
        {
            this.txtID_TipoDispositivo.Clear();
            this.txtNombre_TipoDispositivo.Clear();
            this.txtDescripcion.Clear();
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

                // Validación
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

                // Si trae ID, entonces es edición
                if (!string.IsNullOrWhiteSpace(txtID_TipoDispositivo.Text))
                {
                    oTipoDispositivo.ID_TipoDispositivo = int.Parse(txtID_TipoDispositivo.Text);
                }

                // Asignar datos
                oTipoDispositivo.Nombre_TipoDispositivo = txtNombre_TipoDispositivo.Text.Trim();
                oTipoDispositivo.Descripcion = txtDescripcion.Text.Trim();
                oTipoDispositivo.Estado = true;   // IMPORTANTE

                // Guardar
                var resultado = await _BLLTipoDispositivo.GuardarTipoDispositivo(oTipoDispositivo);

                // Recargar grid
                CargarDatos();
                Limpiar();

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

            TipoDispositivo oTipoDispositivo = this.dgvDatos.SelectedRows[0].DataBoundItem as TipoDispositivo;

            if (oTipoDispositivo != null)
            {
                txtID_TipoDispositivo.Text = oTipoDispositivo.ID_TipoDispositivo.ToString();
                txtNombre_TipoDispositivo.Text = oTipoDispositivo.Nombre_TipoDispositivo;
                txtDescripcion.Text = oTipoDispositivo.Descripcion;
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
    }
}
