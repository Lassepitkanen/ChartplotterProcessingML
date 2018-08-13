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
    public class GpsRounderTests
    {
        private DataProcessingParams _dataProcessingParams = new DataProcessingParams();
        private ProcessingContext _data;

        public GpsRounderTests()
        {
            var testData = new TestData();
            _data = testData.GetData();
        }

        [Fact]
        public void Process_RoundsFirstGpsLat()
        {
            //Arrange
            _dataProcessingParams.GpsRoundPrecision = 4;
            var gpsRounder = new GpsRounder(_dataProcessingParams);
            var expected = Math.Round(_data.Rows.First().GpsLat, 4);

            //Act
            gpsRounder.Process(_data);
            var actual = _data.Rows.First().GpsLat;

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Process_RoundsFirstGpsLng()
        {
            //Arrange
            _dataProcessingParams.GpsRoundPrecision = 2;
            var gpsRounder = new GpsRounder(_dataProcessingParams);
            var expected = Math.Round(_data.Rows.First().GpsLng, 2);

            //Act
            gpsRounder.Process(_data);
            var actual = _data.Rows.First().GpsLng;

            //Assert
            actual.Should().Be(expected);
        }
    }
}
