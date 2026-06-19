using static Layla.Models.MainModels.Booking;

namespace Layla.Models.DtosModels.AdminDashboardDtos
{
    public class StatusStatsDto
    {
        public BookingStatus Status { get; set; }
        public int Count { get; set; }
    }
}
