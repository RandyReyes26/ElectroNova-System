using ElectroNova.Interfaces;
using ElectroNova.Layers.BLL;
using ElectroNova.Layers.DAL;
using ElectroNova.Layers.Entities;
using ElectroNova.Layers.Entities.DTO;
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
using static ElectroNova.Layers.Entities.DTO.UbicacionDTO;

namespace ElectroNova.Layers.UI
{
    public partial class frmClientes : Form
    {
        private int _idCliente = 0;
        //Byte[] imagenbyte;
        private IDALCliente _DALCliente;
        IBLLFoto _bLLFoto = new BLLFoto();
        private readonly BLLUbicacion _BLLDireccion;
        public frmClientes()
        {
            InitializeComponent();
            _BLLDireccion = new BLLUbicacion();
            ConfigurarPictureBox();
            txtIdentificacion.KeyDown += txtIdentificacion_KeyDown;
            //txtID.ReadOnly = true;

            dgvDatos.CellFormatting += dgvDatos_CellFormatting;

            _DALCliente = new DALCliente();

        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            CargarDatos();
            CargarProvincias();
            AplicarEstiloUI();
     

        }
        private async Task CargarProvincias()
        {
            //try
            //{
            List<Provincia> provincias = await _BLLDireccion.ObtenerProvinciasAsync();
            cmbProvincia.DataSource = provincias;
            cmbProvincia.DisplayMember = "Descripcion";
            cmbProvincia.ValueMember = "IdProvincia";
        }


        private void ConfigurarPictureBox()
        {
            pblImagen.SizeMode = PictureBoxSizeMode.Zoom;
            pblImagen.Width = 163; // Ancho del PictureBox
            pblImagen.Height = 171; // Alto del PictureBox
        }

        private async void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BLLCliente _IBLLGestionCliente = new BLLCliente();

            try
            {
                if (dgvDatos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione el registro.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int id = Convert.ToInt32(dgvDatos.SelectedRows[0].Cells["ID_Cliente"].Value);

                if (MessageBox.Show($"¿Seguro que desea borrar el registro de {id}?",
                    "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bool borrado = await _IBLLGestionCliente.BorrarCliente(id);

                    if (borrado)
                    {
                        CargarDatos();
                        Limpiar();
                        MessageBox.Show("Cliente eliminado correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el cliente.");
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show($"Ocurrió un error: {er.Message}");
            }

        }
        private async void CargarDatos()
        {
            IBLLCliente _BLLCliente = new BLLCliente();

            dgvDatos.AutoGenerateColumns = true;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            await Task.Delay(300);

            var clientes = await _BLLCliente.ObtenerClientes();

            var listaMostrar = clientes.Select(c => new
            {
                c.ID_Cliente,
                c.Identificacion,
                c.Nombre,
                c.Apellidos,
                Sexo = c.Sexo == 1 ? "M" : c.Sexo == 2 ? "F" : "",
                c.Telefono,
                c.Email,
                Estado = c.Estado ? "Activo" : "Inactivo"
            }).ToList();

            dgvDatos.DataSource = null;
            dgvDatos.DataSource = listaMostrar;

            dgvDatos.Columns["ID_Cliente"].HeaderText = "ID";
            dgvDatos.Columns["Identificacion"].HeaderText = "Identificación";
        }

        private void BuscarPorCedula()
        {
            IBLLPadron _BLLPadronDTO = new BLLPadron();

            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txtIdentificacion.Text))
            {
                errorProvider1.SetError(txtIdentificacion, "Se necesita el número de identificación");
                txtIdentificacion.Focus();
                return;
            }

            string cedula = txtIdentificacion.Text.Trim();

            if (cedula.Length != 9)
            {
                errorProvider1.SetError(txtIdentificacion, "El largo de la cédula debe ser de 9 dígitos");
                txtIdentificacion.Focus();
                return;
            }

            if (!cedula.All(char.IsDigit))
            {
                errorProvider1.SetError(txtIdentificacion, "La cédula solo debe contener números");
                txtIdentificacion.Focus();
                return;
            }

            try
            {
                PadronDTO oPadronDTO = _BLLPadronDTO.ObtenerPersonaPorId(cedula);

                if (oPadronDTO == null || string.IsNullOrWhiteSpace(oPadronDTO.nombre))
                {
                    MessageBox.Show("No se encontraron datos para esa cédula.");
                    return;
                }

                string[] array = oPadronDTO.nombre.Split(' ');

                txtNombre.Clear();
                txtApellidos.Clear();

                if (array.Length == 3)
                {
                    txtNombre.Text = array[0];
                    txtApellidos.Text = array[1] + " " + array[2];
                }
                else if (array.Length == 4)
                {
                    txtNombre.Text = array[0] + " " + array[1];
                    txtApellidos.Text = array[2] + " " + array[3];
                }
                else if (array.Length > 4)
                {
                    txtNombre.Text = array[0] + " " + array[1];
                    txtApellidos.Text = array[array.Length - 2] + " " + array[array.Length - 1];
                }
                else if (array.Length >= 2)
                {
                    txtNombre.Text = array[0];
                    txtApellidos.Text = string.Join(" ", array.Skip(1));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }

        private void txtIdentificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // quita el sonido del Enter
                BuscarPorCedula();
            }
        }

        private void toolStripEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila para editar.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = Convert.ToInt32(dgvDatos.SelectedRows[0].Cells["ID_Cliente"].Value);
            _idCliente = id;

            IBLLCliente _BLLCliente = new BLLCliente();
            Clientes oCliente = _BLLCliente.ObtenerClientePorId(id);

            if (oCliente == null)
            {
                MessageBox.Show("No se pudo obtener el cliente seleccionado.");
                return;
            }

            txtIdentificacion.Text = oCliente.Identificacion;
            txtNombre.Text = oCliente.Nombre;
            txtApellidos.Text = oCliente.Apellidos;
            mtbTelefono.Text = oCliente.Telefono;
            txtEmail.Text = oCliente.Email;
            txtDireccionExacta.Text = oCliente.DireccionExacta;
            cmbProvincia.Text = oCliente.Provincia;

            rbtMasculino.Checked = oCliente.Sexo == 1;
            rbtFemenino.Checked = oCliente.Sexo == 2;

            chkActivo.Checked = oCliente.Estado;
            chkInactivo.Checked = !oCliente.Estado;

            if (oCliente.Fotografia != null)
            {
                using (MemoryStream ms = new MemoryStream(oCliente.Fotografia))
                {
                    pblImagen.Image = Image.FromStream(ms);
                }
                pblImagen.Tag = oCliente.Fotografia;
            }
            else
            {
                pblImagen.Image = null;
                pblImagen.Tag = null;
            }

        }
        private void Limpiar()
        {
            _idCliente = 0;

            txtApellidos.Clear();
            txtDireccionExacta.Clear();
            mtbTelefono.Clear();
            txtNombre.Clear();
            txtIdentificacion.Clear();
            txtEmail.Clear();

            cmbProvincia.SelectedIndex = -1;

            rbtMasculino.Checked = false;
            rbtFemenino.Checked = false;

            chkActivo.Checked = true;
            chkInactivo.Checked = false;

            pblImagen.Image = null;
            pblImagen.Tag = null;
        }

        private void ToolStripMenuNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            //txtID.Select();
            txtIdentificacion.Focus();

        }

        private async void GuardartoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                IBLLCliente _BLLCliente = new BLLCliente();
                Clientes oCliente = new Clientes();

                oCliente.ID_Cliente = _idCliente;

                errorProvider1.Clear();

                if (string.IsNullOrWhiteSpace(txtIdentificacion.Text))
                {
                    errorProvider1.SetError(txtIdentificacion, "Cédula requerida");
                    txtIdentificacion.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    errorProvider1.SetError(txtNombre, "Nombre requerido");
                    txtNombre.Focus();
                    return;
                }

                if (!mtbTelefono.MaskCompleted)
                {
                    errorProvider1.SetError(mtbTelefono, "Ingrese un teléfono válido (8 dígitos).");
                    mtbTelefono.Focus();
                    return;
                }
                else
                {
                    errorProvider1.SetError(mtbTelefono, "");
                }

                bool esEdicion = _idCliente > 0;

                oCliente.Identificacion = txtIdentificacion.Text.Trim();
                oCliente.Pasaporte = "";
                oCliente.Nombre = txtNombre.Text.Trim();
                oCliente.Apellidos = txtApellidos.Text.Trim();
                oCliente.DireccionExacta = txtDireccionExacta.Text.Trim();
                oCliente.Provincia = cmbProvincia.Text;

                mtbTelefono.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                oCliente.Telefono = mtbTelefono.Text;

                oCliente.Email = txtEmail.Text.Trim();
                oCliente.Estado = chkActivo.Checked;

                if (rbtMasculino.Checked)
                    oCliente.Sexo = 1;
                else if (rbtFemenino.Checked)
                    oCliente.Sexo = 2;
                else
                    oCliente.Sexo = 0;

                if (pblImagen.Tag != null)
                    oCliente.Fotografia = (byte[])pblImagen.Tag;
                else
                    oCliente.Fotografia = null;

                oCliente = await _BLLCliente.GuardarCliente(oCliente);

                if (oCliente != null)
                {
                    CargarDatos();
                    Limpiar();
                    MessageBox.Show(esEdicion ? "Cliente actualizado correctamente." : "Cliente guardado correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el cliente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el cliente: " + ex.Message);
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

        private void txtIdentificacion_Leave(object sender, EventArgs e)
        {
            BuscarPorCedula();
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

        private void AplicarColorRecursivo(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox || ctrl is MaskedTextBox)
                {
                    ctrl.BackColor = Color.White;
                    ctrl.ForeColor = Color.FromArgb(31, 41, 55);
                    ((TextBoxBase)ctrl).BorderStyle = BorderStyle.FixedSingle;
                }

                if (ctrl is ComboBox)
                {
                    ctrl.BackColor = Color.White;
                    ctrl.ForeColor = Color.FromArgb(31, 41, 55);
                }

                if (ctrl is Label)
                {
                    ctrl.ForeColor = Color.FromArgb(75, 85, 99);
                    ctrl.Font = new Font("Segoe UI", 9);
                }

                if (ctrl.HasChildren)
                    AplicarColorRecursivo(ctrl);
            }
        }

        private void AplicarEstiloUI()
        {
            // FORM
            this.BackColor = Color.FromArgb(244, 247, 251);
            // GROUPBOX
            groupBox1.BackColor = Color.White;
            groupBox2.BackColor = Color.White;

            groupBox1.ForeColor = Color.FromArgb(31, 41, 55);
            groupBox2.ForeColor = Color.FromArgb(31, 41, 55);

            // PICTUREBOX
            pblImagen.BackColor = Color.FromArgb(248, 250, 252);

            // TEXTOS
            foreach (Control ctrl in this.Controls)
            {
                AplicarColorRecursivo(ctrl);
            }

            // DATAGRIDVIEW
            EstiloDataGrid();
        }

        private void dgvDatos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDatos.Columns[e.ColumnIndex].Name == "Sexo" && e.Value != null)
            {
                // Si ya es string (M o F), no hacer nada
                if (e.Value is string)
                    return;

                int valor;
                if (int.TryParse(e.Value.ToString(), out valor))
                {
                    if (valor == 1)
                        e.Value = "M";
                    else if (valor == 2)
                        e.Value = "F";
                }
            }
        }
    }
}

