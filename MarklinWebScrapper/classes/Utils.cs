namespace MyApp
{
	public static class Utils
	{
		public static int getReferenceFromString(string title)
		{
			int reference = 0;

			for(int i = 0; i < title.Length; i++)
			{
				if (i + 3 < title.Length && isDigit(title[i]) && isDigit(title[i + 1]) && isDigit(title[i + 2]) && isDigit(title[i + 3]))
				{
					if (i + 4 < title.Length && isDigit(title[i]) && isDigit(title[i + 1]) && isDigit(title[i + 2]) && isDigit(title[i + 3]) && isDigit(title[i + 4]))
					{
						if (i + 5 < title.Length && isDigit(title[i]) && isDigit(title[i + 1]) && isDigit(title[i + 2]) && isDigit(title[i + 3]) && isDigit(title[i + 4]) && isDigit(title[i + 5]))
						{
							reference += title[i];
							reference *= 10;
							reference += title[i + 1];
							reference *= 10;
							reference += title[i + 2];
							reference *= 10;
							reference += title[i + 3];
							reference *= 10;
							reference += title[i + 4];
							reference *= 10;
							reference += title[i + 5];
							return reference;
						}
						reference += title[i];
						reference *= 10;
						reference += title[i + 1];
						reference *= 10;
						reference += title[i + 2];
						reference *= 10;
						reference += title[i + 3];
						reference *= 10;
						reference += title[i + 4];
						return reference;
					}
					reference += title[i];
					reference *= 10;
					reference += title[i + 1];
					reference *= 10;
					reference += title[i + 2];
					reference *= 10;
					reference += title[i + 3];
				}
			}
			return reference;
		}

		public static TrainBrand getTrainBrandFromString(string title)
		{

			return TrainBrand.Other;
		}

		public static bool isDigit(char character)
		{
			if(character >='0' && character <='9')
				return true;
			return false;
		}

		public static int numberFrom4Char(char c1, char c2, char c3, char c4)
		{
			
		}
	}
}