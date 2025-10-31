using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToPSimulation
{
    public class NewsFeed
    {

        public Queue<string> News { get; set; }
        private int newsIndex = 0;
        public NewsFeed()
        {
            News = new Queue<string>();
        }   
        
        public void AddNews(List<string> newsToAdd)
        {
            foreach(string news in newsToAdd)
            {
                News.Enqueue(news);
                newsIndex++;
            }
            while(true)
            {
                if(News.Count > 7)
                {
                    News.Dequeue();
                } else
                {
                    break;
                }
            }
        }

        public void WriteOutNews()
        {
            Console.Write(Helper.newsString);
            int index = 0;
            for (int i = News.Count - 1; i >= 0; i--)
            {
                string news = News.ElementAt(i);
                Console.WriteLine($"{(newsIndex - index)}: {news}");
                index++;
            }
           
        }
    }
}
