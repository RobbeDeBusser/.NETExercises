using DI_Pattern_Good;

Order order1 = new Order(new EmailSender());
order1.ContactCustomer(1, "Your shipment will be delivered tomorrow at 4pm.");

Order order2 = new Order(new TelSender());
order2.ContactCustomer(2, "Your shipment will be delivered tomorrow at 5pm.");

Console.WriteLine("Press any key");
Console.ReadKey();