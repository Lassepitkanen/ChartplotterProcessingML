using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using ChartplotterDataProcessorML.DataProcessing;
using Serilog;

namespace ChartplotterDataProcessorML.Tests.DataProcessing
{
    public class LocationGeneratorTests
    {
        private DataProcessingParams _dataProcessingParams = new DataProcessingParams();
        private ProcessingContext _data;

        public LocationGeneratorTests()
        {
            var data = new List<Data>
            {
                new Data() { GpsLat = 66.84595M, GpsLng = 23.9712M, WaterDepth = 2},
                new Data() { GpsLat = 66.84595M, GpsLng = 23.9711M, WaterDepth = 2},
                //new Data() { GpsLat = 66.84595M, GpsLng = 23.9710M, WaterDepth = 1.5}, MissingLocation
                new Data() { GpsLat = 66.84595M, GpsLng = 23.9709M, WaterDepth = 2},
                new Data() { GpsLat = 66.84595M, GpsLng = 23.9708M, WaterDepth = 2},
                new Data() { GpsLat = 66.84600M, GpsLng = 23.9710M, WaterDepth = 2},
                new Data() { GpsLat = 66.84590M, GpsLng = 23.9710M, WaterDepth = 1},
                new Data() { GpsLat = 66.84600M, GpsLng = 23.9711M, WaterDepth = 1},
                new Data() { GpsLat = 66.84590M, GpsLng = 23.9711M, WaterDepth = 1},
                new Data() { GpsLat = 66.84600M, GpsLng = 23.9712M, WaterDepth = 1},
                new Data() { GpsLat = 66.84590M, GpsLng = 23.9712M, WaterDepth = 1},
            };

            var settings = new LocationSettings
            {
                Shoreline1 = new List<Shoreline>(),
                Shoreline2 = new List<Shoreline>()
            };

            _data = new ProcessingContext
            {
                Rows = data,
                LocationSettings = settings
            };
        }

        private ILogger CreateLogger()
        {
            return Log.Logger = new LoggerConfiguration()
                .CreateLogger();
        }

        [Fact]
        public void Process_CreatesMissingLocation()
        {
            //Arrange
            _dataProcessingParams.GpsRoundPrecision = 4;
            _dataProcessingParams.LocGeneratorRuns = 1;
            _dataProcessingParams.LocGeneratorShoreLineAggression = 6;
            var locationGenerator = new LocationGenerator(_dataProcessingParams, CreateLogger());

            var expectedLat = 66.84595M;
            var expectedLng = 23.9710M;
            var expectedDepth = 1.5;

            //Act
            locationGenerator.Process(_data);
            var actual = _data.Rows.Any(x => x.GpsLat == expectedLat && x.GpsLng == expectedLng && x.WaterDepth == expectedDepth);

            //Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public void Process_CreatesOnlyUniqueLocations()
        {
            //Arrange
            _dataProcessingParams.GpsRoundPrecision = 4;
            _dataProcessingParams.LocGeneratorRuns = 4;
            _dataProcessingParams.LocGeneratorShoreLineAggression = 6;
            var locationGenerator = new LocationGenerator(_dataProcessingParams, CreateLogger());

            //Act
            locationGenerator.Process(_data);
            var actual = _data.Rows;

            //Assert
            actual.Should().OnlyHaveUniqueItems();
        }

        [Theory]
        [InlineData(1, 1.2)]
        [InlineData(2, 1.0)]
        [InlineData(3, 0.9)]
        [InlineData(4, 0.8)]
        [InlineData(5, 0.7)]
        [InlineData(8, 0.5)]
        public void Process_CreatesLocationWithCorrectWaterDepth(int shoreLineAggression, double expected)
        {
            //Arrange
            _dataProcessingParams.GpsRoundPrecision = 4;
            _dataProcessingParams.LocGeneratorRuns = 1;
            _dataProcessingParams.LocGeneratorShoreLineAggression = shoreLineAggression;
            var locationGenerator = new LocationGenerator(_dataProcessingParams, CreateLogger());
            _data.LocationSettings.Shoreline1 = new List<Shoreline>() { new Shoreline() { GpsLat = 66.84595M, GpsLng = 23.9710M }, new Shoreline() { GpsLat = 66.84590M, GpsLng = 23.9710M } };
            var createdLat = 66.84595M;
            var createdLng = 23.9710M;

            //Act
            locationGenerator.Process(_data);
            var actual = _data.Rows.First(x => x.GpsLat == createdLat && x.GpsLng == createdLng).WaterDepth;

            //Assert
            actual.Should().Be(expected);
        }
    }
}
