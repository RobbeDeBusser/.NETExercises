using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_Pattern_Best
{
	public class OrderHandler
	{
		public Order Order { get; set; }

		public OrderHandler(Order order)
		{
			Order = order;
		}

		public OrderHandlerResponse HandleOrder()
		{
			OrderHandlerResponse response = new OrderHandlerResponse();

			// Here we would calculate the price
			response.TotalPrice = 50.99;
			// Here we contact the customer
			response.ContactMessage = Order.ContactCustomer(1, "Your shipment will be delivered tomorrow at 4pm.");
			// Here we save this action in our database
			response.IsHandled = true;

			return response;
		}
	}
}
