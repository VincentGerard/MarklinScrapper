public class EbayItem
{
	private int _price;
	private string _title = default!;
	//Is allowed to be null
	private string _localImagePath = default!;
	private string _uuid = default!;
	//Is allowed to be null
	private string _imageUrl = default!;
	private string _url = default!;
	
	EbayItem(int newPrice, string newTitle, string newLocalImagePath, string newUUID, string newImageUrl, string newUrl)
	{
		Price = newPrice;
		Title = newTitle;
		LocalImagePath = newLocalImagePath;
		UUID = newUUID;
		ImageUrl = newImageUrl;
		Url = newUrl;
	}

	//Price must be at least 0

	public int Price{
		get{return _price;}
		set{
			if (value >= 0)
				_price = value;
		}
	}
	//Every item must have a valid title
	public string Title{
		get{return _title;}
		set{
			if(value != null && value.Length > 0)
				_title = value;
		}
	}
	//Items must not always have an image
	public string LocalImagePath{
		get{return _localImagePath;}
		set{_localImagePath = value;}
	}
	//Items must always have a UUID
	public string UUID{
		get{return _uuid;}
		set{
			if (value != null && value.Length > 0)
				_uuid = value;
		}
	}
	//Items must not always have an image
	public string ImageUrl{
		get{return _imageUrl;}
		set{_imageUrl = value;}
	}
	//Items must always have a valid url
	public string Url{
		get{return _url;}
		set{
			if(value != null && value.Length > 0)
				_url = value;
		}
	}
}