using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpndRr.Models.SubTypes
{
	public class SubType
	{
		[JsonProperty("id")]
		[Required]
		public int Id { get; set; }

		[Required]
		public int ParentType { get; set; }

		[JsonProperty("name")]
		[Required]
		public string Name { get; set; }
	}
}
