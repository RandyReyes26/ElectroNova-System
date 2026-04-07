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
            IBLLMarca _BLLMarca = new BLLMarca();
            IBLLModelo _BLLModelo = new BLLModelo();
            IBLLTipoDispositivo _BLLTipo = new BLLTipoDispositivo();

            dgvDatos.AutoGenerateColumns = true;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            await Task.Delay(200);

            var productos = await _BLLProducto.ObtenerProducto();
            var marcas = await _BLLMarca.ObtenerMarca();
            var modelos = await _BLLModelo.ObtenerModelo();
            var tipos = await _BLLTipo.ObtenerTipoDispositivo();

            var listaMostrar = productos.Select(p => new
            {
                p.ID_Producto,
                p.Codigo_Barras,

                Marca = marcas.FirstOrDefault(m => m.ID_Marca == p.ID_Marca)?.Nombre_Marca,
                Modelo = modelos.FirstOrDefault(m => m.ID_Modelo == p.ID_Modelo)?.Descripcion,
                Tipo = tipos.FirstOrDefault(t => t.ID_TipoDispositivo == p.ID_TipoDispositivo)?.Nombre_TipoDispositivo,

                p.Informacion_General,
                p.Caracteristicas_Tecnicas,
                p.DocumentoEspecificaciones,
                p.Precio,
                p.Existencia,
                p.Extras_Accesorios,
                Estado = p.Estado ? "Activo" : "Inactivo"
            }).ToList();

            dgvDatos.DataSource = null;
            dgvDatos.DataSource = listaMostrar;

            // 🔥 Ocultar ID
            dgvDatos.Columns["ID_Producto"].Visible = false;

            // 🔥 Renombrar columnas
            dgvDatos.Columns["Codigo_Barras"].HeaderText = "Código";
            dgvDatos.Columns["Informacion_General"].HeaderText = "Descripción";
            dgvDatos.Columns["Caracteristicas_Tecnicas"].HeaderText = "Características";
            dgvDatos.Columns["Extras_Accesorios"].HeaderText = "Extras";

            // 🔥 UX PRO
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatos.MultiSelect = false;
            dgvDatos.ReadOnly = true;
            dgvDatos.RowHeadersVisible = false;
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            CargarDatos();
            CargarComboMarca();
            CargarComboModelo();
            CargarComboTipoDispositivo();
            txtCodigoBarras.ForeColor = Color.Gray;
            txtCodigoBarras.Text = "Ej: 7501234567890";
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
                if (string.IsNullOrWhiteSpace(txtCodigoBarras.Text) ||
                    txtCodigoBarras.Text == "Ej: 7501234567890")
                {
                    errorProvider1.SetError(txtCodigoBarras, "El código de barras es requerido.");
                    txtCodigoBarras.Focus();
                    return;
                }
                else
                {
                    errorProvider1.SetError(txtCodigoBarras, "");
                }
                if (txtCodigoBarras.Text.Trim().Length < 8)
                {
                    errorProvider1.SetError(txtCodigoBarras, "Ingrese un código de barras válido.");
                    txtCodigoBarras.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPrecio.Text) || !decimal.TryParse(txtPrecio.Text, out _))
                {
                    errorProvider1.SetError(txtPrecio, "Ingrese un precio válido");
                    txtPrecio.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtExistencia.Text) || !int.TryParse(txtExistencia.Text, out _))
                {
                    errorProvider1.SetError(txtExistencia, "Ingrese una existencia válida");
                    txtExistencia.Focus();
                    return;
                }



                oProducto.Codigo_Barras = txtCodigoBarras.Text.Trim();
                oProducto.ID_Marca = Convert.ToInt32(cboMarca.SelectedValue);
                oProducto.ID_Modelo = Convert.ToInt32(cboModelo.SelectedValue);
                oProducto.ID_TipoDispositivo = Convert.ToInt32(cboTipoProducto.SelectedValue);
                oProducto.Informacion_General = txtInformacion.Text.Trim();
                oProducto.Caracteristicas_Tecnicas = txtCaracteristicas.Text.Trim();
                oProducto.DocumentoEspecificaciones = txtDocumentoEspecificaciones.Text.Trim();
                oProducto.Precio = Convert.ToDecimal(txtPrecio.Text);
                oProducto.Existencia = Convert.ToInt32(txtExistencia.Text);
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

                // 🔥 Obtener ID desde el DataGrid
                int id = Convert.ToInt32(dgvDatos.SelectedRows[0].Cells["ID_Producto"].Value);

                // 🔥 Traer el producto real desde la BD
                IBLLProducto _BLLProducto = new BLLProducto();
                Productos oProducto = _BLLProducto.ObtenerProductoPorId(id);

                if (oProducto != null)
                {
                    txtCodigoBarras.Text = oProducto.Codigo_Barras;
                    cboMarca.SelectedValue = oProducto.ID_Marca;
                    cboModelo.SelectedValue = oProducto.ID_Modelo;
                    cboTipoProducto.SelectedValue = oProducto.ID_TipoDispositivo;

                    txtInformacion.Text = oProducto.Informacion_General;
                    txtCaracteristicas.Text = oProducto.Caracteristicas_Tecnicas;

                    // 🔥 DOCUMENTO
                    txtDocumentoEspecificaciones.Text = oProducto.DocumentoEspecificaciones;

                    // 🔥 PRECIO
                    txtPrecio.Text = oProducto.Precio.ToString();

                    txtExistencia.Text = oProducto.Existencia.ToString();

                    txtExtrasAccesorios.Text = oProducto.Extras_Accesorios;

                    chkActivo.Checked = oProducto.Estado;
                    chkInactivo.Checked = !oProducto.Estado;

                    // 🔥 IMAGEN
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

        private async void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IBLLProducto _BLLProducto = new BLLProducto();

            try
            {
                if (dgvDatos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un producto.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🔥 Obtener ID correctamente desde el DataGrid
                int id = Convert.ToInt32(dgvDatos.SelectedRows[0].Cells["ID_Producto"].Value);

                // 🔥 Confirmación
                var resultado = MessageBox.Show(
                    "¿Seguro que desea eliminar el producto seleccionado?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    await _BLLProducto.BorrarProducto(id);

                    // 🔥 Recargar grid
                    CargarDatos();
                    Limpiar();

                    MessageBox.Show("Producto eliminado correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtCodigoBarras.Text = "Ej: 7501234567890";
            txtCodigoBarras.ForeColor = Color.Gray;
            txtInformacion.Clear();
            txtCaracteristicas.Clear();
            txtExtrasAccesorios.Clear();
            txtExistencia.Clear();

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

        private void txtCodigoBarras_Enter(object sender, EventArgs e)
        {
            if (txtCodigoBarras.Text == "Ej: 7501234567890")
            {
                txtCodigoBarras.Text = "";
                txtCodigoBarras.ForeColor = Color.Black;
            }
        }

        private void txtCodigoBarras_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigoBarras.Text))
            {
                txtCodigoBarras.Text = "Ej: 7501234567890";
                txtCodigoBarras.ForeColor = Color.Gray;
            }
        }

        private void txtCodigoBarras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Seleccione el documento de especificaciones";
            ofd.Filter = "Archivos PDF o Word|*.pdf;*.doc;*.docx|Todos los archivos|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string carpetaDestino = Path.Combine(Application.StartupPath, "Documentos");

                if (!Directory.Exists(carpetaDestino))
                    Directory.CreateDirectory(carpetaDestino);

                string nombreArchivo = Path.GetFileName(ofd.FileName);
                string nuevaRuta = Path.Combine(carpetaDestino, nombreArchivo);

                File.Copy(ofd.FileName, nuevaRuta, true);

                txtDocumentoEspecificaciones.Text = nuevaRuta;
            }
        }
    }
}
