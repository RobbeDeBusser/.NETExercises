using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_Pattern_Better
{
	public class EmailSender: IContact
	{
        private int number = 0;
        public string Contact(int customerId, string message)
		{
			number++;
			// Code to send mail
			string contactMessage = $"#{number} : Mail sent to customer with id = {customerId} : {message}";
			Console.WriteLine(contactMessage);
			return contactMessage;
		}
	}
}
