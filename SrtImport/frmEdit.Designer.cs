namespace SrtImport
{
	partial class frmEdit
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
			this.ctlContent = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.ctlMs = new System.Windows.Forms.NumericUpDown();
			this.ctlTm = new System.Windows.Forms.DateTimePicker();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ctlMs)).BeginInit();
			this.SuspendLayout();
			// 
			// ctlContent
			// 
			this.ctlContent.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ctlContent.Location = new System.Drawing.Point(0, 40);
			this.ctlContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ctlContent.Multiline = true;
			this.ctlContent.Name = "ctlContent";
			this.ctlContent.Size = new System.Drawing.Size(556, 76);
			this.ctlContent.TabIndex = 1;
			// 
			// btnOK
			// 
			this.btnOK.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnOK.Location = new System.Drawing.Point(0, 116);
			this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(556, 35);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.ctlMs);
			this.panel1.Controls.Add(this.ctlTm);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(556, 33);
			this.panel1.TabIndex = 0;
			// 
			// ctlMs
			// 
			this.ctlMs.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctlMs.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.ctlMs.Location = new System.Drawing.Point(436, 0);
			this.ctlMs.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.ctlMs.Name = "ctlMs";
			this.ctlMs.Size = new System.Drawing.Size(120, 26);
			this.ctlMs.TabIndex = 2;
			// 
			// ctlTm
			// 
			this.ctlTm.CustomFormat = "HH:mm:ss";
			this.ctlTm.Dock = System.Windows.Forms.DockStyle.Left;
			this.ctlTm.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.ctlTm.Location = new System.Drawing.Point(0, 0);
			this.ctlTm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ctlTm.Name = "ctlTm";
			this.ctlTm.Size = new System.Drawing.Size(266, 26);
			this.ctlTm.TabIndex = 1;
			// 
			// frmEdit
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(556, 151);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.ctlContent);
			this.Controls.Add(this.btnOK);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmEdit";
			this.Load += new System.EventHandler(this.frmEdit_Load);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ctlMs)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox ctlContent;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DateTimePicker ctlTm;
		private System.Windows.Forms.NumericUpDown ctlMs;
	}
}