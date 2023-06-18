using System;
namespace web_application_task.Dtos.Profile
{
	public class AddProfileDto
	{
        public string Name { get; set; } = "Test Name";
        public string Email { get; set; } = "Test Mail";
        public string Address { get; set; } = "Test Address";
        public string State { get; set; } = "Test State";
        public string City { get; set; } = "Test City";
        public int PinCode { get; set; } = 101101;
        public long TelePhone { get; set; } = 1234567890;
    }
}

