namespace ManagementKimThoa.DTOs.Checkin
{
    public class CheckInRequest
    {
        public int UserId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string? Address { get; set; }

        public string? ImageUrl { get; set; }

        public string? Note { get; set; }
        public IFormFile? Image { get; set; }
    }
}
