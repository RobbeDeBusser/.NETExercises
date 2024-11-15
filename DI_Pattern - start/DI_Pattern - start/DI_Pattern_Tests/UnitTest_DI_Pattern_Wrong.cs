using DI_Pattern_Wrong;

namespace DI_Pattern_Tests
{
	[TestClass]
	public class UnitTest_DI_Pattern_Wrong
	{
		[TestMethod]
		public void Test_OrderHandler_HandleOrder()
		{

			Order order = new Order();
			OrderHandler orderHandler = new OrderHandler(order);

			var response = orderHandler.HandleOrder();

			// We are only interested in the price calculation and the response of the HandleOrder method
			// Testing the HandleOrder method will now execute the ContactCustomer method
			// If you've written logic that does contact the customer, this will happen each time the unit test runs

			// Ideal would be to mock/change its behavior for this test
			// This way we can manipulate what the method should return, without executing the actual code
			// But in this case this is not possible --> Order class has a hard dependency on the EmailSender or TelSender class

			Assert.AreEqual(50.99, response.TotalPrice);
		}
	}
}