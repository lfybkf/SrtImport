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
		public string Content { get; set; } = string.Empty;
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

		public string toSrt() => ID.ToString().addLine($"{TmBeg.ToStrSRT()} --> {TmEnd.ToStrSRT()}", Content, string.Empty);
		public string toLyr() => $"[{TmBeg.ToStrLYR()}]{Content}";
		public bool HasFix => Ficks > 0;

		public void setPack(Pack pack) { this.pack = pack; }//function
		internal void Fix()	{	this.Ticks = this.Ficks; }
	}//class
}//namespace
