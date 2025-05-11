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
            stopWords = new List<string>{"hed", "i", "me", "my", "myself", "we", "our", "ours", "ourselves", "you", "your", "yours", "yourself", "yourselves", "he", "him", "his", "himself", "she", "her", "hers", "herself", "it", "its", "itself", "they", "them", "their", "theirs", "themselves", "what", "which", "who", "whom", "this", "that", "these", "those", "am", "is", "are", "was", "were", "be", "been", "being", "have", "has", "had", "having", "do", "does", "did", "doing", "a", "an", "the", "and", "but", "if", "or", "because", "as", "until", "while", "of", "at", "by", "for", "with", "about", "against", "between", "into", "through", "during", "before", "after", "above", "below", "to", "from", "up", "down", "in", "out", "on", "off", "over", "under", "again", "further", "then", "once", "here", "there", "when", "where", "why", "how", "all", "any", "both", "each", "few", "more", "most", "other", "some", "such", "no", "nor", "not", "only", "own", "same", "so", "than", "too", "very", "s", "t", "can", "will", "just", "don", "should", "now"};
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
            string[] sentences = text.Text.Split(new char[] {'.', '!', '?'});
            int numOfSentences = sentences.Length;

            for (int i = 0; i < sentences.Length; i++)
            {
                if (sentences[i].EndsWith(" eg"))
                {
                    numOfSentences--;
                }
                else if (sentences[i].Length < 3)
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

            //tokens.RemoveAll(x => x == stopWords.Find(y => y == x));

            tokens.RemoveAll(x => stopWords.Contains(x) == true);

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
            List<int> topFiveCounts = new List<int>();
            try
            {
                topFiveCounts = descending.GetRange(0, 5);
            } catch(Exception)
            {
                topFiveCounts = descending.GetRange(0, descending.Count);
            }
            
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

            float index = 0;
            try
            {
                index = (4.71f * (charCount / wordCount)) + (0.5f * (wordCount / sentenceCount)) - 21.43f;
            } catch(DivideByZeroException)
            {
                index = 0;
            }
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
