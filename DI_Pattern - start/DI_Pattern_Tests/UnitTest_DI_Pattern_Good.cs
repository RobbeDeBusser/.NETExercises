using DI_Pattern_Good;
// using Moq;

namespace DI_Pattern_Tests
{
	[TestClass]
	public class UnitTest_DI_Pattern_Good
	{
		/*
		[TestMethod]
		public void Test_OrderHandler_HandleOrder()
		{
		    // Order retrieves its dependency via the constructor
		    // We can mock it and change its behavior for this test
		    var mediumSender = new Mock<IContact>();
		    // The following code simulates the execution of the Contact() method and returns a value that we choose
		    mediumSender.Setup(e => e.Contact(It.IsAny<int>(), It.IsAny<string>()))
		        .Returns("A test message for this wonderful test");

		    Order order = new Order(mediumSender.Object);

		    OrderHandler orderHandler = new OrderHandler(order);

		    // The HandleOrder() doesn't execute the Contact method anymore, just the simulation which returns a string
		    var response = orderHandler.HandleOrder();

		    // Test the price
		    Assert.AreEqual(50.99, response.TotalPrice);
		    // Test the response (contact message) we setup using the mock
		    Assert.AreEqual("A test message for this wonderful test", response.ContactMessage);
		}
		*/
	}
}