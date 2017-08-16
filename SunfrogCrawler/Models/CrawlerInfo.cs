using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunfrogCrawler.Models
{
    public class CrawlerInfo
    {
        public int CurrentIndex { get; set; }
        public CrawlerInfo()
        {
            CurrentIndex = 1;
        }
    }
}
