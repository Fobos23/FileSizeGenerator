using System.IO;
using System.Text;
using System.Xml;

namespace FileSizeGenerator
{
	internal class XmlParser
	{
		/// <summary>
		/// Разделяет полученный Xml на три части: header, body, footer. Где body - указанная нода, header - все, что выше этой ноды, footer - все что ниже.
		/// </summary>
		/// <param name="neededNode"> Название ноды, которую нужно выделить из xml</param>
		/// <param name="pathToXml"></param>
		/// <param name="typeDoc"> Обычно тип документа: "Файл"</param>
		internal void GetFilesWithPartsOfXml(string neededNode, string pathToXml, string typeDoc)
		{
			var startNode = GetLineNumberForNeededElement(neededNode, pathToXml);
			var endNode = GetLineNumberForNeededElement(neededNode, pathToXml, false);
			var endFIle = GetLineNumberForNeededElement(typeDoc, pathToXml, false);

			ReadAndParsXml(pathToXml, startNode, endNode, endFIle, out var header, out var body, out var footer);

			Base.Write(Base.headerFile, header);
			Base.Write(Base.bodyFile, body);
			Base.Write(Base.footerFile, footer);
		}

		private void ReadAndParsXml(string pathToFile, int startNode, int endNode, int endFIle, out string header, out string body, out string footer)
		{
			header = string.Empty;
			body = string.Empty;
			footer = string.Empty;

			endNode = endNode != 0 ? endNode : startNode;

			using (var sr = new StreamReader(pathToFile, Encoding.Default))
			{
				for (var i = 1; i < endFIle + 1; i++)
				{
					if (i < startNode)
						header = header == "" ? sr.ReadLine() : header + "\n" + sr.ReadLine();
					else if (i >= startNode && i <= endNode)
						body = body == "" ? sr.ReadLine() : body + "\n" + sr.ReadLine();
					else
						footer = footer == "" ? sr.ReadLine() : footer + "\n" + sr.ReadLine();
				}
			}
		}

		private int GetLineNumberForNeededElement(string neededElement, string pathToXml, bool isStartElement = true)
		{
			var xmlReader = new XmlTextReader(pathToXml);
			var lineNumber = int.MaxValue;

			if (isStartElement)
			{
				while (xmlReader.Read() && xmlReader.LineNumber < lineNumber)
					if (xmlReader.Name == neededElement)
						lineNumber = xmlReader.LineNumber;
				return lineNumber;
			}

			while (xmlReader.Read())
				if (xmlReader.Name == neededElement && xmlReader.NodeType == XmlNodeType.EndElement)
					lineNumber = xmlReader.LineNumber;

			return lineNumber;
		}
	}
}