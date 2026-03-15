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
    public partial class frmMenuPrincipal : Form
    {
        public static string UsuarioNombre;
        private static Rol usuarioRol;
        private ToolStripMenuItem menuActivo = null;
        private static Form formularioActivo = null;
        public frmMenuPrincipal()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }

        private void frmMenuPrincipal_Load(object sender, EventArgs e)
        {
            ConfigurarPermisos();
            MostrarInformacionUsuario();
        }
        public void MostrarInformacionUsuario()
        {
            lblUsuario.Text = $"{UsuarioNombre}" + $" - {usuarioRol.Nombre_Rol}";
        }
        public static void ConfigurarRol(Rol rol)
        {
            usuarioRol = rol;
        }
        private void ConfigurarPermisos()
        {
            switch (usuarioRol.Nombre_Rol)
            {
                case "ADMINISTRADOR":
                    // Configuración para ADMINISTRADOR
                    toolStripMantenimiento.Enabled = true;
                    toolStripProcesos.Enabled = true;
                    ToolStripReportes.Enabled = true;
                    ToolStripAdministracion.Enabled = true;
                    break;

                case "VENDEDOR":
                    // Configuración para VENDEDOR
                    toolStripMantenimiento.Enabled = true;
                    toolStripProcesos.Enabled = true;
                    ToolStripReportes.Enabled = true;
                    ToolStripAdministracion.Visible = false;
                    break;

                case "REPORTES":
                    // Configuración para Reportes
                    toolStripMantenimiento.Visible = false;
                    toolStripProcesos.Visible = true;
                    ToolStripReportes.Enabled = false;
                    //facturaToolStripMenuItem.Enabled = false;
                    ToolStripAdministracion.Visible = false;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void toolStripMantenimiento_Click(object sender, EventArgs e)
        {
            if (formularioActivo == null || !(formularioActivo is frmAdministracion))
            {

                if (formularioActivo != null)
                {
                    CerrarFormulario(formularioActivo);
                }


                AbrirFormulario((ToolStripMenuItem)sender, new frmAdministracion());
            }


        }
        private void AbrirFormulario(ToolStripMenuItem menu, Form formulario)
        {

            if (menuActivo != null)
            {
                menuActivo.BackColor = SystemColors.Window;
            }
            menu.BackColor = Color.Silver;
            menuActivo = menu;
            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }
            formularioActivo = formulario;
            formularioActivo.TopLevel = false;
            formularioActivo.FormBorderStyle = FormBorderStyle.None;
            formularioActivo.Dock = DockStyle.Fill;
            formulario.BackColor = Color.WhiteSmoke;
            pnlContenido.Controls.Add(formularioActivo);

            formulario.Show();
        }
        private void CerrarFormulario(Form formulario)
        {
            if (formulario != null)
            {

                pnlContenido.Controls.Remove(formulario);
                formulario.Close();
                formulario.Dispose();
            }
        }

        private void AbrirFormulario(ToolStripItem menu, Form formulario)
        {
            if (menuActivo != null)
            {
                menuActivo.BackColor = SystemColors.Window;
            }

            if (menu != null && menu is ToolStripMenuItem itemMenu)
            {
                itemMenu.BackColor = Color.Silver;
                menuActivo = itemMenu;
            }

            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            formularioActivo = formulario;
            formularioActivo.TopLevel = false;
            formularioActivo.FormBorderStyle = FormBorderStyle.None;
            formularioActivo.Dock = DockStyle.Fill;
            formularioActivo.BackColor = Color.WhiteSmoke;

            pnlContenido.Controls.Clear();
            pnlContenido.Controls.Add(formularioActivo);
            formularioActivo.BringToFront();
            formularioActivo.Show();
        }

       

        private void clientesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (formularioActivo == null || !(formularioActivo is frmClientes))
            {

                if (formularioActivo != null)
                {
                    CerrarFormulario(formularioActivo);
                }


                AbrirFormulario((ToolStripMenuItem)sender, new frmClientes());
            }
        }
    }
}
