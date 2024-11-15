using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseDI_FeedReader
{
    public class FeedService
    {
        private readonly IFeedReader _feedReader;

        public FeedService(IFeedReader feedReader)
        {
            _feedReader = feedReader;
        }

        public string GetFeed()
        {
            return _feedReader.GetFeed();
        }
    }
}

