using ChartplotterDataProcessorML.ImageRecognition.MachineLearning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace ChartplotterDataProcessorML.Tests.ImageRecognition
{
    public class OutputCreatorTests
    {
        private List<string[]> _paths;

        public OutputCreatorTests()
        {
            string[] first = { "Images\\1.4\\1529194600.47.jpg", "Images\\1.4\\1529194600.47.jpg",
                "Images\\1.4\\1529194600.47.jpg", "Images\\1.4\\1529194600.47.jpg"};
            string[] second = { "Images\\2.5\\1529194600.47.jpg", "Images\\2.5\\1529194600.47.jpg",
                "Images\\2.5\\1529194600.47.jpg"};
            string[] third = { "Images\\3.6\\1529194600.47.jpg", "Images\\3.6\\1529194600.47.jpg" };
            _paths = new List<string[]> { first, second, third }; ;
        }

        [Fact]
        public void GetOutputModels_ReturnsCorrectModelNumber0()
        {
            //Arrange
            var outputCreator = new OutputCreator();
            var expected = 0;

            //Act
            var sut = outputCreator.GetOutputModels(_paths);
            var actual = sut[3];

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetOutputModels_ReturnsCorrectModelNumber1()
        {
            //Arrange
            var outputCreator = new OutputCreator();
            var expected = 1;

            //Act
            var sut = outputCreator.GetOutputModels(_paths);
            var actual = sut[4];

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetOutputModels_ReturnsCorrectModelNumber2()
        {
            //Arrange
            var outputCreator = new OutputCreator();
            var expected = 2;

            //Act
            var sut = outputCreator.GetOutputModels(_paths);
            var actual = sut[8];

            //Assert
            actual.Should().Be(expected);
        }
    }
}
