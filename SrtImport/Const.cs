using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtImport
{
	public class FLD
	{
		public static readonly string ID = "Id";
		public static readonly string TmBeg = "TmBeg";
		public static readonly string TmEnd = "TmEnd";
		public static readonly string Content = "Content";
		public static readonly string Tm = "Tm";
	}//class

	public static class SRV
	{
		public static Encoding encoding = Encoding.GetEncoding(1251);
		//public static TimeSpan TmDurMin = new TimeSpan(0,0,5);
		public static readonly string Delim = "-->";
		public static readonly string Zpt = ",";
		public static readonly string Point = ".";
		public static readonly string Space = " ";

		public static string none = "none";
		public static string Key = "Key";
		public static string Value = "Value";
	}//class

	public static class DELIM
	{
		public static readonly string Xml = ".xml";
	}//class

	public static class EXT
	{
		public static readonly string Xml = ".xml";
		public static readonly string Srt = ".srt";
		public static readonly string Lyr = ".lrc";
		public static readonly string Trn = ".trn";
	}//class

	public static class FMT
	{
		public static readonly string Grid = "hh:mm:ss";
		public static readonly string SrtTm = "{0}:{1}:{2},{3}";
		public static readonly string SrtDur = "{0}:{1}";
		public static readonly string LyrTm = "{0}:{1}.{2}";
		public static readonly string LyrPar = "[{0}:{1}]";
	}//class

	public static class FILTER
	{
		public static readonly string Lyr = "Lyrics (*.lrc)|*.lrc";
		public static readonly string Srt = "Subtitles (*.srt)|*.srt";
		public static readonly string Xml = "XmlSubtitles (*.xml)|*.xml";
	}//class

	public static class DIR
	{
		public static string Srt = @"C:\Temp";
		public static string Lyr = @"C:\Temp";
		public static string Cur = Environment.CurrentDirectory;
	}//class

}//ns
