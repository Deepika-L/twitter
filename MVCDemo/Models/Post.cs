using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
namespace MVCDemo.Models
{
    public class Post
    {
        private int _id;
        private string _name;
        private string _content;
        private DateTime _time;
        private string _email;
        private int _postedto;


        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

       // [Required(ErrorMessage="Name is required")]
        public string posterName
        {
            get { return _name; }
            set { _name = value; }
        }

        [Required(ErrorMessage="Content is required")]
        [Display(Name = "Enter post:")]
        public string postContent
        {
            get { return _content; }
            set { _content = value; }
        }

        public DateTime Timestamp
        {
            get { return _time; }
            set { _time = value; }
        }

       // [Required(ErrorMessage = "Email is required")]
        public string posterEmail
        {
            get { return _email; }
            set { _email = value; }
        }

        public int postedTo
        {
            get { return _postedto; }
            set { _postedto = value; }
        }
    }
    /* public class MovieDBContext : DbContext
     {
         public DbSet<MovieDB> Movies { get; set; }
     }*/
}