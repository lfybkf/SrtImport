using System.Collections.Generic;
using io = System.IO;
using BDB;
using System.Linq;

namespace Subimp
{
	public sealed class Fix
	{
		public string Content { get; set; }
		public long Ficks { get; set; }

		public static void Save(IEnumerable<Fix> items, string packName)
		{
			if (items.Any())
			{
				string json = items.Serialize();
				io.File.WriteAllText(FileName(packName), json);
			}//if
		}//function


		public static IEnumerable<Fix> Load(string packName)
		{
			string fileName = FileName(packName);
			if (io.File.Exists(fileName))
			{
				string json = io.File.ReadAllText(fileName);
				return json.Deserialize<IEnumerable<Fix>>();
			}//if
			else
			{
				return Enumerable.Empty<Fix>();
			}//else
		}//function

		public static string FileName(string packName)
		{
			return packName + "_fix" + EXT.Json;
		}//function
	}//class
}
