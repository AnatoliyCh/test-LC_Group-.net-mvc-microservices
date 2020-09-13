using PeopleModule.Models;
using System.Data.Entity;

namespace PeopleModule.DataAccessLayer
{
    public class PeopleContext : DbContext
    {
        public PeopleContext() : base("DbConnectionString") { }

        public DbSet<People> Peoples { get; set; }
    }
}