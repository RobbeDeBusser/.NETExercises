using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseDI_FeedReader
{
    public class FeedService
    {
        IFeedReader _reader;
        public FeedService(IFeedReader reader)
        {
            _reader = reader;
        }
        public string GetFeed()
        {
            return _reader.GetSingleFeed() ;      
        }
    }
}
