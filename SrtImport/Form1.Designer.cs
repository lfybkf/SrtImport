namespace SrtImport
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
			this.gridMain = new System.Windows.Forms.DataGridView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnAll = new System.Windows.Forms.Button();
			this.btnFix = new System.Windows.Forms.Button();
			this.ctlFind = new System.Windows.Forms.TextBox();
			this.btnRetime = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnOpen = new System.Windows.Forms.Button();
			this.btnExport = new System.Windows.Forms.Button();
			this.btnImport = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridMain
			// 
			this.gridMain.AllowUserToAddRows = false;
			this.gridMain.AllowUserToDeleteRows = false;
			this.gridMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridMain.Location = new System.Drawing.Point(0, 0);
			this.gridMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.gridMain.Name = "gridMain";
			this.gridMain.ReadOnly = true;
			this.gridMain.Size = new System.Drawing.Size(699, 364);
			this.gridMain.TabIndex = 0;
			this.gridMain.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridMain_KeyUp);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnAll);
			this.panel1.Controls.Add(this.btnFix);
			this.panel1.Controls.Add(this.ctlFind);
			this.panel1.Controls.Add(this.btnRetime);
			this.panel1.Controls.Add(this.btnSave);
			this.panel1.Controls.Add(this.btnOpen);
			this.panel1.Controls.Add(this.btnExport);
			this.panel1.Controls.Add(this.btnImport);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 364);
			this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(699, 37);
			this.panel1.TabIndex = 1;
			// 
			// btnAll
			// 
			this.btnAll.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnAll.Location = new System.Drawing.Point(462, 0);
			this.btnAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnAll.Name = "btnAll";
			this.btnAll.Size = new System.Drawing.Size(81, 37);
			this.btnAll.TabIndex = 7;
			this.btnAll.Text = "ALL";
			this.btnAll.UseVisualStyleBackColor = true;
			this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
			// 
			// btnFix
			// 
			this.btnFix.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnFix.Location = new System.Drawing.Point(381, 0);
			this.btnFix.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnFix.Name = "btnFix";
			this.btnFix.Size = new System.Drawing.Size(81, 37);
			this.btnFix.TabIndex = 6;
			this.btnFix.Text = "Fix";
			this.btnFix.UseVisualStyleBackColor = true;
			this.btnFix.Click += new System.EventHandler(this.btnFix_Click);
			// 
			// ctlFind
			// 
			this.ctlFind.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctlFind.Location = new System.Drawing.Point(590, 0);
			this.ctlFind.Name = "ctlFind";
			this.ctlFind.Size = new System.Drawing.Size(109, 30);
			this.ctlFind.TabIndex = 2;
			this.ctlFind.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ctlFind_KeyUp);
			// 
			// btnRetime
			// 
			this.btnRetime.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnRetime.Location = new System.Drawing.Point(300, 0);
			this.btnRetime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnRetime.Name = "btnRetime";
			this.btnRetime.Size = new System.Drawing.Size(81, 37);
			this.btnRetime.TabIndex = 4;
			this.btnRetime.Text = "Retime";
			this.btnRetime.UseVisualStyleBackColor = true;
			this.btnRetime.Click += new System.EventHandler(this.btnRetime_Click);
			// 
			// btnSave
			// 
			this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnSave.Location = new System.Drawing.Point(225, 0);
			this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 37);
			this.btnSave.TabIndex = 3;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnOpen
			// 
			this.btnOpen.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnOpen.Location = new System.Drawing.Point(155, 0);
			this.btnOpen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(70, 37);
			this.btnOpen.TabIndex = 1;
			this.btnOpen.Text = "Open";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// btnExport
			// 
			this.btnExport.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnExport.Location = new System.Drawing.Point(78, 0);
			this.btnExport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(77, 37);
			this.btnExport.TabIndex = 5;
			this.btnExport.Text = "Export";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// btnImport
			// 
			this.btnImport.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnImport.Location = new System.Drawing.Point(0, 0);
			this.btnImport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(78, 37);
			this.btnImport.TabIndex = 0;
			this.btnImport.Text = "Import";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(699, 401);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "Form1";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
			((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridMain;
        private System.Windows.Forms.Panel panel1;
				private System.Windows.Forms.Button btnImport;
				private System.Windows.Forms.Button btnOpen;
				private System.Windows.Forms.TextBox ctlFind;
				private System.Windows.Forms.Button btnSave;
				private System.Windows.Forms.Button btnRetime;
				private System.Windows.Forms.Button btnExport;
				private System.Windows.Forms.Button btnFix;
				private System.Windows.Forms.Button btnAll;
    }
}

