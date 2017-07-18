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
			btnSaveFix.Click += btnSaveFix_Click;
			btnHelp.Click += btnHelp_Click;
			btnBest.Click += btnBest_Click;
			listMain.KeyUp += listMain_KeyUp;
			ctlFind.KeyUp += ctlFind_KeyUp;

			listMain.Select();
		}

		private void btnBest_Click(object sender, EventArgs e)
		{
			Sub sub = pack.BestCandidat();
			//listMain.SelectedItem = sub;
			SelectedSub = sub;
		}

		private void btnSaveFix_Click(object sender, EventArgs e)
		{
			pack.SaveFix();
			Info("save fix");
		}

		static string help_message = string.Empty;

		void btnHelp_Click(object sender, EventArgs e) { MessageBox.Show(help_message); }

		void ctlFind_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F3 || e.KeyCode == Keys.Enter)
			{
				Sub sub = pack.Find(ctlFind.Text, SelectedSub);
				SelectedSub = sub;
			}//if;
			else if (e.KeyCode == Keys.Escape){	listMain.Select(); }//if
		}


		void doEdit(Sub sub)
		{
			frmEdit frm = new frmEdit();
			frm.Tm = sub.TmBeg;
			frm.Content = sub.Content;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				var updFicks = frm.Tm.Ticks;
				var updContent = frm.Content;
				bool CanUpdateFicks = true;
				bool CanUpdateContent = true;

				var subErr = pack.Items.FirstOrDefault(z => z.Content == updContent && z.ID != sub.ID);
				if (subErr != null)
				{
					MessageBox.Show($"{subErr.ID} has same content - {updContent}");
					CanUpdateFicks = false;
				}//if
				else if ((subErr = pack.Items.Where(z => z.ID < sub.ID && z.HasFix).FirstOrDefault(z => z.Ficks >= updFicks)) != null)
				{
					MessageBox.Show($"{subErr.ID} has fixed content before where Ficks >= {new TimeSpan(updFicks).ToStr()}");
					CanUpdateFicks = false;
				}//if
				else if ((subErr = pack.Items.Where(z => z.ID > sub.ID && z.HasFix).FirstOrDefault(z => z.Ficks <= updFicks)) != null)
				{
					MessageBox.Show($"{subErr.ID} has fixed content after where Ficks <= {new TimeSpan(updFicks).ToStr()}");
					CanUpdateFicks = false;
				}//if

				if (updContent.isEmpty())
				{
					MessageBox.Show("Content is empty");
					CanUpdateContent = false;
				}//if
				if (sub.Content == updContent)
				{
					//MessageBox.Show("Content is not change");
					CanUpdateContent = false;
				}//if

				if (CanUpdateFicks) { sub.Ficks = updFicks; }//if
				if (CanUpdateContent) { sub.Content = updContent; }//if

				if (CanUpdateContent || CanUpdateFicks)
				{
					ListRefresh();
				}//if
				
			}//if
		}//function

		void listMain_KeyUp(object sender, KeyEventArgs e)
		{
			Sub sub = SelectedSub;
			if (e.KeyCode == Keys.Delete && sub != null)
			{
				pack.Remove(sub);
				ListRefresh();
			}//if
			else if (e.KeyCode == Keys.Space)
			{
				btnBest_Click(sender, e);
			}//if
			else if (e.KeyCode == Keys.D && e.Control)
			{
				btnDo_Click(sender, e);
			}//if
			else if (e.KeyCode == Keys.F2 && sub != null)
			{
				doEdit(sub);
			}//if
			else if (e.KeyCode == Keys.F3)
			{
				ctlFind.Select();
			}//if
			else if (e.KeyCode == Keys.F4)
			{
				doGoToNextFix(sub);
			}//if
		}//function

		private void doGoToNextFix(Sub sub)
		{
			Sub next = pack.Items.FirstOrDefault(z => z.ID > sub.ID && z.HasFix) ?? pack.Items.FirstOrDefault(z => z.HasFix);
			if (next != null)
			{
				SelectedSub = next;
			}//if
		}//function

		void frmMainSub_Load(object sender, EventArgs e)
		{
			#region settings
			Settings.Instance = Ini.Load("subimp.ini")?.DeSerialize<Settings>();
			#endregion

			#region help
			help_message = Environment.NewLine.join(
			" - клавиатура -",
			"F2 - редактировать",
			"F3 - искать",
			"F4 - следующий фикс",
			"Space - кандидат",
			"Escape - переход в список",
			"Delete - удалить",
			" - консольные команды -",
			"QUIT - сохранить экспортировать выйти",
			" - ini -",
			Settings.Instance.ToString()
			);
			#endregion

			#region args work
			string[] args = Environment.GetCommandLineArgs();
			if (args.Length > 1)
			{
				string input = args[1];
				if (input.EndsWith(EXT.Srt))
				{
					var ss = io.File.ReadAllLines(input);
					pack = new Pack(io.Path.GetFileNameWithoutExtension(input));
					pack.ImportSrt(ss);
					pack.MergeFix();
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

				if (args.Any(z => z == W.QUIT))
				{
					pack.Save();
					pack.ExportSrt();
					pack.ExportLyr();
					this.Close();
				}//if

				this.Text = pack.with(z => z.Name, string.Empty);
			}//if
			#endregion

			#region events
			btnBest.KeyUp += btn_KeyUp;
			btnDo.KeyUp += btn_KeyUp;
			btnSave.KeyUp += btn_KeyUp;
			btnSaveFix.KeyUp += btn_KeyUp;
			#endregion

			ListRefresh();

			doStart();
		}//func

		private void btn_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape) { listMain.Select(); }
		}//func

		private void doStart()
		{
			pack.Save();
			//pack.ExportSrt();
			//pack.ExportLyr();
		}//function

		public  void Info(string s)
		{
			Text = $"{s} {DateTime.Now.ToShortTimeString()}";
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
			if (pack == null) { return;	}//if
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
