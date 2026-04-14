using ElectroNova.Interfaces;
using ElectroNova.Layers.BLL;
using ElectroNova.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ElectroNova.Layers.Reportes
{
    public partial class frmGraficoVentas : Form
    {
        public frmGraficoVentas()
        {
            InitializeComponent();
        }

        private void frmGraficoVentas_Load(object sender, EventArgs e)
        {
            cmbTipoGrafico.Items.Add("Columnas");
            cmbTipoGrafico.Items.Add("Barras");
            cmbTipoGrafico.Items.Add("Líneas");
            cmbTipoGrafico.Items.Add("Área");
            cmbTipoGrafico.Items.Add("Área Suave");
            cmbTipoGrafico.Items.Add("Línea Suave");
            cmbTipoGrafico.Items.Add("Pastel");
            cmbTipoGrafico.Items.Add("Dona");

            cmbTipoGrafico.SelectedIndex = 0;

            dtpFechaInicial.Value = DateTime.Now.Date;
            dtpFechaFinal.Value = DateTime.Now.Date;

            chartVentas.Series.Clear();
            chartVentas.Titles.Clear();
            chartVentas.ChartAreas[0].AxisX.Title = "Fecha";
            chartVentas.ChartAreas[0].AxisY.Title = "Total Vendido CRC";
        }

        private void btnGrafica_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaInicio = dtpFechaInicial.Value.Date;
                DateTime fechaFin = dtpFechaFinal.Value.Date.AddDays(1).AddSeconds(-1);

                if (fechaInicio > fechaFin)
                {
                    MessageBox.Show("La fecha inicial no puede ser mayor a la fecha final.",
                        "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                IBLLFactura logica = new BLLFactura();
                var listaFacturas = logica.ObtenerFacturas();

                var datos = listaFacturas
                    .Where(f => f.Fecha >= fechaInicio && f.Fecha <= fechaFin && f.Estado)
                    .GroupBy(f => f.Fecha.Date)
                    .Select(g => new
                    {
                        Fecha = g.Key,
                        Total = g.Sum(x => x.TotalCRC)
                    })
                    .OrderBy(x => x.Fecha)
                    .ToList();

                if (datos.Count == 0)
                {
                    chartVentas.Series.Clear();
                    MessageBox.Show("No hay ventas en ese rango de fechas.",
                        "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                chartVentas.Series.Clear();
                chartVentas.Titles.Clear();

                Series serie = new Series("Ventas");
                serie.IsValueShownAsLabel = true;
                serie.BorderWidth = 3;

                switch (cmbTipoGrafico.SelectedItem.ToString())
                {
                    case "Columnas":
                        serie.ChartType = SeriesChartType.Column;
                        break;

                    case "Barras":
                        serie.ChartType = SeriesChartType.Bar;
                        break;

                    case "Líneas":
                        serie.ChartType = SeriesChartType.Line;
                        break;

                    case "Área":
                        serie.ChartType = SeriesChartType.Area;
                        break;

                    case "Área Suave":
                        serie.ChartType = SeriesChartType.SplineArea;
                        break;

                    case "Línea Suave":
                        serie.ChartType = SeriesChartType.Spline;
                        break;

                    case "Pastel":
                        serie.ChartType = SeriesChartType.Pie;
                        break;

                    case "Dona":
                        serie.ChartType = SeriesChartType.Doughnut;
                        break;

                    default:
                        serie.ChartType = SeriesChartType.Column;
                        break;
                }

                foreach (var item in datos)
                {
                    serie.Points.AddXY(item.Fecha.ToString("dd/MM/yyyy"), item.Total);
                }

                chartVentas.Series.Add(serie);
                chartVentas.Titles.Add("Ventas por rango de fecha");

                if (serie.ChartType == SeriesChartType.Pie || serie.ChartType == SeriesChartType.Doughnut)
                {
                    chartVentas.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
                    chartVentas.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
                    chartVentas.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    chartVentas.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                }
                else
                {
                    chartVentas.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
                    chartVentas.ChartAreas[0].AxisY.LabelStyle.Enabled = true;
                    chartVentas.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                    chartVentas.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar la gráfica: " + ex.Message,
                    "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (chartVentas.Series.Count == 0)
                {
                    MessageBox.Show("Primero debe generar el gráfico.",
                        "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Ruta temporal de imagen
                string rutaImagen = System.IO.Path.Combine(
                    System.IO.Path.GetTempPath(),
                    "graficoVentas.png");

                // Guardar gráfico como imagen
                chartVentas.SaveImage(rutaImagen, ChartImageFormat.Png);

                // Generar PDF
                PDFGraficoVentas pdf = new PDFGraficoVentas();
                pdf.Generar(rutaImagen, dtpFechaInicial.Value, dtpFechaFinal.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar PDF: " + ex.Message,
                    "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
