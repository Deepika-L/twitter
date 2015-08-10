using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCDemo.Models;

namespace MVCDemo
{
    public static class Repository
    {
        private static List<Post> listOfPosts { get; set; }

        static Repository()
        {
            listOfPosts = new List<Post>();
            Repository.CreateInitialPosts();
        }

        public static List<Post> GetAllPosts()
        {
            return listOfPosts;
        }

        public static void CreateInitialPosts()
        {
            Post firstPost = new Post() { posterName = "Alice", postContent = "I am happy", Timestamp = DateTime.Now };
            Post secondPost = new Post() { posterName = "Bob", postContent = "I love burgers", Timestamp = DateTime.Now };
            Post thirdPost = new Post() { posterName = "Susan", postContent = "I love shopping", Timestamp = DateTime.Now };
            Post fourthPost = new Post() { posterName = "James", postContent = "I like videogames", Timestamp = DateTime.Now };

            listOfPosts.Add(firstPost);
            listOfPosts.Add(secondPost);
            listOfPosts.Add(thirdPost);
            listOfPosts.Add(fourthPost);
        }

        public static void AddToList(Post newPost)
        {
            listOfPosts.Add(newPost);
        }
    }
}