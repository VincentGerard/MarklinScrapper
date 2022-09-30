namespace MyApp
{
	public static class Utils
	{
		public static int getReferenceFromString(string title)
		{
			for(int i = 0; i < title.Length; i++)
			{
				if (i + 3 < title.Length && isDigit(title[i]) && isDigit(title[i + 1]) && isDigit(title[i + 2]) && isDigit(title[i + 3]))
				{
					if (i + 4 < title.Length && isDigit(title[i]) && isDigit(title[i + 1]) && isDigit(title[i + 2]) && isDigit(title[i + 3]) && isDigit(title[i + 4]))
					{
						if (i + 5 < title.Length && isDigit(title[i]) && isDigit(title[i + 1]) && isDigit(title[i + 2]) && isDigit(title[i + 3]) && isDigit(title[i + 4]) && isDigit(title[i + 5]))
						{
							return numberFrom6Char(title[i], title[i + 1], title[i + 2], title[i + 3], title[i + 4], title[i + 5]);
						}
						return numberFrom5Char(title[i], title[i + 1], title[i + 2], title[i + 3], title[i + 4]);
					}
					return numberFrom4Char(title[i], title[i + 1], title[i + 2], title[i + 3]);
				}
			}
			return 0;
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
			int result = c1 - 48;
			result *= 10;
			result += c2 - 48;
			result *= 10;
			result += c3 - 48;
			result *= 10;
			result += c4 - 48;
			return result;
		}

		public static int numberFrom5Char(char c1, char c2, char c3, char c4, char c5)
		{
			int result = c1 - 48;
			result *= 10;
			result += c2 - 48;
			result *= 10;
			result += c3 - 48;
			result *= 10;
			result += c4 - 48;
			result *= 10;
			result += c5 - 48;
			return result;
		}

		public static int numberFrom6Char(char c1, char c2, char c3, char c4, char c5, char c6)
		{
			int result = c1 - 48;

			result *= 10;
			result += c2 - 48;
			result *= 10;
			result += c3 - 48;
			result *= 10;
			result += c4 - 48;
			result *= 10;
			result += c5 - 48;
			result *= 10;
			result += c6 - 48;
			return result;
		}
	}
}