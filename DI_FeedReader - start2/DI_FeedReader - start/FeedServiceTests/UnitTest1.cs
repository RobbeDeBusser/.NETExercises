using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExerciseDI_FeedReader;

namespace FeedServiceTests
{
    [TestClass]
    public class FeedServiceTests
    {
        [TestMethod]
        public void GetFeed_ShouldReturnPodcastFeed()
        {
            // Arrange
            IFeedReader podcastReader = new PodcastFeedReader();
            FeedService feedService = new FeedService(podcastReader);

            // Act
            string result = feedService.GetFeed();

            // Assert
            Assert.AreEqual("Audio:item 1", result);
        }

        [TestMethod]
        public void GetFeed_ShouldReturnYouTubeFeed()
        {
            // Arrange
            IFeedReader youtubeReader = new YouTubeFeedReader();
            FeedService feedService = new FeedService(youtubeReader);

            // Act
            string result = feedService.GetFeed();

            // Assert
            Assert.AreEqual("Video:item 1", result);
        }

        [TestMethod]
        public void GetFeed_ShouldReturnBlogFeed()
        {
            // Arrange
            IFeedReader blogReader = new BlogFeedReader();
            FeedService feedService = new FeedService(blogReader);

            // Act
            string result = feedService.GetFeed();

            // Assert
            Assert.AreEqual("Blog:item 1", result);
        }
    }
}
