using System;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml.Serialization;
using System.Collections.Generic;
using MyApp;
public static class Test
{
	public static void Run()
	{
		for(int i = 0; i < 50; i++)
		{
			List<EbayItem> list = Scrapper.GetEbayListFromPage(i);
			if (list == null || list.Count == 0)
			{
				Console.WriteLine("[Test]List is empty, aborting!");
				Environment.Exit(1);
			}
		}
	}
}