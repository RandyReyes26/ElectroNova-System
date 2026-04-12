using ElectroNova.Layers.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectroNova.Services
{
    public class PDFFactura
    {
        public void Generar(List<Factura> listaFacturas, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                if (listaFacturas == null || listaFacturas.Count == 0)
                {
                    MessageBox.Show("No hay facturas para generar el reporte.",
                        "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                QuestPDF.Settings.License = LicenseType.Community;

                decimal totalCRC = listaFacturas.Sum(x => x.TotalCRC);
                decimal totalUSD = listaFacturas.Sum(x => x.TotalUSD);

                Document.Create(document =>
                {
                    document.Page(page =>
                    {
                        page.Size(PageSizes.Letter);
                        page.Margin(25);
                        page.PageColor(Colors.White);

                        page.Header().Column(col =>
                        {
                            col.Item().Text("ElectroNova")
                                .FontSize(18)
                                .Bold()
                                .FontColor(Colors.Blue.Darken2);

                            col.Item().Text("Reporte de Facturas")
                                .FontSize(15)
                                .Bold();

                            col.Item().Text($"Rango de fechas: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}")
                                .FontSize(10);

                            col.Item().Text($"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm}")
                                .FontSize(10);

                            col.Item().PaddingTop(5).LineHorizontal(1);
                        });

                        page.Content().PaddingVertical(10).Column(col =>
                        {
                            col.Spacing(10);

                            col.Item().Table(tabla =>
                            {
                                tabla.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2); // Factura
                                    columns.RelativeColumn(2); // Cliente
                                    columns.RelativeColumn(2); // Fecha
                                    columns.RelativeColumn(2); // Subtotal
                                    columns.RelativeColumn(2); // IVA
                                    columns.RelativeColumn(2); // Total CRC
                                    columns.RelativeColumn(2); // Total USD
                                });

                                tabla.Header(header =>
                                {
                                    header.Cell().Background(Colors.Blue.Darken2).Padding(4)
                                        .Text("Factura").FontColor(Colors.White).Bold().FontSize(10);

                                    header.Cell().Background(Colors.Blue.Darken2).Padding(4)
                                        .Text("Cliente").FontColor(Colors.White).Bold().FontSize(10);

                                    header.Cell().Background(Colors.Blue.Darken2).Padding(4)
                                        .Text("Fecha").FontColor(Colors.White).Bold().FontSize(10);

                                    header.Cell().Background(Colors.Blue.Darken2).Padding(4)
                                        .AlignRight().Text("Subtotal").FontColor(Colors.White).Bold().FontSize(10);

                                    header.Cell().Background(Colors.Blue.Darken2).Padding(4)
                                        .AlignRight().Text("IVA").FontColor(Colors.White).Bold().FontSize(10);

                                    header.Cell().Background(Colors.Blue.Darken2).Padding(4)
                                        .AlignRight().Text("Total CRC").FontColor(Colors.White).Bold().FontSize(10);

                                    header.Cell().Background(Colors.Blue.Darken2).Padding(4)
                                        .AlignRight().Text("Total USD").FontColor(Colors.White).Bold().FontSize(10);
                                });

                                foreach (var item in listaFacturas)
                                {
                                    tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(4)
                                        .Text(item.ID_Factura).FontSize(9);

                                    tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(4)
                                        .Text(item.ID_Cliente.ToString()).FontSize(9);

                                    tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(4)
                                        .Text(item.Fecha.ToString("dd/MM/yyyy")).FontSize(9);

                                    tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(4)
                                        .AlignRight().Text(item.Subtotal.ToString("N2")).FontSize(9);

                                    tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(4)
                                        .AlignRight().Text(item.IVA.ToString("N2")).FontSize(9);

                                    tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(4)
                                        .AlignRight().Text(item.TotalCRC.ToString("N2")).FontSize(9);

                                    tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(4)
                                        .AlignRight().Text(item.TotalUSD.ToString("N2")).FontSize(9);
                                }
                            });

                            col.Item().PaddingTop(10).AlignRight().Column(tot =>
                            {
                                tot.Item().Text($"Cantidad de facturas: {listaFacturas.Count}")
                                    .FontSize(11)
                                    .Bold();

                                tot.Item().Text($"Total CRC: ₡{totalCRC:N2}")
                                    .FontSize(11)
                                    .Bold();

                                tot.Item().Text($"Total USD: ${totalUSD:N2}")
                                    .FontSize(11)
                                    .Bold();
                            });
                        });

                        page.Footer().AlignRight().Text(txt =>
                        {
                            txt.Span("Página ").FontSize(10);
                            txt.CurrentPageNumber().FontSize(10);
                            txt.Span(" de ").FontSize(10);
                            txt.TotalPages().FontSize(10);
                        });
                    });
                }).GeneratePdfAndShow();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar PDF: " + ex.Message,
                    "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
