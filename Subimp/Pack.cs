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
			//public int Count;

			public override string ToString() => $"{beg.TmBeg.ToStrSRT()} ({beg.TmFix.ToStrSRT()}) -> {end.TmBeg.ToStrSRT()} ({end.TmFix.ToStrSRT()})";
		}//class

		public string Name {get;set;}
		List<Sub> list = new List<Sub>();
		public IEnumerable<Sub> Items { get { return list; } set { list.AddRange(value); } }
		public IEnumerable<Sub> ItemsFixed { get { return Items.Where(z => z.HasFix); } }
		public IEnumerable<Sub> ItemsUnFixed { get { return Items.Where(z => z.HasFix == false); } }

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
			pack.MergeFix();
			return pack;
		}//function

		internal void MergeFix()
		{
			var fixes = Fix.Load(Name);
			if (fixes.Any() == false)
			{
				return;
			}//if

			Fix fix;
			foreach (var sub in this.Items)
			{
				fix = fixes.FirstOrDefault(z => z.Content == sub.Content);
				if (fix != null) { sub.Ficks = fix.Ficks; }//if
			}//for
		}//function


		private static bool isGood(Sub sub)
		{
			if (sub == null) { return false; }
			if (sub.Content.Length <= Settings.Instance.MinStringLength) { return false; }
			if (!sub.Content.Any(ch => char.IsLetterOrDigit(ch))) { return false; }
			return true;
		}//function

		public void ImportSrt(IEnumerable<string> ss)
		{
			Sub sub = null;
			int Num;
			string sText;
			foreach (var s in ss.Where(z => z.notEmpty()))
			{
				if (int.TryParse(s, out Num)) //число - признак новой секции
				{
					if (isGood(sub))
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
						sText = s.Trim();
						//пустые строки игнорируем
						if (sText.isEmpty()) { continue; }
						//строки-примечания [dog barking] игнорируем
						if (sText.StartsWith(S.LSquare) && sText.EndsWith(S.RSquare)) { continue; }
						if (sText.StartsWith(S.LParenthesis) && sText.EndsWith(S.RParenthesis)) { continue; }
						//добавляем строку к контенту субтитра
						sub.Content = sub.Content.addLine(sText);
					}//else
				}//if
			}//for

			//last
			if (isGood(sub))
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

		public void Save()
		{
			string json = this.Serialize();
			io.File.WriteAllText(Name + EXT.Json, json);
		}//function

		public void SaveFix()
		{
			var fixes = this.Items.Where(z => z.Ficks > 0).Select(sub => new Fix { Content = sub.Content, Ficks = sub.Ficks });
			Fix.Save(fixes, this.Name);
		}//function

		internal void ExportSrt()
		{
			IEnumerable<string> output = Items.Select(z => z.toSrt());
			var path = io.Path.Combine(DIR.Srt, Name) + EXT.Srt;
			if (io.File.Exists(path))
			{
				string pathArh = io.Path.ChangeExtension(path, EXT.Arh);
				io.File.Delete(pathArh);
				io.File.Move(path, pathArh);
			}//if
			io.File.WriteAllLines(path, output);
		}

		internal void ExportLyr()
		{
			IEnumerable<string> output = Items.Select(z => z.toLyr());
			var path = io.Path.Combine(DIR.Lyr, Name) + EXT.Lyr;
			io.File.WriteAllLines(path, output);
		}


		private IEnumerable<Ival> getIvals()
		{
			Sub[] subs = Items.Where(z => z.Ficks > 0).ToArray();
			if (subs.Length <= 1) { return Enumerable.Empty<Ival>() ; }//if

			var subsBeg = subs.Take(subs.Length - 1);
			var subsEnd = subs.Skip(1);
			return subsBeg.Zip(subsEnd, (Beg, End) => new Ival { beg = Beg, end = End });
		}//function

		/// <summary>НЕвключительный список</summary>
		private IEnumerable<Sub> getSubs(Ival ival)
		{
			return Items.SkipWhile(z => z.ID <= ival.beg.ID).TakeWhile(z => z.ID < ival.end.ID);
		}//function

		internal void Retime()
		{
			Numerate();

			Ival[] ivals = getIvals().ToArray();

			foreach (var item in ivals)
			{
				Retime(item);
			}//for
			ivals.LastOrDefault().end.Fix();
		}//function

		void Retime(Ival ival)
		{
			long tiBegFix = ival.beg.Ficks;
			long tiBeg = ival.beg.Ticks;
			long tiEndFix = ival.end.Ficks;
			long tiEnd = ival.end.Ticks;
			double K = (double)(tiEndFix - tiBegFix) / (double)(tiEnd - tiBeg);

			IEnumerable<Sub> subs = getSubs(ival);
			long lenFromBeg;
			double lenFromBegFix;
			foreach (Sub sub in subs)
			{
				lenFromBeg = sub.Ticks - tiBeg;
				lenFromBegFix = lenFromBeg * K;
				sub.Ticks = tiBegFix + (long)lenFromBegFix;
			}//for

			//исправляем начало. конец не трогаем, он будет опорой для следующего интервала. конец последнего исправим в другом месте
			ival.beg.Fix();

		}//function

		public Sub Find(string s, Sub previous = null)
		{
			IEnumerable<Sub> listToFind = (previous == null) ? list : list.SkipWhile(z => !z.Equals(previous)).Skip(1);
			return listToFind.FirstOrDefault(z => z.Content.Contains(s));
		}//function

		internal void Remove(Sub sub)
		{
			list.Remove(sub);
			Numerate();
		}//function

		/// <summary>лучший кандидат на фикс</summary>
		public Sub BestCandidat()
		{
			if (!Items.First().HasFix)
			{	return Items.First();	}//if
			else if (!Items.Last().HasFix)
			{	return Items.Last(); }//else

			//по максимальному разрыву
			//список сабов с разрывом до следующего
			var diffs = Items.Take(Items.Count() - 1)
				.Zip(Items.Skip(1), (one, two) => new { sub = one, diff = two.Ticks - one.Ticks })
				.ToArray();
			//максимальный разрыв
			long max_diff = diffs.Where(z => z.sub.HasFix == false).Max(z => z.diff);
			return diffs.First(z => z.diff == max_diff).sub;

			/** поиск по середине
			var ivals = getIvals().ToArray();
			//считаем длины интервалов
			ivals.forEach(z => z.Count = getSubs(z).Count());
			//считаем максимальную длину
			int max = ivals.Max(z => z.Count);
			//ищем интервал с макс. длиной
			var ivalWithMax = ivals.First(z => z.Count == max);
			//ищем саб в середине этого интервала
			return getSubs(ivalWithMax).Skip(max / 2).First();
			*/
		}//function

		public Sub FindOnContent(string content)
		{
			return Items.FirstOrDefault(z => z.Content == content);
		}//function
	}//class
}//namespace
