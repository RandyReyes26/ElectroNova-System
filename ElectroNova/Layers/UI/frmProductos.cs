using ElectroNova.Interfaces;
using ElectroNova.Layers.BLL;
using ElectroNova.Layers.Entities;
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

namespace ElectroNova.Layers.UI
{
    public partial class frmProductos : Form
    {
        IBLLFoto _bLLFoto = new BLLFoto();
        public frmProductos()
        {
            InitializeComponent();
        }
        private void ConfigurarPictureBox()
        {
            pblImagen.SizeMode = PictureBoxSizeMode.Zoom;
            pblImagen.Width = 163; // Ancho del PictureBox
            pblImagen.Height = 171; // Alto del PictureBox
        }
        private async void CargarDatos()
        {
            IBLLProducto _BLLProducto = new BLLProducto();
            //try
            //{

            dgvDatos.AutoGenerateColumns = true;
            // dgvDatos.RowTemplate.Height = 100;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // Delay forzado
            await Task.Delay(500);

            // Cargar el DataGridView
            this.dgvDatos.DataSource = await _BLLProducto.ObtenerProducto();

        }
        //    catch (Exception er)
        //    {
        //        MessageBox.Show($"Ocurrió un error: {er.Message}");

        //    }
        //}

        private void frmProductos_Load(object sender, EventArgs e)
        {
            CargarDatos();
            CargarComboMarca();
            CargarComboModelo();
            CargarComboTipoDispositivo();
        }
        private async void CargarComboMarca()
        {
            try
            {
                IBLLMarca _BLLMarca = new BLLMarca();
                var lista = await _BLLMarca.ObtenerMarca();

                cboMarca.DataSource = null;
                cboMarca.DataSource = lista.ToList();
                cboMarca.DisplayMember = "Nombre_Marca";
                cboMarca.ValueMember = "ID_Marca";
                cboMarca.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar marcas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CargarComboModelo()
        {
            try
            {
                IBLLModelo _BLLModelo = new BLLModelo();
                var lista = await _BLLModelo.ObtenerModelo();

                cboModelo.DataSource = null;
                cboModelo.DataSource = lista.ToList();
                cboModelo.DisplayMember = "Descripcion";
                cboModelo.ValueMember = "ID_Modelo";
                cboModelo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar modelos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CargarComboTipoDispositivo()
        {
            try
            {
                IBLLTipoDispositivo _BLLTipoDispositivo = new BLLTipoDispositivo();
                var lista = await _BLLTipoDispositivo.ObtenerTipoDispositivo();

                cboTipoProducto.DataSource = null;
                cboTipoProducto.DataSource = lista.ToList();
                cboTipoProducto.DisplayMember = "Nombre_TipoDispositivo";
                cboTipoProducto.ValueMember = "ID_TipoDispositivo";
                cboTipoProducto.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tipos de dispositivo: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToolStripMenuNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();

        }

        private async void GuardartoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                IBLLProducto _BLLProducto = new BLLProducto();
                Productos oProducto = new Productos();

                errorProvider1.Clear();

                if (string.IsNullOrWhiteSpace(txtCodigoBarras.Text))
                {
                    errorProvider1.SetError(txtCodigoBarras, "El código de barras es requerido");
                    txtCodigoBarras.Focus();
                    return;
                }

                if (cboMarca.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar una marca.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboMarca.Focus();
                    return;
                }

                if (cboModelo.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un modelo.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboModelo.Focus();
                    return;
                }

                if (cboTipoProducto.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un tipo de producto.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboTipoProducto.Focus();
                    return;
                }

         
                

                oProducto.Codigo_Barras = txtCodigoBarras.Text.Trim();
                oProducto.ID_Marca = Convert.ToInt32(cboMarca.SelectedValue);
                oProducto.ID_Modelo = Convert.ToInt32(cboModelo.SelectedValue);
                oProducto.ID_TipoDispositivo = Convert.ToInt32(cboTipoProducto.SelectedValue);
                oProducto.Informacion_General = txtInformacion.Text.Trim();
                oProducto.Caracteristicas_Tecnicas = txtCaracteristicas.Text.Trim();
                oProducto.Extras_Accesorios = txtExtrasAccesorios.Text.Trim();
                oProducto.Estado = chkActivo.Checked;

                if (pblImagen.Tag != null)
                    oProducto.Fotografia = (byte[])pblImagen.Tag;
                else
                    oProducto.Fotografia = null;

                var resultado = await _BLLProducto.GuardarProducto(oProducto);

                CargarDatos();
                Limpiar();

                if (resultado != null)
                {
                    MessageBox.Show("Producto guardado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Se guardó el producto, pero el método no retornó datos.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el producto: " + ex.Message,
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

                Productos oProducto = dgvDatos.SelectedRows[0].DataBoundItem as Productos;

                if (oProducto != null)
                {
                    //txtID_Producto.Text = oProducto.ID_Producto.ToString();
                    txtCodigoBarras.Text = oProducto.Codigo_Barras;
                    cboMarca.SelectedValue = oProducto.ID_Marca;
                    cboModelo.SelectedValue = oProducto.ID_Modelo;
                    cboTipoProducto.SelectedValue = oProducto.ID_TipoDispositivo;
                    txtInformacion.Text = oProducto.Informacion_General;
                    txtCaracteristicas.Text = oProducto.Caracteristicas_Tecnicas;
                    txtExtrasAccesorios.Text = oProducto.Extras_Accesorios;
                    chkActivo.Checked = oProducto.Estado;
                    chkInactivo.Checked = !oProducto.Estado;

                    if (oProducto.Fotografia != null)
                    {
                        using (MemoryStream ms = new MemoryStream(oProducto.Fotografia))
                        {
                            pblImagen.Image = Image.FromStream(ms);
                            pblImagen.Tag = oProducto.Fotografia;
                        }
                    }
                    else
                    {
                        pblImagen.Image = null;
                        pblImagen.Tag = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el producto para edición: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IBLLProducto _BLLProducto = new BLLProducto();

            try
            {
                if (dgvDatos.SelectedRows.Count > 0)
                {
                    Productos oProducto = dgvDatos.SelectedRows[0].DataBoundItem as Productos;

                    if (oProducto != null)
                    {
                        if (MessageBox.Show(
                            $"¿Seguro que desea eliminar el producto con ID {oProducto.ID_Producto}?",
                            "Confirmación",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            _BLLProducto.BorrarProducto(oProducto.ID_Producto);
                            CargarDatos();
                            Limpiar();

                            MessageBox.Show("Producto eliminado correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un registro.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el producto: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pblImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog opt = new OpenFileDialog();
            opt.Title = "Seleccione la imagen";
            opt.SupportMultiDottedExtensions = true;
            opt.DefaultExt = "*.jpg";
            opt.Filter = "Archivos de Imagenes (*.jpg;*.png)|*.jpg;*.png|All files (*.*)|*.*";
            opt.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            opt.FileName = "";

            if (opt.ShowDialog(this) == DialogResult.OK)
            {
                pblImagen.ImageLocation = opt.FileName;
                pblImagen.SizeMode = PictureBoxSizeMode.StretchImage;

                byte[] cadenaBytes = File.ReadAllBytes(opt.FileName);

                // Guarda la imagen en bytes en el Tag
                pblImagen.Tag = cadenaBytes;
            }
        }
        private void Limpiar()
        {
            //txtID_Producto.Clear();
            txtCodigoBarras.Clear();
            txtInformacion.Clear();
            txtCaracteristicas.Clear();
            txtExtrasAccesorios.Clear();

            if (cboMarca.Items.Count > 0)
                cboMarca.SelectedIndex = -1;

            if (cboModelo.Items.Count > 0)
                cboModelo.SelectedIndex = -1;

            if (cboTipoProducto.Items.Count > 0)
                cboTipoProducto.SelectedIndex = -1;

            chkActivo.Checked = true;
            chkInactivo.Checked = false;

            pblImagen.Image = null;
            pblImagen.Tag = null;
        }
    }
}
