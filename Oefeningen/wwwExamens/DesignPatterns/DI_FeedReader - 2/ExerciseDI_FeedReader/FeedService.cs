using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseDI_FeedReader
{
    public class FeedService
    {
        IFeedreader _reader;
        public FeedService(IFeedreader reader) {
            _reader = reader;
        }
        
        public string GetFeed()
        {
            return _reader.GetSingleFeed();
        }
    }
}
