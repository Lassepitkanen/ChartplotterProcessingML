 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Serilog;
using ChartplotterDataProcessorML.DataProcessing;

namespace ChartplotterDataProcessorML.Tests.DataProcessing
{
    public class FiltersTests
    {
        private DataProcessingParams _dataProcessingParams = new DataProcessingParams();
        private ProcessingContext _data;

        public FiltersTests()
        {
            var testData = new TestData();
            _data = testData.GetData();
            //Insert to be filtered elements
            _data.Rows.Insert(0, new Data { UnixTime = 1509090005, GpsLat = 68.9999M, GpsLng = 23.8888M, WaterDepth = 9.5, HeatMap = 1 });
            _data.Rows.Insert(0, new Data { UnixTime = 1509090006, GpsLat = 68.9999M, GpsLng = 23.8888M, WaterDepth = 11.0, HeatMap = 0 });
            _data.Rows.Insert(0, new Data { UnixTime = 1509090005, GpsLat = 68.9999M, GpsLng = 23.8888M, WaterDepth = 0.2, HeatMap = 0 });
            _data.Rows.Insert(0, new Data { UnixTime = 1509090006, GpsLat = 68.9999M, GpsLng = 23.8888M, WaterDepth = -1.0, HeatMap = 1 });
        }

        private ILogger CreateLogger()
        {
            return Log.Logger = new LoggerConfiguration()
                .CreateLogger();
        }

        [Fact]
        public void Process_Removes4Elements()
        {
            //Arrange
            _dataProcessingParams.GpsErrorThreshold = 1111000;
            _dataProcessingParams.DepthFilterHigh = 9;
            _dataProcessingParams.DepthFilterLow = 0.4;
            var depthFilters = new Filters(_dataProcessingParams, CreateLogger());
            var expected = _data.Rows.Count - 4;

            //Act
            depthFilters.Process(_data);
            var actual = _data.Rows.Count;

            //Assert
            actual.Should().Be(expected);
        }
    }
}
