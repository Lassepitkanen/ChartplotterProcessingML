using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using ChartplotterDataProcessorML.ImageRecognition.Models;

namespace ChartplotterDataProcessorML.Tests.ImageRecognition
{
    public class ModelMatcherTests
    {
        private IModelRule[] GetModelRules()
        {
            return new IModelRule[]
            {
                new ModelRule(0),
                new ModelRule(1),
                new ModelRule(2),
                new ModelRule(3),
                new ModelRule(4),
                new ModelRule(5),
                new ModelRule(6),
                new ModelRule(7),
                new ModelRule(8),
                new ModelRule(9),
                new ModelRuleNoMatch()
            };
        }

        [Theory]
        [InlineData(new bool[] { true, false, false, false, false, false, false, false, false, false }, 0)]
        [InlineData(new bool[] { false, true, false, false, false, false, false, false, false, false }, 1)]
        [InlineData(new bool[] { false, false, true, false, false, false, false, false, false, false }, 2)]
        [InlineData(new bool[] { false, false, false, true, false, false, false, false, false, false }, 3)]
        [InlineData(new bool[] { false, false, false, false, true, false, false, false, false, false }, 4)]
        [InlineData(new bool[] { false, false, false, false, false, true, false, false, false, false }, 5)]
        [InlineData(new bool[] { false, false, false, false, false, false, true, false, false, false }, 6)]
        [InlineData(new bool[] { false, false, false, false, false, false, false, true, false, false }, 7)]
        [InlineData(new bool[] { false, false, false, false, false, false, false, false, true, false }, 8)]
        [InlineData(new bool[] { false, false, false, false, false, false, false, false, false, true }, 9)]
        [InlineData(new bool[] { false, false, false, false, false, false, false, false, false, false }, -1)]
        public void MatchModel_ReturnsCorrectNumber(bool[] predicted, int expected)
        {
            //Arrange
            IModelMatcher modelMatcher = new SecondDigitModelMatcher(GetModelRules());

            //Act
            var sut = modelMatcher.MatchModel(predicted);
            var actual = sut;

            //Assert
            actual.Should().Be(expected);
        }
    }
}
