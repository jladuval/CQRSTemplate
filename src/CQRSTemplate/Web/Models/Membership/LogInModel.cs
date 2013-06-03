
using System;

namespace Web.Models.Membership
{
	public class LogInModel
	{
		public string Email { get; set; }
		public string LoginPassword { get; set; }
		public string SignUpPassword { get; set; }
		public string ConfirmPassword { get; set; }
		public bool RememberMe { get; set; }
		public bool IsSignUp { get; set; }
	}
}
