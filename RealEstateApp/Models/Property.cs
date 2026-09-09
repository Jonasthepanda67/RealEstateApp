namespace RealEstateApp.Models
{
    public class Property
    {
        public Property()
        {
            Id = Guid.NewGuid().ToString();
            ImageUrls = new List<string>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? Price { get; set; }
        public string Description { get; set; }
        public string VendorId { get; set; }
        public Vendor? Vendor { get; set; }
        public PropertyType Type { get; set; }
        public PropertyTier Tier { get; set; }
        public int? Beds { get; set; }
        public int? Baths { get; set; }
        public int? Parking { get; set; }
        public int? LandSize { get; set; }
        public string NeighborhoodUrl { get; set; } = string.Empty;
        public string AgentId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Aspect { get; set; } = string.Empty;
        public string ContractFilePath { get; set; } = string.Empty;
        public List<string> ImageUrls { get; set; }

        public string MainImageUrl => ImageUrls?.FirstOrDefault() ?? GlobalSettings.Instance.NoImageUrl;
    }
}
