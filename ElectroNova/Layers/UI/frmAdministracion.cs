using ElectroNova.Interfaces;
using ElectroNova.Layers.BLL;
using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectroNova.Layers.UI
{
    public partial class frmAdministracion : Form
    {
        public frmAdministracion()
        {
            InitializeComponent();
        }

        private async void frmAdministracion_Load(object sender, EventArgs e)
        {
            await CargarRoles();
            await CargarDatos();
            Limpiar();
        }

        private async Task CargarRoles()
        {
            try
            {
                IBLLRol _BLLRol = new BLLRol();
                var listaRoles = _BLLRol.ObtenerTodos();

                cboRol.DataSource = null;
                cboRol.DataSource = listaRoles;
                cboRol.DisplayMember = "Nombre_Rol";
                cboRol.ValueMember = "ID_Rol";
                cboRol.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar roles: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarDatos()
        {
            IBLLUsuario _BLLUsuario = new BLLUsuario();

            try
            {
                dgvDatos.AutoGenerateColumns = true;
                dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvDatos.MultiSelect = false;
                dgvDatos.ReadOnly = true;

                await Task.Delay(300);

                this.dgvDatos.DataSource = _BLLUsuario.ObtenerTodos();
            }
            catch (Exception er)
            {
                MessageBox.Show($"Ocurrió un error al cargar los datos: {er.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToolStripMenuNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            txtID_Usuario.Focus();
        }

        private async void GuardartoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            IBLLUsuario _BLLUsuario = new BLLUsuario();

            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
                {
                    MessageBox.Show("Debe ingresar el nombre de usuario.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombreUsuario.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtContrasenia.Text))
                {
                    MessageBox.Show("Debe ingresar la contraseña.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContrasenia.Focus();
                    return;
                }

                if (cboRol.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un rol.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboRol.Focus();
                    return;
                }

                if (!chkActivo.Checked && !chkInactivo.Checked)
                {
                    MessageBox.Show("Debe seleccionar el estado del usuario.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Usuario oUsuario = new Usuario();

                oUsuario.ID_Usuario = string.IsNullOrWhiteSpace(txtID_Usuario.Text)
                    ? 0
                    : Convert.ToInt32(txtID_Usuario.Text);

                oUsuario.NombreUsuario = txtNombreUsuario.Text.Trim();
                oUsuario.Contrasena = txtContrasenia.Text.Trim();
                oUsuario.ID_Rol = Convert.ToInt32(cboRol.SelectedValue);
                oUsuario.Estado = chkActivo.Checked;

                if (oUsuario.ID_Usuario == 0)
                {
                    _BLLUsuario.GuardarUsuario(oUsuario);
                    MessageBox.Show("Usuario guardado correctamente.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _BLLUsuario.Actualizar(oUsuario);
                    MessageBox.Show("Usuario actualizado correctamente.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                await CargarDatos();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error al guardar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDatos.SelectedRows.Count > 0)
                {
                    Usuario oUsuario = dgvDatos.SelectedRows[0].DataBoundItem as Usuario;

                    if (oUsuario != null)
                    {
                        txtID_Usuario.Text = oUsuario.ID_Usuario.ToString();
                        txtNombreUsuario.Text = oUsuario.NombreUsuario;
                        txtContrasenia.Text = oUsuario.Contrasena;
                        cboRol.SelectedValue = oUsuario.ID_Rol;

                        if (oUsuario.Estado)
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
                }
                else
                {
                    MessageBox.Show("Seleccione un registro para editar.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al editar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IBLLUsuario _IBLLUsuario = new BLLUsuario();

            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {
                    Usuario oUsuario = this.dgvDatos.SelectedRows[0].DataBoundItem as Usuario;

                    if (oUsuario != null)
                    {
                        if (MessageBox.Show(
                            $"¿Seguro que desea borrar el registro del usuario {oUsuario.ID_Usuario}?",
                            "Confirmación",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //await _IBLLUsuario.EiminarUsuario(oUsuario.ID_Usuario);
                            await this.CargarDatos();
                            this.Limpiar();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione el registro.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            txtID_Usuario.Clear();
            txtNombreUsuario.Clear();
            txtContrasenia.Clear();
            cboRol.SelectedIndex = -1;
            chkActivo.Checked = false;
            chkInactivo.Checked = false;
        }

        private void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActivo.Checked)
                chkInactivo.Checked = false;
        }

        private void chkInactivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInactivo.Checked)
                chkActivo.Checked = false;
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Usuario oUsuario = dgvDatos.Rows[e.RowIndex].DataBoundItem as Usuario;

                if (oUsuario != null)
                {
                    txtID_Usuario.Text = oUsuario.ID_Usuario.ToString();
                    txtNombreUsuario.Text = oUsuario.NombreUsuario;
                    txtContrasenia.Text = oUsuario.Contrasena;
                    cboRol.SelectedValue = oUsuario.ID_Rol;

                    chkActivo.Checked = oUsuario.Estado;
                    chkInactivo.Checked = !oUsuario.Estado;
                }
            }
        }
    }
}