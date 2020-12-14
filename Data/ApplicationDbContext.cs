using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpndRr.Models.Spends;
using SpndRr.Models.SubTypes;

namespace SpndRr.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<SpndRr.Models.Spends.Spend> Spend { get; set; }

		public DbSet<SubType> SubType { get; set; }
	}
}
