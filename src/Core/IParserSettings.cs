using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films.Core
{
    interface IParserSettings
    {
        string BaseUrl { get; set; }
        string Prefix { get; set; }
        string LinkMovie { get; set; }

        int CurrentPage { get; set; }
        String CurrentTime { get; set; }
    }
}
