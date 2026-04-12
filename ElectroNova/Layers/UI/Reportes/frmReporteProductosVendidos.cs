using ElectroNova.Interfaces;
using ElectroNova.Layers.BLL;
using ElectroNova.Layers.Entities.DTO;
using ElectroNova.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectroNova.Layers.Reportes
{
    public partial class frmReporteProductosVendidos : Form
    {
        private List<ProductoVendidoDTO> _listaProductosVendidos = new List<ProductoVendidoDTO>();
        public frmReporteProductosVendidos()
        {
            InitializeComponent();
            this.Load += frmReporteProductosVendidos_Load;
            dgvDatos.SelectionChanged += dgvDatos_SelectionChanged;
        }
        private async void frmReporteProductosVendidos_Load(object sender, EventArgs e)
        {
            try
            {
                await CargarMarcas();
                await CargarModelos();
                await CargarTiposDispositivo();

                lblCantidadVendida.Text = "Cantidad Vendida: 0";
                lblTotalVendido.Text = "Total Vendido: ₡0.00";

                pblImagen.SizeMode = PictureBoxSizeMode.Zoom;
                pblImagen.BorderStyle = BorderStyle.FixedSingle;
                pblImagen.BackColor = Color.White;

                dgvDatos.AutoGenerateColumns = true;
                dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvDatos.MultiSelect = false;
                dgvDatos.ReadOnly = true;
                dgvDatos.AllowUserToAddRows = false;
                dgvDatos.AllowUserToDeleteRows = false;
                dgvDatos.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el formulario: " + ex.Message,
                    "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task CargarMarcas()
        {
            IBLLMarca logica = new BLLMarca();
            var lista = (await logica.ObtenerMarca()).ToList();

            cmbMarca.DataSource = null;
            cmbMarca.DisplayMember = "Nombre_Marca";
            cmbMarca.ValueMember = "ID_Marca";
            cmbMarca.DataSource = lista;
            cmbMarca.SelectedIndex = -1;
        }

        private async Task CargarModelos()
        {
            IBLLModelo logica = new BLLModelo();
            var lista = (await logica.ObtenerModelo()).ToList();

            cmbModelo.DataSource = null;
            cmbModelo.DisplayMember = "Codigo_Modelo";
            cmbModelo.ValueMember = "ID_Modelo";
            cmbModelo.DataSource = lista;
            cmbModelo.SelectedIndex = -1;
        }

        private async Task CargarTiposDispositivo()
        {
            IBLLTipoDispositivo logica = new BLLTipoDispositivo();
            var lista = (await logica.ObtenerTipoDispositivo()).ToList();

            cmbTipoDispositivo.DataSource = null;
            cmbTipoDispositivo.DisplayMember = "Nombre_TipoDispositivo";
            cmbTipoDispositivo.ValueMember = "ID_TipoDispositivo";
            cmbTipoDispositivo.DataSource = lista;
            cmbTipoDispositivo.SelectedIndex = -1;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int? idMarca = cmbMarca.SelectedValue as int?;
                int? idModelo = cmbModelo.SelectedValue as int?;
                int? idTipo = cmbTipoDispositivo.SelectedValue as int?;

                // Si no selecciona nada, mandamos null
                if (cmbMarca.SelectedIndex == -1) idMarca = null;
                if (cmbModelo.SelectedIndex == -1) idModelo = null;
                if (cmbTipoDispositivo.SelectedIndex == -1) idTipo = null;

                IBLLReporteProducto logica = new BLLReporteProducto();

                _listaProductosVendidos = (logica.ObtenerProductosVendidos( idMarca, idModelo, idTipo)).ToList();

                dgvDatos.DataSource = null;
                dgvDatos.AutoGenerateColumns = true;
                dgvDatos.DataSource = _listaProductosVendidos;

                // 🔢 Totales
                int cantidad = _listaProductosVendidos.Sum(x => x.CantidadVendida);
                decimal total = _listaProductosVendidos.Sum(x => x.TotalVendido);

                lblCantidadVendida.Text = "Cantidad Vendida: " + cantidad;
                lblTotalVendido.Text = "Total Vendido: ₡" + total.ToString("N2");

                // 📸 Mostrar imagen del primero
                if (_listaProductosVendidos.Count > 0)
                {
                    dgvDatos.ClearSelection();
                    dgvDatos.Rows[0].Selected = true;
                    dgvDatos.CurrentCell = dgvDatos.Rows[0].Cells[0];

                    MostrarImagenSeleccionada();
                }
                else
                {
                    pblImagen.Image = null;

                    MessageBox.Show("No se encontraron resultados.",
                        "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar productos vendidos: " + ex.Message,
                    "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void MostrarImagenSeleccionada()
        {
            try
            {
                pblImagen.Image = null;

                if (dgvDatos.CurrentRow == null)
                    return;

                ProductoVendidoDTO obj = dgvDatos.CurrentRow.DataBoundItem as ProductoVendidoDTO;

                if (obj == null)
                {
                    MessageBox.Show("No se pudo obtener el producto seleccionado.");
                    return;
                }

                if (obj.Fotografia == null || obj.Fotografia.Length == 0)
                {
                    MessageBox.Show("Este producto no trae fotografía en el reporte.");
                    return;
                }

                using (MemoryStream ms = new MemoryStream(obj.Fotografia))
                {
                    pblImagen.Image = Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar imagen: " + ex.Message,
                    "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pblImagen.Image = null;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbMarca.SelectedIndex = -1;
            cmbModelo.SelectedIndex = -1;
            cmbTipoDispositivo.SelectedIndex = -1;

            dgvDatos.DataSource = null;
            pblImagen.Image = null;

            lblCantidadVendida.Text = "Cantidad Vendida: 0";
            lblTotalVendido.Text = "Total Vendido: ₡0.00";

            _listaProductosVendidos = new List<ProductoVendidoDTO>();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (_listaProductosVendidos == null || _listaProductosVendidos.Count == 0)
                {
                    MessageBox.Show("Primero debe buscar productos vendidos.",
                        "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PDFProductoVendido pdf = new PDFProductoVendido();
                pdf.Generar(_listaProductosVendidos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message,
                    "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {
            MostrarImagenSeleccionada();
        }
    }
}
