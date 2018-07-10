using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films.Core.KinoPoisk
{
    class FilmsParserSettings : IParserSettings
    {

        public FilmsParserSettings(int currentPage, String currentTime)
        {
            CurrentTime = currentTime;
            CurrentPage = currentPage;
        }

        public FilmsParserSettings(string linkMovie)
        {
            LinkMovie = linkMovie;
        }

        public string BaseUrl { get; set; } = "http://kinogo.cc";

        public string Prefix { get; set; } = "filmy_{CurrentTime}/page/{CurrentPage}";

        public int CurrentPage { get; set; }

        public String CurrentTime { get; set; }

        public string LinkMovie { get; set; }

    }
}
