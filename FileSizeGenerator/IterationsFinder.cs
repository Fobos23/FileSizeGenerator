using System;
using System.IO;

namespace FileSizeGenerator
{
	internal class IterationsFinder
	{
		internal int GetIterationsToNeededSIze(int number, Units units)
		{
			var headersSize = GetFileLength(Base.headerFile);
			var bodysSize = GetFileLength(Base.bodyFile);
			var footerSize = GetFileLength(Base.footerFile);

			var modePow = units == Units.KB ? 1 : units == Units.MB ? 2 : 3;

			var neededSizeInBytes = (int)(number * Math.Pow(2, 10 * modePow));
			var iterations = ((neededSizeInBytes - headersSize - footerSize) / bodysSize);

			return iterations;
		}

		private int GetFileLength(string fileName)
		{
			return (int) new FileInfo(Path.GetFullPath(fileName)).Length;
		}
	}
}