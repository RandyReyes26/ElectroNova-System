using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElectroNova.Interfaces;
using ElectroNova.Layers.BLL;
using ElectroNova.Layers.Entities;
using ElectroNova.Services;

namespace ElectroNova.Layers.Reportes
{
    public partial class frmReporteFacturas : Form
    {
        private List<Factura> _listaFacturasFiltradas = new List<Factura>();
        public frmReporteFacturas()
        {
            InitializeComponent();
            this.Load += frmReporteFacturas_Load;
        }

        private void frmReporteFacturas_Load(object sender, EventArgs e)
        {
            lblTotalFacturas.Text = "Total de facturas: 0";
            lblTotalCRC.Text = "Total CRC: ₡0.00";
            lblTotalUSD.Text = "Total USD: $0.00";

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaInicio = dtpFechaInicio.Value.Date;
                DateTime fechaFin = dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1);

                IBLLFactura logica = new BLLFactura();
                var lista = logica.ObtenerFacturas();

                _listaFacturasFiltradas = lista
                    .Where(f => f.Fecha >= fechaInicio && f.Fecha <= fechaFin)
                    .ToList();

                dgvDatos.DataSource = null;
                dgvDatos.AutoGenerateColumns = true;
                dgvDatos.DataSource = _listaFacturasFiltradas;

                lblTotalFacturas.Text = "Total de facturas: " + _listaFacturasFiltradas.Count;

                decimal totalCRC = _listaFacturasFiltradas.Sum(x => x.TotalCRC);
                lblTotalCRC.Text = "Total CRC: ₡" + totalCRC.ToString("N2");

                decimal totalUSD = _listaFacturasFiltradas.Sum(x => x.TotalUSD);
                lblTotalUSD.Text = "Total USD: $" + totalUSD.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar facturas: " + ex.Message,
                    "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;

            dgvDatos.DataSource = null;

            lblTotalFacturas.Text = "Total de facturas: 0";
            lblTotalCRC.Text = "Total CRC: ₡0.00";
            lblTotalUSD.Text = "Total USD: $0.00";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (_listaFacturasFiltradas == null || _listaFacturasFiltradas.Count == 0)
                {
                    MessageBox.Show("Primero debe buscar facturas.",
                        "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PDFFactura pdf = new PDFFactura();
                pdf.Generar(_listaFacturasFiltradas, dtpFechaInicio.Value.Date, dtpFechaFin.Value.Date);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar PDF: " + ex.Message,
                    "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
