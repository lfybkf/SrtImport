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
			return "{0:D3} {1} {2} {3}".fmt(
				ID
				, Tm.ToStr()
				, Fm.ToStr()
				, Content);
		}

		public Sub getNext() { return pack.Items.SkipWhile(z => z.ID <= this.ID).FirstOrDefault(); } 
		public TimeSpan getTmEnd() {return getNext().with(z => z.Tm, Tm.Add(TS.Dur));}
		public TimeSpan getTmBeg() { return Tm; }
		public TimeSpan getTmDur() { return getTmEnd() - Tm; }

		public void setPack(Pack pack)
		{
			this.pack = pack;
		}//function
	}//class
}//namespace
