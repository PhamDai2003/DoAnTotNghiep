using System;
using System.Collections.Generic;

namespace PhamVanDai_Handmade.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int Month { get; set; }
        public int Year { get; set; }

        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public int TotalCustomers { get; set; }

        public Dictionary<string, int> OrderStatusCount { get; set; } = new();

        public List<string> RevenueMonths { get; set; } = new();
        public List<decimal> RevenueValues { get; set; } = new();

        public List<string> TopProductNames { get; set; } = new();
        public List<int> TopProductQuantities { get; set; } = new();
    }
}
