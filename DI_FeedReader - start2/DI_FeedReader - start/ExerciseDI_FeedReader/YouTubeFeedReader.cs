using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseDI_FeedReader
{
    public class YouTubeFeedReader : IFeedReader
    {
        public YouTubeFeedReader()
        {
            ReturnType = "Video";
        }

        public string ReturnType { get; }

        public string GetFeed()
        {
            return ReturnType + ":item 1";
        }
    }
}
