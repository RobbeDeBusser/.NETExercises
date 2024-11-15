using ExerciseDI_FeedReader;
using System;
using Microsoft.Extensions.DependencyInjection;


internal class Program
{
    private static void Main(string[] args)
    {
        // Register services
        var serviceProvider = RegisterServices();

        // Resolve and use the FeedService with different feed readers
        var feedServicePodcast = new FeedService(serviceProvider.GetServices<IFeedReader>().ElementAt(0));
        Console.WriteLine(feedServicePodcast.GetFeed());

        var feedServiceYouTube = new FeedService(serviceProvider.GetServices<IFeedReader>().ElementAt(1));
        Console.WriteLine(feedServiceYouTube.GetFeed());

        var feedServiceBlog = new FeedService(serviceProvider.GetServices<IFeedReader>().ElementAt(2));
        Console.WriteLine(feedServiceBlog.GetFeed());

        Console.ReadLine();
    }

    private static ServiceProvider RegisterServices()
    {
        var serviceCollection = new ServiceCollection();

        // Register feed readers
        serviceCollection.AddTransient<IFeedReader, PodcastFeedReader>();
        serviceCollection.AddTransient<IFeedReader, YouTubeFeedReader>();
        serviceCollection.AddTransient<IFeedReader, BlogFeedReader>();

        // Register FeedService
        serviceCollection.AddTransient<FeedService>();

        // Build the service provider
        return serviceCollection.BuildServiceProvider();
    }
}
