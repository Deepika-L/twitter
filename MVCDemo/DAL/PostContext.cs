/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;*/
using MVCDemo.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MVCDemo.DAL
{
    public class PostContext: DbContext
    {
        public PostContext() : base("PostContext")
        {
        }
        
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            /*modelBuilder.Entity().ToTable("Artist", "public");
            modelBuilder.Entity().ToTable("Album", "public");
            modelBuilder.Entity().ToTable("Cart", "public");*/
        }
    }
}