using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseDI_FeedReader
{
    public class BlogFeedReader : IFeedReader
    {
        public BlogFeedReader()
        {
            ReturnType = "Blog";
        }

        public string ReturnType { get; }

        public string GetFeed()
        {
            return ReturnType + ":item 1"; ;
        }
    }
}
