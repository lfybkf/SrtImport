namespace Subimp
{
	partial class frmMainSub
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
			this.panManage = new System.Windows.Forms.Panel();
			this.listMain = new System.Windows.Forms.ListBox();
			this.btnDo = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.ctlFind = new System.Windows.Forms.TextBox();
			this.panManage.SuspendLayout();
			this.SuspendLayout();
			// 
			// panManage
			// 
			this.panManage.Controls.Add(this.ctlFind);
			this.panManage.Controls.Add(this.btnSave);
			this.panManage.Controls.Add(this.btnDo);
			this.panManage.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panManage.Location = new System.Drawing.Point(0, 416);
			this.panManage.Name = "panManage";
			this.panManage.Size = new System.Drawing.Size(724, 44);
			this.panManage.TabIndex = 0;
			// 
			// listMain
			// 
			this.listMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listMain.FormattingEnabled = true;
			this.listMain.ItemHeight = 25;
			this.listMain.Location = new System.Drawing.Point(0, 0);
			this.listMain.Name = "listMain";
			this.listMain.Size = new System.Drawing.Size(724, 416);
			this.listMain.TabIndex = 1;
			// 
			// btnDo
			// 
			this.btnDo.AutoSize = true;
			this.btnDo.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnDo.Location = new System.Drawing.Point(0, 0);
			this.btnDo.Name = "btnDo";
			this.btnDo.Size = new System.Drawing.Size(87, 44);
			this.btnDo.TabIndex = 0;
			this.btnDo.Text = "Do";
			this.btnDo.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			this.btnSave.AutoSize = true;
			this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnSave.Location = new System.Drawing.Point(87, 0);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(87, 44);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			// 
			// ctlFind
			// 
			this.ctlFind.Dock = System.Windows.Forms.DockStyle.Left;
			this.ctlFind.Location = new System.Drawing.Point(174, 0);
			this.ctlFind.Name = "ctlFind";
			this.ctlFind.Size = new System.Drawing.Size(100, 30);
			this.ctlFind.TabIndex = 2;
			// 
			// frmMainSub
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(724, 460);
			this.Controls.Add(this.listMain);
			this.Controls.Add(this.panManage);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "frmMainSub";
			this.Text = "Form1";
			this.panManage.ResumeLayout(false);
			this.panManage.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panManage;
		private System.Windows.Forms.ListBox listMain;
		private System.Windows.Forms.Button btnDo;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.TextBox ctlFind;
	}
}

