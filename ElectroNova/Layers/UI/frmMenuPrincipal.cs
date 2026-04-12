using ElectroNova.Layers.BLL;
using ElectroNova.Layers.Entities;
using ElectroNova.Layers.Reportes;
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
            CargarTipoCambio();
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

        private void MarcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formularioActivo == null || !(formularioActivo is frmMarcas))
            {

                if (formularioActivo != null)
                {
                    CerrarFormulario(formularioActivo);
                }


                AbrirFormulario((ToolStripMenuItem)sender, new frmMarcas());
            }

        }

        private void ModeloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formularioActivo == null || !(formularioActivo is frmModelos))
            {

                if (formularioActivo != null)
                {
                    CerrarFormulario(formularioActivo);
                }


                AbrirFormulario((ToolStripMenuItem)sender, new frmModelos());
            }

        }

        private void tIPOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formularioActivo == null || !(formularioActivo is frmTipoDispositivo))
            {

                if (formularioActivo != null)
                {
                    CerrarFormulario(formularioActivo);
                }


                AbrirFormulario((ToolStripMenuItem)sender, new frmTipoDispositivo());
            }
        }

        private void pRODUCTOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formularioActivo == null || !(formularioActivo is frmProductos))
            {

                if (formularioActivo != null)
                {
                    CerrarFormulario(formularioActivo);
                }


                AbrirFormulario((ToolStripMenuItem)sender, new frmProductos());
            }

        }

        private void ToolStripAdministracion_Click(object sender, EventArgs e)
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

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (formularioActivo == null || !(formularioActivo is frmInformacion))
            {

                if (formularioActivo != null)
                {
                    CerrarFormulario(formularioActivo);
                }


                AbrirFormulario((ToolStripMenuItem)sender, new frmInformacion());
            }

        }

        private void controlStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formularioActivo == null || !(formularioActivo is frmControlStock))
            {

                if (formularioActivo != null)
                {
                    CerrarFormulario(formularioActivo);
                }


                AbrirFormulario((ToolStripMenuItem)sender, new frmControlStock());
            }

        }

        private void fACTURACÓNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formularioActivo == null || !(formularioActivo is frmFacturacion))
            {

                if (formularioActivo != null)
                {
                    CerrarFormulario(formularioActivo);
                }


                AbrirFormulario((ToolStripMenuItem)sender, new frmFacturacion());
            }
        }

        private void CargarTipoCambio()
        {
            try
            {
                BLLConexionWebServer service = new BLLConexionWebServer();

                IEnumerable<Dolar> lista = service.GetDolar(DateTime.Now, DateTime.Now, "v");

                if (lista != null && lista.Any())
                {
                    Dolar dolar = lista.Last();

                    lblTipoCambio.Text = $"₡ {dolar.Monto:N2}";
                }
                else
                {
                    lblTipoCambio.Text = "No disponible";
                }
            }
            catch (Exception ex)
            {
                lblTipoCambio.Text = "Error";
                MessageBox.Show("Error al obtener tipo de cambio: " + ex.Message);
            }
        }

        private void impuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formularioActivo == null || !(formularioActivo is frmImpuesto))
            {

                if (formularioActivo != null)
                {
                    CerrarFormulario(formularioActivo);
                }


                AbrirFormulario((ToolStripMenuItem)sender, new frmImpuesto());
            }

        }

        private void rEPORTESCLIENTESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(formularioActivo == null || !(formularioActivo is frmReporteClientes))
            {

                if (formularioActivo != null)
                {
                    CerrarFormulario(formularioActivo);
                }


                AbrirFormulario((ToolStripMenuItem)sender, new frmReporteClientes());
            }

        }

        private void rEPORTESDEFACTURAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formularioActivo == null || !(formularioActivo is frmReporteFacturas))
            {

                if (formularioActivo != null)
                {
                    CerrarFormulario(formularioActivo);
                }


                AbrirFormulario((ToolStripMenuItem)sender, new frmReporteFacturas());
            }

        }

        private void rEPORTEDELPRODUCTOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formularioActivo == null || !(formularioActivo is frmReporteProductosVendidos))
            {

                if (formularioActivo != null)
                {
                    CerrarFormulario(formularioActivo);
                }


                AbrirFormulario((ToolStripMenuItem)sender, new frmReporteProductosVendidos());
            }

        }

        private void rEPORTEGRAFICOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formularioActivo == null || !(formularioActivo is frmGraficoVentas))
            {

                if (formularioActivo != null)
                {
                    CerrarFormulario(formularioActivo);
                }


                AbrirFormulario((ToolStripMenuItem)sender, new frmGraficoVentas());
            }

        }
    }
}
