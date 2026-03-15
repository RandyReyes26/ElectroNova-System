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
    public partial class frmMarcas : Form
    {
        public frmMarcas()
        {
            InitializeComponent();
        }

        private void frmMarcas_Load(object sender, EventArgs e)
        {
            CargarDatos();

        }

        private void ToolStripMenuNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private async void GuardartoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            IBLLMarca _BLLMarca = new BLLMarca();
            Marca oMarca = new Marca();

            try
            {
                errorProvider1.Clear();

                if (string.IsNullOrWhiteSpace(txtNombreMarca.Text))
                {
                    errorProvider1.SetError(txtNombreMarca, "Nombre de marca requerido");
                    txtNombreMarca.Focus();
                    return;
                }

                oMarca.Nombre_Marca = txtNombreMarca.Text.Trim();
                oMarca.Descripcion = txtDescripcion.Text.Trim();
                oMarca.Estado = chkActivo.Checked;

                oMarca = await _BLLMarca.GuardarMarca(oMarca);

                if (oMarca != null)
                {
                    this.CargarDatos();
                    this.Limpiar();

                    MessageBox.Show("Marca guardada correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo guardar la marca.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la marca: {ex.Message}",
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

            Marca oMarca = null;

            oMarca = this.dgvDatos.SelectedRows[0].DataBoundItem as Marca;

            txtID_Marca.Text = oMarca.ID_Marca.ToString();
            txtNombreMarca.Text = oMarca.Nombre_Marca;
            txtDescripcion.Text = oMarca.Descripcion;

            if (oMarca.Estado)
            {
                chkActivo.Checked = true;
                chkInactivo.Checked = false;
            }
            else
            {
                chkActivo.Checked = false;
                chkInactivo.Checked = true;
            }

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IBLLMarca _IBLLMarca = new BLLMarca();

            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {
                    Marca oMarca = this.dgvDatos.SelectedRows[0].DataBoundItem as Marca;

                    if (MessageBox.Show($"¿Seguro que desea borrar el registro de {oMarca.ID_Marca} ?",
                        "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _IBLLMarca.BorrarMarca(oMarca.ID_Marca);
                        this.CargarDatos();
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione el registro !", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show($"Ocurrió un error: {er.Message}");
            }

        }

        private void Limpiar()
        {
            this.txtID_Marca.Clear();
            this.txtNombreMarca.Clear();
            this.txtDescripcion.Clear();

            chkActivo.Checked = false;
            chkInactivo.Checked = false;
        }
        private async void CargarDatos()
        {
            IBLLMarca _BLLMarca = new BLLMarca();
            //try
            //{

            dgvDatos.AutoGenerateColumns = true;
            // dgvDatos.RowTemplate.Height = 100;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // Delay forzado
            await Task.Delay(500);

            // Cargar el DataGridView
            this.dgvDatos.DataSource = await _BLLMarca.ObtenerMarca();

        }

    }
    
}
