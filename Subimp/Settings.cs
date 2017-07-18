using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDB;

namespace Subimp
{
	public class Singleton<T> where T : class, new()
	{
		//private Singleton() { }
		private static T instance;
		public static T Instance { get { return instance ?? (instance = new T()); } }
	}

	public class Settings
	{
		private static Settings instance;
		public static Settings Instance {
			get { return instance ?? (instance = new Settings()); }
			set { if (value != null) { instance = value; }  }
		} 
		
		public string DirOutSrt { get; set; } = @"C:\temp";
		public string DirOutLyr { get; set; } = @"C:\temp";
		public string Exclude { get; set; } = "exclude.txt";
		public int MinStringLength { get; set; } = 5;
		public int TitulOffset { get; set; } = 2;

		public override string ToString()
		{
			return Environment.NewLine.join(
			$"DirOutSrt={DirOutSrt}", 
			$"DirOutLyr={DirOutLyr}",
			$"Exclude={Exclude}",
			$"MinStringLength={MinStringLength}",
			$"TitulOffset={TitulOffset}");
		}
	}//class

	
}
