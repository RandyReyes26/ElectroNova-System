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
        private int _idMarca = 0;
        public frmMarcas()
        {
            InitializeComponent();
        }

        private void frmMarcas_Load(object sender, EventArgs e)
        {
            CargarDatos();
            //txtID_Marca.ReadOnly = true;
            EstiloDataGrid();
   
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

                oMarca.ID_Marca = _idMarca; // 🔥 CLAVE
                oMarca.Nombre_Marca = txtNombreMarca.Text.Trim();
                oMarca.Descripcion = txtDescripcion.Text.Trim();
                oMarca.Estado = chkActivo.Checked;

                await _BLLMarca.GuardarMarca(oMarca);

                this.CargarDatos();
                this.Limpiar();

                if (_idMarca > 0)
                {
                    MessageBox.Show("Marca actualizada correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Marca guardada correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            Marca oMarca = this.dgvDatos.SelectedRows[0].DataBoundItem as Marca;

            if (oMarca != null)
            {
                _idMarca = oMarca.ID_Marca; // 🔥 ESTE ES EL FIX

                txtNombreMarca.Text = oMarca.Nombre_Marca;
                txtDescripcion.Text = oMarca.Descripcion;

                chkActivo.Checked = oMarca.Estado;
                chkInactivo.Checked = !oMarca.Estado;
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
            _idMarca = 0; // 🔥 IMPORTANTE

            txtNombreMarca.Clear();
            txtDescripcion.Clear();

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
            dgvDatos.DefaultCellStyle.SelectionForeColor = Color.FromArgb(6, 78, 59); // verde oscuro

 
            dgvDatos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            dgvDatos.RowHeadersVisible = false;
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatos.MultiSelect = false;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDatos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }
      
    }
    
}
