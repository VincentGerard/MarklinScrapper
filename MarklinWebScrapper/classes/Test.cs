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
		string sTest = "bhfhg 9510 trgrtgtr";

		System.Console.WriteLine("Res=" + Utils.getReferenceFromString(sTest));
	}
}