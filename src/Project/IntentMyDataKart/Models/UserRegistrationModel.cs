using Microsoft.AspNetCore.Mvc.Rendering;

namespace IntentMyDataKart.Models
{
    public class UserRegistrationModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public long? CompanyRegistrationId { get; set; }
        public List<SelectListItem> CompanyList { get; set; }
        public string? Country { get; set; }
        public int? RoleId { get; set; }
        public List<SelectListItem> RoleList { get; set; }
      
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
