using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BDB;

namespace SrtImport
{

	public class Srt
	{
		enum Section {Period, Content, End }

		public class Ival
		{
			public int BegID;
			public int EndID;

			public Ival(int beg, int end) { BegID = beg; EndID = end; }
			public bool IsSimple { get { return (EndID == BegID + 1); } }
		}//struct

		DataTable dt = new DataTable("Srt");
		DataSet ds = new DataSet("Movie");
		string Name;
		Dictionary<string, string> param = new Dictionary<string, string>();
		static readonly string[] LyrPar = { "ti", "ar", "al"};

		private static string SrtDur(TimeSpan ts)
		{
			return FMT.SrtTm.fmt(ts.Minutes.ToString("D2"), ts.Seconds.ToString("D2"));
		}//func

		private static TimeSpan Convert(DateTime dt)
		{
			TimeSpan Ret = dt - new DateTime(2000, 1, 1);
			return Ret;
		}//func

		
		public void SetName(string FileName)
		{
			Name = Path.GetFileName(FileName);
			Name = Name.Substring(0, Name.Length - 4);
		}//func

		public string FileXml 
		{ 
			get 
			{
				string s = Path.Combine(Environment.CurrentDirectory, Name);
				return s + EXT.Xml;
			}
		}//prop

		public string FileTrn
		{
			get
			{
				string s = Path.Combine(Environment.CurrentDirectory, Name);
				return s + EXT.Trn;
			}
		}//prop

		public Srt()
		{
			dt.Columns.Add(FLD.Id, typeof(int));
			dt.Columns.Add(FLD.TmBeg, typeof(TimeSpan));
			dt.Columns.Add(FLD.TmEnd, typeof(TimeSpan));
			dt.Columns.Add(FLD.Tm, typeof(TimeSpan));
			dt.Columns.Add(FLD.Content, typeof(string));
			dt.Columns[FLD.Id].AutoIncrement = true;
			dt.Columns[FLD.Id].AutoIncrementSeed = 1;
			dt.Columns[FLD.Tm].AllowDBNull = true;
			dt.SetPK(FLD.Id);
			
			ds.Tables.Add(dt);
		}//func

		public void Save()
		{
			dt.AcceptChanges();
			ds.WriteXml(FileXml, XmlWriteMode.WriteSchema);
		}//func

		public void Load(string File)
		{
			SetName(File);
			ds.Clear();
			ds.ReadXml(FileXml, XmlReadMode.ReadSchema);
			dt.AcceptChanges();
		}//func

		public void Import(string path)
		{
			SetName(path);
			if (path.EndsWith(EXT.Srt))
				ImportSrt(path);
			else if (path.EndsWith(EXT.Lyr))
				ImportLyr(path);
		}

		private void ImportLyr(string path)
		{
			using (TextReader tr = new StreamReader(path, SRV.encoding))
			{
				string s, Value, Min, Sec;
				TimeSpan TmBeg, TmEnd;
				TimeSpan TmDuration = new TimeSpan(0, 0, 5);
				DataRow dr = null;
				Regex rexPar = new Regex(@"\[(?<Key>\D+):(?<Value>\D+)\].*");
				Regex rexOne = new Regex(@"\[(?<Min>[0-9]{2}):(?<Sec>[0-9]{2})\.[0-9]{2}\](?<Value>.+)");
				Match m = null;
				while ((s = tr.ReadLine()) != null)
				{
					if (string.IsNullOrWhiteSpace(s))
						continue;

					if (rexPar.IsMatch(s))
					{
						m = rexPar.Match(s);
						param[m.Groups[SRV.Key].Value] = m.Groups[SRV.Value].Value;
					}//if
					else if (rexOne.IsMatch(s))
					{
						m = rexOne.Match(s);
						Value = m.Groups[SRV.Value].Value;
						Min = m.Groups["Min"].Value;
						Sec = m.Groups["Sec"].Value;
						TmBeg = new TimeSpan(0, Min.ToInt(), Sec.ToInt());
						TmEnd = TmBeg.Add(TmDuration);
						
						dr = dt.NewRow();
						dr[FLD.Content] = Value;
						dr[FLD.TmBeg] = TmBeg;
						dr[FLD.TmEnd] = TmEnd;
						dt.Rows.Add(dr);
					}//if

				}//while
			}//using
		}//function

		public void ImportSrt(string File)
		{
			using (TextReader tr = new StreamReader(File, SRV.encoding))
			{
				string s;
				int Num;
				TimeSpan TmBeg = TimeSpan.Zero;
				TimeSpan TmEnd = TimeSpan.Zero;
				DataRow dr = null;
				Section sect = Section.End;
				while ( (s = tr.ReadLine()) != null)
				{
					if (string.IsNullOrWhiteSpace(s))
						continue;

					if (int.TryParse(s, out Num))
					{
						if (dr != null)
							dt.Rows.Add(dr); //ранее созданный

						dr = dt.NewRow();
						dr[FLD.Content] = DBNull.Value;
						dr[FLD.Tm] = DBNull.Value;
						sect = Section.Period;
						continue;
					}//if

					if (sect == Section.Content)
					{
						dr[FLD.Content] = (dr[FLD.Content] != DBNull.Value) ?
							(string)dr[FLD.Content] + Environment.NewLine + s : s;
					}//if

					if (sect == Section.Period)
					{
						sect = Section.Content;
						ParsePeriod(s, out TmBeg, out TmEnd);
						dr[FLD.TmBeg] = TmBeg;
						dr[FLD.TmEnd] = TmEnd;
					}//if
				}//while
			}//using
		}//func

		static void ParsePeriod(string s, out TimeSpan TmBeg, out TimeSpan TmEnd)
		{
			string sBeg, sEnd;
			int i = s.IndexOf(SRV.Delim);
			sBeg = s.Substring(0, i).Trim().Replace(SRV.Zpt, SRV.Point);
			sEnd = s.Substring(i + SRV.Delim.Length).Trim().Replace(SRV.Zpt, SRV.Point);
			TmBeg = TimeSpan.Parse(sBeg);
			TmEnd = TimeSpan.Parse(sEnd);
		}//func

		public void Show(System.Windows.Forms.DataGridView grid)
		{
			grid.DataSource = dt;
			grid.Columns[FLD.Id].Width = 50;
			grid.Columns[FLD.Content].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;

			foreach (System.Windows.Forms.DataGridViewColumn item in grid.Columns)
			{
				if (item.ValueType == typeof(TimeSpan))
				{
					item.Width = 120;
				}//if
			}//for
		}//func

		public Ival Find(Ival from)
		{
			Ival Ret = null;
			int IdStart, IdBeg = 0, IdEnd = 0;

			IdStart = (from == null) ? 0 : from.EndID;

			foreach (DataRow dr in dt.Rows)
			{
				if (dr.ID() < IdStart)
					continue;

				if (IdBeg == 0 && dr[FLD.Tm] != DBNull.Value)
				{
					IdBeg = dr.ID();
					continue;
				}//if

				if (IdBeg > 0 && dr[FLD.Tm] != DBNull.Value)
				{
					IdEnd = dr.ID();
					break;
				}//if
			}//for

			Ret = (IdBeg > 0 && IdEnd > 0) ? new Ival(IdBeg, IdEnd) : Ret;
			return Ret;
		}//func

		void Retime(Ival ival)
		{
			if (ival.IsSimple)
				return;

			TimeSpan TmEndNeed = dt.Rows.Find(ival.EndID).Tm();
			TimeSpan TmBegNeed = dt.Rows.Find(ival.BegID).Tm();
			TimeSpan TmEndReal = dt.Rows.Find(ival.EndID).TmBeg();
			TimeSpan TmBegReal = dt.Rows.Find(ival.BegID).TmBeg();
			TimeSpan TmDiffNeed, TmDiffReal, TmBeg, TmDur;
			long Need = (TmEndNeed - TmBegNeed).Ticks;
			long Real = (TmEndReal - TmBegReal).Ticks;

			float K = (float)Need/(float)Real;

			foreach (DataRow dr in dt.Rows)
			{
				if (dr.ID() <= ival.BegID)
					continue;
				if (dr.ID() >= ival.EndID)
					break;

				TmBeg = dr.TmBeg();
				TmDur = dr.TmDur();

				TmDiffReal = TmBeg - TmBegReal;
				TmDiffNeed = new TimeSpan((long)(TmDiffReal.Ticks * K));
				dr[FLD.TmBeg] = (TmBegNeed + TmDiffNeed).RoundToSec();
				UpdateDuration(dr, TmDur);

			}//for
		}//func


		public void Retime()
		{
			Ival ival = null;
			while ((ival = Find(ival))!= null)
			{
				Retime(ival);
			}//while
			#region Align to Tm
			TimeSpan Tm, TmDur;
			var rows = dt.AsEnumerable().Where(r => r[FLD.Tm] != DBNull.Value);
			foreach (DataRow dr in rows)
			{
				Tm = dr.Tm();
				TmDur = dr.TmDur();
				dr[FLD.TmBeg] = Tm;
				dr[FLD.TmEnd] = (Tm + TmDur).RoundToSec();
			}//for
			#endregion

			#region AlignTmEnd
			DataRow drCurrent, drNext;
			for (int i = 0; i < dt.Rows.Count - 1; i++)
			{
				drCurrent = dt.Rows[i];
				drNext = dt.Rows[i + 1];
				if (drCurrent.TmEnd() > drNext.TmBeg()) { drCurrent[FLD.TmEnd] = drNext.TmBeg(); }//if
			}//for
			#endregion
		}//func

		private void ExportSrt(string Folder)
		{
			string path = Folder.addToPath(Name) + EXT.Srt;
			string sAll;
			StringBuilder sb = new StringBuilder();
			foreach (DataRow dr in dt.Rows)
			{
				sb.AppendLine(dr[FLD.Id].ToString());
				sb.AppendFormat("{0} {1} {2}",
					(dr.TmBeg().ToSrt())
					, SRV.Delim
					, dr.TmEnd().ToSrt()
					);
				sb.AppendLine(string.Empty);
				sb.AppendLine(dr.Content() + SRV.Space);
				sb.AppendLine(string.Empty);
			}//for
			sAll = sb.ToString();

			#region Trn
			if (File.Exists(FileTrn))
			{
				string line;
				string D = " - ";
				string sRus, sEng;
				int Didx = 0, Dlen = D.Length;
				Dictionary<string, string> Trn = new Dictionary<string, string>();
				using (StreamReader sr = new StreamReader(FileTrn, SRV.encoding))
				{
					while ((line = sr.ReadLine()) != null)
					{
						Didx = line.IndexOf(D);
						sRus = line.Substring(Didx + Dlen).ToLower();
						if (sRus.Length <= 12) //длинные переводы не влезут в строку субтитров
						{
							sEng = line.Substring(0, Didx).ToLower();
							Trn.Add(sEng, sRus);	
						}//if
					}//while
				}//using

				string withTrn = "{0}({1}) ";
				foreach (var key in Trn.Keys)
				{
					sEng = key + SRV.Space; //чтобы заменять не "word", а "word ". Иначе будут проблемы с "wordish"
					sRus = withTrn.fmt(key, Trn[key]);
					sAll = sAll.Replace(sEng, sRus);
				}//for

			}//if

			#endregion

			File.WriteAllText(path, sAll, SRV.encoding);
		}//func

		internal void ExportLyr(string Folder)
		{
			string path = Path.Combine(Folder, Name) + EXT.Lyr;
			StringBuilder sb = new StringBuilder();
			foreach (var s in LyrPar)
			{
				sb.AppendLine(FMT.LyrPar.fmt(s, param.ContainsKey(s) ? param[s] : Name ));	
			}//for
			sb.AppendLine("[la:en]");
			sb.AppendLine("[by:BDB]");
			foreach (DataRow dr in dt.Rows)
			{
				sb.AppendFormat("[{0}]{1}", dr.TmBeg().ToLyr(), LyrStr(dr.Content()));
				sb.AppendLine(string.Empty);
			}//for
			File.WriteAllText(path, sb.ToString(), SRV.encoding);
		}//func

		public void Export(string Folder)
		{
			ExportSrt(Folder);
			ExportLyr(Folder);	
		}//func

		static string LyrStr(string s)	{	return s.Replace(Environment.NewLine, SRV.Space);	}//func
		public bool IsChanged	{	get	{	return dt.AsEnumerable().Any(dr => dr.RowState == DataRowState.Modified);	}	}
		static void UpdateDuration(DataRow dr, TimeSpan TmDur) { dr[FLD.TmEnd] = dr.TmBeg() + TmDur; }//func
		public void Delete(int aId) { dt.Rows.Find(aId).execute(dr => dt.Rows.Remove(dr)); }//func

	}//class

	public static class LocalExtension
	{
		public static int ToInt(this string s)
		{
			int result;
			if (int.TryParse(s, out result) == false)			{				return default(int);			}//if
			return result;
		}//function

		public static TimeSpan RoundToSec(this TimeSpan ts)		{	return new TimeSpan(ts.Hours, ts.Minutes, ts.Seconds);	}//func
		private static DateTime ToDateTime(this TimeSpan ts)		{	return new DateTime(2000, 1, 1).Add(ts);	}//func

		public static string ToSrt(this TimeSpan ts)
		{
			return FMT.SrtTm.fmt(
				ts.Hours.ToString("D2")
				, ts.Minutes.ToString("D2")
				, ts.Seconds.ToString("D2")
				, ts.Milliseconds.ToString("D3"));
		}//func

		public static string ToLyr(this TimeSpan ts)
		{
			return FMT.LyrTm.fmt(
				ts.Minutes.ToString("D2")
				, ts.Seconds.ToString("D2")
				, (ts.Milliseconds / 10).ToString("D2"));
		}//func

		public static TimeSpan TmBeg(this DataRow dr)	{	return dr.Field<TimeSpan>(FLD.TmBeg);	}
		public static TimeSpan TmEnd(this DataRow dr) { return dr.Field<TimeSpan>(FLD.TmEnd); }
		public static TimeSpan Tm(this DataRow dr) { return dr.Field<TimeSpan>(FLD.Tm); }
		public static TimeSpan TmDur(this DataRow dr) { return dr.TmEnd() - dr.TmBeg(); }
		public static int ID(this DataRow dr) { return dr.Field<int>(FLD.Id); }
		public static string Content(this DataRow dr) { return dr.Field<string>(FLD.Content); }

	}//class
}//ns
