namespace ElectroNova.Layers.UI
{
    partial class frmAdministracion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdministracion));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ToolStripMenuNuevo = new System.Windows.Forms.ToolStripButton();
            this.GuardartoolStripMenuItem1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripEditar = new System.Windows.Forms.ToolStripButton();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboRol = new System.Windows.Forms.ComboBox();
            this.chkInactivo = new System.Windows.Forms.CheckBox();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.txtContrasenia = new System.Windows.Forms.TextBox();
            this.txtNombreUsuario = new System.Windows.Forms.TextBox();
            this.txtID_Usuario = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuNuevo,
            this.GuardartoolStripMenuItem1,
            this.toolStripEditar,
            this.eliminarToolStripMenuItem});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(806, 71);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ToolStripMenuNuevo
            // 
            this.ToolStripMenuNuevo.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuNuevo.Image")));
            this.ToolStripMenuNuevo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripMenuNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripMenuNuevo.Name = "ToolStripMenuNuevo";
            this.ToolStripMenuNuevo.Size = new System.Drawing.Size(178, 68);
            this.ToolStripMenuNuevo.Text = "Agregar Nuevo";
            this.ToolStripMenuNuevo.Click += new System.EventHandler(this.ToolStripMenuNuevo_Click);
            // 
            // GuardartoolStripMenuItem1
            // 
            this.GuardartoolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("GuardartoolStripMenuItem1.Image")));
            this.GuardartoolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.GuardartoolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GuardartoolStripMenuItem1.Name = "GuardartoolStripMenuItem1";
            this.GuardartoolStripMenuItem1.Size = new System.Drawing.Size(130, 68);
            this.GuardartoolStripMenuItem1.Text = "Guardar";
            this.GuardartoolStripMenuItem1.Click += new System.EventHandler(this.GuardartoolStripMenuItem1_Click);
            // 
            // toolStripEditar
            // 
            this.toolStripEditar.Image = ((System.Drawing.Image)(resources.GetObject("toolStripEditar.Image")));
            this.toolStripEditar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEditar.Name = "toolStripEditar";
            this.toolStripEditar.Size = new System.Drawing.Size(116, 68);
            this.toolStripEditar.Text = "Editar";
            this.toolStripEditar.Click += new System.EventHandler(this.toolStripEditar_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("eliminarToolStripMenuItem.Image")));
            this.eliminarToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.eliminarToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(131, 68);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(806, 542);
            this.panel1.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvDatos);
            this.groupBox2.Location = new System.Drawing.Point(12, 361);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(791, 169);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // dgvDatos
            // 
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDatos.Location = new System.Drawing.Point(3, 18);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.RowHeadersWidth = 51;
            this.dgvDatos.Size = new System.Drawing.Size(785, 148);
            this.dgvDatos.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboRol);
            this.groupBox1.Controls.Add(this.chkInactivo);
            this.groupBox1.Controls.Add(this.chkActivo);
            this.groupBox1.Controls.Add(this.txtContrasenia);
            this.groupBox1.Controls.Add(this.txtNombreUsuario);
            this.groupBox1.Controls.Add(this.txtID_Usuario);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(791, 352);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información Usuario";
            // 
            // cboRol
            // 
            this.cboRol.FormattingEnabled = true;
            this.cboRol.Location = new System.Drawing.Point(243, 169);
            this.cboRol.Name = "cboRol";
            this.cboRol.Size = new System.Drawing.Size(384, 27);
            this.cboRol.TabIndex = 42;
            // 
            // chkInactivo
            // 
            this.chkInactivo.AutoSize = true;
            this.chkInactivo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInactivo.Location = new System.Drawing.Point(548, 242);
            this.chkInactivo.Name = "chkInactivo";
            this.chkInactivo.Size = new System.Drawing.Size(79, 21);
            this.chkInactivo.TabIndex = 41;
            this.chkInactivo.Text = "Inactivo";
            this.chkInactivo.UseVisualStyleBackColor = true;
            // 
            // chkActivo
            // 
            this.chkActivo.AutoSize = true;
            this.chkActivo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActivo.Location = new System.Drawing.Point(243, 246);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(69, 21);
            this.chkActivo.TabIndex = 40;
            this.chkActivo.Text = "Activo";
            this.chkActivo.UseVisualStyleBackColor = true;
            // 
            // txtContrasenia
            // 
            this.txtContrasenia.Location = new System.Drawing.Point(243, 122);
            this.txtContrasenia.Name = "txtContrasenia";
            this.txtContrasenia.Size = new System.Drawing.Size(384, 27);
            this.txtContrasenia.TabIndex = 36;
            // 
            // txtNombreUsuario
            // 
            this.txtNombreUsuario.Location = new System.Drawing.Point(243, 75);
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.Size = new System.Drawing.Size(384, 27);
            this.txtNombreUsuario.TabIndex = 35;
            // 
            // txtID_Usuario
            // 
            this.txtID_Usuario.Location = new System.Drawing.Point(243, 33);
            this.txtID_Usuario.Name = "txtID_Usuario";
            this.txtID_Usuario.Size = new System.Drawing.Size(384, 27);
            this.txtID_Usuario.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 246);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 17);
            this.label7.TabIndex = 33;
            this.label7.Text = "Estado";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 17);
            this.label3.TabIndex = 29;
            this.label3.Text = "Rol";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 17);
            this.label2.TabIndex = 28;
            this.label2.Text = "Contraseña";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 17);
            this.label1.TabIndex = 27;
            this.label1.Text = "Nombre Usuario";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 17);
            this.label9.TabIndex = 26;
            this.label9.Text = "ID Usuario";
            // 
            // frmAdministracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 613);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmAdministracion";
            this.Text = "frmAdministracion";
            this.Load += new System.EventHandler(this.frmAdministracion_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ToolStripMenuNuevo;
        private System.Windows.Forms.ToolStripButton GuardartoolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton toolStripEditar;
        private System.Windows.Forms.ToolStripButton eliminarToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtContrasenia;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.TextBox txtID_Usuario;
        private System.Windows.Forms.CheckBox chkActivo;
        private System.Windows.Forms.ComboBox cboRol;
        private System.Windows.Forms.CheckBox chkInactivo;
    }
}