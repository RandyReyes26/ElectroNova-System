using ElectroNova.Layers.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Windows.Forms;

namespace ElectroNova.Services
{
    public class PDFCliente
    {
        public void Generar(Clientes cliente)
        {
            try
            {
                if (cliente == null)
                {
                    MessageBox.Show("No se recibió información del cliente.",
                        "ElectroNova", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                QuestPDF.Settings.License = LicenseType.Community;

                Document.Create(document =>
                {
                    document.Page(page =>
                    {
                        page.Size(PageSizes.Letter);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.Margin(30);

                        page.Header().Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().AlignLeft().Text("ElectroNova")
                                    .Bold()
                                    .FontSize(18)
                                    .FontColor(Colors.Blue.Darken2);

                                col.Item().AlignLeft().Text($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}")
                                    .FontSize(10);

                                col.Item().LineHorizontal(1f);
                            });
                        });

                        page.Content().PaddingVertical(10).Column(col =>
                        {
                            col.Spacing(15);

                            col.Item().AlignCenter().Text("Reporte de Cliente")
                                .FontSize(18)
                                .Bold()
                                .FontColor(Colors.Blue.Darken2);

                            col.Item().LineHorizontal(1);

                            col.Item()
                                .Border(1)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Background(Colors.Grey.Lighten4)
                                .Padding(15)
                                .Row(row =>
                                {
                                    row.RelativeItem(2).Column(info =>
                                    {
                                        info.Spacing(6);

                                        info.Item().Text($"Identificación: {cliente.Identificacion}").Bold();
                                        info.Item().Text($"Nombre: {cliente.Nombre} {cliente.Apellidos}");
                                        info.Item().Text($"Teléfono: {cliente.Telefono}");
                                        info.Item().Text($"Correo: {cliente.Email}");
                                        info.Item().Text($"Provincia: {cliente.Provincia}");
                                        info.Item().Text($"Dirección: {cliente.DireccionExacta}");
                                        info.Item().Text($"Estado: {(cliente.Estado ? "Activo" : "Inactivo")}");
                                    });

                                    row.ConstantItem(150)
                                        .Height(160)
                                        .Border(1)
                                        .BorderColor(Colors.Grey.Medium)
                                        .Padding(5)
                                        .AlignCenter()
                                        .AlignMiddle()
                                        .Element(img =>
                                        {
                                            if (cliente.Fotografia != null && cliente.Fotografia.Length > 0)
                                                img.Image(cliente.Fotografia, ImageScaling.FitArea);
                                            else
                                                img.AlignCenter().AlignMiddle().Text("Sin fotografía").Italic();
                                        });
                                });

                            col.Item().Text("Observación")
                                .Bold()
                                .FontSize(13)
                                .FontColor(Colors.Blue.Darken2);

                            col.Item()
                                .Border(1)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(10)
                                .Background(Colors.Grey.Lighten4)
                                .Text("Este documento corresponde al reporte individual del cliente seleccionado en el sistema ElectroNova.");
                        });

                        page.Footer()
                            .AlignRight()
                            .Text(txt =>
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