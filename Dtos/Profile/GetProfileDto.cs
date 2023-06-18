using System;
namespace web_application_task.Dtos.Profile
{
	public class GetProfileDto
	{
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int PinCode { get; set; }
        public long TelePhone { get; set; }
    }
}

