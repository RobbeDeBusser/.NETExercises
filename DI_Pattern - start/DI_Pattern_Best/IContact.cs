using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_Pattern_Best
{
	public interface IContact
	{
		string Contact(int customerId, string message);
	}
}
