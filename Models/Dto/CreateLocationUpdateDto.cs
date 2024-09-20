namespace BetaLogistics.Models.Dto
{
    public class CreateLocationUpdateDto
    {
        public int LocationId { get; set; }
        public int ShipmentId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public Shipment Shipment { get; set; }
    }
}
