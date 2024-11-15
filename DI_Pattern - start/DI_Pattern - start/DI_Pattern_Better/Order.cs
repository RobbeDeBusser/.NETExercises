using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_Pattern_Better
{
	public class Order
	{
		IContact _medium;

		public Order(IContact medium)
		{
			_medium = medium;
		}

		// Contact customer when order is shipped
		public string ContactCustomer(int customerId, string message)
		{
			return _medium.Contact(customerId, message);
		}
	}
}
