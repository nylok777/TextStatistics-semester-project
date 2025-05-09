using Szovegelemzo.Data;
using Szovegelemzo.Models;

namespace Szovegelemzo.Logic
{
    public class TextAnalyzer : ITextAnalyzer
    {
        TextDataRepository textDataRepo;
        List<string> stopWords;

        public TextAnalyzer(TextDataRepository textData)
        {
            this.textDataRepo = textData;
            stopWords = ["a", "i", "my", "the", "on", "in", "és", "de"];
        }

        public void CreateTextData(string text)
        {
            textDataRepo.CreateTextData(text);
        }

        public int GetWordCount()
        {
            TextData text = textDataRepo.GetTextData();
            int count = text.Tokens.Length;
            return count;
        }

        public int GetCharCount()
        {
            TextData text = textDataRepo.GetTextData();
            int count = 0;
            foreach (var word in text.Tokens)
            {
                count = count + word.Length;
            }
            return count;
        }

        public int GetSentenceCount()
        {
            TextData text = textDataRepo.GetTextData();
            string[] sentences = text.Text.Split(".");
            int numOfSentences = sentences.Length;

            for (int i = 0; i < sentences.Length; i++)
            {
                if (sentences[i].EndsWith(" pl") || sentences[i].EndsWith(" kb"))
                {
                    numOfSentences--;
                }
                else if (sentences[i].Length < 2)
                {
                    numOfSentences--;
                }
            }

            return numOfSentences;
        }
        
        public Dictionary<string, int> MostCommonWords()
        {
            TextData text = textDataRepo.GetTextData();
            List<string> tokens = text.Tokens.ToList();

            tokens.RemoveAll(x => x == stopWords.Find(y => y == x));

            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            foreach (var item in tokens)
            {
                if (wordCount.ContainsKey(item))
                {
                    wordCount[item] = wordCount[item] + 1;
                }
                else
                {
                    wordCount.Add(item, 1);
                }
            }

            List<int> counts = wordCount.Values.ToList();
            List<int> descending = counts.OrderDescending().ToList();
            List<int> topFiveCounts = descending.GetRange(0, 5);
            Dictionary<string, int> topFiveWords = new Dictionary<string, int>();

            foreach (var count in topFiveCounts)
            {
                KeyValuePair<string, int> pair = wordCount.FirstOrDefault(x => 
                x.Value == count && !topFiveWords.ContainsKey(x.Key));                
                topFiveWords.Add(pair.Key, pair.Value);
            }

            return topFiveWords;

            //int max = wordCount.Values.Max();
            //var enumerableWord = wordCount.Where(x => x.Value == max);
            //var word = enumerableWord.First();
            //return word.Key;
        }

        public float ReadabilityIndex()
        {
            //Automated Readability Index (ARI)
            int charCount = GetCharCount();
            int sentenceCount = GetSentenceCount();
            int wordCount = GetWordCount();

            float index = 4.71f * (charCount / wordCount) + 0.5f * (wordCount / sentenceCount) - 21.43f;
            return index;
        }

        public Statistics GenerateStatistics()
        {
            Dictionary<string, int> wordCount = MostCommonWords();

            int wCount = GetWordCount();
            int cCount = GetCharCount();
            int sCount = GetSentenceCount();
            string[] mostComWords = wordCount.Keys.ToArray();
            int[] mostComWordCounts = wordCount.Values.ToArray();
            float index = ReadabilityIndex();
            return new Statistics()
            {
                CharCount = cCount,
                WordCount = wCount,
                SentenceCount = sCount,
                MostComWords = mostComWords,
                MostComWordCounts = mostComWordCounts,
                ReadIndex = index
            };
        }
    }
}
