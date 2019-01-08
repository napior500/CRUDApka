namespace CRUDApka.Models
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class KlienciContext : DbContext
	{
		public KlienciContext()
			: base("name=Klienci")
		{
		}

		public virtual DbSet<Klienci> Klienci { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
		}
	}
}
