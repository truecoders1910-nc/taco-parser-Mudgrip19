﻿
namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            TacoBell restaurant = new TacoBell();

            if (line == null) return null;

            var cells = line.Split(',');
            if (cells.Length < 3)
            {
                logger.LogError(null);
                return null;
            }
            // Do not fail if one record parsing fails, return null
             // TODO Implement

            Point coord = new Point();
            coord.Latitude = double.Parse(cells[0]);
            coord.Longitude = double.Parse(cells[1]);
           
            restaurant.Name = cells[2];

            
            restaurant.Location = coord;

            return restaurant;
            
        }
    }

    public class TacoBell : ITrackable
    {
        public string Name { get; set; }
        public Point Location { get;  set; }
    }
}

