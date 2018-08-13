using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using ChartplotterDataProcessorML.FileIO;
using NSubstitute;

namespace ChartplotterDataProcessorML.Tests.FileIO
{
    public class CsvUtilityTests
    {
        private IFileRepository _fileRepository = Substitute.For<IFileRepository>();
        private IEnumerable<string> _testData = new string[] { "1509090005,4.1,0,68.190877,23.148555,15.1,11.0,90.5,4,5,10,0.0,0.0", "1509090006,4.5,0,68.190877,23.148555,15.1,11.0,90.5,4,5,10,0.0,0.0" };

        public CsvUtilityTests()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        }

        [Fact]
        public void GetCsv_ReturnsCorrectNumberOfElements()
        {
            //Arrange
            _fileRepository.OpenAllFiles(_fileRepository.InputCsvDir, "csv").Returns(_testData);
            var csvUtility = new CsvUtility(_fileRepository);
            var expected = 2;

            //Act
            var sut = csvUtility.GetCsv(_fileRepository.InputCsvDir);
            var actual = sut.Count;

            //Assert
            actual.Should().Be(expected);
        }
    }
}
