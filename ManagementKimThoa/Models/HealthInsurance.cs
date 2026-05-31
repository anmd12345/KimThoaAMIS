using System;
namespace ManagementKimThoa.Models
{
    public class HealthInsurance
    {
        public int Id { get; set; }

        public string? HealthInsuranceNumber { get; set; }

        public string? HospitalRegistration { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}

