﻿using DI_Pattern_Good;

Order order = new Order();
order.ContactCustomer(1, "Your shipment will be delivered tomorrow at 4pm.");

Console.WriteLine("Press any key");
Console.ReadKey();