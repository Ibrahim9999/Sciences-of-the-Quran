using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// Potential False verses 9:128-129

namespace Quran_Word_Search
{
	class Program
	{
		public static string StrToHex(char c)
		{
			return Convert.ToString(c, 16).PadLeft(4,'0');
		}
		
		public static List<string> StrToHex(string input)
		{
		    var s = new List<string>();
		    
		    foreach (char c in input)
		    	s.Add(StrToHex(c));
		    
		    return s;
		}
		
		public static int HexToASCII(string hex)
		{
			return Int32.Parse(hex, System.Globalization.NumberStyles.HexNumber);
		}
		
		public static List<int> HexToASCII(List<string> hex)
		{
			var ascii = new List<int>();
			
			for (int i = 0; i < hex.Count; i++)
				ascii.Add(HexToASCII(hex[i]));
			
			return ascii;
		}
		
		public static List<Arabic.Word> HexToWords(List<string> hex)
		{
			var words = new List<Arabic.Word>();
			var word = new Arabic.Word();
			
			for (int i = 0; i < hex.Count; i++)
				if (hex[i] != "0020")
					word.AddLetter(new Arabic.Letter(hex[i]));
				else if (word.LetterCount() != 0)
				{
					words.Add(word);
					word = new Arabic.Word();
				}
			
			if (word.LetterCount() != 0)
				words.Add(word);
			
			return words;
		}
		
		public static List<string> RemoveDiacritics(List<string> hexInput)
		{
			var output = new List<string>();
			
			for (int i = 0; i < hexInput.Count; i++)
				if (Arabic.Letter.IsCharacter((char) int.Parse(hexInput[i], System.Globalization.NumberStyles.HexNumber)))
				    output.Add(hexInput[i]);
			
			return output;
		}
		public static string RemoveDiacritics(string input)
		{
			var output = "";
			
			for (int i = 0; i < input.Length; i++)
				if (Arabic.Letter.IsCharacter(input[i]))
				    output += input[i];
			
			return output;
		}
		
		public static void AllahCount(List<Arabic.Word> words)
		{
			var keyword = new List<Arabic.Word>
			{
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.HAMZA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Diacritic.HAMZA_ABOVE),
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.FA),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
					new Arabic.Letter(Arabic.Character.MEEM),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.FA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.TA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.TA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
			};
			Console.WriteLine("value of entire phrase: " + Arabic.AbjadValue(words));
			Console.WriteLine("number of \"'aallah\": " + Arabic.SearchQuantity(words, keyword[0]));	// 2
			Console.WriteLine("number of \"'abillah\": " + Arabic.SearchQuantity(words, keyword[1]));	// 1
			Console.WriteLine("number of \"falillah\": " + Arabic.SearchQuantity(words, keyword[2]));	// 6
			Console.WriteLine("number of \"allahum\": " + Arabic.SearchQuantity(words, keyword[3]));	// 5
			Console.WriteLine("number of \"walillah\": " + Arabic.SearchQuantity(words, keyword[4]));	// 27
			Console.WriteLine("number of \"fallah\": " + Arabic.SearchQuantity(words, keyword[5]));	// 6
			Console.WriteLine("number of \"wallah\": " + Arabic.SearchQuantity(words, keyword[6]));	// 240
			Console.WriteLine("number of \"billah\": " + Arabic.SearchQuantity(words, keyword[7]));	// 139
			Console.WriteLine("number of \"lillah\": " + Arabic.SearchQuantity(words, keyword[8]));	// 116
			Console.WriteLine("number of \"allah\": " + Arabic.SearchQuantity(words, keyword[9]));	// 2265
			Console.WriteLine("number of \"watallah\": " + Arabic.SearchQuantity(words, keyword[10]));// 1
			Console.WriteLine("number of \"tallah\": " + Arabic.SearchQuantity(words, keyword[11]));	// 8
			
			//2+1+6+5+27+6+240+139+116+2265+1+8 = 2816
		}
		
		//DayCount
		
		public static void MonthCount(List<Arabic.Word> words)
		{
			var keyword = new List<Arabic.Word>
			{
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.SHEEN),
					new Arabic.Letter(Arabic.Character.HA),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.ALIF),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Diacritic.NONE),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.SHEEN),
					new Arabic.Letter(Arabic.Character.HA),
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.RA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Diacritic.NONE),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Diacritic.HAMZA_ABOVE),
					new Arabic.Letter(Arabic.Character.SHEEN),
					new Arabic.Letter(Arabic.Character.HA),
					new Arabic.Letter(Arabic.Character.RA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Diacritic.NONE),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.SHEEN),
					new Arabic.Letter(Arabic.Character.HA),
					new Arabic.Letter(Arabic.Character.RA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.SHEEN),
					new Arabic.Letter(Arabic.Character.HA),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.YA),
					new Arabic.Letter(Arabic.Character.NOON),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Diacritic.HAMZA_ABOVE),
					new Arabic.Letter(Arabic.Character.SHEEN),
					new Arabic.Letter(Arabic.Character.HA),
					new Arabic.Letter(Arabic.Character.RA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Diacritic.NONE),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.SHEEN),
					new Arabic.Letter(Arabic.Character.HA),
					new Arabic.Letter(Arabic.Character.RA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Diacritic.NONE),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.SHEEN),
					new Arabic.Letter(Arabic.Character.HA),
					new Arabic.Letter(Arabic.Character.RA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.SHEEN),
					new Arabic.Letter(Arabic.Character.HA),
					new Arabic.Letter(Arabic.Character.RA),
				}),
			};
			
			Console.WriteLine("number of \"'shahra\": " + Arabic.SearchQuantity(words, keyword[0]));
			Console.WriteLine("number of \"'ash-shuhoor\": " + Arabic.SearchQuantity(words, keyword[1]));
			Console.WriteLine("number of \"al-ashhur\": " + Arabic.SearchQuantity(words, keyword[2]));
			Console.WriteLine("number of \"wash-shahr\": " + Arabic.SearchQuantity(words, keyword[3]));
			Console.WriteLine("number of \"shahrayn\": " + Arabic.SearchQuantity(words, keyword[4]));
			Console.WriteLine("number of \"ashhur\": " + Arabic.SearchQuantity(words, keyword[5]));
			Console.WriteLine("number of \"bish-shahr\": " + Arabic.SearchQuantity(words, keyword[6]));	
			Console.WriteLine("number of \"ash-shahr\": " + Arabic.SearchQuantity(words, keyword[7]));
			Console.WriteLine("number of \"shahr\": " + Arabic.SearchQuantity(words, keyword[8]));
		}
		
		// unfinished - requries diacritics
		public static void YearCount(List<Arabic.Word> words)
		{
			var keyword = new List<Arabic.Word>
			{
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.HAMZA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Diacritic.HAMZA_ABOVE),
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.FA),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
					new Arabic.Letter(Arabic.Character.MEEM),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.FA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.TA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.TA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
			};
			
			Console.WriteLine("number of \"'sanatin\": " + Arabic.SearchQuantity(words, keyword[0]));	// 2
			Console.WriteLine("number of \"'sanaa?\": " + Arabic.SearchQuantity(words, keyword[1]));	// 1
			Console.WriteLine("number of \"sineen\": " + Arabic.SearchQuantity(words, keyword[2]));	// 6
			Console.WriteLine("number of \"as-sineen\": " + Arabic.SearchQuantity(words, keyword[3]));	// 5
			Console.WriteLine("number of \"bis-sineen\": " + Arabic.SearchQuantity(words, keyword[4]));	// 27
			Console.WriteLine("number of \"sanatan\": " + Arabic.SearchQuantity(words, keyword[5]));	// 6
			Console.WriteLine("number of \"sanatin\" (waslah): " + Arabic.SearchQuantity(words, keyword[6]));	// 240
			
			//2+1+6+5+27+6+240+139+116+2265+1+8 = 2816
		}
		
		//MoonCount
		
		public static void SeaCount(List<Arabic.Word> words)
		{
			var keyword = new List<Arabic.Word>
			{
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.HHA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.RA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.HHA),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.NOON),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Diacritic.HAMZA_ABOVE),
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.HHA),
					new Arabic.Letter(Arabic.Character.RA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.HHA),
					new Arabic.Letter(Arabic.Character.RA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.HHA),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.YA),
					new Arabic.Letter(Arabic.Character.NOON),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.HHA),
					new Arabic.Letter(Arabic.Character.RA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.HHA),
					new Arabic.Letter(Arabic.Character.YA),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.HA, Arabic.Diacritic.MARBUTAH),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.HHA),
					new Arabic.Letter(Arabic.Character.RA),
				}),
			};
			
			Console.WriteLine("number of \"al-bihhar\": " + Arabic.SearchQuantity(words, keyword[0]));
			Console.WriteLine("number of \"al-bahhran\": " + Arabic.SearchQuantity(words, keyword[1]));
			Console.WriteLine("number of \"abhhur\": " + Arabic.SearchQuantity(words, keyword[2]));
			Console.WriteLine("number of \"bahhr\": " + Arabic.SearchQuantity(words, keyword[3]));	
			Console.WriteLine("number of \"al-bahhrayn\": " + Arabic.SearchQuantity(words, keyword[4]));
			Console.WriteLine("number of \"wal-bahhr\": " + Arabic.SearchQuantity(words, keyword[5]));
			Console.WriteLine("number of \"bahheerah\" (waslah): " + Arabic.SearchQuantity(words, keyword[6]));
			Console.WriteLine("number of \"al-bahhr\" (waslah): " + Arabic.SearchQuantity(words, keyword[7]));
		}
		
		public static void ResurrectionCount(List<Arabic.Word> words)
		{
			var keyword = new List<Arabic.Word>
			{
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.QAF),
					new Arabic.Letter(Arabic.Character.YA),
					new Arabic.Letter(Arabic.Character.MEEM),
					new Arabic.Letter(Arabic.Character.HA),
				}),
			};
			
			Console.WriteLine("number of \"al-qiyammah\": " + Arabic.SearchQuantity(words, keyword[0]));
		}
		
		public static void WhiteCount(List<Arabic.Word> words)
		{
			var keyword = new List<Arabic.Word>
			{
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.YA),
					new Arabic.Letter(Arabic.Character.DAD),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.YA),
					new Arabic.Letter(Arabic.Character.DAD),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.HAMZA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.YA),
					new Arabic.Letter(Arabic.Character.DAD),
					new Arabic.Letter(Arabic.Character.TA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.YA),
					new Arabic.Letter(Arabic.Character.DAD),
					new Arabic.Letter(Arabic.Character.TA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.TA),
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.YA),
					new Arabic.Letter(Arabic.Character.DAD),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Diacritic.HAMZA_ABOVE),
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.YA),
					new Arabic.Letter(Arabic.Character.DAD),
				}),
			};
			
			Console.WriteLine("number of \"baydd\": " + Arabic.SearchQuantity(words, keyword[0]));
			Console.WriteLine("number of \"baydda'\": " + Arabic.SearchQuantity(words, keyword[1]));
			Console.WriteLine("number of \"wa-abyaddtu\": " + Arabic.SearchQuantity(words, keyword[2]));
			Console.WriteLine("number of \"abyaddtu\": " + Arabic.SearchQuantity(words, keyword[3]));	
			Console.WriteLine("number of \"tabayadd\": " + Arabic.SearchQuantity(words, keyword[4]));
			Console.WriteLine("number of \"al-abyadd\": " + Arabic.SearchQuantity(words, keyword[5]));
		}
		
		public static void ThisWorldCount(List<Arabic.Word> words)
		{
			var keyword = new List<Arabic.Word>
			{
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.DAL),
					new Arabic.Letter(Arabic.Character.NOON),
					new Arabic.Letter(Arabic.Character.YA),
					new Arabic.Letter(Arabic.Character.ALIF),
				}),
			};
			
			Console.WriteLine("number of \"al-dunya\": " + Arabic.SearchQuantity(words, keyword[0]));
		}
		
		public static void LastWorldCount(List<Arabic.Word> words)
		{
			var keyword = new List<Arabic.Word>
			{
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HAMZA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.KHA),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.HA, Arabic.Diacritic.MARBUTAH),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HAMZA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.KHA),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.HA, Arabic.Diacritic.MARBUTAH),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HAMZA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.KHA),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.HA, Arabic.Diacritic.MARBUTAH),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HAMZA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.KHA),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.HA, Arabic.Diacritic.MARBUTAH),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HAMZA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.KHA),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.HA, Arabic.Diacritic.MARBUTAH),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HAMZA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.KHA),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.HA, Arabic.Diacritic.MARBUTAH),
				}),
			};
			
			Console.WriteLine("number of \"wabel-akhirah\": " + Arabic.SearchQuantity(words, keyword[0]));
			Console.WriteLine("number of \"bel-akhirah\": " + Arabic.SearchQuantity(words, keyword[1]));
			Console.WriteLine("number of \"al-akhirah\": " + Arabic.SearchQuantity(words, keyword[2]));
			Console.WriteLine("number of \"wal-akhirah\": " + Arabic.SearchQuantity(words, keyword[3]));
			Console.WriteLine("number of \"walel-akhirah\": " + Arabic.SearchQuantity(words, keyword[4]));
			Console.WriteLine("number of \"lel-akhirah\": " + Arabic.SearchQuantity(words, keyword[5]));
		}
		
		public static void SabbathCount(List<Arabic.Word> words)
		{
			var keyword = new List<Arabic.Word>
			{
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.SEEN),
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.TA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.SEEN),
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.TA),
					new Arabic.Letter(Arabic.Character.HA),
					new Arabic.Letter(Arabic.Character.MEEM),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.YA),
					new Arabic.Letter(Arabic.Character.SEEN),
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.TA),
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.NOON),
				}),
			};
			
			Console.WriteLine("number of \"al-sabt\": " + Arabic.SearchQuantity(words, keyword[0]));
			Console.WriteLine("number of \"sabtihim\": " + Arabic.SearchQuantity(words, keyword[1]));
			Console.WriteLine("number of \"yasbitoon\": " + Arabic.SearchQuantity(words, keyword[2]));
		}
		
		public static void TrumpetCount(List<Arabic.Word> words)
		{
			var keyword = new List<Arabic.Word>
			{
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.SAD),
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.TA, Arabic.Diacritic.MARBUTAH),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.MEEM),
					new Arabic.Letter(Arabic.Character.SAD),
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.RA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.SAD),
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.KAF),
					new Arabic.Letter(Arabic.Character.MEEM),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.SAD),
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.KAF),
					new Arabic.Letter(Arabic.Character.MEEM),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.SAD),
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.NOON),
					new Arabic.Letter(Arabic.Character.KAF),
					new Arabic.Letter(Arabic.Character.MEEM),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.SAD),
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.RA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.YA),
					new Arabic.Letter(Arabic.Character.SAD),
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.KAF),
					new Arabic.Letter(Arabic.Character.MEEM),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.FA),
					new Arabic.Letter(Arabic.Character.SAD),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.HA),
					new Arabic.Letter(Arabic.Character.NOON),
				}),
			};
			
			Console.WriteLine("number of \"soorratin\": " + Arabic.SearchQuantity(words, keyword[0]));
			Console.WriteLine("number of \"al-musawwir\": " + Arabic.SearchQuantity(words, keyword[1]));
			Console.WriteLine("number of \"suwarakum\": " + Arabic.SearchQuantity(words, keyword[2]));
			Console.WriteLine("number of \"wa-suwarakum\": " + Arabic.SearchQuantity(words, keyword[3]));
			Console.WriteLine("number of \"sawwarnakum\": " + Arabic.SearchQuantity(words, keyword[4]));
			Console.WriteLine("number of \"as-soor\": " + Arabic.SearchQuantity(words, keyword[5]));
			Console.WriteLine("number of \"yu-suwarakum\": " + Arabic.SearchQuantity(words, keyword[6]));
			Console.WriteLine("number of \"fa-surhunna\": " + Arabic.SearchQuantity(words, keyword[7]));
		}
		
		public static void CreateQuran()
		{
			// WordInformation.csv
			const int CHAPTER = 1;
			const int VERSE = 2;
			const int WORD = 3;
			const int ARABIC = 4;
			const int TYPE = 7;
			const int ROOT = 8;
			const int ENGLISH = 9;
			const int OCCURANCE = 10;
			
			// dictionary.txt
			const int LOCATION = 0;
			const int FORM = 1;
			const int TAG = 2;
			const int FEATURES = 3;
			
			// quran-data.xml
			const int SURA = 0;
			const int AYA = 1;
			const int COUNT = 0;
			const int SAJDA = 2;
			const int ARABIC_NAME = 1;
			const int ENGLISH_NAME = 2;
			const int ORIGIN = 3;
			
			var suraData = new List<List<string>>();
			var quarterData = new List<List<int>>();
			var manzilData = new List<List<int>>();
			var rukuData = new List<List<int>>();
			var pageData = new List<List<int>>();
			var sajdaData = new List<List<string>>();
			
			using (var data = new StreamReader(@".\\..\\..\\Files\\Research\\quran-data.xml"))
				while (!data.EndOfStream)
				{
					string line = data.ReadLine();
					
					if (line.Contains("<sura "))
					{
						// <	sura index="1" ayas="7" start="0" name="الفاتحة" tname="Al-Faatiha" ename="The Opening" type="Meccan" order="5" rukus="1" />
						string[] nodes = line.Split('\"');
						
						var lineData = new List<string>
						{
							nodes[5],	// ayas
							nodes[7],	// name
							nodes[11],	// ename
							nodes[13]	// type
						};
						
						suraData.Add(lineData);
					}
					else if (line.Contains("<quarter "))
					{
						// <quarter index="1" sura="1" aya="1" />
						string[] nodes = line.Split('\"');
						
						var lineData = new List<int>
						{
							Convert.ToInt16(nodes[3]),	// sura
							Convert.ToInt16(nodes[5])	// aya
						};
						
						quarterData.Add(lineData);
					}
					else if (line.Contains("<manzil "))
					{
						// <manzil index="1" sura="1" aya="1" />
						string[] nodes = line.Split('\"');
						
						var lineData = new List<int>
						{
							Convert.ToInt16(nodes[3]),	// sura
							Convert.ToInt16(nodes[5])	// aya
						};
						
						manzilData.Add(lineData);
					}
					else if (line.Contains("<ruku "))
					{
						// <ruku index="1" sura="1" aya="1" />
						string[] nodes = line.Split('\"');
						
						var lineData = new List<int>
						{
							Convert.ToInt16(nodes[3]),	// sura
							Convert.ToInt16(nodes[5])	// aya
						};
						
						rukuData.Add(lineData);
					}
					else if (line.Contains("<page "))
					{
						// <page index="1" sura="1" aya="1" />
						string[] nodes = line.Split('\"');
						
						var lineData = new List<int>
						{
							Convert.ToInt16(nodes[3]),	// sura
							Convert.ToInt16(nodes[5])	// aya
						};
						
						pageData.Add(lineData);
					}
					else if (line.Contains("<sajda "))
					{
						// <sajda index="1" sura="7" aya="206" type="recommended" />
						string[] nodes = line.Split('\"');
						
						var lineData = new List<string>
						{
							nodes[3],	// sura
							nodes[5],	// aya
							nodes[7]	// type
						};
						
						sajdaData.Add(lineData);
					}
				}
			
			// Add extra element at the end and remove first element (to account for index errors)
			suraData.Add(suraData[0]);
			suraData.RemoveAt(0);
			quarterData.Add(quarterData[0]);
			quarterData.RemoveAt(0);
			manzilData.Add(manzilData[0]);
			manzilData.RemoveAt(0);
			rukuData.Add(rukuData[0]);
			rukuData.RemoveAt(0);
			pageData.Add(pageData[0]);
			pageData.RemoveAt(0);
			sajdaData.Add(sajdaData[0]);
			sajdaData.RemoveAt(0);
			
			using (var output = new StreamWriter(@".\\..\\..\\Files\\Sources\\Quran\\test.xml", false, Encoding.Unicode))
				using(var roots = new StreamReader(@".\\..\\..\\Files\\Research\\dictionary.txt"))
					using(var input = new StreamReader(@".\\..\\..\\Files\\Research\\WordInformation.csv"))
				    {
						int verseCount = 1;
						int wordCount = 1;
						int wordChapterCount = 1;
						
						int quarter = 1;
						int seventh = 1;
						int bowing = 1;
						int page = 1;
						int prostration = 1;
						
						// Start of file
			            output.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
			            output.WriteLine("<!-- COMMENTS -->");
			            output.WriteLine("<quran>");
				        
			            // Skip Column Titles
			            input.ReadLine();
			            
			            for (int j = 0; j < 131; j++)
			            	roots.ReadLine();
			            
			            var grammar = roots.ReadLine().Split((char) 9);
				        
				        int rootWord = Convert.ToInt16(grammar[LOCATION].Replace("(", "").Split(':')[2]);
				        string form = grammar[FORM];
				        string tag = grammar[TAG];
				        var features = new List<string>(grammar[FEATURES].Split('|'));
				        
				        //while (!input.EndOfStream)
				        //for (int i = 0; i < 6150 && !input.EndOfStream; i++)
				        for (int i = 0; i < 77878 && !input.EndOfStream; i++)
		       			{
				        	var values = input.ReadLine().Split('\t');
				        	
				        	int chapter = Convert.ToInt16(values[CHAPTER]);
				        	int verse = Convert.ToInt16(values[VERSE]);
				        	int word = Convert.ToInt16(values[WORD]);
				        	
				        	// Page Update
				        	if (chapter == pageData[page - 1][SURA] && verse == pageData[page - 1][AYA])
				        		    page++;
				        	
				        	// Quarter Update
				        	if (chapter == quarterData[quarter - 1][SURA] && verse == quarterData[quarter - 1][AYA])
				        		    quarter++;
				        	
				        	// Bowing Update
				        	if (chapter == rukuData[bowing - 1][SURA] && verse == rukuData[bowing - 1][AYA])
				        		    bowing++;
				        	
				        	// Prostration Update
				        	if (chapter == Convert.ToInt16(sajdaData[prostration - 1][SURA]) && verse == Convert.ToInt16(sajdaData[prostration - 1][AYA]))
				        		    prostration++;
				        	
				        	// Seventh Update
				        	if (chapter == manzilData[seventh - 1][SURA] && verse == manzilData[seventh - 1][AYA])
				        		    seventh++;
				        	
				        	int basmallahs = chapter - (chapter >= 9 ? 1 : 0) - 1;
				        	
				        	// Close Previous Verse Node
				        	if (word == 1 && !(chapter == 1 && verse == 1))
				        	{
				        		output.WriteLine("\t\t</verse>");
				        		
				        		verseCount++;
				        	}
				        	// Close Basmallah
				        	else if (word == 5 && verse == 1 && chapter != 1 && chapter != 9)
				        		output.WriteLine("\t\t</verse>");
				        	
				        	// Make Chapter Node
				        	if (verse == 1 && word == 1)
				        	{
				        		if (chapter != 1)
				        			output.WriteLine("\t</chapter>");
				        		
				        		output.WriteLine("\t<chapter index=\"" + chapter + "\">");
				        		
				        		output.WriteLine("\t\t<name>");
				        		{
				        			output.WriteLine("\t\t\t<arabic>" + suraData[chapter - 1][ARABIC_NAME] + "</arabic>");
				        			output.WriteLine("\t\t\t<english>" + suraData[chapter - 1][ENGLISH_NAME] + "</english>");
				        		}
				        		output.WriteLine("\t\t</name>");
				        		output.WriteLine("\t\t<basmallah>" + (chapter == 1 || chapter == 9 ? "false" : "true") + "</basmallah>");
				        		output.WriteLine("\t\t<origin>" + suraData[chapter - 1][ORIGIN].ToLower() + "</origin>");
				        		output.WriteLine("\t\t<page>" + page + "</page>");
				        		output.WriteLine("\t\t<part>" + (((quarter - 1) / 8) + 1) + "</part>");
				        		output.WriteLine("\t\t<group local=\"" + ((quarter - 1) % 2 + 1) + "\">" + (((quarter - 1) / 4) + 1) + "</group>");
				        		output.WriteLine("\t\t<quarter local=\"" + ((quarter - 1) % 4 + 1) + "\">" + quarter + "</quarter>");
				        		output.WriteLine("\t\t<bowing>" + bowing + "</bowing>");
				        		output.WriteLine("\t\t<prostration>" + prostration + "</prostration>");
				        		output.WriteLine("\t\t<seventh>" + seventh + "</seventh>");
				        		
				        		wordChapterCount = 1;
				        	}
				        	
				        	// Make Verse Node
				        	if (word == 1 || (word == 5 && verse == 1 && chapter != 1 && chapter != 9))
				        	{
				        		// Make Basmallah verse 0
				        		if (word == 1 && verse == 1 && chapter != 1 && chapter != 9)
				        			output.Write("\t\t<verse index=\"0\" ");
				        		else
				        			output.Write("\t\t<verse index=\"" + verse + "\" ");
				        		
				        		output.Write("total=\"" + verseCount + "\" ");
				        		output.WriteLine("total_basmallah=\"" + (verseCount + basmallahs) + "\">");
				        		output.WriteLine("\t\t\t<page>" + page + "</page>");
				        		output.WriteLine("\t\t\t<origin></origin>");
				        		output.WriteLine("\t\t\t<part>" + (((quarter - 1) / 8) + 1) + "</part>");
				        		output.WriteLine("\t\t\t<group local=\"" + ((quarter - 1) % 2 + 1) + "\">" + (((quarter - 1) / 4) + 1) + "</group>");
				        		output.WriteLine("\t\t\t<quarter local=\"" + ((quarter - 1) % 4 + 1) + "\">" + quarter + "</quarter>");
				        		output.WriteLine("\t\t\t<bowing>" + bowing + "</bowing>");
				        		output.WriteLine("\t\t\t<prostration>" + prostration + "</prostration>");
				        		output.WriteLine("\t\t\t<seventh>" + seventh + "</seventh>");
				        	}
				        	
				        	// Make Word Node
				        	output.Write("\t\t\t<word index=\"" + (word - (word > 4 && verse == 1 && chapter != 1 && chapter != 9 ? 4 : 0)) + "\" ");
				        	{
				        		output.Write("chapter=\"" + (wordChapterCount - (chapter != 1 && chapter != 9 ? (verse == 1 && word <= 4 ? word : 4) : 0)) + "\" ");
				        		output.Write("chapter_basmallah=\"" + wordChapterCount + "\" ");
				        		output.Write("total=\"" + (wordCount - (basmallahs * 4) + (chapter != 1 && chapter != 9 && verse == 1 && word <= 4 ? 4 - word : 0)) + "\" ");
				        		output.WriteLine("total_basmallah=\"" + wordCount + "\">");
				        		output.WriteLine("\t\t\t\t<arabic>" + values[ARABIC] + "</arabic>");
				        		output.WriteLine("\t\t\t\t<english>" + values[ENGLISH] + "</english>");
				        		output.WriteLine("\t\t\t\t<occurance>" + values[OCCURANCE] + "</occurance>");
				        		
				        		output.WriteLine("\t\t\t\t<root>");
				        		{
				        			output.WriteLine("\t\t\t\t\t<arabic>" + values[ROOT] + "</arabic>");
				        			output.WriteLine("\t\t\t\t\t<english>  </english>");
				        		}
				        		output.WriteLine("\t\t\t\t</root>");
				        		
				        		// Word Grammar
				        		//output.WriteLine("\t\t\t\t<the>" + (tag == "DET" ? "true" : "false") + "</the>");
				        		//output.WriteLine("\t\t\t\t<and>" + (form == "wa" || form == "w~a" ? "true" : "false") + "</and>");
				        		output.WriteLine("\t\t\t\t<the>" + (values[TYPE].Contains("Determiner al") ? "true" : "false") + "</the>");
				        		output.WriteLine("\t\t\t\t<and>" + (values[TYPE].Contains("Conjunction wa") ? "true" : "false") + "</and>");
				        		
				        		for (int j = 1; j < 99 && word == rootWord && !roots.EndOfStream; j++)
				        		{
				        			string type = "";
				        			
				        			if (tag == "N")
				        				type = "noun";
				        			else if (tag == "V")
				        				type = "verb";
				        			
				        			output.WriteLine("\t\t\t\t<segment index = \"" + j + "\">");
					        		{
				        				output.WriteLine("\t\t\t\t\t<stem>" + (features.Contains("STEM") ? "true" : "false") + "</stem>");
					        			output.WriteLine("\t\t\t\t\t<type>" + type + "</type>");
					        			output.WriteLine("\t\t\t\t\t<case>  </case>");
					        			output.WriteLine("\t\t\t\t\t<number>  </number>");
					        			output.WriteLine("\t\t\t\t\t<gender>  </gender>");
					        		}
					        		output.WriteLine("\t\t\t\t</segment>");
					        		
					        		// Get next line
					        		if (!roots.EndOfStream)
					        		{
					        			grammar = roots.ReadLine().Split((char) 9);
								        
								        rootWord = Convert.ToInt16(grammar[LOCATION].Replace("(", "").Split(':')[2]);
								        form = grammar[FORM];
					        			tag = grammar[TAG];
								        features = new List<string>(grammar[FEATURES].Split('|'));
					        		}
				        		}
				        		/*
				        		// Letters (BUGGY - need to get diacritics implemented)
//			        			for (int j = 0; j < values[ARABIC].Length; j++)
//			        			{
//			        				output.WriteLine("\t\t\t\t<letter index = \"" + j + 1 + "\">");
//					        		{
//			        					output.WriteLine("\t\t\t\t\t<text>" + values[ARABIC][j] + "</text>");
//					        		}
//					        		output.WriteLine("\t\t\t\t</letter>");
//			        			}
				        		
				        		
				        		*/
				        		wordCount++;
				        		wordChapterCount++;
				        	}
				        	
				        	// Close Word Node
				        	output.WriteLine("\t\t\t</word>");
				        	
				        	//Console.WriteLine("word: " + "\"" + i + "\"");
				        }
			        	
			        	// Close Final Verse Node
			        	output.WriteLine("\t\t</verse>");
			        	
			        	// Close Final Chapter Node
			        	output.WriteLine("\t</chapter>");
			        		
				        // Close Quran Node
				        output.WriteLine("</quran>");
					}
			
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey(true);
		}
		
		public static void GetData()
		{
			// Quran 12:39 and 12:41: first word has extra empty character; they were removed
			// Get text from quran input
//			const string path = @".\..\..\Files\quran.txt";
			const string path = @".\..\..\Files\arabic.txt";
			var input = new StreamReader(path, Encoding.UTF8, true);
			string line = "";
			string quran = "";
			int lineCounter = 1;
			int verseCounter = 0;
			int lineStart = 1;
//			int lineStart = 4847; // moon miracle
			int lineEnd = 6236;
			
			int wordCounter = 0;
			int letterCounter = 0;
			
			while((line = input.ReadLine()) != null)
			{
				if (lineCounter >= lineStart && lineCounter <= lineEnd)
				{
					quran += line;
					//quran += line + " ";
					Console.WriteLine("string: \"" + line + '\"');
					//Console.WriteLine("verse " + (lineCounter - 7) + ": " + (wordCounter += HexToWords(StrToHex(line)).Count) + " words");
					//Console.WriteLine("verse " + lineCounter + ": " + (letterCounter += Arabic.LetterCount(HexToWords(StrToHex(line)))) + " letters");
					verseCounter++;
				}
				
				lineCounter++;
			}
			
			// Copy string to quran objects
			var hex = StrToHex(quran);
			var ascii = HexToASCII(hex);
			var words = HexToWords(hex);
			
			var verse = new Quran.Verse(ascii, Quran.Chapter.Name.NONE, 0);
			
			Console.WriteLine("ascii count: " + ascii.Count);
			Console.WriteLine("\nletter: " + new Arabic.Letter(ascii));
			Console.WriteLine("\tchar count: " + new Arabic.Letter(ascii).CharCount());
			Console.WriteLine("word: " + new Arabic.Word(ascii));
			Console.WriteLine("\tchar count: " + new Arabic.Word(ascii).CharCount());
			Console.WriteLine("\nwords: " + verse.WordCount);
			Console.WriteLine("\nletters: " + verse.LetterCount);
			Console.WriteLine("\nchars: " + verse.CharCount);
			Console.WriteLine("\tfirst word: " + verse[0]);
			Console.WriteLine("\tlast word: " + verse[verse.WordCount - 1]);
			
			
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		public static void Main(string[] args)
		{
			GetData();
		}
	}
}