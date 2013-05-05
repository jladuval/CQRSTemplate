
using System;

namespace CQRS.Security.Interfaces.Queries
{
	public class UserExistsQuery
	{
		public string Email { get; set; }
	}
}
