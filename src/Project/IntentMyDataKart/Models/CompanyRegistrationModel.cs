namespace IntentMyDataKart.Models
{
    public class CompanyRegistrationModel
    {
        public long CompanyRegistrationId { get; set; }
        public string Company { get; set; } = string.Empty;
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
    }
}
