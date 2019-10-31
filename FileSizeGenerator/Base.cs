using System;
using System.IO;
using System.Text;

namespace FileSizeGenerator
{
	internal class Base
	{
		public static readonly string nameMode = $"{DateTime.Now:yyyyMMddhhmmss}";
		public static readonly string headerFile = $"header {nameMode}.xml";
		public static readonly string bodyFile = $"body {nameMode}.xml";
		public static readonly string footerFile = $"footer {nameMode}.xml";

		internal static void Write(string fileName, string file, bool WithoutNewLine = true)
		{
			using (var sw = new StreamWriter(Path.GetFullPath(fileName), true, Encoding.Default))
			{
				if (WithoutNewLine)
					sw.Write(file);
				else
					sw.WriteLine(file);
			}
		}

		internal static string Read(string pathToFile)
		{
			using (var sr = new StreamReader(pathToFile, Encoding.Default))
			{
				return sr.ReadToEnd();
			}
		}
	}
}