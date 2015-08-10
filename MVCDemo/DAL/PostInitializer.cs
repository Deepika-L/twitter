using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCDemo.Models;

namespace MVCDemo.DAL
{
    public class PostInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PostContext>
    {
        protected override void Seed(PostContext context)
        {
            var posts = new List<Post>
            {
            new Post{id=1,posterName="Carson",postContent="Hello",Timestamp=DateTime.Parse("2005-09-01")},
            new Post{id=2,posterName="Meredith",postContent="Whats up",Timestamp=DateTime.Parse("2002-09-01")},
            new Post{id=3,posterName="Arturo",postContent="I dont understand",Timestamp=DateTime.Parse("2003-09-01")},
            };

            posts.ForEach(s => context.Posts.Add(s));
            context.SaveChanges();
        }
    }
}