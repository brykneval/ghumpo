using System;
using System.Device.Location;
using System.Web;

namespace ghumpo.common
{
    public static class CommonHelper
    {
        public static string GetIpAddress()
        {
            var context = HttpContext.Current;
            var ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                var addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static double GetDistance(string deviceLatitude, string deviceLongitude, string resLatitude,
            string resLongitude)
        {
            var value = 0.00;
            if (!string.IsNullOrEmpty(deviceLatitude) && !string.IsNullOrEmpty(deviceLongitude) &&
                !string.IsNullOrEmpty(resLatitude) && !string.IsNullOrEmpty(resLongitude))
            {
                var sCoord = new GeoCoordinate(Convert.ToDouble(deviceLatitude), Convert.ToDouble(deviceLongitude));
                var eCoord = new GeoCoordinate(Convert.ToDouble(resLatitude), Convert.ToDouble(resLongitude));
                value = (sCoord.GetDistanceTo(eCoord)/1000);
            }
            return value;
        }
    }
}