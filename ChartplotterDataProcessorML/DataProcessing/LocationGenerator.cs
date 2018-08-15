using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.DataProcessing
{
    public class LocationGenerator : IProcessData
    {
        private readonly DataProcessingParams _dataProcessingParams;
        private readonly ILogger _logger;

        private HashSet<Data> NoMissingLocationsNearCache = new HashSet<Data>();
        private ProcessingContext Context;
        private List<Data> CreatedPoints;
        private decimal LngStep;
        private decimal LatStep;
        private decimal LngRange;
        private decimal LatRange;

        public LocationGenerator(DataProcessingParams dataProcessingParams, ILogger logger)
        {
            _dataProcessingParams = dataProcessingParams;
            _logger = logger;
        }

        public void Process(ProcessingContext context)
        {
            Console.WriteLine("Generating locations...");
            Context = context;
            CreateStepsAndRanges();
            var locations = context.Rows.Count;

            for (int i = 1; i <= _dataProcessingParams.LocGeneratorRuns; i++)
            {
                GenerateLocations();
                Console.WriteLine("Location Generator run: {0}/{1} complete", i, _dataProcessingParams.LocGeneratorRuns);
            }

            locations = Context.Rows.Count - locations;
            _logger.Information("Locations Generated: {0}", locations);
        }

        private void CreateStepsAndRanges()
        {   
            LngStep = 1;
            LatStep = 0.5M;
            for (int i = 0; i < _dataProcessingParams.GpsRoundPrecision; i++)
            {
                LngStep = LngStep / 10;
                LatStep = LatStep / 10;
            }
            LngRange = LngStep * 2;
            LatRange = LatStep * 2;
        }

        private void GenerateLocations()
        {
            CreatedPoints = new List<Data>();
            var notFound = new bool[8];
            foreach (var row in Context.Rows.Where(x => x.WaterDepth >= _dataProcessingParams.DepthFilterLow))
            {
                if (!NoMissingLocationsNearCache.Contains(row))
                {
                    notFound[0] = LookForMissingLocation(row, LngStep);
                    notFound[1] = LookForMissingLocation(row, -LngStep);
                    notFound[2] = LookForMissingLocation(row, 0, LatStep);
                    notFound[3] = LookForMissingLocation(row, 0, -LatStep);

                    notFound[4] = LookForMissingLocation(row, LngStep, LatStep);
                    notFound[5] = LookForMissingLocation(row, -LngStep, -LatStep);
                    notFound[6] = LookForMissingLocation(row, -LngStep, LatStep);
                    notFound[7] = LookForMissingLocation(row, LngStep, -LatStep);

                    if (notFound.All(x => x == true))
                    {
                        NoMissingLocationsNearCache.Add(row);
                    }
                }
            }
            Context.Rows.AddRange(CreatedPoints);
        }

        private bool LookForMissingLocation(Data row, decimal lngStep, decimal latStep = 0)
        {
            if (!Context.Rows.Any(x => x.GpsLng == row.GpsLng + lngStep && x.GpsLat == row.GpsLat + latStep) && 
                !CreatedPoints.Any(x => x.GpsLng == row.GpsLng + lngStep && x.GpsLat == row.GpsLat + latStep))
            {
                CreateNewLocation(row, lngStep, latStep);
                return false;
            }
            return true;
        }

        private void CreateNewLocation(Data row, decimal lngStep, decimal latStep)
        {
            var nearestLocations = GetWaterDepthFromNearestLocations(row);
            var shoreLineCount = GetNearestShoreLocationsCount(row);
            if (nearestLocations.Count + shoreLineCount > 9)
            {
                for (int i = 0; i < shoreLineCount; i++)
                {
                    nearestLocations.Add(0.0);
                }
                var averageWaterDepth = Math.Round(nearestLocations.Average(), 1);
                CreatedPoints.Add(new Data() { GpsLat = row.GpsLat + latStep, GpsLng = row.GpsLng + lngStep, WaterDepth = averageWaterDepth });
            }
        }

        private int GetNearestShoreLocationsCount(Data row)
        {
            var shoreLocations1 = Context.LocationSettings.Shoreline1
                .Where(x => x.GpsLng >= row.GpsLng - LngRange &&
                    x.GpsLng <= row.GpsLng + LngRange &&
                    x.GpsLat >= row.GpsLat - LatRange &&
                    x.GpsLat <= row.GpsLat + LatRange);

            var shoreLocations2 = Context.LocationSettings.Shoreline2
                .Where(x => x.GpsLng >= row.GpsLng - LngRange &&
                    x.GpsLng <= row.GpsLng + LngRange &&
                    x.GpsLat >= row.GpsLat - LatRange &&
                    x.GpsLat <= row.GpsLat + LatRange);

            return (shoreLocations2.Count() + shoreLocations1.Count()) * _dataProcessingParams.LocGeneratorShoreLineAggression;
        }

        private List<double> GetWaterDepthFromNearestLocations(Data row)
        {
            var query = Context.Rows
                .Where(x => x.GpsLng >= row.GpsLng - LngRange &&
                    x.GpsLng <= row.GpsLng + LngRange &&
                    x.GpsLat >= row.GpsLat - LatRange &&
                    x.GpsLat <= row.GpsLat + LatRange);

            return query.Select(x => x.WaterDepth).ToList();
        }
    }
}
