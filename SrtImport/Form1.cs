using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BDB;

namespace SrtImport
{
	public partial class Form1 : Form
	{
		Srt srt = new Srt();
		int RowFind = 0;
		public Form1()
		{
			InitializeComponent();
		}

		private void btnImport_Click(object sender, EventArgs e)
		{
			FileDialog fd = new OpenFileDialog();
			fd.DefaultExt = "srt";
			fd.Filter = Srt.SRV.FilterSrt;
			fd.InitialDirectory = Environment.CurrentDirectory;
			if (fd.ShowDialog() == DialogResult.OK)
			{
				srt.Import(fd.FileName);
				srt.Save();
				srt.Show(gridMain);
			}//if
		}//func

		private void Form1_Load(object sender, EventArgs e)
		{
			string[] args = Environment.GetCommandLineArgs();
			if (args.Length > 1)
			{
				string path = args[1];
				if (File.Exists(path))
				{
					if (path.EndsWith(Srt.SRV.ExtSrt))
					{
						srt.Import(path);
						srt.Save();
						path = Path.ChangeExtension(path, Srt.SRV.ExtXml);
					}//if

					Do_Open(path);
				}//if

				if (Directory.Exists(path))
				{
					Srt hObj;
					IEnumerable<string> list = Directory.EnumerateFiles(path).Where(s => s.EndsWith(Srt.SRV.ExtSrt));
					foreach (string s in list)
					{
						hObj = new Srt();
						hObj.Import(s);
						hObj.ExportLyr(path);
					}//for

					Close();
				}//if
			}//if
			else if (args.Length == 1)
			{
			}//if
		}//func

		public void Do_Help()
		{
			frmHelp frm = new frmHelp();
			frm.ShowDialog();
		}//function

		public void Do_Open(string FileName)
		{
			srt.Load(FileName);
			srt.Show(gridMain);
		}//func

		private void btnOpen_Click(object sender, EventArgs e)
		{
			FileDialog fd = new OpenFileDialog();
			fd.DefaultExt = "srt";
			fd.Filter = Srt.SRV.FilterXml;
			fd.InitialDirectory = Environment.CurrentDirectory;
			if (fd.ShowDialog() == DialogResult.OK)
			{
				Do_Open(fd.FileName);
			}//if
		}//func

		private void gridMain_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F3)
			{
				ctlFind.Select();
			}//if
			else if (e.KeyCode== Keys.F2)
			{
				Do_Edit();
			}//if
			else if (e.KeyCode == Keys.F8)
			{
				Do_DeleteRow();
			}//if
			else if (e.KeyCode == Keys.Delete)
			{
				gridMain.CurrentRow.Cells[Srt.FLD.Tm].Value = DBNull.Value;
			}//if
			else if (e.KeyCode == Keys.Down && e.Alt)
			{
				e.Handled = Do_Tm(true);
			}//if
			else if (e.KeyCode == Keys.Up && e.Alt)
			{
				e.Handled = Do_Tm(false);
			}//if

		}//func

		public bool Do_Tm(bool Next)
		{
			DataGridViewCell current_cell = gridMain.CurrentCell;
			if (Next)
			{
				foreach (DataGridViewRow row in gridMain.Rows)
				{
					if (row.Index <= current_cell.RowIndex)
						continue;
					if (row.Index > current_cell.RowIndex && row.Cells[Srt.FLD.Tm].Value != DBNull.Value)
					{
						gridMain.CurrentCell = row.Cells[Srt.FLD.Tm];
						break;
					}//if
				}//for
			}//if
			return true;
		}//func

		public void Do_Edit()
		{
			DataGridViewRow row = gridMain.CurrentRow;
			if (row == null)
				return;

			frmEdit frm = new frmEdit();
			object oTm = row.Cells[Srt.FLD.Tm].Value;
			TimeSpan TmBeg = (TimeSpan)row.Cells[Srt.FLD.TmBeg].Value;
			frm.Tm = (oTm == DBNull.Value) ? TmBeg : (TimeSpan)oTm;
			frm.Content = (string)row.Cells[Srt.FLD.Content].Value;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				row.Cells[Srt.FLD.Tm].Value = frm.Tm;
				row.Cells[Srt.FLD.Content].Value = frm.Content;
			}//if
		}//func

		public void Do_Find(string s)
		{
			ctlFind.BackColor = Color.White;

			string cv;
			DataGridViewCell cell = null;
			bool Done = false;

			for (int i = RowFind; i < gridMain.RowCount; i++)
			{
				cell = gridMain[Srt.FLD.Content, i];
				cv = (string)cell.Value;
				if (cv.Contains(s))
				{
					RowFind = i;
					gridMain.CurrentCell = cell;
					RowFind = (RowFind < gridMain.RowCount - 1) ? RowFind + 1 : 0;
					Done = true;
					break;
				}//if
			}//for

			if (Done==false)
			{
				ctlFind.BackColor = Color.Red;
				RowFind = 0;
			}//if
		}//func

		private void ctlFind_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F3)
			{
				Do_Find(ctlFind.Text);
			}//if
			else if (e.KeyCode == Keys.Enter)
			{
				gridMain.Select();
			}//if
		}//func

		private void btnSave_Click(object sender, EventArgs e)
		{
			srt.Save();
		}

		private void btnRetime_Click(object sender, EventArgs e)
		{
			srt.Retime();
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.SelectedPath = @"C:\Subtitles";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				srt.Export(dlg.SelectedPath);				
			}//if
		}

		private void btnFix_Click(object sender, EventArgs e)
		{
			DataGridViewRow row;
			foreach (DataGridViewCell cell in gridMain.SelectedCells)
			{
				row = cell.OwningRow;
				row.Cells[Srt.FLD.Tm].Value = row.Cells[Srt.FLD.TmBeg].Value;
			}//for
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F1)
				Do_Help();
			else if (e.KeyCode == Keys.Escape)
				gridMain.Select();
			else if (e.Control)
			{
				if (e.KeyCode == Keys.R)
					btnRetime_Click(this, null);
				else if (e.KeyCode == Keys.S)
					btnSave_Click(this, null);
				else if (e.KeyCode == Keys.E)
					btnExport_Click(this, null);
				else if (e.KeyCode == Keys.F)
					btnFix_Click(this, null);
			}//if
		}//func

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (srt.IsChanged
				&& MessageBox.Show("Выйти", "Есть изменения", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
			{
				e.Cancel = true;
			}//if
		}//func

		public void Do_DeleteRow()
		{
			int Id = (int)gridMain.getCurrent(Srt.FLD.Id);
			srt.Delete(Id);
		}

		private void btnAll_Click(object sender, EventArgs e)
		{
			btnRetime_Click(sender, e);
			btnSave_Click(sender, e);
			btnExport_Click(sender, e);
		}//func
  }//class
}
