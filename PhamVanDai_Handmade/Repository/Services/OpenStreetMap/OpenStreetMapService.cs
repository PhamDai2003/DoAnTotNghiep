using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PhamVanDai_Handmade.Repository.Services.OpenStreetMap
{
    public class OpenStreetMapService
    {
        private readonly HttpClient _httpClient;

        public OpenStreetMapService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Lấy tọa độ từ địa chỉ (Nominatim API).
        /// </summary>
        public async Task<(double lat, double lon)?> GetCoordinatesFromAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address)) return null;

            var url = $"https://nominatim.openstreetmap.org/search?format=json&q={Uri.EscapeDataString(address)}";

            // thêm user-agent
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("User-Agent", "PhamVanDai_HandmadeApp/1.0");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            var results = JArray.Parse(content);

            if (results.Count == 0) return null;

            var lat = double.Parse(results[0]["lat"].ToString());
            var lon = double.Parse(results[0]["lon"].ToString());

            return (lat, lon);
        }


        /// <summary>
        /// Tính khoảng cách bằng công thức Haversine.
        /// </summary>
        public double CalculateDistance((double lat, double lon) from, (double lat, double lon) to)
        {
            double R = 6371; // km
            double dLat = ToRadians(to.lat - from.lat);
            double dLon = ToRadians(to.lon - from.lon);
            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(from.lat)) * Math.Cos(ToRadians(to.lat)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        /// <summary>
        /// Tính phí ship = khoảng cách * 5000đ/km (ví dụ).
        /// </summary>
        public decimal CalculateShippingFee((double lat, double lon) shop, (double lat, double lon) customer)
        {
            var distance = CalculateDistance(shop, customer);
            return (decimal)(distance * 5000);
        }

        private double ToRadians(double angle) => Math.PI * angle / 180.0;
    }
}
