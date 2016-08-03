using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BDB;
using io = System.IO;

namespace Subimp
{
	public partial class frmMainSub : Form
	{
		Pack pack;
		public Sub SelectedSub { 
			get {return listMain.SelectedItem as Sub; }
			set { if (value != null && listMain.Items.Contains(value)) { listMain.SelectedItem = value; } }
		}

		public frmMainSub()
		{
			this.Load += frmMainSub_Load;

			InitializeComponent();
			
			btnDo.Click += btnDo_Click;
			btnSave.Click += btnSave_Click;
			listMain.KeyUp += listMain_KeyUp;
			ctlFind.KeyUp += ctlFind_KeyUp;

			listMain.Select();
		}

		void ctlFind_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F3 || e.KeyCode == Keys.Enter)
			{
				Sub sub = pack.Find(ctlFind.Text, SelectedSub);
				SelectedSub = sub;
			}//if;
			else if (e.KeyCode == Keys.Escape)
			{
				listMain.Select();
			}//if
		}

		void listMain_KeyUp(object sender, KeyEventArgs e)
		{
			Sub sub = SelectedSub;
			if (e.KeyCode == Keys.Delete && sub != null)
			{
				pack.Remove(sub);
				ListRefresh();
			}//if
			else if (e.KeyCode == Keys.F2 && sub != null)
			{
				frmEdit frm = new frmEdit();
				frm.Tm = sub.TmBeg;
				frm.Content = sub.Content;
				if (frm.ShowDialog() == DialogResult.OK)
				{
					sub.Ficks = frm.Tm.Ticks;
					sub.Content = frm.Content;
					ListRefresh();
				}//if
			}//if
			else if (e.KeyCode == Keys.F3)
			{
				ctlFind.Select();
			}//if
		}//function

		void frmMainSub_Load(object sender, EventArgs e)
		{
			string[] args = Environment.GetCommandLineArgs();
			if (args.Length > 1)
			{
				string input = args[1];
				if (input.EndsWith(EXT.Srt))
				{
					var ss = io.File.ReadAllLines(input);
					pack = new Pack(io.Path.GetFileNameWithoutExtension(input));
					pack.ImportSrt(ss);
				}//if
				else if (input.EndsWith(EXT.Lyr))
				{
					var ss = io.File.ReadAllLines(input);
					pack = new Pack(io.Path.GetFileNameWithoutExtension(input));
					pack.ImportLyr(ss);
				}//if
				else if (input.EndsWith(EXT.Json))
				{
					pack = Pack.Load(input);
				}//if

				this.Text = pack.with(z => z.Name, string.Empty);
			}//if

			ListRefresh();
		}//function

		public  void Info(string s)
		{
			Text = "{0} {1}".fmt(s, DateTime.Now.ToShortTimeString());
		}//function

		void btnSave_Click(object sender, EventArgs e)
		{
			pack.Save();
			Info("save");
		}

		void btnDo_Click(object sender, EventArgs e)
		{
			pack.Retime();
			pack.ExportSrt();
			pack.ExportLyr();
			Info("export");
			ListRefresh();
		}

		public  void ListRefresh()
		{
			if (pack == null)			{				return;			}//if
			var selected = listMain.with(z => z.SelectedItem);
			listMain.DataSource = null;
			listMain.DataSource = pack.Items;
			if (selected != null)
			{
				listMain.SelectedItem = selected;
			}//if
		}//function
	}//function
}
