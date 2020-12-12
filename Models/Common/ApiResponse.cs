using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpndRr.Controllers;

namespace SpndRr.Models.Common
{
	public class ApiResponse
	{
		public static ApiResponse NotSucessful()
		{
			return new ApiResponse() { Success = false, Data = null };
		}

		public static ApiResponse Sucessful(object data = null)
		{
			return new ApiResponse() { Success = true, Data = data };
		}

		[JsonProperty("success")]
		public bool Success { get; set; }

		[JsonProperty("data")]
		public object Data { get; set; }
	}
}
