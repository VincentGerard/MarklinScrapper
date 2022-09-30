using System;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml.Serialization;
using System.Collections.Generic;
using MyApp;

namespace MyApp
{
	public static class Scrapper
	{	
		public static HtmlWeb web = new HtmlWeb();
		static Scrapper()
		{
			web.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11";
		}
		public static List<EbayItem> GetEbayListFromPage(int pageNumber)
		{
			string url = Url.ebayUrlStart + pageNumber + Url.ebayUrlEnd;
			List<EbayItem> itemList = new List<EbayItem>();
			System.Console.WriteLine("[GetEbayListFromPage]Url=" + url);
			HtmlDocument document = GetPage(url);
			if (document == null)
				return itemList;

			HtmlNodeCollection itemCollection = document.DocumentNode.SelectNodes(".//li[normalize-space(@class)='ad-listitem lazyload-item']");
			if (itemCollection == null)
				return itemList;

			foreach(HtmlNode node in itemCollection)
			{
				EbayItem item = Functions.LoadEbayItemFromNode(node);
				if (item.Url.Length == 0)
					Log.write("[GetEbayListFromPage]Not adding empty EbayItem");
				else
					itemList.Add(item);
			}
			return itemList;
		}
		private static HtmlDocument GetPage(string page)
		{
			return web.Load(page);
		}
	}
}