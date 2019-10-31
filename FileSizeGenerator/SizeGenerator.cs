using System.IO;

namespace FileSizeGenerator
{
	public class SizeGenerator
	{
		public static void GetBigFile(string pathToFile, string element, int number, Units units,
			string docType = "Файл")
		{
			XmlParser xmlParser = new XmlParser();
			IterationsFinder iterationsFinder = new IterationsFinder();
			Clonner clonner = new Clonner();

			xmlParser.GetFilesWithPartsOfXml(element, Path.GetFullPath(pathToFile), docType);
			var iterations = iterationsFinder.GetIterationsToNeededSIze(number, units);
			clonner.GetFileAfterClone(iterations);
		}
	}
}