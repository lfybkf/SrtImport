using System.Collections.Generic;
using io = System.IO;
using BDB;

namespace Subimp
{
	public sealed class Fix
	{
		public string Content { get; set; }
		public long Ficks { get; set; }

		public static void Save(IEnumerable<Fix> items, string Name)
		{
			string json = items.Serialize();
			io.File.WriteAllText(Name + "_fix" + EXT.Json, json);
		}//function
	}//class
}
