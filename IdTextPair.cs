using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpndRr.Models.SubTypes;

namespace SpndRr
{
	public class IdTextPair
	{
		public IdTextPair() {}

		public IdTextPair(int id, string name)
		{
			Id = id;
			Name = name;
		}

		public IdTextPair(SubType subType)
		{
			Id = subType.Id;
			Name = subType.Name;
		}

		[JsonProperty("id")]
		[Required]
		public int Id { get; set; }

		[JsonProperty("name")]
		[Required]
		public string Name { get; set; }
	}
}
