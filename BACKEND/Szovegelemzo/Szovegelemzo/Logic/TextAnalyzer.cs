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
            stopWords = ["a", "i", "my", "the", "és", "de"];
        }

        public void CreateTextData(string text)
        {
            this.TextData.Text = text.ToLower();
            this.TextData.Tokens = text.ToLower().Split(" ");
        }

        public int GetWordCount()
        {
            int count = TextData.Tokens.Length;
            return count;
        }

        public int GetCharCount()
        {
            int count = 0;
            foreach (var word in TextData.Tokens)
            {
                count = count + word.Length;
            }
            return count;
        }

        public int GetSentenceCount()
        {
            string[] sentences = TextData.Text.Split(".");
            return sentences.Length;
        }

        public string MostCommonWord()
        {
            List<string> tokens = TextData.Tokens.ToList();
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
            string word = wordCount.Max().Key;
            return word;
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
            int wCount = GetWordCount();
            int cCount = GetCharCount();
            int sCount = GetSentenceCount();
            string mostComWord = MostCommonWord();
            float index = ReadabilityIndex();
            return new Statistics(cCount, wCount, sCount, mostComWord, index);
        }
    }
}
