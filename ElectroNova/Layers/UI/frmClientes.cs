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

            _DALCliente = new DALCliente();

        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            CargarDatos();
            CargarProvincias();

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

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IBLLCliente _IBLLGestionCliente = new BLLCliente();
            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {
                    Clientes oGestionCliente = this.dgvDatos.SelectedRows[0].DataBoundItem as Clientes;
                    if (MessageBox.Show($"¿Seguro que desea borrar el registro de {oGestionCliente.ID_Cliente}?",
                        "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _IBLLGestionCliente.BorrarCliente(oGestionCliente.ID_Cliente);
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
        private async void CargarDatos()
        {
            IBLLCliente _BLLCliente = new BLLCliente();
            //try
            //{

            dgvDatos.AutoGenerateColumns = true;
            // dgvDatos.RowTemplate.Height = 100;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // Delay forzado
            await Task.Delay(500);

            // Cargar el DataGridView
            this.dgvDatos.DataSource = await _BLLCliente.ObtenerClientes();

        }
        //    catch (Exception er)
        //    {
        //        MessageBox.Show($"Ocurrió un error: {er.Message}");

        //    }
        //}

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
                MessageBox.Show("Debe seleccionar una fila para editar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Clientes oCliente = null;

            oCliente = this.dgvDatos.SelectedRows[0].DataBoundItem as Clientes;
            this.txtID.Text = oCliente.ID_Cliente.ToString();
            oCliente.Identificacion = this.txtIdentificacion.Text;
            this.txtIdentificacion.Text = oCliente.Pasaporte.ToString();
            this.txtNombre.Text = oCliente.Nombre;
            this.txtApellidos.Text = oCliente.Apellidos;
            this.rbtFemenino.Checked = oCliente.Sexo == 2 ? true : false;
            this.rbtMasculino.Checked = oCliente.Sexo == 1 ? true : false;
            this.txtTelefono.Text = oCliente.Telefono;
            this.txtEmail.Text = oCliente.Email;
            this.cmbProvincia.Text = oCliente.Provincia;
            chkActivo.Checked = oCliente.Estado;
            chkInactivo.Checked = !oCliente.Estado;

            // Convertir el texto del ComboBox a bool
            //bool estadoSeleccionado = this.cboEstadoCliente.Text == "1";

        }
        private void Limpiar()
        {
            this.txtID.Clear();
            this.txtApellidos.Clear();
            this.txtDireccionExacta.Clear();
            this.txtTelefono.Clear();
            this.txtNombre.Clear();
            this.txtIdentificacion.Clear();
            this.txtEmail.Clear(); ;
            //cboEstadoCliente.SelectedIndex = -1;
            cmbProvincia.SelectedIndex = -1;
        }

        private void ToolStripMenuNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            txtID.Select();

        }

        private async void GuardartoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            IBLLCliente _BLLCliente = new BLLCliente();
            Clientes oCliente = new Clientes();

            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txtIdentificacion.Text))
            {
                errorProvider1.SetError(txtIdentificacion, "Cédula requerida");
                txtIdentificacion.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtID.Text))
            {
                oCliente.ID_Cliente = int.Parse(this.txtID.Text);
            }

            oCliente.Identificacion = this.txtIdentificacion.Text.Trim();
            oCliente.Pasaporte = this.txtIdentificacion.Text.Trim();
            oCliente.Nombre = this.txtNombre.Text.Trim();
            oCliente.Apellidos = this.txtApellidos.Text.Trim();
            oCliente.DireccionExacta = this.txtDireccionExacta.Text.Trim();
            oCliente.Provincia = this.cmbProvincia.Text;
            oCliente.Telefono = this.txtTelefono.Text.Trim();
            oCliente.Email = this.txtEmail.Text.Trim();
            oCliente.Estado = chkActivo.Checked;

            if (rbtMasculino.Checked)
                oCliente.Sexo = 1;
            else if (rbtFemenino.Checked)
                oCliente.Sexo = 2;

            if (pblImagen.Tag != null)
                oCliente.Fotografia = (byte[])pblImagen.Tag;

            oCliente = await _BLLCliente.GuardarCliente(oCliente);

            if (oCliente != null)
            {
                this.CargarDatos();
                MessageBox.Show("Cliente guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo guardar el cliente.");
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

    }
}

