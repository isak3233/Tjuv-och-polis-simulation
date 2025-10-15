using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToPSimulation
{
    internal class NewsFeed
    {

        public List<string> News { get; set; }

        public NewsFeed()
        {
            News = new List<string>();
        }

        public List<string> LatestNews()
        {
            List<string> latestNews = new List<string>();
            int latestSevenNews = News.Count - 8;
            if (latestSevenNews < 0 )
            {
                latestSevenNews = 0; 
            }
            for (int i = News.Count - 1; i > latestSevenNews; i--)
            {
                latestNews.Add(i + ": " + News[i]);

            }

            return latestNews;
        }


        public void PrintNews()
        {
            List<string> latestNews = LatestNews();
            
            foreach (string news in latestNews)
            {
                Console.WriteLine(news);
               

            }
        }
    }
}
