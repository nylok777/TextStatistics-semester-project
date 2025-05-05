using Szovegelemzo.Models;

namespace Szovegelemzo.Logic
{
    public class TextAnalyzer : ITextAnalyzer
    {
        TextData? textData;
        List<string> stopWords;

        public TextAnalyzer(TextData textData)
        {         
            stopWords = ["a", "i", "my", "the", "és", "de"];
            this.textData = new TextData();
        }

        public void CreateTextData(string text)
        {
            this.textData = new TextData(text);
        }

        public int GetWordCount()
        {
            int count = textData.Tokens.Length;
            return count;
        }

        public int GetCharCount()
        {
            int count = 0;
            foreach (var word in textData.Tokens)
            {
                count = count + word.Length;
            }
            return count;
        }

        public int GetSentenceCount()
        {
            string[] sentences = textData.Text.Split(".");
            return sentences.Length;
        }

        public string MostCommonWord()
        {
            List<string> tokens = textData.Tokens.ToList();
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
