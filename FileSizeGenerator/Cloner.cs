using System.IO;
using System.Text;

namespace FileSizeGenerator
{
	internal class Cloner
	{
		internal void GetFileAfterClone(int iterations)
		{
			var nameOfNewFile = $"FileAfterCloning {Base.nameMode}.xml";
			var header = Base.Read(Base.headerFile);
			var body = Base.Read(Base.bodyFile);
			var footer = Base.Read(Base.footerFile);

			Base.Write(nameOfNewFile, header, false);

			Clonning(iterations, nameOfNewFile, body);

			Base.Write(nameOfNewFile, footer, false);
		}

		private void Clonning(int iterations, string nameOfNewFile, string fileForCloning)
		{
			using (var sw = new StreamWriter(Path.GetFullPath(nameOfNewFile), true, Encoding.Default))
			{
				for (int i = iterations; i > 0; i--)
					sw.WriteLine(fileForCloning);
			}
		}
	}
}