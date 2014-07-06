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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.mi_file = new System.Windows.Forms.ToolStripMenuItem();
			this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.miSave = new System.Windows.Forms.ToolStripMenuItem();
			this.miImport = new System.Windows.Forms.ToolStripMenuItem();
			this.miExport = new System.Windows.Forms.ToolStripMenuItem();
			this.mi_subtitles = new System.Windows.Forms.ToolStripMenuItem();
			this.miEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.miRetime = new System.Windows.Forms.ToolStripMenuItem();
			this.miFix = new System.Windows.Forms.ToolStripMenuItem();
			this.miTimeClear = new System.Windows.Forms.ToolStripMenuItem();
			this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.miTimeNext = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.miAll = new System.Windows.Forms.ToolStripMenuItem();
			this.mi_other = new System.Windows.Forms.ToolStripMenuItem();
			this.miHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.ctlFind = new System.Windows.Forms.ToolStripTextBox();
			this.miFind = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridMain
			// 
			this.gridMain.AllowUserToAddRows = false;
			this.gridMain.AllowUserToDeleteRows = false;
			this.gridMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridMain.Location = new System.Drawing.Point(0, 31);
			this.gridMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.gridMain.Name = "gridMain";
			this.gridMain.ReadOnly = true;
			this.gridMain.Size = new System.Drawing.Size(993, 370);
			this.gridMain.TabIndex = 0;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_file,
            this.mi_subtitles,
            this.mi_other,
            this.ctlFind});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(993, 31);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// mi_file
			// 
			this.mi_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpen,
            this.miSave,
            this.miImport,
            this.miExport});
			this.mi_file.Name = "mi_file";
			this.mi_file.Size = new System.Drawing.Size(44, 27);
			this.mi_file.Text = "File";
			// 
			// miOpen
			// 
			this.miOpen.Name = "miOpen";
			this.miOpen.Size = new System.Drawing.Size(140, 24);
			this.miOpen.Text = "miOpen";
			// 
			// miSave
			// 
			this.miSave.Name = "miSave";
			this.miSave.Size = new System.Drawing.Size(140, 24);
			this.miSave.Text = "miSave";
			// 
			// miImport
			// 
			this.miImport.Name = "miImport";
			this.miImport.Size = new System.Drawing.Size(140, 24);
			this.miImport.Text = "miImport";
			// 
			// miExport
			// 
			this.miExport.Name = "miExport";
			this.miExport.Size = new System.Drawing.Size(140, 24);
			this.miExport.Text = "miExport";
			// 
			// mi_subtitles
			// 
			this.mi_subtitles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEdit,
            this.miFind,
            this.miRetime,
            this.miFix,
            this.miTimeClear,
            this.miDelete,
            this.miTimeNext,
            this.toolStripSeparator1,
            this.miAll});
			this.mi_subtitles.Name = "mi_subtitles";
			this.mi_subtitles.Size = new System.Drawing.Size(78, 27);
			this.mi_subtitles.Text = "Subtitles";
			// 
			// miEdit
			// 
			this.miEdit.Name = "miEdit";
			this.miEdit.Size = new System.Drawing.Size(211, 24);
			this.miEdit.Text = "miEdit";
			// 
			// miRetime
			// 
			this.miRetime.Name = "miRetime";
			this.miRetime.Size = new System.Drawing.Size(211, 24);
			this.miRetime.Text = "miRetime";
			// 
			// miFix
			// 
			this.miFix.Name = "miFix";
			this.miFix.Size = new System.Drawing.Size(211, 24);
			this.miFix.Text = "miFix";
			// 
			// miTimeClear
			// 
			this.miTimeClear.Name = "miTimeClear";
			this.miTimeClear.Size = new System.Drawing.Size(211, 24);
			this.miTimeClear.Text = "miTimeClear";
			// 
			// miDelete
			// 
			this.miDelete.Name = "miDelete";
			this.miDelete.Size = new System.Drawing.Size(211, 24);
			this.miDelete.Text = "miDelete";
			// 
			// miTimeNext
			// 
			this.miTimeNext.Name = "miTimeNext";
			this.miTimeNext.Size = new System.Drawing.Size(211, 24);
			this.miTimeNext.Text = "miTimeNext";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(208, 6);
			// 
			// miAll
			// 
			this.miAll.Name = "miAll";
			this.miAll.Size = new System.Drawing.Size(211, 24);
			this.miAll.Text = "miAll";
			// 
			// mi_other
			// 
			this.mi_other.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHelp});
			this.mi_other.Name = "mi_other";
			this.mi_other.Size = new System.Drawing.Size(58, 27);
			this.mi_other.Text = "Other";
			// 
			// miHelp
			// 
			this.miHelp.Name = "miHelp";
			this.miHelp.Size = new System.Drawing.Size(152, 24);
			this.miHelp.Text = "miHelp";
			// 
			// ctlFind
			// 
			this.ctlFind.Name = "ctlFind";
			this.ctlFind.Size = new System.Drawing.Size(100, 27);
			// 
			// toolStripMenuItem1
			// 
			this.miFind.Name = "miFind";
			this.miFind.Size = new System.Drawing.Size(211, 24);
			this.miFind.Text = "miFind";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(993, 401);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "Form1";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
			((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.DataGridView gridMain;
				private System.Windows.Forms.MenuStrip menuStrip1;
				private System.Windows.Forms.ToolStripMenuItem mi_file;
				private System.Windows.Forms.ToolStripMenuItem miOpen;
				private System.Windows.Forms.ToolStripMenuItem miSave;
				private System.Windows.Forms.ToolStripMenuItem miImport;
				private System.Windows.Forms.ToolStripMenuItem miExport;
				private System.Windows.Forms.ToolStripMenuItem mi_subtitles;
				private System.Windows.Forms.ToolStripMenuItem miRetime;
				private System.Windows.Forms.ToolStripMenuItem miFix;
				private System.Windows.Forms.ToolStripMenuItem miTimeClear;
				private System.Windows.Forms.ToolStripMenuItem miDelete;
				private System.Windows.Forms.ToolStripMenuItem miTimeNext;
				private System.Windows.Forms.ToolStripMenuItem miAll;
				private System.Windows.Forms.ToolStripMenuItem mi_other;
				private System.Windows.Forms.ToolStripMenuItem miHelp;
				private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
				private System.Windows.Forms.ToolStripTextBox ctlFind;
				private System.Windows.Forms.ToolStripMenuItem miEdit;
				private System.Windows.Forms.ToolStripMenuItem miFind;
    }
}

