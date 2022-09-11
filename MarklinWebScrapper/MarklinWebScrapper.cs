using System;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml.Serialization;
using System.Collections.Generic;
using MyApp;

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
			System.Console.WriteLine("G = Get page and replace items.xml");
			key = Console.ReadKey();

			if (key.Key == ConsoleKey.T)
			{
				Test.Run();
				System.Environment.Exit(0);
			}
			else if (key.Key == ConsoleKey.S)
			{
				//Load  from file
				Functions.ImportItemList(ref itemList);
				foreach(EbayItem item in itemList.Take(10))
				{
					item.Show();
					System.Console.WriteLine(Environment.NewLine);
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
				System.Console.WriteLine("[Update]List= " + itemList.Count);
				Functions.ImportItemList(ref itemList);
				System.Console.WriteLine("[Update]Imported= " + itemList.Count);
				Functions.UpdateItemList(ref itemList);
				System.Console.WriteLine("[Update]Updated = " + itemList.Count);
				Functions.ExportItemList(itemList);
				System.Environment.Exit(0);
			}
			else if (key.Key == ConsoleKey.G)
			{
				System.Console.WriteLine("[Get]What page should I fetch?");
				string? line = Console.ReadLine();
				if (line == null)
					System.Environment.Exit(1);
				int pageNumber = int.Parse(line);
				System.Console.WriteLine("[Get]Fetching page " + pageNumber);
				itemList = Scrapper.GetEbayListFromPage(pageNumber);
				Functions.ExportItemList(itemList);
				System.Console.WriteLine(itemList.Count());
				System.Environment.Exit(0);
			}
			System.Environment.Exit(0);
        }
    }
}