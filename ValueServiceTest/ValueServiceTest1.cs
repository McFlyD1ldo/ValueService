using libValueService;

namespace ValueServiceTest
{
    public class ValueServiceTest1
    {
        [Fact]
        public void CountPFList()
        { 
            ValueService vs = new ValueService();
            Assert.Equal(10,vs.PostFactors.Count);
        }

        [Theory]
        [InlineData(3, "k")]
        [InlineData(6, "M")]
        [InlineData(9, "G")]
        [InlineData(12, "T")]
        [InlineData(15, "P")]
        [InlineData(18,"E")]
        [InlineData(-3, "m")]
        [InlineData(-6, "µ")]
        [InlineData(-9, "n")]
        [InlineData(-12, "p")]
        [InlineData(null, "z")]
        [InlineData(0, "")]

        public void CheckGetPotenz(int? power, string postfactor)
        {
            var vs = new ValueService();

            Assert.Equal(power, vs.GetPotenz(postfactor));
        }


        [Theory]
        [InlineData(200000, "200k")]
        [InlineData(0.003, "3m")]
        [InlineData(1500000, "1M5")]
        [InlineData(0.0000000000015, "1p5")]
        [InlineData(50500, "50k500")]
        [InlineData(100, "100")]
        [InlineData(3500, "3.5k")]
        [InlineData(3500, "3,5k")]
        public void CheckGetDecimal(decimal result, string input)
        {
            var vs = new ValueService();

            Assert.Equal(result, vs.GetDecimal(input));
        }

        [Theory]
        [InlineData("1kk")]
        [InlineData("1x")]
        public void CheckGetDecimalException(string input)
        {
            var vs = new ValueService();

            Assert.Throws<FormatException>(() => vs.GetDecimal(input));
        }

        [Theory]
        [InlineData(100000, 0, "", "100000")]
        [InlineData(0.001, 0, "µ", "1000µ")]
        [InlineData(123456, 2, "k", "123,46k")]
        [InlineData(154.56, 0, "", "155")]
        public void CheckGetDisplayValue(decimal number, int precision, string desiredpf, string result)
        {
            var vs = new ValueService();

            Assert.Equal(result, vs.GetDisplayValue(number, precision, desiredpf));
        }

        [Theory]
        [InlineData(0.000001, "µ")]
        [InlineData(1000, "k")]
        [InlineData(1000000, "M")]
        [InlineData(1000000000, "G")]
        public void checkGetPostFactor(decimal input, string expected)
        {
            var vs = new ValueService();
            Assert.Equal(expected, vs.GetPostFactor(input));
        }

    }
}