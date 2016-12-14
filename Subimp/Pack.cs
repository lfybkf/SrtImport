using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using io = System.IO;
using BDB;
using System.Text.RegularExpressions;

namespace Subimp
{
	public class Pack
	{
		class Ival
		{
			public Sub beg;
			public Sub end;

			public override string ToString()
			{
				return "{0} ({1}) -> {2} ({3})".fmt(beg.TmBeg.ToStrSRT(), beg.TmFix.ToStrSRT(), end.TmBeg.ToStrSRT(), end.TmFix.ToStrSRT());
			}
		}//class

		public string Name {get;set;}
		List<Sub> list = new List<Sub>();
		public IEnumerable<Sub> Items { get { return list; } set { list.AddRange(value); } }

		public Pack()
		{
		}//constructor

		public Pack(string Name)
		{
			this.Name = Name;
		}//constructor
		
		public void NumerateAndPack()
		{
			Sub sub;
			for (int i = 0; i < list.Count(); i++)
			{
				sub = list.ElementAt(i); 
				sub.ID = i + 1;
				sub.setPack(this);
			}//for
		}//function

		public void Numerate()
		{
			Sub sub;
			for (int i = 0; i < list.Count(); i++)
			{
				sub = list.ElementAt(i);
				sub.ID = i + 1;
			}//for
		}//function

		public static Pack Load(string path)
		{
			Pack pack;
			string json = io.File.ReadAllText(path);
			pack = json.Deserialize<Pack>();
			pack.NumerateAndPack();
			return pack;
		}//function

		public void ImportSrt(IEnumerable<string> ss)
		{
			Sub sub = null;
			int Num;
			foreach (var s in ss.Where(z => z.notEmpty()))
			{
				if (int.TryParse(s, out Num)) //число - признак новой секции
				{
					if (sub != null)
					{
						list.Add(sub);
					}//if

					sub = new Sub();
					continue;
				}//if
				if (sub != null)
				{
					if (s.Contains(STR.Arrow))
					{
						string parse = s.before(STR.Arrow).Trim().Replace(S.Comma, S.Point);
						sub.Ticks = TimeSpan.Parse(parse).Ticks;
					}//if
					else
					{
						sub.Content = sub.Content.notEmpty() ? sub.Content.addLine(s) : s;
					}//else
				}//if
			}//for

			//last
			if (sub.Content.notEmpty())
			{
				list.Add(sub);
			}//if

			this.NumerateAndPack();
		}//function

		public void ImportLyr(IEnumerable<string> ss)
		{
			Sub sub = null;
			Regex rexOne = new Regex(@"\[(?<Min>[0-9]{2}):(?<Sec>[0-9]{2})\.[0-9]{2}\](?<Content>.+)");
			Match m = null;
			string Min, Sec;
			foreach (var s in ss)
			{
				if (string.IsNullOrWhiteSpace(s))
					continue;

				if (rexOne.IsMatch(s))
				{
					m = rexOne.Match(s);
					sub = new Sub();
					sub.Content = m.Groups[W.Content].Value;
					Min = m.Groups[W.Min].Value;
					Sec = m.Groups[W.Sec].Value;
					sub.TmBeg = new TimeSpan(0, Min.ToInt(), Sec.ToInt());
					list.Add(sub);
				}//if
			}//for

			this.NumerateAndPack();
		}//function

		public  void Save()
		{
			string json = this.Serialize();
			io.File.WriteAllText(Name + EXT.Json, json);
		}//function

		internal void ExportSrt()
		{
			IEnumerable<string> output = Items.Select(z => z.toSrt());
			var path = io.Path.Combine(DIR.Srt, Name) + EXT.Srt;
			io.File.WriteAllLines(path, output);
		}

		internal void ExportLyr()
		{
			IEnumerable<string> output = Items.Select(z => z.toLyr());
			var path = io.Path.Combine(DIR.Lyr, Name) + EXT.Lyr;
			io.File.WriteAllLines(path, output);
		}

		internal void Retime()
		{
			Numerate();

			#region create ivals
			List<Ival> ivals = new List<Ival>();
			Ival ival = null;
			Sub sub = null;
			for (int i = 0; i < list.Count; i++)
			{
				sub = list[i];
				if (sub.IsIvalable && ival == null)
				{
					ival = new Ival { beg = sub };
				}//if
				else if (sub.IsIvalable && ival != null)
				{
					ival.end = sub;
					ivals.Add(ival);
					ival = null;
				}//if
			}//for
			#endregion

			foreach (var item in ivals)
			{
				Retime(item);
			}
		}//function

		void Retime(Ival ival)
		{
			long tiBegFix = ival.beg.Ficks;
			long tiBeg = ival.beg.Ticks;
			long tiEndFix = ival.end.Ficks;
			long tiEnd = ival.end.Ticks;

			double K = (double)(tiEndFix - tiBegFix) / (double)(tiEnd - tiBeg);

			Sub sub=null;
			long lenFromBeg;
			double lenFromBegFix;
			for (int i = 0; i < list.Count; i++)
			{
				sub = list[i];
				if (sub.ID < ival.beg.ID) { continue; }
				if (sub.ID > ival.end.ID) { break; }
				if (sub.ID == ival.end.ID || sub.ID == ival.beg.ID) { sub.Ticks = sub.Ficks; continue; }

				lenFromBeg = sub.Ticks - tiBeg;
				lenFromBegFix = lenFromBeg * K;
				sub.Ticks = tiBegFix + (long)lenFromBegFix;
			}//for
		}//function


		public  Sub Find(string s, Sub previous = null)
		{
			IEnumerable<Sub> listToFind = (previous == null) ? list : list.SkipWhile(z => !z.Equals(previous)).Skip(1);
			return listToFind.FirstOrDefault(z => z.Content.Contains(s));
		}//function

		internal void Remove(Sub sub)
		{
			list.Remove(sub);
			Numerate();
		}
	}//class
}//namespace
