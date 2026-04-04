using ElectroNova.Layers.BLL;
using ElectroNova.Layers.Entities;
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

namespace ElectroNova.Layers.UI
{
    public partial class frmFacturacion : Form
    {
        private bool dibujando = false;
        private Point puntoAnterior;
        private Bitmap canvas;
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");


        private readonly BLLConexionWebServer _service;
        public frmFacturacion()
        {
            InitializeComponent();
        }

        private void frmFacturación_Load(object sender, EventArgs e)
        {
            CargarTipoCambio();
            canvas = new Bitmap(pictureFirma.Width, pictureFirma.Height);
            pictureFirma.Image = canvas;
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

        private void pictureFirma_MouseDown(object sender, MouseEventArgs e)
        {
            dibujando = true;
            puntoAnterior = e.Location;
        }

        private void pictureFirma_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dibujando) return;

            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.DrawLine(Pens.Black, puntoAnterior, e.Location);
            }

            pictureFirma.Invalidate();
            puntoAnterior = e.Location;
        }

        private void pictureFirma_MouseUp(object sender, MouseEventArgs e)
        {
            dibujando = false;
        }

        private void btnLimpiarFirma_Click(object sender, EventArgs e)
        {
            canvas = new Bitmap(pictureFirma.Width, pictureFirma.Height);
            pictureFirma.Image = canvas;
        }
    }
}
