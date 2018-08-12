using System;
using System.Collections.Generic;

namespace Quran_Word_Search
{
	/// <summary>
	/// Description of Arabic.
	/// </summary>
	public static class Arabic
	{
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
			DAMMAH,
			SUKOON,
			SHADDAH,
			MADDAH,
			TANWEEN_FATHAH,
			TANWEEN_KASRAH,
			TANWEEN_DAMMAH,
			HAMZA_ABOVE,
			HAMZA_BELOW,
			HAMZA_MIDDLE,
			ALIF_WASLAH,
			ALIF_KHANJARIYAH,
			ALIF_MAQSURAH,
			MARBUTAH,
		}
		
		public class Letter
		{
			Character character;
			Diacritic modification;
			int asciiCharacter;
			string hexCharacter;
			List<Diacritic> diacritics;
			List<int> asciiDiacritics;
			List<string> hexDiacritics;
			
			public Letter()
			{
				RemoveCharacter();
				RemoveDiacritics();
			}
			public Letter(int ascii)
			{
				ChangeCharacter(ascii);
				RemoveDiacritics();
			}
			public Letter(string hex)
			{
				ChangeCharacter(hex);
				RemoveDiacritics();
			}
			public Letter(List<int> ascii)
			{
				var temp = new List<int>(ascii);
				
				RemoveDiacritics();
				int i = 0;
				
				if (temp.Count != 0 && IsArabic((char) temp[i], true, true, false, false))
				{
					// If the first value is a character
					if (IsCharacter((char) temp[i]))
					{
						ChangeCharacter(temp[i]);
						
						i++;
					}
					
					// Loop through ascii list until a space, character, or invalid value is found
					for (; i < temp.Count && IsDiacritic((char) temp[i]); i++)
						AddDiacritic(temp[i]);
				}
			}
			public Letter(List<string> hex)
			{
				var temp = new List<string>(hex);
				
				RemoveDiacritics();
				int i = 0;
				
				if (IsArabic((char) Convert.ToInt16(temp[i]), true, true, false, false))
				{
					// If the first value is a character
					if (IsCharacter((char) Convert.ToInt16(temp[i])))
					{
						ChangeCharacter(temp[i]);
						
						i++;
					}
					
					// Loop through ascii list until a space, character, or invalid value is found
					for (; i < temp.Count && IsDiacritic((char) Convert.ToInt16(temp[i])); i++)
						AddDiacritic(temp[i]);
				}
			}
			public Letter(Character character)
			{
				ChangeCharacter(character);
				RemoveDiacritics();
			}
			public Letter(Character character, Diacritic modification)
			{
				ChangeCharacter(character, modification);
				RemoveDiacritics();
			}
			
			public override string ToString()
			{
				//return string.Format("{0}", character);
				//return string.Format("[{0}, {1}]", character, modification);
				//return string.Format("[{0}{1}]", character, modification == Diacritic.NONE ? "" : ", " + modification);
				
				string s = string.Format("[{0}{1}", character, modification == Diacritic.NONE ? "" : "-" + modification);
				
				if (diacritics.Count != 0)
					for (int i = 0; i < diacritics.Count; i++)
						s += string.Format(", {0}", diacritics[i]);
				
				s += ']';
				
				return s;
			}
			
			public override bool Equals(object obj)
			{
				var other = obj as Letter;
				
				if (other == null)
					return false;
				
				return character == other.character && modification == other.modification && asciiCharacter == other.asciiCharacter && hexCharacter == other.hexCharacter && object.Equals(diacritics, other.diacritics) && object.Equals(asciiDiacritics, other.asciiDiacritics) && object.Equals(hexDiacritics, other.hexDiacritics);
			}

			public override int GetHashCode()
			{
				int hashCode = 0;
				
				unchecked
				{
					hashCode += 1000000007 * character.GetHashCode();
					hashCode += 1000000009 * modification.GetHashCode();
					hashCode += 1000000021 * asciiCharacter.GetHashCode();
					if (hexCharacter != null)
						hashCode += 1000000033 * hexCharacter.GetHashCode();
					if (diacritics != null)
						hashCode += 1000000087 * diacritics.GetHashCode();
					if (asciiDiacritics != null)
						hashCode += 1000000093 * asciiDiacritics.GetHashCode();
					if (hexDiacritics != null)
						hashCode += 1000000097 * hexDiacritics.GetHashCode();
				}
				
				return hashCode;
			}
			
			public int CharCount(bool asciiCount = false)
			{
				return (asciiCharacter == 0 ? 0 : 1) + (!asciiCount && (modification == Diacritic.HAMZA_ABOVE || modification == Diacritic.HAMZA_BELOW || modification == Diacritic.ALIF_WASLAH) ? 1 : 0) + asciiDiacritics.Count;
			}
			
			public Character Character
			{
				get { return character; }
				set { ChangeCharacter(value); }
			}
			
			public Diacritic Modification
			{
				get { return modification; }
				set { ChangeCharacter(character, value); }
			}
			
			public int ASCIICharacter
			{
				get { return asciiCharacter; }
			}
			
			public string HexCharacter
			{
				get { return hexCharacter; }
			}
			
			public List<Diacritic> Diacritics
			{
				get { return diacritics; }
				set
				{
					diacritics = value;
					asciiDiacritics = new List<int>();
					hexDiacritics = new List<string>();
				}
			}
			
			public List<int> ASCIIDiacritics
			{
				get { return asciiDiacritics; }
			}
			
			public List<string> HexDiacritics
			{
				get { return hexDiacritics; }
			}
			
			public List<int> ASCII
			{
				get
				{
					var ascii = new List<int>();
					
					if (asciiCharacter != 0)
						ascii.Add(asciiCharacter);
					
					ascii.AddRange(asciiDiacritics);
					
					return ascii;
				}
			}
			
			public List<string> Hex
			{
				get
				{
					var hex = new List<string>();
					
					if (asciiCharacter != 0)
						hex.Add(hexCharacter);
					
					hex.AddRange(hexDiacritics);
					
					return hex;
				}
			}
			
			public void ChangeCharacter(int ascii)
			{
				if (IsCharacter((char) ascii))
				{
					character = ASCIIToCharacter(ascii).Character;
					modification = ASCIIToCharacter(ascii).Modification;
					asciiCharacter = ascii;
					hexCharacter = CharacterToHex(character, modification);
				}
			}
			public void ChangeCharacter(string hex)
			{
				if (IsCharacter((char) Convert.ToInt16(hex, 16)))
				{
					character = HexToCharacter(hex).Character;
					modification = HexToCharacter(hex).Modification;
					asciiCharacter = CharacterToASCII(character, modification);
					hexCharacter = hex;
				}
			}
			public void ChangeCharacter(Character character)
			{
				this.character = character;
				asciiCharacter = CharacterToASCII(character, modification);
				hexCharacter = CharacterToHex(character, modification);
			}
			public void ChangeCharacter(Character character, Diacritic modification)
			{
				this.character = character;
				this.modification = modification;
				asciiCharacter = CharacterToASCII(character, modification);
				hexCharacter = CharacterToHex(character, modification);
			}
			
			public void RemoveCharacter(bool removeModification = true)
			{
				character = Character.NONE;
				if (removeModification)
					modification = Diacritic.NONE;
				asciiCharacter = 0;
				hexCharacter = "0000";
			}
			
			public void ChangeModification(Diacritic modification)
			{
				this.modification = modification;
				asciiCharacter = CharacterToASCII(character, modification);
				hexCharacter = CharacterToHex(character, modification);
			}
			
			public void RemoveModification()
			{
				modification = Diacritic.NONE;
				
				ChangeCharacter(character);
			}
			
			public void AddDiacritic(int ascii)
			{
				if (IsDiacritic((char) ascii))
				{
					diacritics.Add(ASCIIToDiacritic(ascii));
					asciiDiacritics.Add(ascii);
					hexDiacritics.Add(DiacriticToHex(diacritics[diacritics.Count - 1]));
				}
			}
			public void AddDiacritic(string hex)
			{
				if (IsDiacritic((char) Convert.ToInt16(hex, 16)))
				{
					diacritics.Add(HexToDiacritic(hex));
					asciiDiacritics.Add(DiacriticToASCII(diacritics[diacritics.Count - 1]));
					hexDiacritics.Add(hex);
				}
			}
			public void AddDiacritic(Diacritic diacritic)
			{
				diacritics.Add(diacritic);
				asciiDiacritics.Add(DiacriticToASCII(diacritic));
				hexDiacritics.Add(DiacriticToHex(diacritic));
			}
			
			public void RemoveDiacritic(int index)
			{
				diacritics.RemoveAt(index);
				hexDiacritics.RemoveAt(index);
				hexDiacritics.RemoveAt(index);
			}
			public void RemoveDiacritic(Diacritic diacritic)
			{
				var index = new List<int>();
				
				for (int i = 0; i < diacritics.Count; i++)
					if (diacritics[i] == diacritic)
						index.Add(i);
				
				for (int i = index.Count - 1; i >= 0; i--)
				{
					diacritics.RemoveAt(index[i]);
					hexDiacritics.RemoveAt(index[i]);
					hexDiacritics.RemoveAt(index[i]);
				}
			}
			
			public void RemoveDiacritics()
			{
				diacritics = new List<Diacritic>();
				asciiDiacritics = new List<int>();
				hexDiacritics = new List<string>();
			}
			
			public bool Contains(Diacritic diacritic)
			{
				return diacritics.Contains(diacritic);
			}
			
			public static bool IsCharacter(char character, bool checkSpace = false)
			{
				// Hamza to Ghayn
			    if (character >= 0x621 && character <= 0x63a)
			        return true;
				
			    // Fa to Ya
			    if (character >= 0x641 && character <= 0x064a)
			        return true;
				
			    // Alif Waslah and Space
			    return character == 0x671 || (character == ' ' && checkSpace);
			}
			
			public static bool IsDiacritic(char diacritic)
			{
				// hamzah - 0621
				// hamzah above alif - 0623
				// hamzah on waw - 0624
				// hamzah under alif - 0625
				// hamzah in the middle - 0626
				// alif waslah - 0671
				// meem saakin - 06e2   <------------
				
				// ----------------------------------------
				
				// tanween fathah - 064b
				// tanween dammah - 064c
				// tanween kasrah - 064d
				// fathah - 064e
				// dammah - 064f
				// kasrah - 0650
				// shaddah - 0651
				// sukoon - 0652
				// maddah - 0653
				
				// alif khanjariyah - 0670
				
				// sukoon 2 - 06df
				
				return (diacritic >= 0x64b && diacritic <= 0x653) || diacritic == 0x670 || diacritic == 0x6df;
			}
			
			public static bool IsArabic(char character, bool checkCharacter, bool checkDiacritic, bool checkSpace, bool checkExtra)
			{
			    if (character >= 0x600 && character <= 0x620 && checkExtra)
			        return true;
			    
			    if (character >= 0x63b && character <= 0x640 && checkExtra)
			        return true;
				
			    if (character >= 0x654 && character <= 0x66f && checkExtra)
			        return true;
				
			    // Not second sukkon
			    if (character >= 0x672 && character <= 0x6ff && character != 0x6df && checkExtra) // probably could be split up more
			        return true;
			    
			    return (IsCharacter(character, checkSpace) && checkCharacter) || (IsDiacritic(character) && checkDiacritic) || (character == ' ' && checkSpace);
			}
		}
		
		public class Word
		{
			List<Letter> letters;
			
			public Word()
			{
				letters = new List<Letter>();
			}
			public Word(Letter letter)
			{
				this.letters = new List<Letter> { letter };
			}
			public Word(List<int> ascii, char endChar = ' ')
			{
				letters = new List<Letter>();
				
				for (int i = 0; i < ascii.Count && (char) ascii[i] != endChar; i++)
				{
					var temp = new List<int>(ascii);
					temp.RemoveRange(0, i);
					
					var letter = new Letter(temp);
					
					if (letter.CharCount(true) != 0)
					{
						letters.Add(letter);
						
						i += letter.CharCount(true) - 1;
					}
				}
			}
			public Word(List<string> hex, char endChar = ' ')
			{
				letters = new List<Letter>();
				
				for (int i = 0; i < hex.Count && (char) Convert.ToInt16(hex[i], 16) != endChar; i++)
				{
					var temp = new List<string>(hex);
					temp.RemoveRange(0, i);
					
					var letter = new Letter(temp);
					
					if (letter.CharCount(true) != 0)
					{
						letters.Add(letter);
						
						i += letter.CharCount(true) - 1;
					}
				}
			}
			public Word(List<Letter> letters)
			{
				this.letters = new List<Letter>(letters);
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
			
			public enum Number
			{
				NONE = 0,
				SINGULAR,
				DUAL,
				PLURAL
			}
			
			public enum Gender
			{
				NONE = 0,
				MASCULINE,
				FEMININE
			}
			
			public override string ToString()
			{
				string s = "";
				
				for (int i = 0; i < letters.Count; i++)
					s += letters[i];
					//s += SoundOfLetter(letters[i]);
				
				return s;
			}
			
			public override bool Equals(object obj)
			{
				var other = obj as Word;
				
				return other != null && object.Equals(this.letters, other.letters);
			}

			public override int GetHashCode()
			{
				int hashCode = 0;
				unchecked {
					if (letters != null)
						hashCode += 1000000007 * letters.GetHashCode();
				}
				return hashCode;
			}
			
			public static bool operator == (Word a, Word b)
			{
				if (a.LetterCount(true) != b.LetterCount(true))
					return false;
				
				for (int i = 0; i < a.LetterCount(true); i++)
					if (a[i] != b[i])
						return false;
				
				return true;
			}
			
			public static bool operator != (Word a, Word b)
			{
				return !(a == b);
			}
			
			public List<Letter> Letters
			{
				get
				{
					return letters;
				}
			}
			
			public Letter this[int index]
			{
				get { return letters[index]; }
			}
			
			public int LetterCount(bool countEmptyLetters = false)
			{
				int n = 0;
				
				for (int i = 0; i < letters.Count; i++)
					if (countEmptyLetters || letters[i].Character != Character.NONE)
						n++;
				
				return n;
			}
			
			public int CharCount(bool asciiCount = false)
			{
				int n = 0;
				
				for (int i = 0; i < letters.Count; i++)
					n += letters[i].CharCount(asciiCount);
				
				return n;
			}
			
			public List<int> ASCII
			{
				get
				{
					var ascii = new List<int>();
					
					for (int i = 0; i < letters.Count; i++)
						ascii.AddRange(letters[i].ASCII);
					
					return ascii;
				}
			}
			
			public List<string> Hex
			{
				get
				{
					var hex = new List<string>();
					
					for (int i = 0; i < letters.Count; i++)
						hex.AddRange(letters[i].Hex);
					
					return hex;
				}
			}
			
			public void AddLetter(Letter letter)
			{
				letters.Add(letter);
			}
			
			public void InsertLetter(int index, Letter letter)
			{
				
				
				letters.Insert(index, letter);
			}
			
			public void RemoveLetter(Letter letter)
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
		
		public static List<Word> Words(List<int> ascii, char separator = ' ')
		{
			var temp = new List<int>(ascii);
			
			var words = new List<Word>();
			var asciiWord = new List<int>();
			
			for (int i = 0; i < temp.Count; i++)
			{
				if ((char) temp[i] == separator && asciiWord.Count != 0)
				{
					words.Add(new Word(asciiWord, separator));
					
					temp.RemoveRange(0, asciiWord.Count + 1);
					asciiWord = new List<int>();
					i = -1;
				}
				else if (Letter.IsArabic((char) temp[i], true, true, false, false))
				{
					asciiWord.Add(temp[i]);
				}
			}
			
			return words;
		}
		
		public static int DiacriticToASCII(Diacritic diacritic)
		{
			switch (diacritic)
			{
				case Diacritic.TANWEEN_FATHAH:
					return 1611;
				case Diacritic.TANWEEN_DAMMAH:
					return 1612;
				case Diacritic.TANWEEN_KASRAH:
					return 1613;
				case Diacritic.FATHAH:
					return 1614;
				case Diacritic.DAMMAH:
					return 1615;
				case Diacritic.KASRAH:
					return 1616;
				case Diacritic.SHADDAH:
					return 1617;
				case Diacritic.SUKOON:
					return 1618;
				case Diacritic.MADDAH:
					return 1619;
				case Diacritic.ALIF_KHANJARIYAH:
					return 1648;
			}
			
			return 0;
		}
		public static Arabic.Diacritic ASCIIToDiacritic(int ascii)
		{
			// https://www.andmine.com/ascii-codes-html-table-reference/index.php?start=1000
			switch (ascii)
			{
				case 1611:
					return Diacritic.TANWEEN_FATHAH;
				case 1612:
					return Diacritic.TANWEEN_DAMMAH;
				case 1613:
					return Diacritic.TANWEEN_KASRAH;
				case 1614:
					return Diacritic.FATHAH;
				case 1615:
					return Diacritic.DAMMAH;
				case 1616:
					return Diacritic.KASRAH;
				case 1617:
					return Diacritic.SHADDAH;
				case 1618:
					return Diacritic.SUKOON;
				case 1619:
					return Diacritic.MADDAH;
				case 1648:
					return Diacritic.ALIF_KHANJARIYAH;
				case 1759:
					return Diacritic.SUKOON;
			}
			
			return Diacritic.NONE;
		}
		
		public static string DiacriticToHex(Diacritic diacritic)
		{
			switch (diacritic)
			{
				case Diacritic.TANWEEN_FATHAH:
					return "064b";
				case Diacritic.TANWEEN_DAMMAH:
					return "064c";
				case Diacritic.TANWEEN_KASRAH:
					return "064d";
				case Diacritic.FATHAH:
					return "064e";
				case Diacritic.DAMMAH:
					return "064f";
				case Diacritic.KASRAH:
					return "0650";
				case Diacritic.SHADDAH:
					return "0651";
				case Diacritic.SUKOON:
					return "0652";
				case Diacritic.MADDAH:
					return "0653";
				case Diacritic.ALIF_KHANJARIYAH:
					return "0670";
			}
			
			return "0000";
		}
		public static Arabic.Diacritic HexToDiacritic(string hex)
		{
			// http://jrgraphix.net/r/Unicode/0600-06FF
			switch (hex)
			{
				case "064b":
					return Diacritic.TANWEEN_FATHAH;
				case "064c":
					return Diacritic.TANWEEN_DAMMAH;
				case "064d":
					return Diacritic.TANWEEN_KASRAH;
				case "064e":
					return Diacritic.FATHAH;
				case "064f":
					return Diacritic.DAMMAH;
				case "0650":
					return Diacritic.KASRAH;
				case "0651":
					return Diacritic.SHADDAH;
				case "0652":
					return Diacritic.SUKOON;
				case "0653":
					return Diacritic.MADDAH;
				case "0670":
					return Diacritic.ALIF_KHANJARIYAH;
				case "06df":
					return Diacritic.SUKOON;
			}
			
			return Diacritic.NONE;
		}
		
		public static int CharacterToASCII(Character character)
		{
			return CharacterToASCII(character, Diacritic.NONE);
		}
		public static int CharacterToASCII(Character character, Diacritic modification)
		{
			// https://www.andmine.com/ascii-codes-html-table-reference/index.php?start=1000
			switch (character)
			{
				case Character.SPACE:
					return 32;
				case Character.HAMZA:
					if (modification == Diacritic.HAMZA_MIDDLE)
						return 1574;
					
					return 1569;
				case Character.ALIF:
					if (modification == Diacritic.HAMZA_ABOVE)
						return 1571;
					if (modification == Diacritic.HAMZA_BELOW)
						return 1573;
					if (modification == Diacritic.ALIF_WASLAH)
						return 1649;
					if (modification == Diacritic.MADDAH)
						return 1570;
					if (modification == Diacritic.ALIF_KHANJARIYAH)
						return 1648;
					
					return 1575;
				case Character.BA:
						return 1576;
				case Character.TA:
					if (modification == Diacritic.MARBUTAH)
						return 1577;
					
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
					//if (modification == Diacritic.MARBUTAH)
						//return 1577;
					
					return 1607;
				case Character.WAW:
					if (modification == Diacritic.HAMZA_ABOVE)
						return 1572;
						
					return 1608;
				case Character.YA:
					if (modification == Diacritic.ALIF_MAQSURAH)
						return 1574;
					
					return 1609;
			}
			
			return 0;
		}
		public static Arabic.Letter ASCIIToCharacter(int ascii)
		{
			switch (ascii)
			{
				case 32:
					return new Arabic.Letter(Character.SPACE, Diacritic.NONE);
				case 1569:
					return new Arabic.Letter(Character.HAMZA, Diacritic.NONE);
				case 1570:
					return new Arabic.Letter(Character.ALIF, Diacritic.MADDAH);
				case 1571:
					return new Arabic.Letter(Character.ALIF, Diacritic.HAMZA_ABOVE);
				case 1572:
					return new Arabic.Letter(Character.WAW, Diacritic.HAMZA_ABOVE);
				case 1573:
					return new Arabic.Letter(Character.ALIF, Diacritic.HAMZA_BELOW);
				case 1574:
					return new Arabic.Letter(Character.HAMZA, Diacritic.HAMZA_MIDDLE);
				case 1575:
					return new Arabic.Letter(Character.ALIF, Diacritic.NONE);
				case 1576:
					return new Arabic.Letter(Character.BA, Diacritic.NONE);
				case 1577:
					return new Arabic.Letter(Character.TA, Diacritic.MARBUTAH);
				case 1578:
					return new Arabic.Letter(Character.TA, Diacritic.NONE);
				case 1579:
					return new Arabic.Letter(Character.THA, Diacritic.NONE);
				case 1580:
					return new Arabic.Letter(Character.JEEM, Diacritic.NONE);
				case 1581:
					return new Arabic.Letter(Character.HHA, Diacritic.NONE);
				case 1582:
					return new Arabic.Letter(Character.KHA, Diacritic.NONE);
				case 1583:
					return new Arabic.Letter(Character.DAL, Diacritic.NONE);
				case 1584:
					return new Arabic.Letter(Character.DHAL, Diacritic.NONE);
				case 1585:
					return new Arabic.Letter(Character.RA, Diacritic.NONE);
				case 1586:
					return new Arabic.Letter(Character.ZAYN, Diacritic.NONE);
				case 1587:
					return new Arabic.Letter(Character.SEEN, Diacritic.NONE);
				case 1588:
					return new Arabic.Letter(Character.SHEEN, Diacritic.NONE);
				case 1589:
					return new Arabic.Letter(Character.SAD, Diacritic.NONE);
				case 1590:
					return new Arabic.Letter(Character.DAD, Diacritic.NONE);
				case 1591:
					return new Arabic.Letter(Character.TTA, Diacritic.NONE);
				case 1592:
					return new Arabic.Letter(Character.THHA, Diacritic.NONE);
				case 1593:
					return new Arabic.Letter(Character.AYN, Diacritic.NONE);
				case 1594:
					return new Arabic.Letter(Character.GHAYN, Diacritic.NONE);
				case 1601:
					return new Arabic.Letter(Character.FA, Diacritic.NONE);
				case 1602:
					return new Arabic.Letter(Character.QAF, Diacritic.NONE);
				case 1603:
					return new Arabic.Letter(Character.KAF, Diacritic.NONE);
				case 1604:
					return new Arabic.Letter(Character.LAM, Diacritic.NONE);
				case 1605:
					return new Arabic.Letter(Character.MEEM, Diacritic.NONE);
				case 1606:
					return new Arabic.Letter(Character.NOON, Diacritic.NONE);
				case 1607:
					return new Arabic.Letter(Character.HA, Diacritic.NONE);
				case 1608:
					return new Arabic.Letter(Character.WAW, Diacritic.NONE);
				case 1609:
					return new Arabic.Letter(Character.YA, Diacritic.ALIF_MAQSURAH);
				case 1610:
					return new Arabic.Letter(Character.YA, Diacritic.NONE);
				case 1648:
					return new Arabic.Letter(Character.ALIF, Diacritic.ALIF_KHANJARIYAH);
				case 1649:
					return new Arabic.Letter(Character.ALIF, Diacritic.ALIF_WASLAH);
			}
			
			return new Arabic.Letter();
		}
		
		public static string CharacterToHex(Character character)
		{
			return CharacterToHex(character, Diacritic.NONE);
		}
		public static string CharacterToHex(Character character, Diacritic diacritic)
		{
			// http://jrgraphix.net/r/Unicode/0600-06FF
			switch (character)
			{
				case Character.SPACE:
					return "0020";
				case Character.HAMZA:
					if (diacritic == Diacritic.HAMZA_MIDDLE)
						return "0626";
					
					return "0621";
				case Character.ALIF:
					if (diacritic == Diacritic.HAMZA_ABOVE)
						return "0623";
					if (diacritic == Diacritic.HAMZA_BELOW)
						return "0625";
					if (diacritic == Diacritic.ALIF_WASLAH)
						return "0671";
					if (diacritic == Diacritic.MADDAH)
						return "0622";
					if (diacritic == Diacritic.ALIF_KHANJARIYAH)
						return "0670";
					
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
					//if (diacritic == Diacritic.MARBUTAH)
						//return "0629";
					
					return "0647";
				case Character.WAW:
					if (diacritic == Diacritic.HAMZA_ABOVE)
						return "0624";
						
					return "0648";
				case Character.YA:
					if (diacritic == Diacritic.ALIF_MAQSURAH)
						return "0649";
					
					return "064a";
			}
			
			return "0000";
		}
		public static Arabic.Letter HexToCharacter(string hex)
		{
			switch (hex)
			{
				case "0020":
					return new Arabic.Letter(Character.SPACE, Diacritic.NONE);
				case "0621":
					return new Arabic.Letter(Character.HAMZA, Diacritic.NONE);
				case "0622":
					return new Arabic.Letter(Character.ALIF, Diacritic.MADDAH);
				case "0623":
					return new Arabic.Letter(Character.ALIF, Diacritic.HAMZA_ABOVE);
				case "0624":
					return new Arabic.Letter(Character.WAW, Diacritic.HAMZA_ABOVE);
				case "0625":
					return new Arabic.Letter(Character.ALIF, Diacritic.HAMZA_BELOW);
				case "0626":
					return new Arabic.Letter(Character.HAMZA, Diacritic.HAMZA_MIDDLE);
				case "0627":
					return new Arabic.Letter(Character.ALIF, Diacritic.NONE);
				case "0628":
					return new Arabic.Letter(Character.BA, Diacritic.NONE);
				case "0629":
					return new Arabic.Letter(Character.TA, Diacritic.MARBUTAH);
				case "062a":
					return new Arabic.Letter(Character.TA, Diacritic.NONE);
				case "062b":
					return new Arabic.Letter(Character.THA, Diacritic.NONE);
				case "062c":
					return new Arabic.Letter(Character.JEEM, Diacritic.NONE);
				case "062d":
					return new Arabic.Letter(Character.HHA, Diacritic.NONE);
				case "062e":
					return new Arabic.Letter(Character.KHA, Diacritic.NONE);
				case "062f":
					return new Arabic.Letter(Character.DAL, Diacritic.NONE);
				case "0630":
					return new Arabic.Letter(Character.DHAL, Diacritic.NONE);
				case "0631":
					return new Arabic.Letter(Character.RA, Diacritic.NONE);
				case "0632":
					return new Arabic.Letter(Character.ZAYN, Diacritic.NONE);
				case "0633":
					return new Arabic.Letter(Character.SEEN, Diacritic.NONE);
				case "0634":
					return new Arabic.Letter(Character.SHEEN, Diacritic.NONE);
				case "0635":
					return new Arabic.Letter(Character.SAD, Diacritic.NONE);
				case "0636":
					return new Arabic.Letter(Character.DAD, Diacritic.NONE);
				case "0637":
					return new Arabic.Letter(Character.TTA, Diacritic.NONE);
				case "0638":
					return new Arabic.Letter(Character.THHA, Diacritic.NONE);
				case "0639":
					return new Arabic.Letter(Character.AYN, Diacritic.NONE);
				case "063a":
					return new Arabic.Letter(Character.GHAYN, Diacritic.NONE);
				case "0641":
					return new Arabic.Letter(Character.FA, Diacritic.NONE);
				case "0642":
					return new Arabic.Letter(Character.QAF, Diacritic.NONE);
				case "0643":
					return new Arabic.Letter(Character.KAF, Diacritic.NONE);
				case "0644":
					return new Arabic.Letter(Character.LAM, Diacritic.NONE);
				case "0645":
					return new Arabic.Letter(Character.MEEM, Diacritic.NONE);
				case "0646":
					return new Arabic.Letter(Character.NOON, Diacritic.NONE);
				case "0647":
					return new Arabic.Letter(Character.HA, Diacritic.NONE);
				case "0648":
					return new Arabic.Letter(Character.WAW, Diacritic.NONE);
				case "0649":
					return new Arabic.Letter(Character.YA, Diacritic.ALIF_MAQSURAH);
				case "064a":
					return new Arabic.Letter(Character.YA, Diacritic.NONE);
				case "0670":
					return new Arabic.Letter(Character.ALIF, Diacritic.ALIF_KHANJARIYAH);
				case "0671":
					return new Arabic.Letter(Character.ALIF, Diacritic.ALIF_WASLAH);
			}
			
			return new Arabic.Letter();
		}
		
		public static string SoundOfLetter(Arabic.Letter letter)
		{
			switch (letter.Character)
			{
				case Character.SPACE:
					return " ";
				case Character.HAMZA:
					return "'";
				case Character.ALIF:
					if (letter.Diacritics.Contains(Diacritic.HAMZA_BELOW))
						return "EE";
					if (letter.Diacritics.Contains(Diacritic.ALIF_WASLAH))
						return "";
					if (letter.Diacritics.Contains(Diacritic.MADDAH))
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
					if (letter.Diacritics.Contains(Diacritic.MARBUTAH))
						return "T";
					
					return "H";
				case Character.WAW:
					if (letter.Diacritics.Contains(Diacritic.HAMZA_ABOVE))
						return "'WOO";
						
					return "W";
				case Character.YA:
					if (letter.Diacritics.Contains(Diacritic.HAMZA_ABOVE))
						return "'";
					if (letter.Diacritics.Contains(Diacritic.ALIF_MAQSURAH))
						return "AA";
					
					return "Y";
			}
			
			return "";
		}
		
		public static int AbjadValue(Arabic.Letter letter)
		{
			if (letter.Character == Character.HA && letter.Diacritics.Contains(Diacritic.MARBUTAH))
				return (int) Character.TA;
			
			return Math.Abs((int) letter.Character);
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
			
			for (int i = 0; i < word.LetterCount(); i++)
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
		
		public static int LetterCount(List<Arabic.Word> phrase, bool checkEmptyLetter = false)
		{
			int n = 0;
			
			for (int i = 0; i < phrase.Count; i++)
				n += phrase[i].LetterCount(checkEmptyLetter);
			
			return n;
		}
		public static int LetterCount(Arabic.Word word, Arabic.Letter letter, bool checkEmptyLetter = false)
		{
			int n = 0;
			
			for (int i = 0; i < word.LetterCount(checkEmptyLetter); i++)
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
