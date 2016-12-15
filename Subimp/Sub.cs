using System;
using System.Linq;
using BDB;

namespace Subimp
{
	public class Sub
	{
		public int ID { get; set; }
		public long Ticks { get { return TmBeg.Ticks; } set { TmBeg = new TimeSpan(value); } }
		public long Ficks { get { return TmFix.Ticks; } set { TmFix = new TimeSpan(value); } }
		public string Content { get; set; }
		internal TimeSpan TmBeg;
		internal TimeSpan TmFix;
		
		Pack pack = null;

		public Sub()
		{
			ID = 0;
			TmFix = TS.Zero;
			Content = string.Empty;
		}//constructor

		public override string ToString()
		{
			return "{0:D3} | {1} {2} {3}".fmt(
				ID
				, TmBeg.ToStr()
				, TmFix.ToStr()
				, Content);
		}

		public static int MaxLength = 50;
		public void NormalizeContent()
		{
			if (Content.Contains(Environment.NewLine) && Content.Length > MaxLength)
			{
				;
			}//if
		}//function

		internal Sub Next { get { return pack.Items.SkipWhile(z => z.ID <= this.ID).FirstOrDefault(); } }
		internal TimeSpan TmDur { get { return TmEnd - TmBeg; } }
		internal TimeSpan TmEnd { 
			get {
				Sub next = Next;
				if (next == null)
				{
					return TmBeg.Add(TS.Dur);
				}//if
				else
				{
					TimeSpan result = next.TmBeg - TS.Delta;
					if (result - TmBeg > TS.Dur)
					{
						return TmBeg + TS.Dur;
					}//if
					else
					{
						return result;
					}//else
				}//else
			} }

		public  string toSrt()
		{
			return ID.ToString()
						.addLine(
						"{0} --> {1}".fmt(TmBeg.ToStrSRT(), TmEnd.ToStrSRT())
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
				return TmFix != TS.Zero;
			}
		}

		internal void Fix()
		{
			this.Ticks = this.Ficks;
		}
	}//class
}//namespace
