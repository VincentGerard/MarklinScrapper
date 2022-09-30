using System;

namespace MyApp
{
	public class Article
	{
		private int _reference;
		private int _quantity;
		private TrainBrand _brand;


		public Article() : this(0, TrainBrand.Other, 1)
		{

		}

		public Article(int newReference, TrainBrand newBrand, int newQuantity)
		{
			Reference = newReference;
			Brand = newBrand;
			Quantity = newQuantity;
		}

		public int Reference
		{
			get{return _reference;}
			set
			{
				if (value < 0)
					_reference = 0;
				else
					_reference = value;
			}
		}

		public int Quantity
		{
			get{return _quantity;}
			set
			{
				if (value < 0)
					_quantity = 0;
				else
					_quantity = value;
			}
		}

		public TrainBrand Brand
		{
			get{return _brand;}
			set{_brand = value;}
		}
	}
}