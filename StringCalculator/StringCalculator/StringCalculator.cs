namespace StringCalculatorKata
{
    public class StringCalculator
    {
        public class NegativeNumbersException : Exception
        {
            public NegativeNumbersException(string message) : base(message)
            {
            }
        }
        public static int Add(string value)
        {
            var negatives = new List<string>();
            if (value == String.Empty)
            {
                return 0;
            }
            else
            {
                int result = 0;
                var values = value.Split(',');
                foreach (var v in values)
                {
                    int num = int.Parse(v); // Parse once for efficiency
                    if (num < 0)
                    {
                        negatives.Add(v);
                    }
                    else if (num <= 1000)
                    {
                        result += num;
                    }
                }
                if (negatives.Count == 0) // Changed to negatives.Count
                {
                    return result;
                }
                else
                {
                    throw new NegativeNumbersException("Negative numbers are not allowed: " + string.Join(", ", negatives));
                }
            }
        }
    }
}