using Microsoft.Extensions.DependencyInjection;
using DI_Pattern_Better;
using System;

IServiceProvider serviceProvider;

var services = new ServiceCollection();
// Register interfaces using factory that returns implementation
services.AddSingleton<IContact, TelSender>();
services.AddSingleton<IContact, EmailSender>();
serviceProvider = services.BuildServiceProvider(true);

Order order = new Order(serviceProvider.GetServices<IContact>().ElementAt(0));
order.ContactCustomer(1, "Finally shipped");

Console.WriteLine("Press any key");
Console.ReadKey();