using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_Pattern_Wrong
{
	public class OrderHandlerResponse
	{
		public double TotalPrice { get; set; }
		public string ContactMessage { get; set; }
		public bool IsHandled { get; set; }
	}
}
