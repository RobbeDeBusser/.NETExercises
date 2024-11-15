using StringCalculatorKata;
using static StringCalculatorKata.StringCalculator;

namespace StringCalculatorKataTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Add_EmptyStringAsParam_ReturnsZero()
        {
            // ARRANGE
            var empty = String.Empty;
            // ACT
            var result = StringCalculator.Add(empty);
            // ASSERT
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Add_StringContainingSingleNumber_ReturnsTheNumberItself()
        {
            // ARRANGE
            var value = "2";
            // ACT
            var result = StringCalculator.Add(value);
            // ASSERT
            Assert.AreEqual(2, result);
        }
        [TestMethod]
        public void Add_TwoNumbersSeparatedByComma_ReturnsTheirSum()
        {
            // ARRANGE
            var value = "2,1";
            // ACT
            var result = StringCalculator.Add(value);
            // ASSERT
            Assert.AreEqual(3, result);
        }
        [TestMethod]
        public void Add_MoreThanThreeNumbersSeparatedByComma_ReturnsTheirSum()
        {
            // ARRANGE
            var value = "1,2,3,4,5";
            // ACT
            var result = StringCalculator.Add(value);
            // ASSERT
            Assert.AreEqual(15, result);
        }
        [DataTestMethod]
        [DataRow("1,8", 9)]
        [DataRow("10,8,3", 21)]
        [DataRow("5", 5)]
        [DataRow("1,2,3,4,5", 15)]
        [DataRow("1,2,3,1000", 1006)]
        [DataRow("1,2,3,1001", 6)]

        public void Add_MultipleNumbers_ReturnsSum(string numbers, int result)
        {
            //Arrange Act Assert
            Assert.AreEqual(result, StringCalculator.Add(numbers));
        }
        [TestMethod]
        public void Add_NegativeNumber_ReturnException()
        {
            var input = "1,-8,3,-10,5,2";

            var exception = Assert.ThrowsException<NegativeNumbersException>(() => StringCalculator.Add(input));
            Assert.AreEqual("Negative numbers are not allowed: -8, -10", exception.Message);
        }
    }
}