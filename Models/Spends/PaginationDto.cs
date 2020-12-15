using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpndRr.Models.Spends
{
	public class PaginationDto
	{
		public PaginationDto(List<Spend> items, int total, int prevPage, int nextPage)
		{
			Data = items;
			HasData = items.Any();
			Total = total;
			PrevPage = prevPage;
			NextPage = nextPage;
		}

		public int Total { get; set; }

		public int PrevPage { get; set; }

		public int NextPage { get; set; }

		public bool HasData { get; set; }

		public List<Spend> Data { get; set; }
	}
}
