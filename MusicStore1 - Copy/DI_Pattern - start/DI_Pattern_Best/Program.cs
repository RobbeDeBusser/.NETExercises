using DI_Pattern_Best;
using Microsoft.Extensions.DependencyInjection;
using System.Xml;

internal class Program
{
	private static IServiceProvider _serviceProvider;

	private static void RegisterServices()
	{
		var services = new ServiceCollection();
		// We get the correct instances from the xml file
		XmlReaderSettings settings = new XmlReaderSettings();
		settings.IgnoreWhitespace = true;

		// Load the document and set the root element.  
		XmlDocument doc = new XmlDocument();
		doc.Load("config\\di_configuration.xml");
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
	}

	private static void DisposeServices()
	{
		if (_serviceProvider == null)
		{
			return;
		}
		if (_serviceProvider is IDisposable)
		{
			((IDisposable)_serviceProvider).Dispose();
		}
	}

	private static void Main(string[] args)
	{
		RegisterServices();

		// We get the correct instance from the RegisterServices method
		Order order = new Order(_serviceProvider.GetService<IContact>());
		order.ContactCustomer(1, "Your shipment will be delivered tomorrow at 4pm.");
		
		DisposeServices();

		Console.WriteLine("Press any key");
		Console.ReadKey();
	}
}