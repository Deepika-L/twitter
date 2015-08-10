namespace MVCDemo.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using MVCDemo.Models;

    public partial class UserContext : DbContext
    {
        public UserContext()
            : base("name=UserContext1")
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
