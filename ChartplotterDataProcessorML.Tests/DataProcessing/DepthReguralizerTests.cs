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
    public class DepthRegularizerTests
    {
        private DataProcessingParams _dataProcessingParams = new DataProcessingParams();
        private ProcessingContext _data;

        public DepthRegularizerTests()
        {
            var testData = new TestData();
            _data = testData.GetData();

            //Insert data to reguralize
            _data.Rows.Insert(0, new Data { UnixTime = 1111111111, GpsLat = 68.9999M, GpsLng = 23.8888M, WaterDepth = 5, HeatMap = 0 });
            _data.Rows.Insert(_data.Rows.Count, new Data { UnixTime = 2222222222, GpsLat = 68.9999M, GpsLng = 23.8888M, WaterDepth = 5, HeatMap = 0 });

            _data.LocationSettings.DefaultWaterLevel = 11;
            _data.LocationSettings.MeasurementPoint.Add(new MeasurementPoint { UnixTime = 1111111111, WaterLevel = 10 });
            _data.LocationSettings.MeasurementPoint.Add(new MeasurementPoint { UnixTime = 2222222222, WaterLevel = 12 });
        }

        private ILogger CreateLogger()
        {
            return Log.Logger = new LoggerConfiguration()
                .CreateLogger();
        }

        [Fact]
        public void Process_Adds1ToFirstWaterDepth()
        {
            //Arrange
            var depthRegularizer = new DepthRegularizer(_dataProcessingParams, CreateLogger());
            double expected = 6.0;

            //Act
            depthRegularizer.Process(_data);
            var actual = _data.Rows.First().WaterDepth;

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Process_Substracts1FromLastWaterDepth()
        {
            var depthRegularizer = new DepthRegularizer(_dataProcessingParams, CreateLogger());
            double expected = 4.0;

            //Act
            depthRegularizer.Process(_data);
            var actual = _data.Rows.Last().WaterDepth;

            //Assert
            actual.Should().Be(expected);
        }
    }
}
