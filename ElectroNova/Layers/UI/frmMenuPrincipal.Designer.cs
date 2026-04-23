namespace ElectroNova.Layers.UI
{
    partial class frmMenuPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuPrincipal));
            this.MenuVertical = new System.Windows.Forms.Panel();
            this.lblTipoCambio = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMantenimiento = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MarcaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModeloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tIPOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pRODUCTOSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlStockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.impuestoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripProcesos = new System.Windows.Forms.ToolStripMenuItem();
            this.fACTURACÓNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.rEPORTESCLIENTESToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rEPORTESDEFACTURAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rEPORTEDELPRODUCTOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rEPORTEGRAFICOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripAdministracion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.MenuVertical.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuVertical
            // 
            this.MenuVertical.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.MenuVertical.Controls.Add(this.lblTipoCambio);
            this.MenuVertical.Controls.Add(this.label1);
            this.MenuVertical.Controls.Add(this.panel1);
            this.MenuVertical.Controls.Add(this.lblUsuario);
            this.MenuVertical.Controls.Add(this.pictureBox1);
            this.MenuVertical.Dock = System.Windows.Forms.DockStyle.Left;
            this.MenuVertical.Location = new System.Drawing.Point(0, 0);
            this.MenuVertical.Name = "MenuVertical";
            this.MenuVertical.Size = new System.Drawing.Size(308, 661);
            this.MenuVertical.TabIndex = 0;
            // 
            // lblTipoCambio
            // 
            this.lblTipoCambio.AutoSize = true;
            this.lblTipoCambio.Location = new System.Drawing.Point(148, 566);
            this.lblTipoCambio.Name = "lblTipoCambio";
            this.lblTipoCambio.Size = new System.Drawing.Size(10, 16);
            this.lblTipoCambio.TabIndex = 46;
            this.lblTipoCambio.Text = ".";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 566);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo Cambio";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCerrarSesion);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Location = new System.Drawing.Point(12, 150);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(280, 402);
            this.panel1.TabIndex = 45;
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMantenimiento,
            this.toolStripProcesos,
            this.ToolStripReportes,
            this.ToolStripAdministracion,
            this.toolStripMenuItem6});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(280, 402);
            this.menuStrip1.TabIndex = 44;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMantenimiento
            // 
            this.toolStripMantenimiento.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientesToolStripMenuItem,
            this.MarcaToolStripMenuItem,
            this.ModeloToolStripMenuItem,
            this.tIPOToolStripMenuItem,
            this.pRODUCTOSToolStripMenuItem,
            this.controlStockToolStripMenuItem,
            this.impuestoToolStripMenuItem});
            this.toolStripMantenimiento.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMantenimiento.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMantenimiento.Image")));
            this.toolStripMantenimiento.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolStripMantenimiento.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMantenimiento.Name = "toolStripMantenimiento";
            this.toolStripMantenimiento.Size = new System.Drawing.Size(274, 36);
            this.toolStripMantenimiento.Text = "MANTENIMIENTOS";
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(273, 28);
            this.clientesToolStripMenuItem.Text = "CLIENTES";
            this.clientesToolStripMenuItem.Click += new System.EventHandler(this.clientesToolStripMenuItem_Click_1);
            // 
            // MarcaToolStripMenuItem
            // 
            this.MarcaToolStripMenuItem.Name = "MarcaToolStripMenuItem";
            this.MarcaToolStripMenuItem.Size = new System.Drawing.Size(273, 28);
            this.MarcaToolStripMenuItem.Text = "MARCA";
            this.MarcaToolStripMenuItem.Click += new System.EventHandler(this.MarcaToolStripMenuItem_Click);
            // 
            // ModeloToolStripMenuItem
            // 
            this.ModeloToolStripMenuItem.Name = "ModeloToolStripMenuItem";
            this.ModeloToolStripMenuItem.Size = new System.Drawing.Size(273, 28);
            this.ModeloToolStripMenuItem.Text = "MODELO";
            this.ModeloToolStripMenuItem.Click += new System.EventHandler(this.ModeloToolStripMenuItem_Click);
            // 
            // tIPOToolStripMenuItem
            // 
            this.tIPOToolStripMenuItem.Name = "tIPOToolStripMenuItem";
            this.tIPOToolStripMenuItem.Size = new System.Drawing.Size(273, 28);
            this.tIPOToolStripMenuItem.Text = "TIPO DISPOSITIVO";
            this.tIPOToolStripMenuItem.Click += new System.EventHandler(this.tIPOToolStripMenuItem_Click);
            // 
            // pRODUCTOSToolStripMenuItem
            // 
            this.pRODUCTOSToolStripMenuItem.Name = "pRODUCTOSToolStripMenuItem";
            this.pRODUCTOSToolStripMenuItem.Size = new System.Drawing.Size(273, 28);
            this.pRODUCTOSToolStripMenuItem.Text = "PRODUCTOS";
            this.pRODUCTOSToolStripMenuItem.Click += new System.EventHandler(this.pRODUCTOSToolStripMenuItem_Click);
            // 
            // controlStockToolStripMenuItem
            // 
            this.controlStockToolStripMenuItem.Name = "controlStockToolStripMenuItem";
            this.controlStockToolStripMenuItem.Size = new System.Drawing.Size(273, 28);
            this.controlStockToolStripMenuItem.Text = "CONTROL STOCK";
            this.controlStockToolStripMenuItem.Click += new System.EventHandler(this.controlStockToolStripMenuItem_Click);
            // 
            // impuestoToolStripMenuItem
            // 
            this.impuestoToolStripMenuItem.Name = "impuestoToolStripMenuItem";
            this.impuestoToolStripMenuItem.Size = new System.Drawing.Size(273, 28);
            this.impuestoToolStripMenuItem.Text = "IMPUESTO";
            this.impuestoToolStripMenuItem.Click += new System.EventHandler(this.impuestoToolStripMenuItem_Click);
            // 
            // toolStripProcesos
            // 
            this.toolStripProcesos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fACTURACÓNToolStripMenuItem});
            this.toolStripProcesos.Image = ((System.Drawing.Image)(resources.GetObject("toolStripProcesos.Image")));
            this.toolStripProcesos.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripProcesos.Name = "toolStripProcesos";
            this.toolStripProcesos.Size = new System.Drawing.Size(274, 36);
            this.toolStripProcesos.Text = "PROCESOS";
            // 
            // fACTURACÓNToolStripMenuItem
            // 
            this.fACTURACÓNToolStripMenuItem.Name = "fACTURACÓNToolStripMenuItem";
            this.fACTURACÓNToolStripMenuItem.Size = new System.Drawing.Size(235, 28);
            this.fACTURACÓNToolStripMenuItem.Text = "FACTURACION";
            this.fACTURACÓNToolStripMenuItem.Click += new System.EventHandler(this.fACTURACÓNToolStripMenuItem_Click);
            // 
            // ToolStripReportes
            // 
            this.ToolStripReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rEPORTESCLIENTESToolStripMenuItem,
            this.rEPORTESDEFACTURAToolStripMenuItem,
            this.rEPORTEDELPRODUCTOToolStripMenuItem,
            this.rEPORTEGRAFICOToolStripMenuItem});
            this.ToolStripReportes.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripReportes.Image")));
            this.ToolStripReportes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripReportes.Name = "ToolStripReportes";
            this.ToolStripReportes.Size = new System.Drawing.Size(274, 36);
            this.ToolStripReportes.Text = "REPORTES";
            // 
            // rEPORTESCLIENTESToolStripMenuItem
            // 
            this.rEPORTESCLIENTESToolStripMenuItem.Name = "rEPORTESCLIENTESToolStripMenuItem";
            this.rEPORTESCLIENTESToolStripMenuItem.Size = new System.Drawing.Size(355, 28);
            this.rEPORTESCLIENTESToolStripMenuItem.Text = "REPORTES CLIENTES";
            this.rEPORTESCLIENTESToolStripMenuItem.Click += new System.EventHandler(this.rEPORTESCLIENTESToolStripMenuItem_Click);
            // 
            // rEPORTESDEFACTURAToolStripMenuItem
            // 
            this.rEPORTESDEFACTURAToolStripMenuItem.Name = "rEPORTESDEFACTURAToolStripMenuItem";
            this.rEPORTESDEFACTURAToolStripMenuItem.Size = new System.Drawing.Size(355, 28);
            this.rEPORTESDEFACTURAToolStripMenuItem.Text = "REPORTES DE FACTURA";
            this.rEPORTESDEFACTURAToolStripMenuItem.Click += new System.EventHandler(this.rEPORTESDEFACTURAToolStripMenuItem_Click);
            // 
            // rEPORTEDELPRODUCTOToolStripMenuItem
            // 
            this.rEPORTEDELPRODUCTOToolStripMenuItem.Name = "rEPORTEDELPRODUCTOToolStripMenuItem";
            this.rEPORTEDELPRODUCTOToolStripMenuItem.Size = new System.Drawing.Size(355, 28);
            this.rEPORTEDELPRODUCTOToolStripMenuItem.Text = "REPORTE DEL PRODUCTO";
            this.rEPORTEDELPRODUCTOToolStripMenuItem.Click += new System.EventHandler(this.rEPORTEDELPRODUCTOToolStripMenuItem_Click);
            // 
            // rEPORTEGRAFICOToolStripMenuItem
            // 
            this.rEPORTEGRAFICOToolStripMenuItem.Name = "rEPORTEGRAFICOToolStripMenuItem";
            this.rEPORTEGRAFICOToolStripMenuItem.Size = new System.Drawing.Size(355, 28);
            this.rEPORTEGRAFICOToolStripMenuItem.Text = "REPORTE GRAFICO";
            this.rEPORTEGRAFICOToolStripMenuItem.Click += new System.EventHandler(this.rEPORTEGRAFICOToolStripMenuItem_Click);
            // 
            // ToolStripAdministracion
            // 
            this.ToolStripAdministracion.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripAdministracion.Image")));
            this.ToolStripAdministracion.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripAdministracion.Name = "ToolStripAdministracion";
            this.ToolStripAdministracion.Size = new System.Drawing.Size(274, 36);
            this.ToolStripAdministracion.Text = "ADMINISTRACIÓN";
            this.ToolStripAdministracion.Click += new System.EventHandler(this.ToolStripAdministracion_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem6.Image")));
            this.toolStripMenuItem6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(274, 36);
            this.toolStripMenuItem6.Text = "INFO";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(22, 620);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(44, 16);
            this.lblUsuario.TabIndex = 2;
            this.lblUsuario.Text = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(302, 124);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pnlContenido
            // 
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(308, 0);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Size = new System.Drawing.Size(1296, 661);
            this.pnlContenido.TabIndex = 1;
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.Color.Red;
            this.btnCerrarSesion.Location = new System.Drawing.Point(13, 364);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(121, 38);
            this.btnCerrarSesion.TabIndex = 45;
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // frmMenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1604, 661);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.MenuVertical);
            this.Name = "frmMenuPrincipal";
            this.Text = "Menú Principal";
            this.Load += new System.EventHandler(this.frmMenuPrincipal_Load);
            this.MenuVertical.ResumeLayout(false);
            this.MenuVertical.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MenuVertical;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMantenimiento;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripProcesos;
        private System.Windows.Forms.ToolStripMenuItem ToolStripReportes;
        private System.Windows.Forms.ToolStripMenuItem ToolStripAdministracion;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.ToolStripMenuItem MarcaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModeloToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tIPOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pRODUCTOSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controlStockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem impuestoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fACTURACÓNToolStripMenuItem;
        private System.Windows.Forms.Label lblTipoCambio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem rEPORTESCLIENTESToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rEPORTESDEFACTURAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rEPORTEDELPRODUCTOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rEPORTEGRAFICOToolStripMenuItem;
        private System.Windows.Forms.Button btnCerrarSesion;
    }
}