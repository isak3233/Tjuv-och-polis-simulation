using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToPSimulation
{
    public class NewsFeed
    {

        public List<string> News { get; set; }

        public NewsFeed()
        {
            News = new List<string>();
        }

        public List<string> LatestNews(int amountOfNews)
        {
            List<string> latestNewsToGet = new List<string>();
            int latestNews = News.Count - (amountOfNews + 1);
            if (latestNews < 0 )
            {
                latestNews = 0; 
            }
            for (int i = News.Count - 1; i >= latestNews; i--)
            {
                latestNewsToGet.Add((i + 1) + ": " + News[i]);

            }

            return latestNewsToGet;
        }


        public void WriteOutNews()
        {
            List<string> latestNews = LatestNews(7);
            Console.Write(Helper.newsString);
            foreach (string news in latestNews)
            {
                Console.WriteLine(news);
               

            }
        }
    }
}
