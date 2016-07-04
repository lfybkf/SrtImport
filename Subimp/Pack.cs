using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using io = System.IO;
using BDB;

namespace Subimp
{
	public class Pack
	{
		class Ival
		{
			public Sub beg;
			public Sub end;
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
		
		public void AfterWork()
		{
			Sub sub;
			for (int i = 0; i < list.Count(); i++)
			{
				sub = list.ElementAt(i); 
				sub.ID = i + 1;
				sub.setPack(this);
			}//for
		}//function

		public static Pack Load(string path)
		{
			Pack pack;
			string json = io.File.ReadAllText(path);
			pack = json.Deserialize<Pack>();
			pack.AfterWork();
			return pack;
		}//function

		public void ImportSrt(IEnumerable<string> ss)
		{
			Sub item = null;
			int Num;
			foreach (var s in ss.Where(z => z.Any()))
			{
				if (int.TryParse(s, out Num)) //число - признак новой секции
				{
					if (item != null)
					{
						list.Add(item);
					}//if

					item = new Sub();
					continue;
				}//if
				if (item != null)
				{
					if (s.Contains(STR.Arrow))
					{
						string parse = s.before(STR.Arrow).Trim().Replace(STR.Zpt, STR.Point);
						item.Ticks = TimeSpan.Parse(parse).Ticks;
					}//if
					else
					{
						item.Content = item.Content.Any() ? item.Content.addLine(s) : s;
					}//else
				}//if
			}//for

			this.AfterWork();
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
			//create ivals
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
		}//function

		void Retime(Ival ival)
		{
			long tiBegFix = ival.beg.Ficks;
			long tiBeg = ival.beg.Ticks;
			long tiEndFix = ival.end.Ficks;
			long tiEnd = ival.end.Ticks;

			float K = (tiEndFix - tiBegFix) / (tiEnd - tiBeg);


		}//function


		public  Sub Find(string s, Sub previous = null)
		{
			IEnumerable<Sub> listToFind = (previous == null) ? list : list.SkipWhile(z => !z.Equals(previous)).Skip(1);
			return listToFind.FirstOrDefault(z => z.Content.Contains(s));
		}//function

		internal void Remove(Sub sub)
		{
			list.Remove(sub);
		}
	}//class
}//namespace
