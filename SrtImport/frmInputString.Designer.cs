namespace SrtImport
{
	partial class frmInputString
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
			this.ctlName = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// ctlName
			// 
			this.ctlName.Dock = System.Windows.Forms.DockStyle.Top;
			this.ctlName.Location = new System.Drawing.Point(0, 0);
			this.ctlName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ctlName.Name = "ctlName";
			this.ctlName.Size = new System.Drawing.Size(438, 26);
			this.ctlName.TabIndex = 0;
			// 
			// btnOK
			// 
			this.btnOK.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnOK.Location = new System.Drawing.Point(0, 33);
			this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(438, 35);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// frmInputString
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(438, 68);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.ctlName);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmInputString";
			this.Text = "frmInputString";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox ctlName;
		private System.Windows.Forms.Button btnOK;
	}
}