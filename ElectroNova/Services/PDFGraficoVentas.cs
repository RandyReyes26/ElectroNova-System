using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Diagnostics;
using System.IO;

namespace ElectroNova.Services
{
    public class PDFGraficoVentas
    {
        public void Generar(string rutaImagen, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                QuestPDF.Settings.License = LicenseType.Community;

                string ruta = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "ReporteGraficoVentas.pdf");

                string rutaLogo = Path.Combine(
                    System.Windows.Forms.Application.StartupPath,
                    "Resources",
                    "logoLogin.png");

                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.Letter);
                        page.Margin(30);
                        page.PageColor(Colors.White);

                        page.Header().Column(headerCol =>
                        {
                            headerCol.Item().Row(row =>
                            {
                                row.RelativeItem().Column(col =>
                                {
                                    col.Item().Text("ELECTRONOVA")
                                        .FontSize(18)
                                        .Bold()
                                        .FontColor(Colors.Blue.Darken2);

                                    col.Item().Text("REPORTE DE GRÁFICO DE VENTAS")
                                        .FontSize(14)
                                        .Bold();

                                    col.Item().Text($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}")
                                        .FontSize(10);

                                    col.Item().Text($"Rango: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}")
                                        .FontSize(10);
                                });

                                row.ConstantItem(100)
                                    .Height(70)
                                    .Border(1)
                                    .AlignCenter()
                                    .AlignMiddle()
                                    .Element(contenedor =>
                                    {
                                        if (File.Exists(rutaLogo))
                                        {
                                            contenedor.Padding(5)
                                                .Height(60)
                                                .Image(rutaLogo, ImageScaling.FitArea);
                                        }
                                        else
                                        {
                                            contenedor.Text("LOGO").AlignCenter();
                                        }
                                    });
                            });

                            headerCol.Item().PaddingTop(8).LineHorizontal(1);
                        });

                        page.Content().PaddingVertical(15).Column(col =>
                        {
                            col.Spacing(12);

                            col.Item().AlignCenter().Text("Reporte de Gráfico de Ventas")
                                .FontSize(18)
                                .Bold()
                                .FontColor(Colors.Blue.Darken2);

                            col.Item().AlignCenter().Text($"Período: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}")
                                .FontSize(11);

                            col.Item()
                                .PaddingTop(10)
                                .Border(1)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(10)
                                .Element(img =>
                                {
                                    if (File.Exists(rutaImagen))
                                    {
                                        img.Height(350)
                                           .AlignCenter()
                                           .Image(rutaImagen, ImageScaling.FitArea);
                                    }
                                    else
                                    {
                                        img.Height(100)
                                           .AlignCenter()
                                           .AlignMiddle()
                                           .Text("No se encontró la imagen del gráfico.");
                                    }
                                });
                        });

                        page.Footer().AlignCenter().Text(txt =>
                        {
                            txt.Span("ElectroNova © 2026  |  Página ").FontSize(10);
                            txt.CurrentPageNumber().FontSize(10);
                            txt.Span(" de ").FontSize(10);
                            txt.TotalPages().FontSize(10);
                        });
                    });
                })
                .GeneratePdf(ruta);

                Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Error al generar el PDF del gráfico: " + ex.Message,
                    "ElectroNova",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}