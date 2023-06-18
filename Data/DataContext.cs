using System;
using web_application_task.Models;

namespace web_application_task.Data
{
	public class DataContext : DbContext
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<UserProfile> UserProfiles=> Set<UserProfile>();
        public DbSet<User> Users => Set<User>();
    }
}

