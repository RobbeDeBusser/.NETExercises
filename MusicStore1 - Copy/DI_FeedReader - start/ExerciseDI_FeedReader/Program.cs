using ExerciseDI_FeedReader;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices.JavaScript;
using System.Xml;

internal class Program
{
    private static IServiceProvider _serviceProvider;
    /*
    private static void RegisterServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IFeedReader, PodcastFeedReader>();
        services.AddSingleton<IFeedReader, BlogFeedReader>();
        services.AddSingleton<IFeedReader, YouTubeFeedReader>();
        _serviceProvider = services.BuildServiceProvider(true);
    }
    */
    /*
    private static void RegisterServices()
    {
        var services = new ServiceCollection();
        // We get the correct instances from the xml file
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.IgnoreWhitespace = true;

        // Load the document and set the root element.  
        XmlDocument doc = new XmlDocument();
        doc.Load("config\\myconfig.xml");
        XmlNode root = doc.DocumentElement;

        XmlNodeList nodeList = root.SelectNodes("implementation");
        foreach (XmlNode service in nodeList)
        {
            //firstchild = interface
            //lastchild = instance
            services.AddSingleton(Type.GetType(service.FirstChild.InnerText),
                Type.GetType(service.LastChild.InnerText));
        }
        _serviceProvider = services.BuildServiceProvider(true);
    }*/
    public static void RegisterServices()
    {
        var services = new ServiceCollection();
        string json = File.ReadAllText("Config\\myconfig.json");

        // Access the value using the property name
        string serviceName = jsonData.IFeedReader;
    }

    private static void Main(string[] args)
	{
		RegisterServices();
		FeedService servicePodcast = new FeedService(
            //_serviceProvider.GetServices<IFeedReader>().ElementAt(2)
            _serviceProvider.GetService<IFeedReader>()

            );
		string feed = servicePodcast.GetFeed();

		Console.WriteLine(feed);
		Console.ReadLine();
	}
}