using System;
using System.ComponentModel.DataAnnotations;

namespace web_application_task.Models
{
	public class UserProfile
	{
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int PinCode { get; set; }
        public long TelePhone { get; set; }
        public User? User { get; set; }
    }
}

