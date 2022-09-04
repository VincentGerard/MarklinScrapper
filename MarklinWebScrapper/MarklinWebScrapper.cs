using System;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Net;

namespace MyApp
{
    internal class MarklinWebScrapper
    {
        static void Main(string[] args)
        {
			string ebayUrlStart = "https://www.ebay-kleinanzeigen.de/s-seite:";
			string ebayUrlEnd = "/marklin/k0";
			int maxPage = 50;
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
				HtmlDocument document = web.Load(ebayUrlStart + currentPage + ebayUrlEnd);
				System.Console.WriteLine("Status Code: " + web.StatusCode);
				System.Console.WriteLine("Status Code: " + lastStatusCode);
				//document.Load("D:\\Vincent\\Progra\\C#\\MarklinWebScrapper\\MarklinWebScrapper\\ebay.html");

				//var title = document.DocumentNode.SelectNodes("//*[@id=\"srchrslt-adtable\"]/li[11]/article/div[2]/div[3]/p");
				HtmlNodeCollection articleCollection = document.DocumentNode.SelectNodes("//*[@id=\"srchrslt-adtable\"]/li/article");

				if (articleCollection == null)
				{
					System.Console.WriteLine("ArticleCollection is null!");
					break;
				}
				int itemCount = articleCollection.ToList().Count;
				int itemNumber = 0;
				Console.WriteLine("Number of orticle:" + itemCount);

				foreach(HtmlNode node in articleCollection)
				{
					string stringPrice = node.SelectNodes(".//p[@class='aditem-main--middle--price']").First().InnerText;
					
					stringPrice = Regex.Match(stringPrice, @"\d+").Value;
					
					int price = 0;
					string title = "";
					string localImagePath = "";
					string uuid = "";
					string ebayImageUrl = "";
					//string ebayUrl = ""; 
					
					if (stringPrice.Length != 0)
						price = int.Parse(stringPrice);

					title = node.SelectNodes(".//p[@class='aditem-main--middle--description']").First().InnerText;
					title = Regex.Replace(title, @"\t|\n|\r", "");
					uuid = node.Attributes["data-adid"].Value;
					HtmlNodeCollection imageCollection = node.SelectNodes(".//div[@class='imagebox srpimagebox']");
					if (imageCollection != null)
						ebayImageUrl = imageCollection.First().Attributes["data-imgsrc"].Value;
					else
					{
						System.Console.Beep();
						ebayImageUrl = "";
					}

					System.Console.WriteLine("Page: " + currentPage);
					System.Console.WriteLine("Item: " + itemNumber);
					System.Console.WriteLine(title);
					System.Console.WriteLine(price);
					System.Console.WriteLine(uuid);
					System.Console.WriteLine(ebayImageUrl);
					System.Console.WriteLine(Environment.NewLine);
					itemNumber++;
				}
				Thread.Sleep(5000);
			}
        }
    }
}