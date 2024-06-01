using EBikeAppWebAPI.business.ViewModel.Location;
using EBikeAppWebAPI.entity.Bike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.Operations.Statics
{
    public static class GeoLocationCalculator
    {
        public static double CalculateDistanceBetweenTwoPoint(Location firstLocation, Location secondLocation, DistanceUnit distanceUnit = DistanceUnit.Kilometer)
        {
            double fl_lat = firstLocation.Lat * Math.PI / 180;
            double fl_long = firstLocation.Long * Math.PI / 180;

            double sl_lat = secondLocation.Lat * Math.PI / 180;
            double sl_long = secondLocation.Long * Math.PI / 180;

            double deltaLat = sl_lat - fl_lat;
            double deltaLon = sl_long - fl_long;

            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                       Math.Cos(fl_lat) * Math.Cos(sl_lat) *
                       Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double pow = 0;
            switch (distanceUnit)
            {
                case DistanceUnit.Kilometer:
                    pow = 0;
                    break;
                case DistanceUnit.Meter:
                    pow = 3;
                    break;
                case DistanceUnit.Centimeter:
                    pow = 5;
                    break;
            }
            
            return Math.Pow(10, pow) * 6371 * c;
        }
    }

    public enum DistanceUnit
    {
        Kilometer,
        Meter,
        Centimeter
    }
}
