using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using ChartplotterDataProcessorML.DataProcessing;

namespace ChartplotterDataProcessorML.Tests.DataProcessing
{
    public class ShoreLineFillerTests
    {
        private DataProcessingParams _dataProcessingParams = new DataProcessingParams();
        private ProcessingContext _data;

        public ShoreLineFillerTests()
        {
            var testData = new TestData();
            _data = testData.GetData();
            var shoreline1 = new List<Shoreline>()
            {
                new Shoreline() { GpsLat = 67.247207M, GpsLng = 22.887933M },
                new Shoreline() { GpsLat = 67.248397M, GpsLng = 22.875103M }
            };
            var shoreline2 = new List<Shoreline>() { };
            _data.LocationSettings.Shoreline1 = shoreline1;
            _data.LocationSettings.Shoreline2 = shoreline2;
        }

        [Fact]
        public void Process_CreatesCorrectAmountOfLocations()
        {
            //Arrange
            var shoreLineFiller = new ShorelineFiller(_dataProcessingParams);
            var expected = 285;

            //Act
            shoreLineFiller.Process(_data);
            var actual = _data.LocationSettings.Shoreline1.Count;

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Process_CreatesCorrectLastLocation()
        {
            //Arrange
            var shoreLineFiller = new ShorelineFiller(_dataProcessingParams);
            var expectedLat = 67.248397M;
            var expectedLng = 22.875103M;

            //Act
            shoreLineFiller.Process(_data);
            var actualLat = Math.Round(_data.LocationSettings.Shoreline1.Last().GpsLat, 6);
            var actualLng = Math.Round(_data.LocationSettings.Shoreline1.Last().GpsLng, 6);

            //Assert
            actualLat.Should().Be(expectedLat);
            actualLng.Should().Be(expectedLng);
        }

        [Fact]
        public void Process_CreatesOnlyUniqueItems()
        {
            //Arrange
            var shoreLineFiller = new ShorelineFiller(_dataProcessingParams);

            //Act
            shoreLineFiller.Process(_data);
            var actual = _data.LocationSettings.Shoreline1;

            //Assert
            actual.Should().OnlyHaveUniqueItems();
        }
    }
}
