using ElectroNova.Interfaces;
using ElectroNova.Layers.BLL;
using ElectroNova.Layers.Entities;
using ElectroNova.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ElectroNova.Layers.Reportes
{
    public partial class frmReporteClientes : Form
    {
        private Clientes _clienteSeleccionado = null;
        public frmReporteClientes()
        {
            InitializeComponent();
        }
      
        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarClientePorIdentificacion();

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtIdentificacion.Clear();
            txtNombre.Clear();
            pblImagen.Image = null;
            _clienteSeleccionado = null;
            txtIdentificacion.Focus();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (_clienteSeleccionado == null)
                {
                    MessageBox.Show("Debe buscar un cliente primero.",
                        "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PDFCliente pdf = new PDFCliente();
                pdf.Generar(_clienteSeleccionado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message,
                    "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private async void BuscarClientePorIdentificacion()
        {
            try
            {
                string identificacion = txtIdentificacion.Text.Trim();

                if (string.IsNullOrWhiteSpace(identificacion))
                {
                    txtNombre.Clear();
                    pblImagen.Image = null;
                    _clienteSeleccionado = null;
                    return;
                }

                IBLLCliente logica = new BLLCliente();
                var listaClientes = await logica.ObtenerClientes();

                _clienteSeleccionado = listaClientes
                    .FirstOrDefault(c => c.Identificacion != null &&
                                         c.Identificacion.Trim() == identificacion);

                if (_clienteSeleccionado != null)
                {
                    txtNombre.Text = $"{_clienteSeleccionado.Nombre} {_clienteSeleccionado.Apellidos}";

                    if (_clienteSeleccionado.Fotografia != null && _clienteSeleccionado.Fotografia.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(_clienteSeleccionado.Fotografia))
                        {
                            pblImagen.Image = System.Drawing.Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        pblImagen.Image = null;
                    }
                }
                else
                {
                    txtNombre.Clear();
                    pblImagen.Image = null;
                    _clienteSeleccionado = null;

                    MessageBox.Show("No se encontró ningún cliente con esa identificación.",
                        "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el cliente: " + ex.Message,
                    "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BuscarClientePorIdentificacion();
            }
        }

        public void Generar(Clientes cliente)
        {
            try
            {
                if (cliente == null)
                    throw new Exception("No se recibió información del cliente.");

                QuestPDF.Settings.License = LicenseType.Community;

                string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReportesPDF");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                string nombreArchivo = $"ReporteCliente_{cliente.Identificacion}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string rutaCompleta = Path.Combine(carpeta, nombreArchivo);

                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(30);
                        page.Size(PageSizes.A4);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(12));

                        page.Header().Element(c => CrearEncabezado(c));
                        page.Content().Element(c => CrearContenido(c, cliente));
                        page.Footer().AlignCenter().Text(txt =>
                        {
                            txt.Span("ElectroNova - Reporte de Cliente").FontSize(10);
                        });
                    });
                })
                .GeneratePdf(rutaCompleta);

                MessageBox.Show("PDF generado correctamente en:\n" + rutaCompleta,
                    "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Process.Start(new ProcessStartInfo()
                {
                    FileName = rutaCompleta,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar PDF: " + ex.Message,
                    "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearEncabezado(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("ElectroNova")
                        .FontSize(20)
                        .Bold()
                        .FontColor(Colors.Blue.Darken2);

                    col.Item().Text("Reporte de Cliente")
                        .FontSize(16)
                        .SemiBold();

                    col.Item().Text($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}");
                });
            });
        }

        private void CrearContenido(IContainer container, Clientes cliente)
        {
            container.PaddingTop(20).Column(col =>
            {
                col.Spacing(15);

                col.Item().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(15).Row(row =>
                {
                    row.RelativeItem(2).Column(info =>
                    {
                        info.Spacing(8);

                        info.Item().Text($"Identificación: {cliente.Identificacion}").Bold();
                        info.Item().Text($"Nombre: {cliente.Nombre} {cliente.Apellidos}");
                        info.Item().Text($"Teléfono: {cliente.Telefono}");
                        info.Item().Text($"Correo: {cliente.Email}");
                        info.Item().Text($"Provincia: {cliente.Provincia}");
                        info.Item().Text($"Dirección: {cliente.DireccionExacta}");
                        info.Item().Text($"Estado: {(cliente.Estado ? "Activo" : "Inactivo")}");
                    });

                    row.ConstantItem(140).Height(160).Border(1).BorderColor(Colors.Grey.Lighten2).AlignCenter().AlignMiddle().Element(img =>
                    {
                        if (cliente.Fotografia != null && cliente.Fotografia.Length > 0)
                        {
                            img.Image(cliente.Fotografia);
                        }
                        else
                        {
                            img.Text("Sin fotografía").Italic().FontColor(Colors.Grey.Darken1);
                        }
                    });
                });

                col.Item().PaddingTop(10).Text("Observación")
                    .Bold()
                    .FontSize(13);

                col.Item().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(10)
                    .Text("Este documento corresponde al reporte individual del cliente seleccionado en el sistema ElectroNova.");
            });
        }
        private async void CargarTodosLosClientes()
        {
            try
            {
                IBLLCliente logica = new BLLCliente();
                var listaClientes = await logica.ObtenerClientes();

                dgvDatos.AutoGenerateColumns = true;
                dgvDatos.DataSource = null;
                dgvDatos.DataSource = listaClientes.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los clientes: " + ex.Message,
                    "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmReporteClientes_Load(object sender, EventArgs e)
        {
            CargarTodosLosClientes();   
        }
    }
}
