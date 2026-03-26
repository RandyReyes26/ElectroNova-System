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
        public frmModelos()
        {
            InitializeComponent();
        }

        private void frmModelos_Load(object sender, EventArgs e)
        {
            CargarDatos();
            txtID_Modelo.ReadOnly = true;

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

                // Validación
                if (string.IsNullOrWhiteSpace(txtCodigoModelo.Text))
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

                // Si trae ID, entonces es edición
                if (!string.IsNullOrWhiteSpace(txtID_Modelo.Text))
                {
                    oModelo.ID_Modelo = int.Parse(txtID_Modelo.Text);
                }

                // Asignar datos
                oModelo.Codigo_Modelo = txtCodigoModelo.Text.Trim();
                oModelo.Descripcion = txtDescripcion.Text.Trim();
                oModelo.Estado = true;   // IMPORTANTE

                // Guardar
                var resultado = await _BLLModelo.GuardarModelo(oModelo);

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

            Modelo oModelo = this.dgvDatos.SelectedRows[0].DataBoundItem as Modelo;

            if (oModelo != null)
            {
                txtID_Modelo.Text = oModelo.ID_Modelo.ToString();
                txtCodigoModelo.Text = oModelo.Codigo_Modelo;
                txtDescripcion.Text = oModelo.Descripcion;
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
            this.txtID_Modelo.Clear();
            this.txtCodigoModelo.Clear();
            this.txtDescripcion.Clear();
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
    }
}
