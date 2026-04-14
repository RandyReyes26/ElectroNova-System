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
    public partial class frmControlStock : Form
    {
        private int _idIngresoStock = 0;
        public frmControlStock()
        {
            InitializeComponent();
        }

        private void ToolStripMenuNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();

        }

        private async void GuardartoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                IBLLControlStock _BLLControlStock = new BLLControlStock();
                ControlStock oControlStock = new ControlStock();

                oControlStock.ID_IngresoStock = _idIngresoStock;

                errorProvider1.Clear();

                if (cboProductos.SelectedValue == null || cboProductos.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un producto.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboProductos.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCantidad.Text))
                {
                    errorProvider1.SetError(txtCantidad, "La cantidad es requerida.");
                    txtCantidad.Focus();
                    return;
                }

                if (!int.TryParse(txtCantidad.Text.Trim(), out int cantidad) || cantidad <= 0)
                {
                    errorProvider1.SetError(txtCantidad, "Ingrese una cantidad válida mayor que cero.");
                    txtCantidad.Focus();
                    return;
                }

                if (cboMovimientos.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cboMovimientos.Text))
                {
                    MessageBox.Show("Debe seleccionar el tipo de movimiento.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboMovimientos.Focus();
                    return;
                }

                if (cboMovimientos.Text == "Entrada")
                {
                    if (string.IsNullOrWhiteSpace(txtFacturaCompra.Text) || txtFacturaCompra.Text == "Ej: FAC-001")
                    {
                        errorProvider1.SetError(txtFacturaCompra, "La factura de compra es requerida.");
                        txtFacturaCompra.Focus();
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtFacturaCompra, "");
                    }
                }

                bool esEdicion = _idIngresoStock > 0;

                oControlStock.ID_Producto = Convert.ToInt32(cboProductos.SelectedValue);
                oControlStock.FacturaCompra = txtFacturaCompra.Text.Trim();
                oControlStock.Cantidad = cantidad;
                oControlStock.TipoMovimiento = cboMovimientos.Text;
                oControlStock.Observaciones = txtObservaciones.Text.Trim();

                await _BLLControlStock.GuardarStock(oControlStock);

                CargarDatos();
                Limpiar();

                MessageBox.Show(
                    esEdicion ? "Movimiento actualizado correctamente." : "Movimiento guardado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el movimiento: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void toolStripEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDatos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar una fila para editar.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int id = Convert.ToInt32(dgvDatos.SelectedRows[0].Cells["ID_IngresoStock"].Value);
                _idIngresoStock = id;

                IBLLControlStock _BLLControlStock = new BLLControlStock();
                ControlStock oControlStock = _BLLControlStock.ObtenerStockPorId(id);

                if (oControlStock != null)
                {
                    cboProductos.SelectedValue = oControlStock.ID_Producto;
                    cboMovimientos.Text = oControlStock.TipoMovimiento;
                    txtCantidad.Text = oControlStock.Cantidad.ToString();
                    txtFacturaCompra.Text = oControlStock.FacturaCompra;
                    txtObservaciones.Text = oControlStock.Observaciones;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el movimiento para edición: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IBLLControlStock _BLLControlStock = new BLLControlStock();

            try
            {
                if (dgvDatos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un registro.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = Convert.ToInt32(dgvDatos.SelectedRows[0].Cells["ID_IngresoStock"].Value);

                if (MessageBox.Show("¿Seguro que desea eliminar este movimiento?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bool borrado = await _BLLControlStock.BorrarStock(id);

                    if (borrado)
                    {
                        CargarDatos();
                        Limpiar();

                        MessageBox.Show("Movimiento eliminado correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el movimiento.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el movimiento: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void frmControlStock_Load(object sender, EventArgs e)
        {
            CargarDatos();
            CargarComboProducto();
            CargarComboMovimiento();
            txtFacturaCompra.ForeColor = Color.Gray;
            txtFacturaCompra.Text = "Ej: FAC-001";
            EstiloDataGrid();

        }

        private async void CargarDatos()
        {
            try
            {
                IBLLControlStock _BLLControlStock = new BLLControlStock();
                IBLLProducto _BLLProducto = new BLLProducto();

                dgvDatos.AutoGenerateColumns = true;
                dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                var stock = await _BLLControlStock.ObtenerStock();
                var productos = await _BLLProducto.ObtenerProducto();

                var listaMostrar = stock.Select(s => new
                {
                    s.ID_IngresoStock,
                    Producto = productos.FirstOrDefault(p => p.ID_Producto == s.ID_Producto)?.Informacion_General,
                    s.TipoMovimiento,
                    s.Cantidad,
                    s.FacturaCompra,
                    s.Observaciones
                }).ToList();

                dgvDatos.DataSource = null;
                dgvDatos.DataSource = listaMostrar;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CargarComboProducto()
        {
            try
            {
                IBLLProducto _BLLProducto = new BLLProducto();
                var lista = await _BLLProducto.ObtenerProducto();

                cboProductos.DataSource = null;
                cboProductos.DataSource = lista.ToList();
                cboProductos.DisplayMember = "Informacion_General"; // cambia esto si quieres mostrar otro campo
                cboProductos.ValueMember = "ID_Producto";
                cboProductos.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarComboMovimiento()
        {
            cboMovimientos.DataSource = null;
            cboMovimientos.Items.Clear();
            cboMovimientos.Items.Add("Entrada");
            cboMovimientos.Items.Add("Salida");
            cboMovimientos.SelectedIndex = -1;
        }
        private void Limpiar()
        {
            _idIngresoStock = 0;

            txtCantidad.Clear();
            txtFacturaCompra.Clear();
            txtObservaciones.Clear();

            if (cboProductos.Items.Count > 0)
                cboProductos.SelectedIndex = -1;

            if (cboMovimientos.Items.Count > 0)
                cboMovimientos.SelectedIndex = -1;

            txtFacturaCompra.Text = "Ej: FAC-001";
            txtFacturaCompra.ForeColor = Color.Gray;
            txtFacturaCompra.Enabled = true;
        }

        private void cboMovimientos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMovimientos.Text == "Salida")
            {
                txtFacturaCompra.Text = "";
                txtFacturaCompra.Enabled = false;
            }
            else
            {
                txtFacturaCompra.Enabled = true;

                if (string.IsNullOrWhiteSpace(txtFacturaCompra.Text))
                {
                    txtFacturaCompra.Text = "Ej: FAC-001";
                    txtFacturaCompra.ForeColor = Color.Gray;
                }
            }
        }

        private void txtFacturaCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) &&
               e.KeyChar != '-' &&
               e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtFacturaCompra_Enter(object sender, EventArgs e)
        {
            if (txtFacturaCompra.Text == "Ej: FAC-001")
            {
                txtFacturaCompra.Text = "";
                txtFacturaCompra.ForeColor = Color.Black;
            }
        }

        private void txtFacturaCompra_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFacturaCompra.Text))
            {
                txtFacturaCompra.Text = "Ej: FAC-001";
                txtFacturaCompra.ForeColor = Color.Gray;
            }
        }
        private void EstiloDataGrid()
        {
            dgvDatos.BorderStyle = BorderStyle.None;
            dgvDatos.BackgroundColor = Color.White;
            dgvDatos.EnableHeadersVisualStyles = false;

            // HEADER (lo dejamos azul porque se ve fino)
            dgvDatos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(47, 128, 237);
            dgvDatos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDatos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvDatos.ColumnHeadersHeight = 35;

            dgvDatos.DefaultCellStyle.BackColor = Color.White;
            dgvDatos.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);

            // 💚 VERDE SUAVE BONITO
            dgvDatos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(209, 250, 229);
            dgvDatos.DefaultCellStyle.SelectionForeColor = Color.FromArgb(6, 78, 59);

            // FILAS ALTERNAS
            dgvDatos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            dgvDatos.RowHeadersVisible = false;
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatos.MultiSelect = false;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDatos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }
    }
}
