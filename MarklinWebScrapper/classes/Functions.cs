using System;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace MyApp
{
	public static class Functions
	{
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
			item.UUID = node.SelectNodes("article[@class='aditem']").First().Attributes["data-adid"].Value;
			HtmlNodeCollection imageCollection = node.SelectNodes(".//div[@class='imagebox srpimagebox']");
			if (imageCollection != null)
				item.ImageUrl = imageCollection.First().Attributes["data-imgsrc"].Value;
			item.Url = Url.ebay + node.SelectNodes(".//a").First().Attributes["href"].Value;
			return item;
		}

		public static void ImportItemList(ref List<EbayItem> itemList)
		{
			itemList = ReadFromXmlFile<List<EbayItem>>("data/items.xml");
			System.Console.WriteLine("LoadItemList: " + itemList.Count);
		}

		public static void ExportItemList(ref List<EbayItem> itemList)
		{
			WriteToXmlFile<List<EbayItem>>("data/items.xml", itemList);
		}
		//Updates the list of items in data/items.xml
		public static void UpdateItemList(ref List<EbayItem> itemList)
		{
			List<EbayItem> newList = new List<EbayItem>();
			ImportItemList(ref itemList);
			foreach(EbayItem item in itemList)
			{
				newList.Insert(0, item);
				newList.Insert(0, item);
				newList.Insert(0, item);
				newList.Insert(0, item);
			}
			itemList = newList;
			//itemList.Add(new EbayItem());
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