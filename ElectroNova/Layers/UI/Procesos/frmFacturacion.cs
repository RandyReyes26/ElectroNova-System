using ElectroNova.Layers.BLL;
using ElectroNova.Layers.Entities;
using ElectroNova.Layers.UI.Filtros;
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
        private Clientes _clienteSeleccionado;
        private Productos _productoSeleccionado;
        private List<dynamic> detalleFactura = new List<dynamic>();
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
            ConfigurarMetodoPago(); 
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
            }
        }


        private void ConfigurarMetodoPago()
        {
            // TARJETA
            gbTarjeta.Enabled = rbtTarjeta.Checked;

            // TRANSFERENCIA
            gbTransferencia.Enabled = rbtTransferencia.Checked || rbtSINPE.Checked;

            // CONTROLES DE TRANSFERENCIA
            cboTransferencia.Enabled = rbtTransferencia.Checked;
            txtNumeroTarjeta.Enabled = rbtTransferencia.Checked;

            // SINPE (dentro del mismo groupbox)
            txtNumeroSINPE.Enabled = rbtSINPE.Checked;
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_productoSeleccionado == null)
                {
                    MessageBox.Show("Debe seleccionar un producto.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out int cantidad))
                {
                    MessageBox.Show("Ingrese una cantidad válida.");
                    return;
                }

                int existencia = _productoSeleccionado.Existencia;
                decimal precio = _productoSeleccionado.Precio;

                // 🔥 VALIDACIÓN CLAVE
                if (cantidad > existencia)
                {
                    MessageBox.Show("La cantidad supera la existencia disponible.");
                    return;
                }

                decimal subtotal = cantidad * precio;

                // 🔥 AGREGAR A LA LISTA
                detalleFactura.Add(new
                {
                    ID_Producto = _productoSeleccionado.ID_Producto,
                    Producto = _productoSeleccionado.Informacion_General,
                    Cantidad = cantidad,
                    Precio = precio,
                    Subtotal = subtotal
                });

                // 🔥 REFRESCAR GRID
                dgvDatos.DataSource = null;
                dgvDatos.DataSource = detalleFactura;

                // 🔥 LIMPIAR CAMPOS
                txtProducto.Clear();
                txtCantidad.Clear();
                txtExistencia.Clear();
                txtPrecioUnitario.Clear();

                _productoSeleccionado = null;

                CalcularTotales();
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
                txtSubtotal.Text = "0,00";
                txtImpuesto.Text = "0,00";
                txtTotal.Text = "0,00";
                return;
            }

            decimal subtotal = detalleFactura.Sum(x => (decimal)x.Subtotal);
            decimal impuesto = subtotal * 0.13m;
            decimal total = subtotal + impuesto;

            txtSubtotal.Text = subtotal.ToString("N2");
            txtImpuesto.Text = impuesto.ToString("N2");
            txtTotal.Text = total.ToString("N2");
        }
    }
}
