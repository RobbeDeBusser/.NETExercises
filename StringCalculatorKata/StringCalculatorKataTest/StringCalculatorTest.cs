using StringCalculatorKata;
using static StringCalculatorKata.StringCalculator;

namespace StringCalculatorKataTest
{
    [TestClass]
    public class StringCalculatorTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // ARRANGE
            var empty = String.Empty;

            // ACT
            var result = StringCalculator.Add(empty);

            // ASSERT
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Add_SingleNumebr_ReturnsThatNumber()
        {
            // ARRANGE
            var input = "6";

            // ACT
            var result = StringCalculator.Add(input);

            // ASSERT
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Add_ThreeNumbers_ReturnSum()
        {
            // ARRANGE
            var input = "1,8,3";

            // ACT
            var result = StringCalculator.Add(input);

            // ASSERT
            Assert.AreEqual(12, result);
        }

        [DataTestMethod]
        [DataRow("1,8", 9)]
        [DataRow("10,8,3", 21)]
        [DataRow("5", 5)]
        [DataRow("5,0,10", 15)]
        [DataRow("5,0,1000", 1005)]
        [DataRow("5,10,1001", 15)]
        public void Add_MultipleNumbers_ReturnSum(string numbers, int result)
        {
            // ARRANGE // ACT // ASSERT
            Assert.AreEqual(result, StringCalculator.Add(numbers));
        }

        [TestMethod]
        public void Add_NegativeNumber_ThrowsNegativeNumbersException()
        {
            // ARRANGE 
            var input = "1,-8,3,-10,5,2";
            // ASSERT
            var exception = 
                Assert.ThrowsException<NegativeNumbersException>(
                    () => StringCalculator.Add(input) // ACT
                    );
            Assert.AreEqual("Negative numbers not allowed: -8, -10", exception.Message);

        }

    }
}