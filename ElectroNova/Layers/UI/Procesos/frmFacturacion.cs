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
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");


        private readonly BLLConexionWebServer _service;
        public frmFacturacion()
        {
            InitializeComponent();
        }

        private void frmFacturación_Load(object sender, EventArgs e)
        {

        }

     

        private void txtColones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (double.TryParse(txtColones.Text, out double precioColones))
                    {
                        BLLConexionWebServer services = new BLLConexionWebServer();

                        IEnumerable<Dolar> listaDolares = services.GetDolar(DateTime.Now, DateTime.Now, "v");

                        if (listaDolares != null && listaDolares.Any())
                        {
                            Dolar ultimoDolar = listaDolares.Last();

                            double precioDolares = precioColones / ultimoDolar.Monto;

                            txtDolar.Text = $"{precioDolares:F2}";
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron datos para el tipo de cambio.");
                            txtDolar.Text = "0";
                        }
                    }
                    else
                    {
                        txtDolar.Text = "0";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al calcular: {ex.Message}");
                }
            }
        }
    }
}
