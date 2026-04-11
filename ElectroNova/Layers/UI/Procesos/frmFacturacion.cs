using ElectroNova.Enumeraciones;
using ElectroNova.Interfaces;
using ElectroNova.Layers.BLL;
using ElectroNova.Layers.Entities;
using ElectroNova.Layers.UI.Filtros;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QRCoder;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ElectroNova.Layers.UI
{
    public partial class frmFacturacion : Form
    {
        private bool dibujando = false;
        private Point puntoAnterior;
        private Bitmap canvas;
        private Clientes _clienteSeleccionado;
        private Productos _productoSeleccionado;
        private List<DetalleFactura> detalleFactura = new List<DetalleFactura>();
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");


        private readonly BLLConexionWebServer _service;
        public frmFacturacion()
        {
            InitializeComponent();
        }

        private void frmFacturación_Load(object sender, EventArgs e)
        {
            CargarTipoTarjeta();
            CargarNumeroFactura();
            CargarTipoCambio();
            canvas = new Bitmap(pictureFirma.Width, pictureFirma.Height);
            pictureFirma.Image = canvas;
            ConfigurarMetodoPago(); 
            CargarBancos();
            CargarBancosTransferencia();
       
        }
        private void CargarBancos()
        {
            cboBancoTarjeta.DataSource = Enum.GetValues(typeof(BancoTarjeta));
        }
        private void CargarBancosTransferencia()
        {
            cboTransferencia.DataSource = Enum.GetValues(typeof(BancoTransferencia));
        }

        private void CargarNumeroFactura()
        {
            try
            {
                BLLFactura logica = new BLLFactura();
                int siguiente = logica.ObtenerSiguienteNumeroFactura();

                txtNumeroFactura.Text = $"FAC-{siguiente:0000}";
                txtNumeroFactura.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar número de factura: " + ex.Message);
                txtNumeroFactura.Text = "FAC-0001";
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

        private void txtCliente_Click(object sender, EventArgs e)
        {
            frmFiltroCliente frm = new frmFiltroCliente();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _clienteSeleccionado = frm.ClienteSeleccionado;

                txtCliente.Text = _clienteSeleccionado.NombreCompleto;
                txtEmail.Text = _clienteSeleccionado.Email;
            }
        }
        private void CargarTipoTarjeta()
        {
            try
            {
                IBLLTarjeta logica = new BLLTarjeta();
                cboTipoTarjeta.DataSource = logica.ObtenerTarjeta();
                cboTipoTarjeta.DisplayMember = "DescripcionTarjeta";
                cboTipoTarjeta.ValueMember = "ID_Tarjeta";
                cboTipoTarjeta.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tipos de tarjeta: " + ex.Message);
            }
        }


        private void ConfigurarMetodoPago()
        {
            gbTarjeta.Enabled = rbtTarjeta.Checked;
            gbTransferencia.Enabled = rbtTransferencia.Checked || rbtSINPE.Checked;

            txtNumeroTarjeta.Enabled = rbtTarjeta.Checked;
            cboTipoTarjeta.Enabled = rbtTarjeta.Checked;
            cboBancoTarjeta.Enabled = rbtTarjeta.Checked;

            cboTransferencia.Enabled = rbtTransferencia.Checked;
            txtTransferencia.Enabled = rbtTransferencia.Checked;

            txtNumeroSINPE.Enabled = rbtSINPE.Checked;

            if (!rbtTarjeta.Checked)
            {
                txtNumeroTarjeta.Clear();
                cboTipoTarjeta.SelectedIndex = -1;
                cboBancoTarjeta.SelectedIndex = -1;
            }

            if (!rbtTransferencia.Checked)
            {
                cboTransferencia.SelectedIndex = -1;
                txtTransferencia.Clear();
            }

            if (!rbtSINPE.Checked)
            {
                txtNumeroSINPE.Clear();
            }
        }

        private void rbtTarjeta_CheckedChanged(object sender, EventArgs e)
        {
            ConfigurarMetodoPago();
        }

        private void rbtTransferencia_CheckedChanged(object sender, EventArgs e)
        {
            ConfigurarMetodoPago();
        }

        private void rbtSINPE_CheckedChanged(object sender, EventArgs e)
        {
            ConfigurarMetodoPago();
        }

        private void txtProducto_Click(object sender, EventArgs e)
        {
            frmFiltroProducto frm = new frmFiltroProducto();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _productoSeleccionado = frm.ProductoSeleccionado;

                if (_productoSeleccionado != null)
                {
                    txtProducto.Text = _productoSeleccionado.Informacion_General;
                    txtExistencia.Text = _productoSeleccionado.Existencia.ToString();
                    txtPrecioUnitario.Text = _productoSeleccionado.Precio.ToString("N2");

                    txtCantidad.Clear();
                    txtCantidad.Focus();
                }
            }

        }
        private void RefrescarGridDetalle()
        {
            dgvDatos.DataSource = null;
            dgvDatos.DataSource = detalleFactura;

            if (dgvDatos.Columns["ID_DetalleFactura"] != null)
                dgvDatos.Columns["ID_DetalleFactura"].Visible = false;

            if (dgvDatos.Columns["ID_Factura"] != null)
                dgvDatos.Columns["ID_Factura"].Visible = false;

            if (dgvDatos.Columns["ID_Producto"] != null)
                dgvDatos.Columns["ID_Producto"].HeaderText = "ID Producto";

            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatos.MultiSelect = false;
            dgvDatos.ReadOnly = true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_productoSeleccionado == null)
                {
                    MessageBox.Show("Debe seleccionar un producto.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida.");
                    return;
                }

                int existencia = _productoSeleccionado.Existencia;
                decimal precio = _productoSeleccionado.Precio;

                if (cantidad > existencia)
                {
                    MessageBox.Show("La cantidad supera la existencia disponible.");
                    return;
                }

                decimal subtotal = cantidad * precio;
                decimal iva = subtotal * 0.13m;
                decimal total = subtotal + iva;

                DetalleFactura detalle = new DetalleFactura
                {
                    ID_Producto = _productoSeleccionado.ID_Producto,
                    NombreProducto = _productoSeleccionado.Informacion_General,
                    Cantidad = cantidad,
                    Precio = Convert.ToDouble(precio),
                    Subtotal = Convert.ToDouble(subtotal),
                    IVA = Convert.ToDouble(iva),
                    Total = Convert.ToDouble(total)
                };

                detalleFactura.Add(detalle);

                RefrescarGridDetalle();
                CalcularTotales();

                txtProducto.Clear();
                txtCantidad.Clear();
                txtExistencia.Clear();
                txtPrecioUnitario.Clear();

                _productoSeleccionado = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar: " + ex.Message);
            }
        }
        private void CalcularTotales()
        {
            if (detalleFactura == null || detalleFactura.Count == 0)
            {
                txtSubtotal.Text = "0.00";
                txtImpuesto.Text = "0.00";
                txtTotal.Text = "0.00";
                return;
            }

            decimal subtotal = detalleFactura.Sum(x => Convert.ToDecimal(x.Subtotal));
            decimal impuesto = detalleFactura.Sum(x => Convert.ToDecimal(x.IVA));
            decimal total = detalleFactura.Sum(x => Convert.ToDecimal(x.Total));

            txtSubtotal.Text = subtotal.ToString("N2");
            txtImpuesto.Text = impuesto.ToString("N2");
            txtTotal.Text = total.ToString("N2");
        }

        private void btnBorrarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDatos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un producto del detalle.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int fila = dgvDatos.SelectedRows[0].Index;

                if (fila >= 0 && fila < detalleFactura.Count)
                {
                    detalleFactura.RemoveAt(fila);
                    RefrescarGridDetalle();
                    CalcularTotales();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar producto: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (_clienteSeleccionado == null)
                {
                    MessageBox.Show("Debe seleccionar un cliente.");
                    return;
                }

                string correoDestino = txtEmail.Text.Trim();

                if (string.IsNullOrWhiteSpace(correoDestino))
                {
                    MessageBox.Show("Debe ingresar un correo destino.");
                    txtEmail.Focus();
                    return;
                }

                try
                {
                    MailAddress mail = new MailAddress(correoDestino);
                }
                catch
                {
                    MessageBox.Show("El correo destino no tiene un formato válido.");
                    txtEmail.Focus();
                    return;
                }

                if (detalleFactura == null || detalleFactura.Count == 0)
                {
                    MessageBox.Show("Debe agregar productos.");
                    return;
                }

                string tipoPago = ObtenerTipoPago();
                if (string.IsNullOrWhiteSpace(tipoPago))
                {
                    MessageBox.Show("Debe seleccionar un método de pago.");
                    return;
                }

                decimal subtotal = detalleFactura.Sum(x => Convert.ToDecimal(x.Subtotal));
                decimal iva = detalleFactura.Sum(x => Convert.ToDecimal(x.IVA));
                decimal totalCRC = detalleFactura.Sum(x => Convert.ToDecimal(x.Total));
                decimal tipoCambio = ObtenerTipoCambio();
                decimal totalUSD = tipoCambio > 0 ? totalCRC / tipoCambio : 0;

                byte[] firma = ObtenerFirmaComoBytes();
                string xml = GenerarXMLFactura(subtotal, iva, totalCRC);
                string banco = ObtenerBancoSeleccionado();

                Factura factura = new Factura
                {
                    ID_Factura = txtNumeroFactura.Text.Trim(),
                    ID_Cliente = _clienteSeleccionado.ID_Cliente,
                    Fecha = dtpFechaFactura.Value,

                    Subtotal = subtotal,
                    IVA = iva,
                    TotalCRC = totalCRC,
                    TotalUSD = totalUSD,
                    TipoCambio = tipoCambio,

                    FirmaCliente = firma,
                    DocumentoXML = xml,
                    TipoPago = tipoPago,
                    Banco = banco,
                    Descuento = 0,
                    Estado = true,

                    ID_Tarjeta = rbtTarjeta.Checked && cboTipoTarjeta.SelectedValue != null
                        ? Convert.ToInt32(cboTipoTarjeta.SelectedValue)
                        : (int?)null
                };

                foreach (DetalleFactura item in detalleFactura)
                {
                    item.ID_Factura = factura.ID_Factura;
                }

                IBLLFactura logicaFactura = new BLLFactura();
                Factura facturaGuardada = logicaFactura.GuardarFactura(factura, detalleFactura);

                if (facturaGuardada != null)
                {
                    EnviarFacturaPorCorreo(correoDestino, factura, detalleFactura);

                    MessageBox.Show("Factura guardada y enviada correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarFormulario();
                    CargarNumeroFactura();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar la factura.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar factura: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GenerarXMLFactura(decimal subtotal, decimal iva, decimal total)
        {
            try
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

                var raiz = doc.CreateElement("Factura");
                doc.AppendChild(raiz);

                // ENCABEZADO
                var encabezado = doc.CreateElement("Encabezado");

                var id = doc.CreateElement("NumeroFactura");
                id.InnerText = txtNumeroFactura.Text;

                var cliente = doc.CreateElement("Cliente");
                cliente.InnerText = _clienteSeleccionado.NombreCompleto;

                var fecha = doc.CreateElement("Fecha");
                fecha.InnerText = DateTime.Now.ToString("yyyy-MM-dd");

                encabezado.AppendChild(id);
                encabezado.AppendChild(cliente);
                encabezado.AppendChild(fecha);

                raiz.AppendChild(encabezado);

                // DETALLE
                var detalle = doc.CreateElement("Detalle");

                foreach (var item in detalleFactura)
                {
                    var linea = doc.CreateElement("Linea");

                    var producto = doc.CreateElement("ID_Producto");
                    producto.InnerText = item.ID_Producto.ToString();

                    var cantidad = doc.CreateElement("Cantidad");
                    cantidad.InnerText = item.Cantidad.ToString();

                    var precio = doc.CreateElement("Precio");
                    precio.InnerText = item.Precio.ToString();

                    var subtotalLinea = doc.CreateElement("Subtotal");
                    subtotalLinea.InnerText = item.Subtotal.ToString();

                    var ivaLinea = doc.CreateElement("IVA");
                    ivaLinea.InnerText = item.IVA.ToString();

                    var totalLinea = doc.CreateElement("Total");
                    totalLinea.InnerText = item.Total.ToString();

                    linea.AppendChild(producto);
                    linea.AppendChild(cantidad);
                    linea.AppendChild(precio);
                    linea.AppendChild(subtotalLinea);
                    linea.AppendChild(ivaLinea);
                    linea.AppendChild(totalLinea);

                    detalle.AppendChild(linea);
                }

                raiz.AppendChild(detalle);

                // TOTALES
                var totales = doc.CreateElement("Totales");

                var sub = doc.CreateElement("Subtotal");
                sub.InnerText = subtotal.ToString();

                var imp = doc.CreateElement("IVA");
                imp.InnerText = iva.ToString();

                var tot = doc.CreateElement("Total");
                tot.InnerText = total.ToString();

                totales.AppendChild(sub);
                totales.AppendChild(imp);
                totales.AppendChild(tot);

                raiz.AppendChild(totales);

                return doc.OuterXml;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generando XML: " + ex.Message);
                return "";
            }
        }
        private void LimpiarFormulario()
        {
            txtCliente.Clear();
            txtProducto.Clear();
            txtCantidad.Clear();
            txtExistencia.Clear();
            txtPrecioUnitario.Clear();
            txtEmail.Clear();

            txtSubtotal.Text = "0.00";
            txtImpuesto.Text = "0.00";
            txtTotal.Text = "0.00";

            rbtTarjeta.Checked = false;
            rbtTransferencia.Checked = false;
            rbtSINPE.Checked = false;

            txtNumeroTarjeta.Clear();
            txtTransferencia.Clear();
            txtNumeroSINPE.Clear();

            cboTipoTarjeta.SelectedIndex = -1;
            cboBancoTarjeta.SelectedIndex = -1;
            cboTransferencia.SelectedIndex = -1;

            detalleFactura.Clear();
            dgvDatos.DataSource = null;

            canvas = new Bitmap(pictureFirma.Width, pictureFirma.Height);
            pictureFirma.Image = canvas;

            _clienteSeleccionado = null;
            _productoSeleccionado = null;

            ConfigurarMetodoPago();
        }

        private string ObtenerTipoPago()
        {
            if (rbtTarjeta.Checked) return "Tarjeta";
            if (rbtTransferencia.Checked) return "Transferencia";
            if (rbtSINPE.Checked) return "SINPE";
            return string.Empty;
        }

        private string ObtenerBancoSeleccionado()
        {
            if (rbtTarjeta.Checked)
                return cboBancoTarjeta.Text.Trim();

            if (rbtTransferencia.Checked)
                return cboTransferencia.Text.Trim();

            if (rbtSINPE.Checked)
                return "SINPE";

            return string.Empty;
        }

        private byte[] ObtenerFirmaComoBytes()
        {
            if (pictureFirma.Image == null)
                return null;

            using (var ms = new System.IO.MemoryStream())
            {
                pictureFirma.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private decimal ObtenerTipoCambio()
        {
            string valor = lblTipoCambio.Text.Replace("₡", "").Trim();
            return decimal.Parse(valor);
        }

        private void EnviarFacturaPorCorreo(string correoDestino, Factura factura, List<DetalleFactura> detalle)
        {
            try
            {
                string cuentaCorreoElectronico = "randy022315@gmail.com";
                string contrasenaGeneradaXGmail = "kirocqksouyxwmol";

                // Generar PDF y XML
                byte[] pdfBytes = GenerarPDF(factura, detalle);
                byte[] xmlBytes = Encoding.UTF8.GetBytes(factura.DocumentoXML);

                using (MailMessage mensaje = new MailMessage())
                {
                    mensaje.From = new MailAddress(cuentaCorreoElectronico);
                    mensaje.To.Add(correoDestino);

                    mensaje.Subject = $"Factura {factura.ID_Factura}";
                    mensaje.Body = $"Hola {_clienteSeleccionado.NombreCompleto},\n\nAdjunto encontrarás tu factura.\n\nGracias por tu compra.";

                    // 📎 ADJUNTAR PDF
                    mensaje.Attachments.Add(new Attachment(new MemoryStream(pdfBytes), "Factura.pdf", "application/pdf"));

                    // 📎 ADJUNTAR XML
                    mensaje.Attachments.Add(new Attachment(new MemoryStream(xmlBytes), "Factura.xml", "application/xml"));

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(cuentaCorreoElectronico, contrasenaGeneradaXGmail);
                        smtp.EnableSsl = true;

                        smtp.Send(mensaje);
                    }
                }

                MessageBox.Show("Factura enviada correctamente 🚀");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar correo: " + ex.Message);
            }
        }
        private byte[] GenerarQR(string texto)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);
                PngByteQRCode qrCode = new PngByteQRCode(qrData);
                return qrCode.GetGraphic(20);
            }
        }


        private byte[] GenerarPDF(Factura factura, List<DetalleFactura> detalle)
        {
            byte[] qrBytes = GenerarQR(factura.ID_Factura);

            string rutaLogo = Path.Combine(Application.StartupPath, "Resources", "logoLogin.png");
            byte[] logoBytes = File.Exists(rutaLogo) ? File.ReadAllBytes(rutaLogo) : null;

            using (MemoryStream stream = new MemoryStream())
            {
                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(25);

                        page.Header().Column(header =>
                        {
                            header.Item().Row(row =>
                            {
                                row.RelativeItem().Column(col =>
                                {
                                    col.Item().Text("FACTURA ELECTRÓNICA")
                                        .FontSize(20)
                                        .Bold();

                                    col.Item().Text("ELECTRONOVA")
                                        .FontSize(16)
                                        .SemiBold();

                                    col.Item().PaddingTop(5).Text($"Factura: {factura.ID_Factura}");
                                    col.Item().Text($"Fecha: {factura.Fecha:dd/MM/yyyy}");
                                });

                                if (logoBytes != null)
                                {
                                    row.ConstantItem(95)
                                        .Height(75)
                                        .AlignRight()
                                        .Image(logoBytes);
                                }
                            });

                            header.Item().PaddingTop(10).LineHorizontal(1);
                        });

                        page.Content().PaddingVertical(10).Column(content =>
                        {
                            content.Spacing(10);

                            content.Item().Row(row =>
                            {
                                row.RelativeItem().Border(1).Padding(8).Column(col =>
                                {
                                    col.Item().Text("DATOS DEL CLIENTE").Bold().FontSize(12);
                                    col.Item().PaddingTop(4).Text($"Cliente: {_clienteSeleccionado.NombreCompleto}");
                                    col.Item().Text($"Correo: {txtEmail.Text.Trim()}");
                                });

                                row.ConstantItem(220).Border(1).Padding(8).Column(col =>
                                {
                                    col.Item().Text("DATOS DE LA FACTURA").Bold().FontSize(12);
                                    col.Item().PaddingTop(4).Text($"Método de pago: {factura.TipoPago}");
                                    col.Item().Text($"Banco: {factura.Banco}");
                                    col.Item().Text($"Tipo de cambio: ₡ {factura.TipoCambio:N2}");
                                });
                            });

                            content.Item().PaddingTop(5).Text("DETALLE DE FACTURA").Bold().FontSize(12);

                            content.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(4);
                                    columns.RelativeColumn(1.2f);
                                    columns.RelativeColumn(1.5f);
                                    columns.RelativeColumn(1.5f);
                                    columns.RelativeColumn(1.6f);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().BorderBottom(1).Padding(5).Text("Producto").Bold();
                                    header.Cell().BorderBottom(1).Padding(5).AlignCenter().Text("Cant.").Bold();
                                    header.Cell().BorderBottom(1).Padding(5).AlignRight().Text("Precio").Bold();
                                    header.Cell().BorderBottom(1).Padding(5).AlignRight().Text("IVA").Bold();
                                    header.Cell().BorderBottom(1).Padding(5).AlignRight().Text("Total").Bold();
                                });

                                foreach (DetalleFactura item in detalle)
                                {
                                    table.Cell().BorderBottom(0.5f).Padding(5).Text(item.NombreProducto ?? item.ID_Producto.ToString());
                                    table.Cell().BorderBottom(0.5f).Padding(5).AlignCenter().Text(item.Cantidad.ToString());
                                    table.Cell().BorderBottom(0.5f).Padding(5).AlignRight().Text($"₡ {item.Precio:N2}");
                                    table.Cell().BorderBottom(0.5f).Padding(5).AlignRight().Text($"₡ {item.IVA:N2}");
                                    table.Cell().BorderBottom(0.5f).Padding(5).AlignRight().Text($"₡ {item.Total:N2}");
                                }
                            });

                            content.Item().PaddingTop(10).Row(row =>
                            {
                                row.RelativeItem().Column(col =>
                                {
                                    col.Item().Text("FIRMA DEL CLIENTE").Bold().FontSize(12);
                                    col.Item().PaddingTop(5);

                                    if (factura.FirmaCliente != null && factura.FirmaCliente.Length > 0)
                                    {
                                        col.Item().Height(85).Width(220).Border(1).Padding(4).Image(factura.FirmaCliente);
                                    }
                                    else
                                    {
                                        col.Item().Height(85).Width(220).Border(1).AlignMiddle().AlignCenter().Text("Sin firma");
                                    }
                                });

                                row.ConstantItem(220).Border(1).Padding(10).Column(col =>
                                {
                                    col.Item().Text("RESUMEN").Bold().FontSize(12);
                                    col.Item().PaddingTop(5).AlignRight().Text($"Subtotal: ₡ {factura.Subtotal:N2}");
                                    col.Item().AlignRight().Text($"IVA: ₡ {factura.IVA:N2}");
                                    col.Item().AlignRight().Text($"Total CRC: ₡ {factura.TotalCRC:N2}").Bold();
                                    col.Item().AlignRight().Text($"Total USD: $ {factura.TotalUSD:N2}").Bold();
                                });
                            });
                        });

                        page.Footer().PaddingTop(8).Column(col =>
                        {
                            col.Item().LineHorizontal(1);

                            col.Item().PaddingTop(5).Row(row =>
                            {
                                row.RelativeItem().Text("Gracias por su compra en ElectroNova.");
                                row.ConstantItem(75).Height(75).Image(qrBytes);
                            });
                        });
                    });
                }).GeneratePdf(stream);

                return stream.ToArray();
            }
        }
    }
}
