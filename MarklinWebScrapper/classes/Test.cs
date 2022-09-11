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
		System.Console.WriteLine("Launching run test!");
		List<EbayItem> list2 = Scrapper.GetEbayListFromPage(2);
		List<EbayItem> list3 = Scrapper.GetEbayListFromPage(3);

	// 	System.Console.WriteLine("List2 = " + list2.Count);
	// 	System.Console.WriteLine("List3 = " + list3.Count);

	// 	list2.AddRange(list3);
	// 	System.Console.WriteLine("List = " + list2.Count);
		
		list2.AddRange(list3.Take(-1));

		IEnumerable<EbayItem> ferger = list3.Take(1);
		System.Console.WriteLine(ferger.Count());
	}
}