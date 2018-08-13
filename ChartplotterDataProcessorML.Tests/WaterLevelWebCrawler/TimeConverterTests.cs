using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace ChartplotterDataProcessorML.Tests
{
    public class TimeConverterTests
    {
        [Fact]
        public void ConvertToUnixTime_ReturnsCorrectUnixTime()
        {
            //Arrange
            var timeConverter = new TimeConverter();
            var expected = 1000000000;

            //Act
            var sut = timeConverter.ConvertToUnixTime(2001, 9, 9, 1, 46, 40);
            var actual = sut;

            //Assert
            actual.Should().Be(expected);
        }
    }
}
