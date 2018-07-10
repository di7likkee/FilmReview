using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Films.Core
{
    class HtmlLoader
    {
        readonly WebClient client;
        readonly string urlNoveltiesOfFilms, urlMovie;

        public HtmlLoader(IParserSettings settings)
        {
            client = new WebClient();
            urlNoveltiesOfFilms = $"{ settings.BaseUrl}/{ settings.Prefix}/";
            urlMovie = settings.LinkMovie;
        }

        public async Task<string> GetSourceByPageId(int id, String time)
        {
            var currentUrl = urlNoveltiesOfFilms.Replace("{CurrentPage}", id.ToString());
            currentUrl = currentUrl.Replace("{CurrentTime}", time);
            string source = await client.DownloadStringTaskAsync(new Uri(currentUrl));

            return source;
        }

        public async Task<string> GetSourceByPageMovie()
        {
            string source = await client.DownloadStringTaskAsync(new Uri(urlMovie));

            return source;
        }

    }
}
