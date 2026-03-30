namespace ElectroNova.Layers.UI
{
    partial class frmImpuesto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImpuesto));
            this.panel1 = new System.Windows.Forms.Panel();
            this.mnuStrip1 = new System.Windows.Forms.MenuStrip();
            this.GuardartoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIva = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.mnuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtIva);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.mnuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 348);
            this.panel1.TabIndex = 0;
            // 
            // mnuStrip1
            // 
            this.mnuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.mnuStrip1.Font = new System.Drawing.Font("Yu Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuStrip1.GripMargin = new System.Windows.Forms.Padding(2);
            this.mnuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GuardartoolStripMenuItem1});
            this.mnuStrip1.Location = new System.Drawing.Point(0, 0);
            this.mnuStrip1.Name = "mnuStrip1";
            this.mnuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.mnuStrip1.Size = new System.Drawing.Size(529, 98);
            this.mnuStrip1.TabIndex = 95;
            this.mnuStrip1.Text = "menuStrip1";
            // 
            // GuardartoolStripMenuItem1
            // 
            this.GuardartoolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("GuardartoolStripMenuItem1.Image")));
            this.GuardartoolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.GuardartoolStripMenuItem1.Name = "GuardartoolStripMenuItem1";
            this.GuardartoolStripMenuItem1.Size = new System.Drawing.Size(105, 94);
            this.GuardartoolStripMenuItem1.Text = "Guardar";
            this.GuardartoolStripMenuItem1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 195);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 24);
            this.label1.TabIndex = 96;
            this.label1.Text = "Costo IVA:";
            // 
            // txtIva
            // 
            this.txtIva.Location = new System.Drawing.Point(160, 195);
            this.txtIva.Name = "txtIva";
            this.txtIva.Size = new System.Drawing.Size(114, 22);
            this.txtIva.TabIndex = 97;
            // 
            // frmImpuesto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 348);
            this.Controls.Add(this.panel1);
            this.Name = "frmImpuesto";
            this.Text = "frmImpuesto";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.mnuStrip1.ResumeLayout(false);
            this.mnuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip mnuStrip1;
        private System.Windows.Forms.ToolStripMenuItem GuardartoolStripMenuItem1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIva;
    }
}