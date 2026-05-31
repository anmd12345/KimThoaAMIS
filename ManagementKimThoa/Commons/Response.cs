using System;
namespace ManagementKimThoa.Commons
{
	public class Response
	{
		public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public dynamic? Data { get; set; }
	}
}

