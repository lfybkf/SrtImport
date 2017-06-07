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
			this.btnBest = new System.Windows.Forms.Button();
			this.btnSaveFix = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.ctlFind = new System.Windows.Forms.TextBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnDo = new System.Windows.Forms.Button();
			this.listMain = new System.Windows.Forms.ListBox();
			this.panManage.SuspendLayout();
			this.SuspendLayout();
			// 
			// panManage
			// 
			this.panManage.Controls.Add(this.btnBest);
			this.panManage.Controls.Add(this.btnSaveFix);
			this.panManage.Controls.Add(this.btnHelp);
			this.panManage.Controls.Add(this.ctlFind);
			this.panManage.Controls.Add(this.btnSave);
			this.panManage.Controls.Add(this.btnDo);
			this.panManage.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panManage.Location = new System.Drawing.Point(0, 481);
			this.panManage.Name = "panManage";
			this.panManage.Size = new System.Drawing.Size(962, 38);
			this.panManage.TabIndex = 0;
			// 
			// btnBest
			// 
			this.btnBest.AutoSize = true;
			this.btnBest.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnBest.Location = new System.Drawing.Point(268, 0);
			this.btnBest.Name = "btnBest";
			this.btnBest.Size = new System.Drawing.Size(94, 38);
			this.btnBest.TabIndex = 5;
			this.btnBest.Text = "Best";
			this.btnBest.UseVisualStyleBackColor = true;
			// 
			// btnSaveFix
			// 
			this.btnSaveFix.AutoSize = true;
			this.btnSaveFix.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnSaveFix.Location = new System.Drawing.Point(174, 0);
			this.btnSaveFix.Name = "btnSaveFix";
			this.btnSaveFix.Size = new System.Drawing.Size(94, 38);
			this.btnSaveFix.TabIndex = 4;
			this.btnSaveFix.Text = "SaveFix";
			this.btnSaveFix.UseVisualStyleBackColor = true;
			// 
			// btnHelp
			// 
			this.btnHelp.AutoSize = true;
			this.btnHelp.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnHelp.Location = new System.Drawing.Point(775, 0);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(87, 38);
			this.btnHelp.TabIndex = 7;
			this.btnHelp.Text = "Help";
			this.btnHelp.UseVisualStyleBackColor = true;
			// 
			// ctlFind
			// 
			this.ctlFind.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctlFind.Location = new System.Drawing.Point(862, 0);
			this.ctlFind.Name = "ctlFind";
			this.ctlFind.Size = new System.Drawing.Size(100, 26);
			this.ctlFind.TabIndex = 8;
			// 
			// btnSave
			// 
			this.btnSave.AutoSize = true;
			this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnSave.Location = new System.Drawing.Point(87, 0);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(87, 38);
			this.btnSave.TabIndex = 3;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			// 
			// btnDo
			// 
			this.btnDo.AutoSize = true;
			this.btnDo.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnDo.Location = new System.Drawing.Point(0, 0);
			this.btnDo.Name = "btnDo";
			this.btnDo.Size = new System.Drawing.Size(87, 38);
			this.btnDo.TabIndex = 2;
			this.btnDo.Text = "Do";
			this.btnDo.UseVisualStyleBackColor = true;
			// 
			// listMain
			// 
			this.listMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listMain.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.listMain.FormattingEnabled = true;
			this.listMain.ItemHeight = 21;
			this.listMain.Location = new System.Drawing.Point(0, 0);
			this.listMain.Name = "listMain";
			this.listMain.Size = new System.Drawing.Size(962, 481);
			this.listMain.TabIndex = 0;
			// 
			// frmMainSub
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(962, 519);
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
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnSaveFix;
		private System.Windows.Forms.Button btnBest;
	}
}

