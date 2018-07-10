using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AngleSharp.Dom;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using System.Text.RegularExpressions;

namespace Films.Core.KinoPoisk
{
    class FilmsParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll("div.shortstorytitle > h2 > a");

            foreach (var item in items)
            {
                list.Add(item.InnerHtml + "_" + item.GetAttribute("href"));
            }

            return list.ToArray();
        }

        public string[] ParsePageMovie(IHtmlDocument document)
        {
            var list = new List<string>();
            var htmlCode = document.QuerySelectorAll("div.fullimg");
            var htmlLinks = document.QuerySelectorAll("div.fullimg > div > a");
            var htmlScreens = document.QuerySelectorAll("div.fullimg > div.screens > a");
            string content = RemakeContent(htmlCode[0].TextContent);
            string linkImg = htmlLinks[0].GetAttribute("href");            

            AddToList(ref list, content, linkImg, htmlScreens);

            return list.ToArray();
        }

        private void AddToList(ref List<string> list, string textContent, string linkImg, IHtmlCollection<IElement> htmlScreens)
        {
            List<string> listСriteria = new List<string> { "Год выпуска:", "Страна:", "Жанр:", "Качество:", "Перевод:", "Продолжительность:", "Премьера", "Режиссер:" , "В ролях:" };
            int i;
            foreach (string item in listСriteria)
            {
                i = textContent.IndexOf(item);
                if (i != -1) {
                    list.Add(textContent.Substring(0, i));
                    textContent = textContent.Substring(i);
                }
            }
            for (int j = 0; j < htmlScreens.Length; j++)
            {
                list.Add(htmlScreens[j].GetAttribute("href"));
            }

            list.Add(linkImg);
        }

        private string RemakeContent(string textContent)
        {
            if (textContent.Contains("Лицензия"))
                return Regex.Replace(textContent, @"\s+", " ").Substring("Лицензия".Length + 1).Trim();
            else return Regex.Replace(textContent, @"\s+", " ").Trim();
        }
    }
}
