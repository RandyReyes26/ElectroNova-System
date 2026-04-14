using ElectroNova.Layers.Entities.DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ElectroNova.Services
{
    public class PDFProductoVendido
    {
        public void Generar(List<ProductoVendidoDTO> listaProductos)
        {
            try
            {
                if (listaProductos == null || listaProductos.Count == 0)
                {
                    MessageBox.Show("No hay datos para generar el reporte.",
                        "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                QuestPDF.Settings.License = LicenseType.Community;

                decimal totalGeneral = listaProductos.Sum(x => x.TotalVendido);
                int cantidadGeneral = listaProductos.Sum(x => x.CantidadVendida);

                string rutaLogo = Path.Combine(Application.StartupPath, "Resources", "logoLogin.png");

                Document.Create(document =>
                {
                    document.Page(page =>
                    {
                        page.Size(PageSizes.Letter);
                        page.Margin(30);
                        page.PageColor(Colors.White);

                        // 🔥 HEADER CON LOGO
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

                                    col.Item().Text("REPORTE DE PRODUCTOS VENDIDOS")
                                        .FontSize(14)
                                        .Bold();

                                    col.Item().Text($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}")
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

                        // 🔥 CONTENIDO
                        page.Content().PaddingVertical(10).Column(col =>
                        {
                            col.Spacing(15);

                            col.Item().AlignCenter().Text("Reporte de Productos Vendidos")
                                .FontSize(20)
                                .Bold()
                                .FontColor(Colors.Blue.Darken2);

                            col.Item().LineHorizontal(1);

                            foreach (var item in listaProductos)
                            {
                                col.Item()
                                    .Border(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .Background(Colors.Grey.Lighten4)
                                    .Padding(12)
                                    .Row(row =>
                                    {
                                        row.RelativeItem(2).Column(info =>
                                        {
                                            info.Spacing(5);

                                            info.Item().Text($"Código: {item.CodigoProducto}").Bold();
                                            info.Item().Text($"Marca: {item.Marca}");
                                            info.Item().Text($"Modelo: {item.Modelo}");
                                            info.Item().Text($"Tipo: {item.TipoDispositivo}");
                                            info.Item().Text($"Cantidad Vendida: {item.CantidadVendida}");
                                            info.Item().Text($"Total Vendido: ₡{item.TotalVendido:N2}");
                                        });

                                        row.ConstantItem(140)
                                            .Height(140)
                                            .Border(1)
                                            .BorderColor(Colors.Grey.Medium)
                                            .Padding(5)
                                            .AlignCenter()
                                            .AlignMiddle()
                                            .Element(img =>
                                            {
                                                if (item.Fotografia != null && item.Fotografia.Length > 0)
                                                    img.Image(item.Fotografia, ImageScaling.FitArea);
                                                else
                                                    img.AlignCenter().AlignMiddle().Text("Sin imagen").Italic();
                                            });
                                    });
                            }

                            col.Item().PaddingTop(10).LineHorizontal(1);

                            col.Item().AlignRight().Column(tot =>
                            {
                                tot.Item().Text($"Cantidad Total Vendida: {cantidadGeneral}")
                                    .FontSize(12)
                                    .Bold();

                                tot.Item().Text($"Total General Vendido: ₡{totalGeneral:N2}")
                                    .FontSize(12)
                                    .Bold();
                            });
                        });

                        // 🔥 FOOTER
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