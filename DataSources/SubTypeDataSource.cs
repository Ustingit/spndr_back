using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpndRr.Data;
using SpndRr.Models.Spends;

namespace SpndRr.DataSources
{
	public class SubTypeDataSource
	{
		private readonly ApplicationDbContext _context;

		public SubTypeDataSource(ApplicationDbContext context)
		{
			_context = context;
		}

		public IdTextPair[] GetAll()
		{
			return _context.SubType.Select(x => new IdTextPair(x)).ToArray();
		}

		public IdTextPair[] GetIncomes()
		{
			return _context.SubType
				.Where(x => x.ParentType == (int)SpendType.Income)
				.Select(x => new IdTextPair(x)).ToArray();
		}

		public IdTextPair[] GetOutcomes()
		{
			return _context.SubType
				.Where(x => x.ParentType == (int)SpendType.Outcome)
				.Select(x => new IdTextPair(x)).ToArray();
		}
	}
}
