using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Szovegelemzo.Logic;
using Szovegelemzo.Models;

namespace Szovegelemzo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextController : ControllerBase
    {
        ITextAnalyzer analyzer;

        public TextController(ITextAnalyzer analyzer)
        {
            this.analyzer = analyzer;
        }

        [HttpPost]
        public void CreateTextData([FromBody] string text)
        {
            analyzer.CreateTextData(text);
        }

        [HttpGet]
        public int GetStatistics()
        {
            return analyzer.GetCharCount();
        }
    }
}
