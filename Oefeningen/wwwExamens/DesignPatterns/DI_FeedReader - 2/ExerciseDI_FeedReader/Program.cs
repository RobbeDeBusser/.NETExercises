using ExerciseDI_FeedReader;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
	private static IServiceProvider _serviceProvider;
	private static void Main(string[] args)
	{
		var services = new ServiceCollection();
		services.AddSingleton<IFeedreader, BlogFeedReader>();
		_serviceProvider = services.BuildServiceProvider(true);
		FeedService servicePodcast = new FeedService(_serviceProvider.GetService<IFeedreader>());
		string feed = servicePodcast.GetFeed();

		Console.WriteLine(feed);
		Console.ReadLine();
	}
}