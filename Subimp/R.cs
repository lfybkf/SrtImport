using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subimp
{
	public static class TS
	{
		public static readonly TimeSpan Dur = new TimeSpan(0, 0, 0, 5);
		public static readonly TimeSpan Delta = new TimeSpan(0, 0, 0, 0, 200);
		public static readonly TimeSpan Zero = TimeSpan.Zero;
	}//class

	public static class zFMT
	{
		public static readonly string SrtTm = "{0}:{1}:{2},{3}";
		public static readonly string SrtDur = "{0}:{1}";
		public static readonly string LyrTm = "{0}:{1}.{2}";
		public static readonly string LyrPar = "[{0}:{1}]";
	}//class

	public static class STR
	{
		public static readonly string Arrow = "-->";
	}//class

	public static class EXT
	{
		public static readonly string Json = ".json";
		public static readonly string Xml = ".xml";
		public static readonly string Srt = ".srt";
		public static readonly string Lyr = ".lrc";
		public static readonly string Trn = ".trn";
		public static readonly string Arh = ".arh";
	}//class

	public static class FILTER
	{
		public static readonly string Lyr = "Lyrics (*.lrc)|*.lrc";
		public static readonly string Srt = "Subtitles (*.srt)|*.srt";
		public static readonly string Xml = "XmlSubtitles (*.xml)|*.xml";
	}//class

	public static class DIR
	{
		public static string Srt => Settings.Instance.DirOutSrt;
		public static string Lyr => Settings.Instance.DirOutLyr;
		public static string Cur = Environment.CurrentDirectory;
	}//class

}//namespace
