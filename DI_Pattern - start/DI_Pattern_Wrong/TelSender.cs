using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_Pattern_Wrong
{
	public class TelSender
	{
		public string Contact(int customerId, string message)
		{
			// Code to make phone call
			string contactMessage = $"Phone call to customer with id = {customerId} : {message}";
			Console.WriteLine(contactMessage);
			return contactMessage;
		}
	}
}
