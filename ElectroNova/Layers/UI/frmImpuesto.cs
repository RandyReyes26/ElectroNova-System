using ElectroNova.Interfaces;
using ElectroNova.Layers.BLL;
using ElectroNova.Layers.Entities;
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
    public partial class frmImpuesto : Form
    {
        public frmImpuesto()
        {
            InitializeComponent();
        }

        private void frmImpuesto_Load(object sender, EventArgs e)
        {
            CargarImpuesto();
        }
        private async Task CargarImpuesto()
        {
            try
            {
                IBLLImpuesto oBLLImpuesto = new BLLImpuesto();
                Impuesto oImpuesto = await oBLLImpuesto.ObtenerImpuesto();

                if (oImpuesto != null)
                {
                    txtIva.Text = oImpuesto.Valor.ToString("N2") + " %";
                    txtIva.ReadOnly = true;
                }
                else
                {
                    MessageBox.Show("No se encontró información del IVA.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el IVA: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
