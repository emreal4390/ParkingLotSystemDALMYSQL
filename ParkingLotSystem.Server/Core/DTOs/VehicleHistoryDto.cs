namespace ParkingLotSystem.Server.Core.DTOs
{
    public class VehicleHistoryDto
    {
        public string LicensePlate { get; set; }
        public string OwnerName { get; set; }
        public string ApartmentNumber { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public int DurationMinutes { get; set; }
    }
}
