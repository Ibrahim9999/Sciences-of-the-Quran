using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// False verses 9:128-129

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
		
		public static List<Arabic.Word> HexToWords(List<string> hex)
		{
			var words = new List<Arabic.Word>();
			var word = new Arabic.Word();
			
			for (int i = 0; i < hex.Count; i++)
				if (hex[i] != "0020")
					word.AddLetter(new Arabic.Letter(hex[i]));
				else if (word.Count != 0)
				{
					words.Add(word);
					word = new Arabic.Word();
				}
			
			return words;
		}
		
		public static List<string> RemoveDiacritics(List<string> hexInput)
		{
			var output = new List<string>();
			
			for (int i = 0; i < hexInput.Count; i++)
				if (Arabic.Letter.IsValidChar((char) int.Parse(hexInput[i], System.Globalization.NumberStyles.HexNumber), false, false, true))
				    output.Add(hexInput[i]);
			
			return output;
		}
		public static string RemoveDiacritics(string input)
		{
			var output = "";
			
			for (int i = 0; i < input.Length; i++)
				if (Arabic.Letter.IsValidChar(input[i], false, false, true))
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
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Position.NONE, Arabic.Diacritic.HAMZA_ABOVE),
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
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Position.NONE, Arabic.Diacritic.NONE),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.SHEEN),
					new Arabic.Letter(Arabic.Character.HA),
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.RA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Position.NONE, Arabic.Diacritic.NONE),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Position.NONE, Arabic.Diacritic.HAMZA_ABOVE),
					new Arabic.Letter(Arabic.Character.SHEEN),
					new Arabic.Letter(Arabic.Character.HA),
					new Arabic.Letter(Arabic.Character.RA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.WAW),
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Position.NONE, Arabic.Diacritic.NONE),
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
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Position.NONE, Arabic.Diacritic.HAMZA_ABOVE),
					new Arabic.Letter(Arabic.Character.SHEEN),
					new Arabic.Letter(Arabic.Character.HA),
					new Arabic.Letter(Arabic.Character.RA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.BA),
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Position.NONE, Arabic.Diacritic.NONE),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.SHEEN),
					new Arabic.Letter(Arabic.Character.HA),
					new Arabic.Letter(Arabic.Character.RA),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Position.NONE, Arabic.Diacritic.NONE),
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
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Position.NONE, Arabic.Diacritic.HAMZA_ABOVE),
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
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Position.NONE, Arabic.Diacritic.HAMZA_ABOVE),
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
					new Arabic.Letter(Arabic.Character.HA, Arabic.Position.NONE, Arabic.Diacritic.MARBUTAH),
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
					new Arabic.Letter(Arabic.Character.ALIF, Arabic.Position.NONE, Arabic.Diacritic.HAMZA_ABOVE),
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
					new Arabic.Letter(Arabic.Character.HA, Arabic.Position.NONE, Arabic.Diacritic.MARBUTAH),
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
					new Arabic.Letter(Arabic.Character.HA, Arabic.Position.NONE, Arabic.Diacritic.MARBUTAH),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HAMZA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.KHA),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.HA, Arabic.Position.NONE, Arabic.Diacritic.MARBUTAH),
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
					new Arabic.Letter(Arabic.Character.HA, Arabic.Position.NONE, Arabic.Diacritic.MARBUTAH),
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
					new Arabic.Letter(Arabic.Character.HA, Arabic.Position.NONE, Arabic.Diacritic.MARBUTAH),
				}),
				new Arabic.Word(new List<Arabic.Letter>
				{
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.LAM),
					new Arabic.Letter(Arabic.Character.HAMZA),
					new Arabic.Letter(Arabic.Character.ALIF),
					new Arabic.Letter(Arabic.Character.KHA),
					new Arabic.Letter(Arabic.Character.RA),
					new Arabic.Letter(Arabic.Character.HA, Arabic.Position.NONE, Arabic.Diacritic.MARBUTAH),
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
					new Arabic.Letter(Arabic.Character.TA, Arabic.Position.NONE, Arabic.Diacritic.MARBUTAH),
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
		
		public static void Main(string[] args)
		{
			// Get text from quran file
			const string path = @".\..\..\Files\quran.txt";
//			const string path = @".\..\..\Files\arabic.txt";
			var file = new StreamReader(path, Encoding.UTF8, true);
			string line = "";
			string quran = "";
			int lineCounter = 1;
			int verseCounter = 0;
			int lineStart = 1;
//			int lineStart = 4847; // moon miracle
			int lineEnd = 6236;
			
			while((line = file.ReadLine()) != null)
			{
				if (lineCounter >= lineStart && lineCounter <= lineEnd)
				{
					quran += line + " ";
					verseCounter++;
				}
				
				lineCounter++;
			}
			
			// Copy string to quran objects
			var hex = StrToHex(quran);
			var words = HexToWords(hex);
			
			Console.WriteLine("verse count: " + verseCounter);
			Console.WriteLine("word count: " + words.Count);
			Console.WriteLine("letter count: " + Arabic.LetterCount(words));
			Console.WriteLine("abjad value: " + Arabic.AbjadValue(words));
			
			//Console.WriteLine("\nya count: " + Arabic.LetterCount(words, new Arabic.Letter(Arabic.Character.YA)));
			
			
			
			//AllahCount(words);
			
			//MonthCount(words);
			
			//YearCount(words);
			
			//SeaCount(words);
			
			//ResurrectionCount(words);
			
			//WhiteCount(words);
			
			//ThisWorldCount(words);
			//LastWorldCount(words);
			
			//SabbathCount(words);
			
			TrumpetCount(words);
			
			
			
			
			
			
			
			/*
			var pi = new List<int> {3,1,4,1,5,9,2,6,5,3,5,8,9,7,9};
			var phi = new List<int> {1,6,1,8,0,3,3,9};
			
			for (int i = 0; i < phi.Count; i++)
				Console.WriteLine((Arabic.Character) phi[i]);
			
			Console.WriteLine("value of ts'ah 'ashr: " + Arabic.AbjadValue(new List<Arabic.Letter> { new Arabic.Letter(Arabic.Character.TA), new Arabic.Letter(Arabic.Character.SEEN), new Arabic.Letter(Arabic.Character.AYN), new Arabic.Letter(Arabic.Character.TA), new Arabic.Letter(Arabic.Character.AYN), new Arabic.Letter(Arabic.Character.SHEEN), new Arabic.Letter(Arabic.Character.RA) }));
			Console.WriteLine("value of sqr: " + Arabic.AbjadValue(new List<Arabic.Letter> { new Arabic.Letter(Arabic.Character.SEEN), new Arabic.Letter(Arabic.Character.QAF), new Arabic.Letter(Arabic.Character.RA) }));
			*/
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}