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
            return sentences.Length;
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
                    wordCount[item]++;
                }
                else
                {
                    wordCount.Add(item, 0);
                }
            }

            List<int> counts = wordCount.Values.ToList();
            counts.Sort();
            counts = counts.GetRange(0, 5);
            Dictionary<string, int> topFiveWords = new Dictionary<string, int>();

            foreach (var pair in wordCount)
            {
                if (counts.Contains(pair.Value))
                {
                    topFiveWords.Add(pair.Key, pair.Value);
                }
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
