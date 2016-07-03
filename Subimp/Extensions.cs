using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDB;

namespace Subimp
{
	public static class Extensions
	{
		public static int ToInt(this string s)
		{
			int result;
			if (int.TryParse(s, out result) == false) { return default(int); }//if
			return result;
		}//function

		public static TimeSpan Round(this TimeSpan ts) 
		{
			int ms = (ts.Milliseconds / 1000) * 1000;
			return new TimeSpan(ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ms);
		}//func

		public static string ToStr(this TimeSpan ts)
		{
			if (ts == TimeSpan.Zero)
			{
				return "__:__:__._";
			}//if

			string result = "{0:D2}:{1:D2}:{2:D2},{3:D1}".fmt(
				ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
			return result;
		}//func

		public static string ToStrSRT(this TimeSpan ts)
		{
			string result = "{0:D2}:{1:D2}:{2:D2},{3:D3}".fmt(
				ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
			return result;
		}//func

		public static string ToStrLYR(this TimeSpan ts)
		{
			string result = "{0:D2}:{1:D2}:{2:D2}".fmt(
				ts.Hours, ts.Minutes, ts.Seconds);
			return result;
		}//func

		public static string Serialize<T>(this T o) where T : class
		{
			var js = new System.Web.Script.Serialization.JavaScriptSerializer();
			string result = js.Serialize(o);
			return result;
		}//function

		public static T Deserialize<T>(this string json)
		{
			var js = new System.Web.Script.Serialization.JavaScriptSerializer();
			T result = js.Deserialize<T>(json);
			return result;
		}//function
	}//class
}//namespace
