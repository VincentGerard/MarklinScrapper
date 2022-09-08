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
		static HtmlWeb web = new HtmlWeb();
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
				itemList.Add(item);
			}
			return itemList;
		}
		public static HtmlDocument GetPage(string page)
		{
			return web.Load(page);
		}
	}
}