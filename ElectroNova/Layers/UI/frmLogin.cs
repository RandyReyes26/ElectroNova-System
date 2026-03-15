using ElectroNova.Layers.BLL;
using ElectroNova.Layers.DAL;
using ElectroNova.Layers.Entities;
using ElectroNova.Layers.UI;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectroNova
{
    public partial class frmLogin : Form
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        private int intentosFallidos = 0;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            string login = this.txtLogin.Text.Trim();
            string password = this.mskContrasena.Text.Trim();

            if (string.IsNullOrEmpty(this.txtLogin.Text))
            {
                errorProvider1.SetError(txtLogin, "Usuario requerido");
                this.txtLogin.Focus();
                return;
            }
            if (string.IsNullOrEmpty(this.mskContrasena.Text))
            {
                errorProvider1.SetError(mskContrasena, "Contraseña requerida");
                this.mskContrasena.Focus();
                return;
            }

            Usuario usuario = new Usuario();
            BLLUsuario bLLUsuario = new BLLUsuario();

            try
            {
                // Intentar autenticar al usuario
                usuario = bLLUsuario.Login(login, password);

                if (usuario != null)
                {
                    // Obtener el rol del usuario
                    Rol rolUsuario = ObtenerRolUsuario(usuario);

                    frmMenuPrincipal.UsuarioNombre = usuario.NombreUsuario;

                    // Configurar el rol en FrmMenu
                    frmMenuPrincipal.ConfigurarRol(rolUsuario);

                    MessageBox.Show("¡Ingreso exitoso!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    frmMenuPrincipal frmMenu = new frmMenuPrincipal();
                    frmMenu.ShowDialog();
                }
                else
                {
                    intentosFallidos++;
                    MessageBox.Show("Nombre de usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (intentosFallidos >= 3)
                    {
                        MessageBox.Show("Ha alcanzado el número máximo de intentos fallidos. El botón de ingreso ha sido deshabilitado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnAcceder.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Muestra un mensaje más detallado sobre el error
                MessageBox.Show($"Ocurrió un error al intentar iniciar sesión: {ex.Message}\n\nDetalles: {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtLogin.Text = "admin";
            mskContrasena.Text = "123";

        }
        private Rol ObtenerRolUsuario(Usuario usuario)
        {
            DALRol dalRol = new DALRol();
            List<Rol> roles = dalRol.ObtenerTodosRol();
            return roles.FirstOrDefault(r => r.ID_Rol == usuario.ID_Rol);
        }
    }
}
