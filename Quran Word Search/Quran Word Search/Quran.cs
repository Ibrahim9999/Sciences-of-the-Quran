using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Quran_Word_Search
{
	/// <summary>
	/// Description of Quran.
	/// </summary>
	
	public class Verse
	{
		List<Arabic.Word> words;
		Chapter.Name chapter;
		int verseNum;
		
		public Verse()
		{
			chapter = Chapter.Name.NONE;
			words = new List<Arabic.Word>();
			verseNum = 0;
		}
		public Verse(Chapter.Name name, int verseNum)
		{
			chapter = name;
			words = new List<Arabic.Word>();
			this.verseNum = verseNum;
		}
		public Verse(List<Arabic.Word> words, Chapter.Name name, int verseNum)
		{
			chapter = name;
			this.words = words;
			this.verseNum = verseNum;
		}
		
		//public void AddWord
	}
	
	public class Chapter
	{
		List<Verse> verses;
		Chapter.Name name;
		
		public Chapter()
		{
			name = Name.NONE;
			verses = new List<Verse>();
		}
		public Chapter(int chapterNum)
		{
			name = (Name) chapterNum;
			Initialize();
		}
		public Chapter(Name name)
		{
			this.name = name;
			Initialize();
		}
		
		public enum Name
		{
			NONE = 0,
			AL_FATIHA ,		// 1
			AL_BAQARAH, 	// 2
			ALI_IMRAN,  	// 3
			AL_NISA,    	// 4
			AL_MAIDAH, 		// 5
			AL_ANAM, 		// 6
			AL_ARAF, 		// 7
			AL_ANFAL, 		// 8
			AT_TAWBAH, 		// 9
			YUNUS,			// 10
			HUD,			// 11
			YUSUF,			// 12
			AR_RAD,			// 13
			IBRAHIM,		// 14
			AL_HIJR,		// 15
			AN_NAHL,		// 16
			AL_ISRA,		// 17
			AL_KAHF,		// 18
			MARYAM,			// 19
			TAHA, 			// 20
			AL_ANBIYA,		// 21
			AL_HAJJ,		// 22
			AL_MUMINUN,		// 23
			AN_NUR,			// 24
			AL_FURQAN,		// 25
			ASH_SHUARA,		// 26
			AN_NAML,		// 27
			AL_QASAS,		// 28
			AL_ANKABUT, 	// 29
			AR_RUM,			// 30
			LUQMAN,			// 31
			AS_SAJDAH,		// 32
			AL_AHZAB,		// 33
			AS_SABA,		// 34
			AL_FATIR,		// 35
			YASIN,			// 36
			AS_SAFFAT,		// 37
			SAD,			// 38
			AZ_ZUMAR,		// 39
			AL_GHAFIR,		// 40
			FUSSILAT,		// 41
			ASH_SHURA,		// 42
			AZ_ZUKHRUF,		// 43
			AD_DUKHAN,		// 44
			AL_JATHIYAH,	// 45
			AL_AHQAF,		// 46
			MUHAMMAD,		// 47
			AL_FATH,		// 48
			AL_HUJURAT,		// 49
			QAF,			// 50
			AD_DHARIYAT,	// 51
			AT_TUR,			// 52
			AN_NAJM,		// 53
			AL_QAMAR,		// 54
			AR_RAHMAN,		// 55
			AL_WAQIAH,		// 56
			AL_HADID,		// 57
			AL_MUJADILAH,	// 58
			AL_HASHR,		// 59
			AL_MUMTAHANAH,	// 60
			AS_SAFF,		// 61
			AL_JUMUAH,		// 62
			AL_MUNAFIQUN,	// 63
			AL_TAGHABUN,	// 64
			AT_TALAQ,		// 65
			AT_TAHRIM,		// 66
			AL_MULK,		// 67
			AL_QALAM,		// 68
			AL_HAQQAH,		// 69
			AL_MAARIJ,		// 70
			NUH,			// 71
			AL_JINN,		// 72
			AL_MUZZAMMIL,	// 73
			AL_MUDDATHTHIR,	// 74
			AL_QIYAMAH,		// 75
			AL_INSAN,		// 76
			AL_MURSALAT,	// 77
			AN_NABA,		// 78
			AN_NAZIAT,		// 79
			ABASA,			// 80
			AT_TAKWIR,		// 81
			AL_INFITAR,		// 82
			AT_MUTAFFIFIN,	// 83
			AL_INSHIQAQ,	// 84
			AL_BURUJ,		// 85
			AL_TARIQ,		// 86
			AL_ALA,			// 87
			AL_GHASHIYAH,	// 88
			AL_FAJR,		// 89
			AL_BALAD,		// 90
			ASH_SHAMS,		// 91
			AL_LAIL,		// 92
			AD_DUHA,		// 93
			ASH_SHARH,		// 94
			AT_TIN,			// 95
			AL_ALAQ,		// 96
			AL_QADR,		// 97
			AL_BAYINNAH,	// 98
			AL_ZALZALAH,	// 99
			AL_ADIYAT,		// 100
			AL_QARIAH,		// 101
			AT_TAKATHUR,	// 102
			AL_ASR,			// 103
			AL_HUMUZAH,		// 104
			AL_FIL,			// 105
			AL_QURAYSH,		// 106
			AL_MAUN,		// 107
			AL_KAUTHAR,		// 108
			AL_KAFIRUN,		// 109
			AN_NASR,		// 110
			AL_LAHAB,		// 111
			AL_IKHLAS,		// 112
			AL_FALAQ,		// 113
			AL_NAS			//114
		}
		
		void Initialize()
		{
			string path = @"C:\Users\PC\Documents\SharpDevelop Projects\Quran Word Search\Quran Word Search\Files\Chapters\" + Quran.GetVerseNum(name) + ".txt";
			verses = new List<Verse>(Quran.GetVerseNum(name));
			var file = new StreamReader(path, Encoding.UTF8, true);
			
			for (int i = 0; i < verses.Count; i++)
			{
				var verse = new Verse();
			}
		}
	}
	
	public class Quran
	{
		readonly List<Chapter> chapters;
		
		public const int ChapterNum = 114;
		
		public Quran()
		{
			//for (int i = 0; i < 
		}
		
		public static int GetVerseNum(Chapter.Name name)
		{
			return 0;
		}
	}
}