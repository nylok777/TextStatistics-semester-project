using Szovegelemzo.Models;

namespace Szovegelemzo.Data
{
    public class TextDataRepository
    {
        List<TextData> textDataList;

        public TextDataRepository()
        {
            textDataList = new List<TextData>();
        }

        public void CreateTextData(string text)
        {
            TextData textData = new TextData(text);
            textDataList.Insert(0, textData);
        }

        public TextData GetTextData()
        {
            return textDataList.First();
        }
    }
}
