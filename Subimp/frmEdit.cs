using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Subimp
{
	public partial class frmEdit : Form
	{
		public TimeSpan Tm { get; set; }
		public string Content;
		DateTime Dt = new DateTime(2000, 1, 1);

		public frmEdit()
		{
			InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Content = ctlContent.Text;
			Tm = ctlTm.Value - Dt;
			Tm = Tm + TimeSpan.FromMilliseconds((double)ctlMs.Value);
			DialogResult = DialogResult.OK;
			Close();
		}//func

		private void frmEdit_Load(object sender, EventArgs e)
		{
			ctlTm.Format = DateTimePickerFormat.Custom;
			ctlTm.CustomFormat = "HH:mm:ss";
			ctlContent.Text = Content;
			ctlTm.Value = Dt + Tm.Subtract(new TimeSpan(0, 0, 0, 0,Tm.Milliseconds) );
			ctlMs.Value = Tm.Milliseconds;
		}//func
	}//class
}//ns
