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

		public frmMainSub()
		{
			this.Load += frmMainSub_Load;

			InitializeComponent();
			
			btnDo.Click += btnDo_Click;
			btnSave.Click += btnSave_Click;
		}

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
				else if (input.EndsWith(EXT.Json))
				{
					pack = Pack.Load(input);
				}//if
			}//if
		}

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
		}
	}//function
}
