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
	public partial class frmMain : Form
	{
		Srt srt = new Srt();
		IList<Kommand> kmds = new List<Kommand>();

		public frmMain()
		{
			InitializeComponent();
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			InitKmd();

			string[] args = Environment.GetCommandLineArgs();
			if (args.Length > 1)
			{
				string path = args[1];
				if (File.Exists(path))
				{
					if (path.EndsWith(EXT.Xml) == false)
					{
						srt.Import(path);
						srt.Save();
						path = Path.ChangeExtension(path, EXT.Xml);
					}//if

					doOpen(path);
				}//if

				//если параметр каталог, то это массовая конвертация
				if (Directory.Exists(path) && args.Any(s => s == "cmdSrt2Lrc"))
				{
					Srt hSrt;
					IEnumerable<string> list = Directory.EnumerateFiles(path).Where(s => s.EndsWith(EXT.Srt));
					foreach (string s in list)
					{
						hSrt = new Srt();
						hSrt.Import(s);
						hSrt.ExportLyr(path);
					}//for

					Close();
				}//if

				if (Directory.Exists(path) && args.Any(s => s == "cmdSrtWithTrn"))
				{
					Srt hSrt;
					string FileTrnDirect = args.FirstOrDefault(s => s.EndsWith("trn"));
					IEnumerable<string> list = Directory.EnumerateFiles(path).Where(s => s.EndsWith(EXT.Srt));
					foreach (string s in list)
					{
						hSrt = new Srt();
						hSrt.FileTrnDirect = FileTrnDirect;
						hSrt.Import(s);
						hSrt.Save();
						hSrt.ExportSrt(DIR.Srt);
					}//for

					Close();
				}//if
			}//if
			else if (args.Length == 1)
			{
			}//if
		}//func

		private void InitKmd()
		{
			kmds.Add(new Kommand("Help", doHelp, Keys.F1));
			kmds.Add(new Kommand("Open", doOpen, Keys.Control | Keys.O));
			kmds.Add(new Kommand("Save", srt.Save, Keys.Control | Keys.S));
			kmds.Add(new Kommand("Import", doImport, Keys.Control | Keys.I));
			kmds.Add(new Kommand("Export", doExport, Keys.Control | Keys.E));
			kmds.Add(new Kommand("Edit", doEdit, Keys.F2));
			kmds.Add(new Kommand("Retime", srt.Retime, Keys.Control | Keys.R));
			kmds.Add(new Kommand("Fix", doFix, Keys.Control | Keys.F));
			kmds.Add(new Kommand("Delete", doDelete, Keys.F8));
			kmds.Add(new Kommand("TimeClear", doTimeClear, Keys.Delete));
			kmds.Add(new Kommand("TimeNext", doTimeNext, Keys.F4));
			kmds.Add(new Kommand("Find", doFind, Keys.F3));
			kmds.Add(new Kommand("All", Keys.Control | Keys.A).Add(srt.Retime).Add(srt.Save).Add(doExport));

			foreach (Kommand kmd in kmds) { kmd.isExecuted += kmd_isExecuted; }
 			foreach (var mi in mi_file.DropDownItems.OfType<ToolStripItem>())	{kmds.LinkToComponent(mi);	}//for
			foreach (var mi in mi_subtitles.DropDownItems.OfType<ToolStripItem>()) { kmds.LinkToComponent(mi); }//for
			foreach (var mi in mi_other.DropDownItems.OfType<ToolStripItem>()) { kmds.LinkToComponent(mi); }//for
		}//function
		
		void kmd_isExecuted(object sender, EventArgs e) { this.Text = (sender as Kommand).Caption; }
		
		void doOpen()
		{
			FileDialog fd = new OpenFileDialog();
			fd.DefaultExt = "srt";
			fd.Filter = FILTER.Xml;
			fd.InitialDirectory = Environment.CurrentDirectory;
			if (fd.ShowDialog() == DialogResult.OK)
			{
				doOpen(fd.FileName);
			}//if
		}//func
		void doTimeClear() 
		{ 
			gridMain.CurrentRow.Cells[FLD.Tm].Value = DBNull.Value; 
		}//function

		void doImport()
		{
			FileDialog fd = new OpenFileDialog();
			fd.DefaultExt = "srt";
			fd.Filter = FILTER.Srt;
			fd.InitialDirectory = Environment.CurrentDirectory;
			if (fd.ShowDialog() == DialogResult.OK)
			{
				srt.Import(fd.FileName);
				srt.Save();
				srt.Show(gridMain);
			}//if
		}//func
		void doHelp()  
		{	
			frmHelp frm = new frmHelp();	frm.ShowDialog();	
		}//function
		void doTimeNext()
		{
			DataGridViewCell current_cell = gridMain.CurrentCell;
			foreach (DataGridViewRow row in gridMain.Rows)
			{
				if (row.Index <= current_cell.RowIndex)
					continue;
				if (row.Index > current_cell.RowIndex && row.Cells[FLD.Tm].Value != DBNull.Value)
				{
					gridMain.CurrentCell = row.Cells[FLD.Tm];
					break;
				}//if
			}//for
		}//function

		void doEdit()
		{
			DataGridViewRow row = gridMain.CurrentRow;
			if (row == null)
				return;

			frmEdit frm = new frmEdit();
			object oTm = row.Cells[FLD.Tm].Value;
			TimeSpan TmBeg = (TimeSpan)row.Cells[FLD.TmBeg].Value;
			frm.Tm = (oTm == DBNull.Value) ? TmBeg : (TimeSpan)oTm;
			frm.Content = (string)row.Cells[FLD.Content].Value;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				row.Cells[FLD.Tm].Value = frm.Tm;
				row.Cells[FLD.Content].Value = frm.Content;
			}//if
		}//function
		void doExport()
		{
			if (Directory.Exists(DIR.Srt))
			{
				srt.Export(DIR.Srt);
				return;
			}//if
			
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				srt.Export(dlg.SelectedPath);
			}//if
		}//function
		void doFix()
		{
			DataGridViewRow row;
			foreach (DataGridViewCell cell in gridMain.SelectedCells)
			{
				row = cell.OwningRow;
				row.Cells[FLD.Tm].Value = row.Cells[FLD.TmBeg].Value;
			}//for
		}//function
		void doDelete()
		{
			int Id = (int)gridMain.getCurrent(FLD.Id);
			srt.Delete(Id);
		}//function
		void doFind()
		{
			if (ctlFind.Focused)
			{
				ctlFind.BackColor = Color.White;
				string s = ctlFind.Text;
				DataGridViewCell cell = null;
				bool Done = false;

				for (int i = gridMain.CurrentCell.RowIndex + 1; i < gridMain.RowCount; i++)
				{
					cell = gridMain[FLD.Content, i];
					if (((string)cell.Value).Contains(s))
					{
						gridMain.CurrentCell = cell;
						Done = true;
						break;
					}//if
				}//for
				ctlFind.BackColor = Done ? Color.White : Color.Red;
			}//if
			else
			{
				ctlFind.Focus();
				ctlFind.BackColor = Color.Yellow;
			}
		}//func
	
		
		public void doOpen(string FileName)
		{
			srt.Load(FileName);
			srt.Show(gridMain);
		}//func

		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape) { gridMain.Focus();}
		}//func

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (srt.IsChanged
				&& MessageBox.Show("Выйти", "Есть изменения", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
			{
				e.Cancel = true;
			}//if
		}//func

  }//class
}
