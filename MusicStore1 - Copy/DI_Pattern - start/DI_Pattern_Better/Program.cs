using Microsoft.Extensions.DependencyInjection;
using DI_Pattern_Better;
using System;

IServiceProvider serviceProvider;

var services = new ServiceCollection();
// Register interfaces using factory that returns implementation
services.AddSingleton<IContact, EmailSender>();
services.AddSingleton<IContact, TelSender>();
serviceProvider = services.BuildServiceProvider(true);

Order order1 = new Order(serviceProvider.GetServices<IContact>().ElementAt(0));
order1.ContactCustomer(1, "Finally shipped");

Order order2 = new Order(serviceProvider.GetServices<IContact>().ElementAt(0));
order2.ContactCustomer(2, "Finally shipped");

Order order3 = new Order(serviceProvider.GetServices<IContact>().ElementAt(0));
order3.ContactCustomer(3, "Finally shipped");

Console.WriteLine("Press any key");
Console.ReadKey();