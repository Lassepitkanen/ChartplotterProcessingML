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
    public class DuplicateCombinerTests
    {
        private ProcessingContext _data;

        public DuplicateCombinerTests()
        {
            var testData = new TestData();
            _data = testData.GetData();
            //Insert duplicates
            _data.Rows.Insert(0, new Data { UnixTime = 1509090005, GpsLat = 68.9999M, GpsLng = 23.8888M, WaterDepth = 2, HeatMap = 1 });
            _data.Rows.Insert(0, new Data { UnixTime = 1509090006, GpsLat = 68.9999M, GpsLng = 23.8888M, WaterDepth = 2, HeatMap = 0 });
            _data.Rows.Insert(0, new Data { UnixTime = 1509090005, GpsLat = 68.9999M, GpsLng = 23.8888M, WaterDepth = 4, HeatMap = 0 });
            _data.Rows.Insert(0, new Data { UnixTime = 1509090006, GpsLat = 68.9999M, GpsLng = 23.8888M, WaterDepth = 4, HeatMap = 1 });
        }

        [Fact]
        public void Process_SetsAverageWaterDepthToFirstElement()
        {
            //Arrange
            var duplicateCombiner = new DuplicateCombiner();
            var expected = 3;

            //Act
            duplicateCombiner.Process(_data);
            var actual = _data.Rows.First().WaterDepth;

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Process_SumsHeatMapsToFirstElement()
        {
            //Arrange
            var duplicateCombiner = new DuplicateCombiner();
            var expected = 2;

            //Act
            duplicateCombiner.Process(_data);
            var actual = _data.Rows.First().HeatMap;

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Process_Removes3Elements()
        {
            //Arrange
            var duplicateCombiner = new DuplicateCombiner();
            var expected = _data.Rows.Count - 3;

            //Act
            duplicateCombiner.Process(_data);
            var actual = _data.Rows.Count;

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Process_CombinesDuplicateItems()
        {
            //Arrange
            var duplicateCombiner = new DuplicateCombiner();

            //Act
            duplicateCombiner.Process(_data);
            var actual = _data.Rows;

            //Assert
            actual.Should().OnlyHaveUniqueItems();
        }
    }
}
