using System;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml.Serialization;

namespace MyApp
{
    public class MarklinWebScrapper
    {
        static void Main(string[] args)
        {
			List<EbayItem> itemList = new List<EbayItem>();
			ConsoleKeyInfo key;
			System.Console.WriteLine("Escape to quit!");
			System.Console.WriteLine("T = Run Test");
			System.Console.WriteLine("S = Show itemList in items.xml");
			System.Console.WriteLine("U = Update Current in items.xml");
			key = Console.ReadKey();

			if (key.Key == ConsoleKey.T)
			{
				Test.Run();
				System.Environment.Exit(0);
			}
			else if (key.Key == ConsoleKey.S)
			{
				//Load  from file
				LoadItemList(ref itemList);
				foreach(EbayItem item in itemList)
				{
					item.Show();
				}
				System.Console.WriteLine("Number of items: " + itemList.Count);
				System.Environment.Exit(0);
			}
			else if (key.Key == ConsoleKey.Escape)
			{
				System.Environment.Exit(0);
			}
			else if (key.Key == ConsoleKey.U)
			{
				System.Console.WriteLine("Update");
				UpdateItemList(ref itemList);
			}
		
			int maxPage = 1;
			var lastStatusCode = HttpStatusCode.OK;
			
			//https://www.ebay-kleinanzeigen.de/s-modellbau/m%C3%A4rklin/k0c249
			//https://www.ebay-kleinanzeigen.de/s-marklin/k0
			//https://www.ebay-kleinanzeigen.de/s-seite:1/marklin/k0

			HtmlWeb web = new HtmlWeb();
			web.PostResponse = (request, response) =>
			{
				if (response != null)
				{
					lastStatusCode = response.StatusCode;
				}
			};

			for(int currentPage = 0; currentPage < maxPage; currentPage++)
			{
				HtmlDocument document = web.Load(Url.ebayUrlStart + currentPage + Url.ebayUrlEnd);
				System.Console.WriteLine("Status Code: " + web.StatusCode);
				System.Console.WriteLine("Status Code: " + lastStatusCode);
				//document.Load("D:\\Vincent\\Progra\\C#\\MarklinWebScrapper\\MarklinWebScrapper\\ebay.html");

				//var title = document.DocumentNode.SelectNodes("//*[@id=\"srchrslt-adtable\"]/li[11]/article/div[2]/div[3]/p");
				HtmlNodeCollection articleCollection = document.DocumentNode.SelectNodes("//*[@id=\"srchrslt-adtable\"]/li/article");

				if (articleCollection == null)
				{
					System.Console.WriteLine("ArticleCollection is null!");
					System.Console.WriteLine("Url: " + Url.ebayUrlStart + currentPage + Url.ebayUrlEnd);
					break;
				}
				int itemCount = articleCollection.ToList().Count;
				int itemNumber = 0;
				Console.WriteLine("Number of orticle:" + itemCount);

				foreach(HtmlNode node in articleCollection)
				{
					System.Console.WriteLine("Page: " + currentPage);
					System.Console.WriteLine("Item: " + itemNumber);
					EbayItem item = LoadEbayItemFromNode(node);
					item.Show();
					itemList.Insert(0, item);
					System.Console.WriteLine(Environment.NewLine);
					itemNumber++;
				}
				Thread.Sleep(5000);
			}
			System.Console.WriteLine("Items in list: " + itemList.Count);
			WriteToXmlFile<List<EbayItem>>("data/items.xml", itemList);
        }

		public static EbayItem LoadEbayItemFromNode(HtmlNode node)
		{
			EbayItem item = new EbayItem();

			string stringPrice = node.SelectNodes(".//p[@class='aditem-main--middle--price']").First().InnerText;
			string title = "";
					
			stringPrice = Regex.Match(stringPrice, @"\d+").Value;
			if (stringPrice.Length != 0)
				item.Price = int.Parse(stringPrice);
			title = node.SelectNodes(".//p[@class='aditem-main--middle--description']").First().InnerText;
			item.Title = Regex.Replace(title, @"\t|\n|\r", "");
			item.UUID = node.Attributes["data-adid"].Value;
			HtmlNodeCollection imageCollection = node.SelectNodes(".//div[@class='imagebox srpimagebox']");
			if (imageCollection != null)
				item.ImageUrl = imageCollection.First().Attributes["data-imgsrc"].Value;
			item.Url = Url.ebay + node.SelectNodes(".//a").First().Attributes["href"].Value;
			//item.Url = node.Attributes["href"].Value;
			return item;
		}

		public static void LoadItemList(ref List<EbayItem> itemList)
		{
			itemList = ReadFromXmlFile<List<EbayItem>>("data/items.xml");
			System.Console.WriteLine("LoadItemList: " + itemList.Count);
		}
		//Updates the list of items in data/items.xml
		public static void UpdateItemList(ref List<EbayItem> itemList)
		{
			itemList.Add(new EbayItem());
		}

		/// <summary>
		/// Writes the given object instance to an XML file.
		/// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
		/// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [XmlIgnore] attribute.</para>
		/// <para>Object type must have a parameterless constructor.</para>
		/// </summary>
		/// <typeparam name="T">The type of object being written to the file.</typeparam>
		/// <param name="filePath">The file path to write the object instance to.</param>
		/// <param name="objectToWrite">The object instance to write to the file.</param>
		/// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
		public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
		{
			TextWriter writer = null;
			try
			{
				var serializer = new XmlSerializer(typeof(T));
				writer = new StreamWriter(filePath, append);
				serializer.Serialize(writer, objectToWrite);
			}
			finally
			{
				if (writer != null)
					writer.Close();
			}
		}

		/// <summary>
		/// Reads an object instance from an XML file.
		/// <para>Object type must have a parameterless constructor.</para>
		/// </summary>
		/// <typeparam name="T">The type of object to read from the file.</typeparam>
		/// <param name="filePath">The file path to read the object instance from.</param>
		/// <returns>Returns a new instance of the object read from the XML file.</returns>
		public static T ReadFromXmlFile<T>(string filePath) where T : new()
		{
			TextReader reader = null;
			try
			{
				var serializer = new XmlSerializer(typeof(T));
				reader = new StreamReader(filePath);
				return (T)serializer.Deserialize(reader);
			}
			finally
			{
				if (reader != null)
					reader.Close();
			}
		}
    }
}