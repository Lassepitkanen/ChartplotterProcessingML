using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using ChartplotterDataProcessorML.WaterLevelWebCrawler;

namespace ChartplotterDataProcessorML.Tests.WaterLevelWebCrawler
{
    public class StringEditUtilityTests
    {
        public StringEditUtilityTests()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        }

        private string testData = @"var nykytila = [
            [Date.UTC(2017, 03, 10, 0), 11.010],
            [Date.UTC(2017, 03, 11, 0), 11.050],
            [Date.UTC(2017, 03, 12, 0), 11.070],
            [Date.UTC(2017, 03, 13, 0), 11.090],
            [Date.UTC(2017, 03, 14, 0), 11.110],
            [Date.UTC(2017, 03, 15, 0), 11.120],
            [Date.UTC(2017, 03, 16, 0), 11.130],
            [Date.UTC(2017, 03, 17, 0), 11.140],
            [Date.UTC(2017, 03, 18, 0), 11.120],
            [Date.UTC(2017, 03, 19, 0), 11.090],];";

        [Fact]
        public void GetWaterLevelData_ReturnsCorrectNumberOfElements()
        {
            //Arrange
            var stringEditUtility = new StringEditUtility();
            var waterLevelData = stringEditUtility.GetWaterLevelData(testData);
            var expected = 10;

            //Act
            var actual = waterLevelData.Count;

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetWaterLevelData_ReturnsCorrectFirstWaterLevel()
        {
            //Arrange
            var stringEditUtility = new StringEditUtility();
            var waterLevelData = stringEditUtility.GetWaterLevelData(testData);
            var expected = 11.010;

            //Act
            var actual = waterLevelData.First().WaterLevel;

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetWaterLevelData_ReturnsCorrectLastWaterLevel()
        {
            //Arrange
            var stringEditUtility = new StringEditUtility();
            var waterLevelData = stringEditUtility.GetWaterLevelData(testData);
            var expected = 11.090;

            //Act
            var actual = waterLevelData.Last().WaterLevel;

            //Assert
            actual.Should().Be(expected);
        }
    }
}
