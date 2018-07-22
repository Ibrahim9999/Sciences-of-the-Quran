using System;
using System.Collections.Generic;

namespace Quran_Word_Search
{
	/// <summary>
	/// Description of Arabic.
	/// </summary>
	//adam mentioned 25 times
	public static class Arabic
	{
		public class Letter
		{
			public Arabic.Character character;
			public Arabic.Position position;
			public Arabic.Diacritic diacritic;
			int ascii;
			string hex;
			
			public Letter()
			{
				character = Arabic.Character.NONE;
				position = Arabic.Position.NONE;
				diacritic = Arabic.Diacritic.NONE;
				ascii = 0;
			}
			public Letter(int ascii)
			{
				character = Arabic.ASCIIToLetter(ascii).character;
				position = Arabic.ASCIIToLetter(ascii).position;
				diacritic = Arabic.ASCIIToLetter(ascii).diacritic;
				this.ascii = ascii;
				this.hex = Arabic.LetterToHex(character, position, diacritic);
			}
			public Letter(string hex)
			{
				character = Arabic.HexToLetter(hex).character;
				position = Arabic.HexToLetter(hex).position;
				diacritic = Arabic.HexToLetter(hex).diacritic;
				this.ascii = Arabic.LetterToASCII(character, position, diacritic);
				this.hex = hex;
			}
			public Letter(Arabic.Character character)
			{
				this.character = character;
				position = Arabic.Position.NONE;
				diacritic = Arabic.Diacritic.NONE;
				ascii = Arabic.LetterToASCII(character, position, diacritic);
				hex = Arabic.LetterToHex(character, position, diacritic);
			}
			public Letter(Arabic.Character character, Arabic.Position position, Arabic.Diacritic diacritic)
			{
				this.character = character;
				this.position = position;
				this.diacritic = diacritic;
				ascii = Arabic.LetterToASCII(character, position, diacritic);
				hex = Arabic.LetterToHex(character, position, diacritic);
			}
			
			public override string ToString()
			{
				//return string.Format("{0}", character);
				//return string.Format("[{0}, {1}]", character, diacritic);
				return string.Format("[{0}{1}]", character, diacritic == Arabic.Diacritic.NONE ? "" : ", " + diacritic);
			}
			
			public static bool operator == (Letter a, Letter b)
			{
				return a.hex == b.hex;
			}
			
			public static bool operator != (Letter a, Letter b)
			{
				return !(a == b);
			}
	
			public int ASCII
			{
				get { return ascii; }
			}
	
			public string Hex
			{
				get { return hex; }
			}
			
			public static bool IsValidChar(char character, bool checkAll, bool checkDiacritic, bool checkSpace)
			{
			    if (character >= 0x600 && character <= 0x60f && checkAll)
			        return true;
				
			    if (character >= 0x610 && character <= 0x61a && checkDiacritic)
			        return true;
				
			    if (character >= 0x61b && character <= 0x620 && checkAll)
			        return true;
				
			    if (character >= 0x621 && character <= 0x63a)
			        return true;
				
			    if (character >= 0x63b && character <= 0x640 && checkAll)
			        return true;
				
			    if (character >= 0x641 && character <= 0x064a)
			        return true;
				
			    if (character >= 0x64b && character <= 0x65f && checkDiacritic)
			        return true;
				
			    if (character >= 0x660 && character <= 0x6ff && checkAll) // probably could be split up more
			        return true;
			    
			    return character == ' ' && checkSpace;
			}
		}
		
		public class Word
		{
			readonly List<Arabic.Letter> letters;
			
			public Word()
			{
				letters = new List<Arabic.Letter>();
			}
			public Word(List<Arabic.Letter> letters)
			{
				this.letters = letters;
			}
			
			public enum Type
			{
				NONE = 0,
				NOUN,
				VERB,
				ADJECTIVE,
				ADVERB,
				PRONOUN
			}
			
			public enum Conjugation
			{
				NONE = 0,
				ROOT,
				PAST,
				PRESENT,
				FUTURE,
				COMMAND
			}
			
			public enum Person
			{
				NONE = 0,
				FIRST,
				SECOND,
				THIRD
			}
			
			public enum Plurality
			{
				NONE = 0,
				SINGULAR,
				DUAL,
				PLURAL
			}
			
			public enum GrammatialGender
			{
				NONE = 0,
				MASCULINE,
				FEMININE
			}
			
			public override string ToString()
			{
				string s = "";
				
				for (int i = 0; i < letters.Count; i++)
					//s += letters[i];
					s += Arabic.SoundOfLetter(letters[i]);
				
				return s;
			}
			
			public static bool operator == (Word a, Word b)
			{
				if (a.Count != b.Count)
					return false;
				
				for (int i = 0; i < a.Count; i++)
					if (a[i] != b[i])
						return false;
				
				return true;
			}
			
			public static bool operator != (Word a, Word b)
			{
				return !(a == b);
			}
			
			public List<Arabic.Letter> Letters
			{
				get
				{
					return letters;
				}
			}
			
			public Arabic.Letter this[int index]
			{
				get { return letters[index]; }
			}
			
			public int Count
			{
				get
				{
					return letters.Count;
				}
			}
			
			public void AddLetter(Arabic.Letter letter)
			{
				letters.Add(letter);
			}
			
			public void InsertLetter(int index, Arabic.Letter letter)
			{
				
				
				letters.Insert(index, letter);
			}
			
			public void RemoveLetter(Arabic.Letter letter)
			{
				letters.Remove(letter);
			}
			
			public void RemoveLetterAt(int index)
			{
				letters.RemoveAt(index);
			}
		}
		
		public class Dictionary
		{
			
		}
		
		public enum Character
		{
			NONE = 0,
			SPACE = 0,
			HAMZA = -1,
			ALIF = 1,
			BA = 2,
			JEEM = 3,
			DAL = 4,
			HA = 5,
			WAW = 6,
			ZAYN = 7,
			HHA = 8,
			TTA = 9,
			YA = 10,
			KAF = 20,
			LAM = 30,
			MEEM = 40,
			NOON = 50,
			SEEN = 60,
			AYN = 70,
			FA = 80,
			SAD = 90,
			QAF = 100,
			RA = 200,
			SHEEN = 300,
			TA = 400,
			THA = 500,
			KHA = 600,
			DHAL = 700,
			DAD = 800,
			THHA = 900,
			GHAYN = 1000,
			ONE,
			TWO,
			THREE,
			FOUR,
			FIVE,
			SIX,
			SEVEN,
			EIGHT,
			NINE,
			ZERO,
			SALA,
			QALA,
		}
		
		public enum Position
		{
			NONE = 0,
			ISOLATED,
			BEGINNING,
			MIDDLE,
			END
		}
		
		public enum Diacritic
		{
			NONE = 0,
			FATHAH,
			KASRAH,
			DOMMAH,
			SUKOON,
			SHADDAH,
			MADDAH,
			TANWEEN_FATHAH,
			TANWEEN_KASRAH,
			TANWEEN_DOMMAH,
			HAMZA_ABOVE,
			HAMZA_BELOW,
			ALIF_WASLAH,
			ALIF_KHANJARIYAH,
			ALIF_MAQSURAH,
			MARBUTAH,
		}
		
		public static int LetterToASCII(Arabic.Letter letter)
		{
			return LetterToASCII(letter.character, letter.position, letter.diacritic);
		}
		
		public static int LetterToASCII(Character character, Position position, Diacritic diacritic)
		{
			// https://www.andmine.com/ascii-codes-html-table-reference/index.php?start=1000
			switch (character)
			{
				case Character.SPACE:
					return 32;
				case Character.HAMZA:
					return 1569;
				case Character.ALIF:
					if (diacritic == Diacritic.HAMZA_ABOVE)
						return 1571;
					if (diacritic == Diacritic.HAMZA_BELOW)
						return 1573;
					if (diacritic == Diacritic.ALIF_WASLAH)
						return 1649;
					if (diacritic == Diacritic.MADDAH)
						return 1570;
					if (diacritic == Diacritic.ALIF_KHANJARIYAH)
						return 0;
					
					return 1575;
				case Character.BA:
						return 1576;
				case Character.TA:
						return 1578;
				case Character.THA:
						return 1579;
				case Character.JEEM:
						return 1580;
				case Character.HHA:
						return 1581;
				case Character.KHA:
						return 1582;
				case Character.DAL:
						return 1583;
				case Character.DHAL:
						return 1584;
				case Character.RA:
						return 1585;
				case Character.ZAYN:
						return 1586;
				case Character.SEEN:
						return 1587;
				case Character.SHEEN:
						return 1588;
				case Character.SAD:
						return 1589;
				case Character.DAD:
						return 1590;
				case Character.TTA:
						return 1591;
				case Character.THHA:
						return 1592;
				case Character.AYN:
						return 1593;
				case Character.GHAYN:
						return 1594;
				case Character.FA:
						return 1601;
				case Character.QAF:
						return 1602;
				case Character.KAF:
						return 1603;
				case Character.LAM:
						return 1604;
				case Character.MEEM:
						return 1605;
				case Character.NOON:
						return 1606;
				case Character.HA:
					if (diacritic == Diacritic.MARBUTAH)
						return 1577;
					
					return 1607;
				case Character.WAW:
					if (diacritic == Diacritic.HAMZA_ABOVE)
						return 1572;
						
					return 1608;
				case Character.YA:
					if (diacritic == Diacritic.HAMZA_ABOVE)
						return 1610;
					if (diacritic == Diacritic.ALIF_MAQSURAH)
						return 1574;
					
					return 1609;
			}
			
			return 0;
		}
		
		public static Arabic.Letter ASCIIToLetter(int ascii)
		{
			switch (ascii)
			{
				case 32:
					return new Arabic.Letter(Character.SPACE, Position.NONE, Diacritic.NONE);
				case 1569:
					return new Arabic.Letter(Character.HAMZA, Position.NONE, Diacritic.NONE);
				case 1570:
					return new Arabic.Letter(Character.ALIF, Position.NONE, Diacritic.MADDAH);
				case 1571:
					return new Arabic.Letter(Character.ALIF, Position.NONE, Diacritic.HAMZA_ABOVE);
				case 1572:
					return new Arabic.Letter(Character.WAW, Position.NONE, Diacritic.HAMZA_ABOVE);
				case 1573:
					return new Arabic.Letter(Character.ALIF, Position.NONE, Diacritic.HAMZA_BELOW);
				case 1574:
					return new Arabic.Letter(Character.YA, Position.NONE, Diacritic.HAMZA_ABOVE);
				case 1575:
					return new Arabic.Letter(Character.ALIF, Position.NONE, Diacritic.NONE);
				case 1576:
					return new Arabic.Letter(Character.BA, Position.NONE, Diacritic.NONE);
				case 1577:
					return new Arabic.Letter(Character.HA, Position.NONE, Diacritic.MARBUTAH);
				case 1578:
					return new Arabic.Letter(Character.TA, Position.NONE, Diacritic.NONE);
				case 1579:
					return new Arabic.Letter(Character.THA, Position.NONE, Diacritic.NONE);
				case 1580:
					return new Arabic.Letter(Character.JEEM, Position.NONE, Diacritic.NONE);
				case 1581:
					return new Arabic.Letter(Character.HHA, Position.NONE, Diacritic.NONE);
				case 1582:
					return new Arabic.Letter(Character.KHA, Position.NONE, Diacritic.NONE);
				case 1583:
					return new Arabic.Letter(Character.DAL, Position.NONE, Diacritic.NONE);
				case 1584:
					return new Arabic.Letter(Character.DHAL, Position.NONE, Diacritic.NONE);
				case 1585:
					return new Arabic.Letter(Character.RA, Position.NONE, Diacritic.NONE);
				case 1586:
					return new Arabic.Letter(Character.ZAYN, Position.NONE, Diacritic.NONE);
				case 1587:
					return new Arabic.Letter(Character.SEEN, Position.NONE, Diacritic.NONE);
				case 1588:
					return new Arabic.Letter(Character.SHEEN, Position.NONE, Diacritic.NONE);
				case 1589:
					return new Arabic.Letter(Character.SAD, Position.NONE, Diacritic.NONE);
				case 1590:
					return new Arabic.Letter(Character.DAD, Position.NONE, Diacritic.NONE);
				case 1591:
					return new Arabic.Letter(Character.TTA, Position.NONE, Diacritic.NONE);
				case 1592:
					return new Arabic.Letter(Character.THHA, Position.NONE, Diacritic.NONE);
				case 1593:
					return new Arabic.Letter(Character.AYN, Position.NONE, Diacritic.NONE);
				case 1594:
					return new Arabic.Letter(Character.GHAYN, Position.NONE, Diacritic.NONE);
				case 1601:
					return new Arabic.Letter(Character.FA, Position.NONE, Diacritic.NONE);
				case 1602:
					return new Arabic.Letter(Character.QAF, Position.NONE, Diacritic.NONE);
				case 1603:
					return new Arabic.Letter(Character.KAF, Position.NONE, Diacritic.NONE);
				case 1604:
					return new Arabic.Letter(Character.LAM, Position.NONE, Diacritic.NONE);
				case 1605:
					return new Arabic.Letter(Character.MEEM, Position.NONE, Diacritic.NONE);
				case 1606:
					return new Arabic.Letter(Character.NOON, Position.NONE, Diacritic.NONE);
				case 1607:
					return new Arabic.Letter(Character.HA, Position.NONE, Diacritic.NONE);
				case 1608:
					return new Arabic.Letter(Character.WAW, Position.NONE, Diacritic.NONE);
				case 1609:
					return new Arabic.Letter(Character.YA, Position.NONE, Diacritic.ALIF_MAQSURAH);
				case 1610:
					return new Arabic.Letter(Character.YA, Position.NONE, Diacritic.NONE);
				case 1649:
					return new Arabic.Letter(Character.ALIF, Position.NONE, Diacritic.ALIF_WASLAH);
			}
			
			return new Arabic.Letter();
		}
		
		public static string LetterToHex(Arabic.Letter letter)
		{
			return LetterToHex(letter.character, letter.position, letter.diacritic);
		}
		
		public static string LetterToHex(Character character, Position position, Diacritic diacritic)
		{
			// http://jrgraphix.net/r/Unicode/0600-06FF
			switch (character)
			{
				case Character.SPACE:
					return "0020";
				case Character.HAMZA:
					return "0621";
				case Character.ALIF:
					if (diacritic == Diacritic.HAMZA_ABOVE)
						return "0623";
					if (diacritic == Diacritic.HAMZA_BELOW)
						return "0625";
					if (diacritic == Diacritic.ALIF_WASLAH)
						return "0000";
					if (diacritic == Diacritic.MADDAH)
						return "0622";
					if (diacritic == Diacritic.ALIF_KHANJARIYAH)
						return "0000";
					
					return "0627";
				case Character.BA:
						return "0628";
				case Character.TA:
					if (diacritic == Diacritic.MARBUTAH)
						return "0629";
						
						return "062a";
				case Character.THA:
						return "062b";
				case Character.JEEM:
						return "062c";
				case Character.HHA:
						return "062d";
				case Character.KHA:
						return "062e";
				case Character.DAL:
						return "062f";
				case Character.DHAL:
						return "0630";
				case Character.RA:
						return "0631";
				case Character.ZAYN:
						return "0632";
				case Character.SEEN:
						return "0633";
				case Character.SHEEN:
						return "0634";
				case Character.SAD:
						return "0635";
				case Character.DAD:
						return "0636";
				case Character.TTA:
						return "0637";
				case Character.THHA:
						return "0638";
				case Character.AYN:
						return "0639";
				case Character.GHAYN:
						return "063a";
				case Character.FA:
						return "0641";
				case Character.QAF:
						return "0642";
				case Character.KAF:
						return "0643";
				case Character.LAM:
						return "0644";
				case Character.MEEM:
						return "0645";
				case Character.NOON:
						return "0646";
				case Character.HA:
					if (diacritic == Diacritic.MARBUTAH)
						return "0629";
					
					return "0647";
				case Character.WAW:
					if (diacritic == Diacritic.HAMZA_ABOVE)
						return "0624";
						
					return "0648";
				case Character.YA:
					if (diacritic == Diacritic.HAMZA_ABOVE)
						return "0626";
					if (diacritic == Diacritic.ALIF_MAQSURAH)
						return "0649";
					
					return "064a";
			}
			
			return "0000";
		}
		
		public static Arabic.Letter HexToLetter(string hex)
		{
			switch (hex)
			{
				case "0020":
					return new Arabic.Letter(Character.SPACE, Position.NONE, Diacritic.NONE);
				case "0621":
					return new Arabic.Letter(Character.HAMZA, Position.NONE, Diacritic.NONE);
				case "0622":
					return new Arabic.Letter(Character.ALIF, Position.NONE, Diacritic.MADDAH);
				case "0623":
					return new Arabic.Letter(Character.ALIF, Position.NONE, Diacritic.HAMZA_ABOVE);
				case "0624":
					return new Arabic.Letter(Character.WAW, Position.NONE, Diacritic.HAMZA_ABOVE);
				case "0625":
					return new Arabic.Letter(Character.ALIF, Position.NONE, Diacritic.HAMZA_BELOW);
				case "0626":
					return new Arabic.Letter(Character.YA, Position.NONE, Diacritic.HAMZA_ABOVE);
				case "0627":
					return new Arabic.Letter(Character.ALIF, Position.NONE, Diacritic.NONE);
				case "0628":
					return new Arabic.Letter(Character.BA, Position.NONE, Diacritic.NONE);
				case "0629":
					return new Arabic.Letter(Character.HA, Position.NONE, Diacritic.MARBUTAH);
				case "062a":
					return new Arabic.Letter(Character.TA, Position.NONE, Diacritic.NONE);
				case "062b":
					return new Arabic.Letter(Character.THA, Position.NONE, Diacritic.NONE);
				case "062c":
					return new Arabic.Letter(Character.JEEM, Position.NONE, Diacritic.NONE);
				case "062d":
					return new Arabic.Letter(Character.HHA, Position.NONE, Diacritic.NONE);
				case "062e":
					return new Arabic.Letter(Character.KHA, Position.NONE, Diacritic.NONE);
				case "062f":
					return new Arabic.Letter(Character.DAL, Position.NONE, Diacritic.NONE);
				case "0630":
					return new Arabic.Letter(Character.DHAL, Position.NONE, Diacritic.NONE);
				case "0631":
					return new Arabic.Letter(Character.RA, Position.NONE, Diacritic.NONE);
				case "0632":
					return new Arabic.Letter(Character.ZAYN, Position.NONE, Diacritic.NONE);
				case "0633":
					return new Arabic.Letter(Character.SEEN, Position.NONE, Diacritic.NONE);
				case "0634":
					return new Arabic.Letter(Character.SHEEN, Position.NONE, Diacritic.NONE);
				case "0635":
					return new Arabic.Letter(Character.SAD, Position.NONE, Diacritic.NONE);
				case "0636":
					return new Arabic.Letter(Character.DAD, Position.NONE, Diacritic.NONE);
				case "0637":
					return new Arabic.Letter(Character.TTA, Position.NONE, Diacritic.NONE);
				case "0638":
					return new Arabic.Letter(Character.THHA, Position.NONE, Diacritic.NONE);
				case "0639":
					return new Arabic.Letter(Character.AYN, Position.NONE, Diacritic.NONE);
				case "063a":
					return new Arabic.Letter(Character.GHAYN, Position.NONE, Diacritic.NONE);
				case "0641":
					return new Arabic.Letter(Character.FA, Position.NONE, Diacritic.NONE);
				case "0642":
					return new Arabic.Letter(Character.QAF, Position.NONE, Diacritic.NONE);
				case "0643":
					return new Arabic.Letter(Character.KAF, Position.NONE, Diacritic.NONE);
				case "0644":
					return new Arabic.Letter(Character.LAM, Position.NONE, Diacritic.NONE);
				case "0645":
					return new Arabic.Letter(Character.MEEM, Position.NONE, Diacritic.NONE);
				case "0646":
					return new Arabic.Letter(Character.NOON, Position.NONE, Diacritic.NONE);
				case "0647":
					return new Arabic.Letter(Character.HA, Position.NONE, Diacritic.NONE);
				case "0648":
					return new Arabic.Letter(Character.WAW, Position.NONE, Diacritic.NONE);
				case "0649":
					return new Arabic.Letter(Character.YA, Position.NONE, Diacritic.ALIF_MAQSURAH);
				case "064a":
					return new Arabic.Letter(Character.YA, Position.NONE, Diacritic.NONE);
				case "0671":
					return new Arabic.Letter(Character.ALIF, Position.NONE, Diacritic.ALIF_WASLAH);
			}
			
			return new Arabic.Letter();
		}
		
		public static string SoundOfLetter(Arabic.Letter letter)
		{
			return SoundOfLetter(letter.character, letter.diacritic);
		}
		
		public static string SoundOfLetter(Character character, Diacritic diacritic)
		{
			switch (character)
			{
				case Character.SPACE:
					return " ";
				case Character.HAMZA:
					return "'";
				case Character.ALIF:
					if (diacritic == Diacritic.HAMZA_BELOW)
						return "EE";
					if (diacritic == Diacritic.ALIF_WASLAH)
						return "";
					if (diacritic == Diacritic.MADDAH)
						return "AA";
					
					return "A";
				case Character.BA:
						return "B";
				case Character.TA:
						return "T";
				case Character.THA:
						return "TH";
				case Character.JEEM:
						return "J";
				case Character.HHA:
						return "HH";
				case Character.KHA:
						return "KH";
				case Character.DAL:
						return "D";
				case Character.DHAL:
						return "DH";
				case Character.RA:
						return "R";
				case Character.ZAYN:
						return "Z";
				case Character.SEEN:
						return "S";
				case Character.SHEEN:
						return "SH";
				case Character.SAD:
						return "SS";
				case Character.DAD:
						return "DD";
				case Character.TTA:
						return "TT";
				case Character.THHA:
						return "THH";
				case Character.AYN:
						return "AY";
				case Character.GHAYN:
						return "GH";
				case Character.FA:
						return "F";
				case Character.QAF:
						return "Q";
				case Character.KAF:
						return "K";
				case Character.LAM:
						return "L";
				case Character.MEEM:
						return "M";
				case Character.NOON:
						return "N";
				case Character.HA:
					if (diacritic == Diacritic.MARBUTAH)
						return "T";
					
					return "H";
				case Character.WAW:
					if (diacritic == Diacritic.HAMZA_ABOVE)
						return "'WOO";
						
					return "W";
				case Character.YA:
					if (diacritic == Diacritic.HAMZA_ABOVE)
						return "'";
					if (diacritic == Diacritic.ALIF_MAQSURAH)
						return "AA";
					
					return "Y";
			}
			
			return "";
		}
		
		public static int AbjadValue(Arabic.Letter letter)
		{
			if (letter.character == Character.HA && letter.diacritic == Diacritic.MARBUTAH)
				return (int) Character.TA;
			
			return Math.Abs((int) letter.character);
		}
		
		public static int AbjadValue(List<Arabic.Letter> letters)
		{
			int n = 0;
			
			for (int i = 0; i < letters.Count; i++)
				n += AbjadValue(letters[i]);
			
			return n;
		}
		
		public static int AbjadValue(Arabic.Word word)
		{
			int n = 0;
			
			for (int i = 0; i < word.Count; i++)
				n += AbjadValue(word[i]);
			
			return n;
		}
		
		public static int AbjadValue(List<Arabic.Word> phrase)
		{
			int n = 0;
			
			for (int i = 0; i < phrase.Count; i++)
				n += AbjadValue(phrase[i]);
			
			return n;
		}
		
		public static int LetterCount(List<Arabic.Word> phrase)
		{
			int n = 0;
			
			for (int i = 0; i < phrase.Count; i++)
				n += phrase[i].Count;
			
			return n;
		}
		
		public static int LetterCount(Arabic.Word word, Arabic.Letter letter)
		{
			int n = 0;
			
			for (int i = 0; i < word.Count; i++)
			{//Console.WriteLine("Testing " + word[i] + " (" + word[i].Hex + ") with " + letter + " (" + letter.Hex + "): " + (word[i] == letter));
				if (word[i] == letter)
					n++;
			}
			
			return n;
		}
		
		public static int LetterCount(List<Arabic.Word> phrase, Arabic.Letter letter)
		{
			int n = 0;
			
			for (int i = 0; i < phrase.Count; i++)
				n += LetterCount(phrase[i], letter);
			
			return n;
		}
		
		public static int SearchQuantity(List<Arabic.Word> phrase, Arabic.Word word)
		{
			int n = 0;
			
			foreach (Arabic.Word a in phrase)
				if (a == word)
					n++;
			
			return n;
		}
		
		public static int SearchQuantity(List<Arabic.Word> phrase, List<Arabic.Word> words)
		{
			int n = 0;
			
			foreach (Arabic.Word word in words)
				n += SearchQuantity(phrase, word);
			
			return n;
		}
	}
}
