namespace EmployeeCRUD.Server.Data
{
	public class DataContext : DbContext
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Employee>().ToTable("Employees");
		}

		public DbSet<Employee> Employees { get; set; }
	}
}
