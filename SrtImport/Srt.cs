using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BDB;

namespace SrtImport
{
	public class Ival
	{
		public int Beg;
		public int End;

		public Ival(int beg, int end)
		{
			Beg = beg;
			End = end;
		}

		public bool IsSimple
		{
			get { return (End == Beg + 1); }
		}//func
	}//struct

	public class Srt
	{
		enum Section {Period, Content, End }
		public class FLD
		{
			public static readonly string Id = "Id";
			public static readonly string TmBeg = "TmBeg";
			public static readonly string TmEnd = "TmEnd";
			public static readonly string Dur = "Dur";
			public static readonly string Content = "Content";
			public static readonly string Tm = "Tm";
		}//class

		public class SRV
		{
			public static Encoding enc = Encoding.GetEncoding(1251);
			public static readonly string Delim = "-->";
			public static readonly string Zpt = ",";
			public static readonly string Point = ".";
			public static readonly string Space = " ";
			public static readonly string FilterSrt = "Subtitles (*.srt)|*.srt";
			public static readonly string FilterXml = "XmlSubtitles (*.xml)|*.xml";
			public static readonly string FmtGrid = "hh:mm:ss";
			public static readonly string FmtTmSrt = "{0}:{1}:{2},{3}";
			public static readonly string FmtTmLyr = "{0}:{1}.{2}";
			public static readonly string FmtDurSrt = "{0}:{1}";

			public static readonly string ExtXml = ".xml";
			public static readonly string ExtSrt = ".srt";
			public static readonly string ExtLyr = ".lrc";

			public static string DirSrt = @"C:\Subtitles";
			public static string DirLyr = @"C:\Subtitles";
		}//class

		DataTable dt = new DataTable("Srt");
		DataSet ds = new DataSet("Movie");
		string Name;

		public static string SrtTS(TimeSpan ts)
		{
			return SRV.FmtTmSrt.fmt(
				ts.Hours.ToString("D2")
				, ts.Minutes.ToString("D2")
				, ts.Seconds.ToString("D2")
				, ts.Milliseconds.ToString("D3"));
		}//func

		public static string LyrTS(TimeSpan ts)
		{
			return string.Format(SRV.FmtTmLyr
				, ts.Minutes.ToString("D2")
				, ts.Seconds.ToString("D2")
				, (ts.Milliseconds/10).ToString("D2"));
		}//func


		public static string SrtDur(TimeSpan ts)
		{
			return string.Format(SRV.FmtTmSrt
				, ts.Minutes.ToString("D2")
				, ts.Seconds.ToString("D2")
				);
		}//func

		public static DateTime Convert(TimeSpan ts)
		{
			DateTime dt = new DateTime(2000,1,1);
			return dt + ts;
		}//func

		public static TimeSpan Convert(DateTime dt)
		{
			TimeSpan Ret = dt - new DateTime(2000, 1, 1);
			return Ret;
		}//func

		
		public void SetName(string FileName)
		{
			Name = Path.GetFileName(FileName);
			Name = Name.Substring(0, Name.Length - 4);
		}//func

		public string FileName 
		{ 
			get 
			{
				string s = Path.Combine(Environment.CurrentDirectory, Name);
				return s + SRV.ExtXml;
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
			ds.WriteXml(FileName, XmlWriteMode.WriteSchema);
		}//func

		public void Load(string File)
		{
			SetName(File);
			ds.Clear();
			ds.ReadXml(FileName, XmlReadMode.ReadSchema);
			dt.AcceptChanges();
		}//func

		public void Import(string File)
		{
			SetName(File);
			using (TextReader tr = new StreamReader(File, SRV.enc))
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

		public void Show(DataGridView grid)
		{
			grid.DataSource = dt;
			grid.Columns[FLD.Id].Width = 50;
			grid.Columns[FLD.Content].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

			foreach (DataGridViewColumn item in grid.Columns)
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

			IdStart = (from == null) ? 0 : from.End;

			foreach (DataRow dr in dt.Rows)
			{
				if ((int)dr[FLD.Id] < IdStart)
					continue;

				if (IdBeg == 0 && dr[FLD.Tm] != DBNull.Value)
				{
					IdBeg = (int)dr[FLD.Id];
					continue;
				}//if

				if (IdBeg > 0 && dr[FLD.Tm] != DBNull.Value)
				{
					IdEnd = (int)dr[FLD.Id];
					break;
				}//if
			}//for

			Ret = (IdBeg > 0 && IdEnd > 0) ? new Ival(IdBeg, IdEnd) : Ret;
			return Ret;
		}//func

		void SetBounds()
		{
			TimeSpan Tm, TmDur;
			foreach (DataRow dr in dt.Rows)
			{
				if (dr[FLD.Tm] == DBNull.Value)
					continue;
				Tm =	(TimeSpan)dr[FLD.Tm];
				TmDur = SelectDuration(dr);
				dr[FLD.TmBeg] = Tm;
				dr[FLD.TmEnd] = Tm + TmDur;
			}//for
		}//func

		void Retime(Ival ival)
		{
			if (ival.IsSimple)
				return;

			TimeSpan TmEndNeed = (TimeSpan)dt.Rows.Find(ival.End)[FLD.Tm];
			TimeSpan TmBegNeed = (TimeSpan)dt.Rows.Find(ival.Beg)[FLD.Tm];
			TimeSpan TmEndReal = (TimeSpan)dt.Rows.Find(ival.End)[FLD.TmBeg];
			TimeSpan TmBegReal = (TimeSpan)dt.Rows.Find(ival.Beg)[FLD.TmBeg];
			TimeSpan TmDiffNeed, TmDiffReal, TmBeg, TmDur;
			long Need = (TmEndNeed - TmBegNeed).Ticks;
			long Real = (TmEndReal - TmBegReal).Ticks;

			float K = (float)Need/(float)Real;

			foreach (DataRow dr in dt.Rows)
			{
				if ((int)dr[FLD.Id] <= ival.Beg)
					continue;
				if ((int)dr[FLD.Id] >= ival.End)
					break;

				TmBeg = (TimeSpan)dr[FLD.TmBeg];
				TmDur = SelectDuration(dr);

				TmDiffReal = TmBeg - TmBegReal;
				TmDiffNeed = new TimeSpan((long)(TmDiffReal.Ticks * K));
				dr[FLD.TmBeg] = TmBegNeed + TmDiffNeed;
				UpdateDuration(dr, TmDur);

			}//for
		}//func

		static void UpdateDuration(DataRow dr, TimeSpan TmDur)
		{
			dr[FLD.TmEnd] = (TimeSpan)dr[FLD.TmBeg] + TmDur;
		}//func

		static TimeSpan SelectDuration(DataRow dr)
		{
			return (TimeSpan)dr[FLD.TmEnd] - (TimeSpan)dr[FLD.TmBeg];
		}//func

		public void Delete(int aId)
		{
			DataRow dr = dt.Rows.Find(aId);
			if (dr != null)
				dt.Rows.Remove(dr);
		}//func

		public void Retime()
		{
			Ival ival = null;
			while ((ival = Find(ival))!= null)
			{
				Retime(ival);
			}//while
			SetBounds();
		}//func

		public void ExportSrt(string Folder)
		{
			string path = Folder.addToPath(Name) + SRV.ExtSrt;
			StringBuilder sb = new StringBuilder();
			foreach (DataRow dr in dt.Rows)
			{
				sb.AppendLine(dr[FLD.Id].ToString());
				sb.AppendFormat("{0} {1} {2}",
					(SrtTS((TimeSpan)dr[FLD.TmBeg]))
					, SRV.Delim
					, SrtTS((TimeSpan)dr[FLD.TmEnd])
					);
				sb.AppendLine(string.Empty);
				sb.AppendLine(dr[FLD.Content].ToString());
				sb.AppendLine(string.Empty);
			}//for

			File.WriteAllText(path, sb.ToString(), SRV.enc);
		}//func

		public void ExportLyr(string Folder)
		{
			string path = Path.Combine(Folder, Name) + SRV.ExtLyr;
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("[ti:{0}]".fmt(Name));
			sb.AppendLine("[ar:none]");
			sb.AppendLine("[al:none]");
			sb.AppendLine("[la:en]");
			sb.AppendLine("[by:BDB]");
			foreach (DataRow dr in dt.Rows)
			{
				sb.AppendFormat("[{0}]{1}",
					(LyrTS((TimeSpan)dr[FLD.TmBeg]))
					, LyrStr((string)dr[FLD.Content]));
				sb.AppendLine(string.Empty);
			}//for
			File.WriteAllText(path, sb.ToString(), SRV.enc);
		}//func

		public void Export(string Folder)
		{
			ExportSrt(Folder);
			if (File.Exists(Folder.addToPath("make_lyr_also.txt")))
				ExportLyr(Folder);
		}//func

		static string LyrStr(string s)
		{
			return s.Replace(Environment.NewLine, SRV.Space);
		}//func

		public bool IsChanged
		{
			get
			{
				return dt.AsEnumerable().Any(dr => dr.RowState == DataRowState.Modified);
			}
		}//func
	}//class
}//ns
