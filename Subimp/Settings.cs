using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDB;

namespace Subimp
{
	public class Settings
	{
		public static Settings Instance;

		public string DirOutSrt { get; set; } = @"C:\temp";
		public string DirOutLyr { get; set; } = @"C:\temp";
		public int MinStringLength { get; set; } = 5;
	}//class

	
}
