using Microsoft.Extensions.DependencyInjection;
using ExerciseDI_FeedReader;
using System;

internal class Program
{
	private static void Main(string[] args)
	{
		FeedService servicePodcast = new FeedService(new BlogFeedReader());
		string feed = servicePodcast.GetFeed();

		Console.WriteLine(feed);
		Console.ReadLine();
	}
}