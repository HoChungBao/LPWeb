using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GoogleMaps.LocationServices;
using LienPhatERP.Services;
using Newtonsoft.Json;


namespace LienPhatERP.Helper
{
    public class StaticHelper
    {
        public static string FormatMoney(double num)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            string money = num.ToString("#,###", cul.NumberFormat);
            return money;
        }
        public static string FormatMoney(decimal num)
        {
           // CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN"); 
            string money = num.ToString("#,###");
            return money;
        }
        public static string FormatDatetime(string date)
        {
            DateTime time;
            if (DateTime.TryParse(date, out time))
            {


                return DateUtils.FormatDateTime(date, "dd-MM-yyyy HH:mm:ss");

            }
            return date;
        }

        /// <summary>
        /// Returns a pretty date like Facebook
        /// </summary>
        /// <param name="date"></param>
        /// <returns>28 Days Ago</returns>
        public static string GetFormatDate(string date)
        {

            DateTime time;
            if (DateTime.TryParse(date, out time))
            {
                var span = DateTime.Now.Subtract(time);
                var totalDays = (int)span.TotalDays;
                var totalSeconds = (int)span.TotalSeconds;
                return DateUtils.FormatDateTime(date, "dd-MM-yyyy HH:mm");

            }
            return date;
        }
        public static RootObject GetLatLongByAddress(string address)
        {
            try
            {
                var root = new RootObject();

                var url =
                    string.Format(
                        "http://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=true_or_false", address);
                var req = (HttpWebRequest)WebRequest.Create(url);

                var res = (HttpWebResponse)req.GetResponse();

                using (var streamreader = new StreamReader(res.GetResponseStream()))
                {
                    var result = streamreader.ReadToEnd();

                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        root = JsonConvert.DeserializeObject<RootObject>(result);
                    }
                }
                return root;
            }
            catch
            {
                return null;
            }

        }
        public static MapPoint GetLatLong(string address)
        {
            try
            {
                var googleLocation = new GoogleLocationService("AIzaSyCv-VJuonOOmaXpJLsqenv0bUTskGAeBWQ&am");
                return googleLocation.GetLatLongFromAddress(address);
            }
            catch
            {
                return null;
            }

        }
    }
}
