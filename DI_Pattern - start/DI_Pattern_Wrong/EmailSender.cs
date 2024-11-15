using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_Pattern_Wrong
{
	public class EmailSender
	{
		public string Contact(int customerId, string message)
		{
			// Code to send mail
			string contactMessage = $"Mail sent to customer with id = {customerId} : {message}";
			Console.WriteLine(contactMessage);
			return contactMessage;
		}
	}
}
