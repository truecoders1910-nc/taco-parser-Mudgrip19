using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            
            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

            //logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            logger.LogInfo("Begin parsing");
            
            var locations = lines.Select(parser.Parse).ToArray();

            // TODO:  Find the two Taco Bells in Alabama that are the furthest from one another.
            // HINT:  You'll need two nested forloops

            ITrackable point1 = null;
            ITrackable point2 = null; 
            ITrackable locA;
            ITrackable locB; 
            double distance = 0;
            double furthestDistance = 0;

            
            for (int i = 0; i < locations.Length; i++)
            {
                locA = locations[i];
                double latitudeA = locA.Location.Latitude;
                double longitudeA = locA.Location.Longitude;
                var corA = new GeoCoordinate(latitudeA, longitudeA);

                for (int j = 0; j < locations.Length; j++)
                {
                    locB = locations[j];
                    double latitudeB = locB.Location.Latitude;
                    double longitudeB = locB.Location.Longitude;
                    var corB = new GeoCoordinate(latitudeB, longitudeB);

                    distance = corA.GetDistanceTo(corB);
                    if (furthestDistance < distance) 
                    {
                        point1 = locA;
                        point2 = locB;
                        furthestDistance = distance; 
                    }
                    
                    
                }
            }
            Console.WriteLine($"{point1.Name.Replace("Taco Bell ", "").Replace("... (Free trial * Add to Cart for a full POI info)", "")}" +
                $" and {point2.Name.Replace("Taco Bell ", "").Replace("... (Free trial * Add to Cart for a full POI info)", "")} " +
                $" are furthest from each other.");
                
        }
    }
}