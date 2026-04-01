using ElectroNova.Layers.BLL;
using ElectroNova.Layers.Entities;
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
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");


        private readonly BLLConexionWebServer _service;
        public frmFacturacion()
        {
            InitializeComponent();
        }

        private void frmFacturación_Load(object sender, EventArgs e)
        {

        }

     

        
        
    }
}
