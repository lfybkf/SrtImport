using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDB;

namespace Subimp
{
	public class Sub
	{
		public int ID { get; set; }
		public long Ticks { get { return Tm.Ticks; } set { Tm = new TimeSpan(value); } }
		public long Ficks { get { return Fm.Ticks; } set { Fm = new TimeSpan(value); } }
		public string Content { get; set; }
		TimeSpan Tm;
		TimeSpan Fm;
		
		Pack pack = null;

		public Sub()
		{
			ID = 0;
			Fm = TS.Zero;
			Content = string.Empty;
		}//constructor

		public override string ToString()
		{
			return "{0:D3} - {1} - {2} - {3}".fmt(
				ID
				, Tm.ToStr()
				, Fm.ToStr()
				, Content);
		}

		internal Sub Next { get { return pack.Items.SkipWhile(z => z.ID <= this.ID).FirstOrDefault(); } }
		internal TimeSpan TmEnd { get { return Next.with(z => z.Tm - TS.Delta, Tm.Add(TS.Dur)); } }
		internal TimeSpan TmBeg { get { return Tm; } }
		internal TimeSpan TmFix { get { return Fm; } }
		internal TimeSpan TmDur { get { return TmEnd - Tm; } }

		public  string toSrt()
		{
			return ID.ToString()
						.addLine(
						"{0} --> {1}".fmt(Tm.ToStrSRT(), TmEnd.ToStrSRT())
						, Content
						, string.Empty);
		}//function

		public  string toLyr()
		{
			return "[{0}]{1}".fmt(TmBeg.ToStrLYR(), Content);
		}//function

		public void setPack(Pack pack)
		{
			this.pack = pack;
		}//function

		internal bool IsIvalable
		{
			get
			{
				return Fm != TS.Zero;
			}
		}
	}//class
}//namespace
