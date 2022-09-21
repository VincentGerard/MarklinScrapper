using System;

namespace MyApp
{
	public class EbayItem
	{
		private int _price;
		private string _title = default;
		private string _uuid = default;
		private string _imageUrl = default;
		private string _url = default;
		
		public EbayItem() : this(0,"","","","")
		{

		}
		
		public EbayItem(int newPrice, string newTitle, string newUUID, string newImageUrl, string newUrl)
		{
			Price = newPrice;
			Title = newTitle;
			UUID = newUUID;
			ImageUrl = newImageUrl;
			Url = newUrl;
		}

		public int Price{
			get{return _price;}
			set{_price = value;}
		}
		public string Title{
			get{return _title;}
			set
			{
				if (value == null)
					_title = "";
				else 
					_title = value;
			}
		}
		public string UUID{
			get{return _uuid;}
			set
			{
				if (value == null)
					_uuid = "";
				else 
					_uuid = value;
			}
		}
		public string ImageUrl{
			get{return _imageUrl;}
			set
			{
				if (value == null)
					_imageUrl = "";
				else 
					_imageUrl = value;
			}
		}
		public string Url{
			get{return _url;}
			set
			{
				if (value == null)
					_url = "";
				else
					_url = value;
			}
		}

		public void Show()
		{
			Console.WriteLine("Title   : " + Title);
			Console.WriteLine("Price   : " + Price);
			Console.WriteLine("UUID    : " + UUID);
			Console.WriteLine("ImageUrl: " + ImageUrl);
			Console.WriteLine("Url     : " + Url);
		}
	}
}