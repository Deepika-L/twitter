using System;
using System.Collections.Generic;
using Npgsql;
using MVCDemo.Models;
using System.Configuration;


namespace MVCDemo.DAL
{
    public class DataAccess
    {
        public static IEnumerable<Post> GetAllPosts()
        {
            List<Post> posts = new List<Post>();

            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["PostgresDB"].ConnectionString;
            var conn = new NpgsqlConnection(connstring);
            string sqlquery = "Select * from Posts where postedto=0 order by timestamp desc";
            var cmd = new NpgsqlCommand(sqlquery, conn);
            conn.Open();

            NpgsqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Post post = new Post();
                    post.id = Convert.ToInt32(reader["postid"]);
                    post.posterName = reader["postername"].ToString();
                    post.postContent = reader["postcontent"].ToString();
                    post.Timestamp = Convert.ToDateTime(reader["timestamp"]);
                    posts.Add(post);
                }
            }

            return posts;
        }

        public static int CreatePost(Post newPost)
        {
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["PostgresDB"].ConnectionString;
            var conn = new NpgsqlConnection(connstring);
            conn.Open();
            string sqlquery = "insert into posts (posterName,postContent,timestamp, posterEmail,postedto) values ('" + newPost.posterName + "','" + newPost.postContent + "','" + newPost.Timestamp + "','" + newPost.posterEmail + "','"+newPost.postedTo+"')";
            var cmd = new NpgsqlCommand(sqlquery, conn);

            int rowsaffected = cmd.ExecuteNonQuery();
            conn.Close();
            return rowsaffected;


        }

        public static List<Post> allUserPosts(string userName, int postedTo)
        {
            List<Post> posts = new List<Post>();
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["PostgresDB"].ConnectionString;
            var conn = new NpgsqlConnection(connstring);
            string sqlquery = "select * from posts where postername='" + userName + "' or postedto="+postedTo+" order by timestamp desc";
            var cmd = new NpgsqlCommand(sqlquery, conn);
            conn.Open();

            NpgsqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Post post = new Post();
                    post.id = Convert.ToInt32(reader["postid"]);
                    post.posterName = reader["postername"].ToString();
                    post.postContent = reader["postcontent"].ToString();
                    post.Timestamp = Convert.ToDateTime(reader["timestamp"]);
                    post.posterEmail = reader["posteremail"].ToString();
                    post.postedTo = Convert.ToInt32(reader["postedto"]);
                    posts.Add(post);
                }
            }

            return posts;
        }

        public static User IsValid(string _username, string _pwd)
        {
            User user = null;

            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["PostgresDB"].ConnectionString;
            var conn = new NpgsqlConnection(connstring);
            string sqlquery = "Select * From users Where username='" + _username + "' And pwd='" + _pwd + "'";
            var cmd = new NpgsqlCommand(sqlquery, conn);
            conn.Open();

            NpgsqlDataReader reader = cmd.ExecuteReader();
            user = new User();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    
                    user.UserName = reader["username"].ToString();
                    user.Email = reader["useremail"].ToString();
                }
               
            }
            else
            {
                
                user.UserName = null;

            }

            return user;
        }

        public static int CreateUser(User newUser)
        {
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["PostgresDB"].ConnectionString;
            var conn = new NpgsqlConnection(connstring);
            conn.Open();
            string sqlquery = "insert into users (username,pwd,useremail) values ('" + newUser.UserName + "','" + newUser.Password + "','" + newUser.Email + "')";
            var cmd = new NpgsqlCommand(sqlquery, conn);

            int rowsaffected = cmd.ExecuteNonQuery();
            conn.Close();
            return rowsaffected;

        }

        public static int ReturnUserID(string UserName)
        {
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["PostgresDB"].ConnectionString;
            var conn = new NpgsqlConnection(connstring);
            conn.Open();
            string sqlquery = "select userid from users where username='" + UserName + "'";
            var cmd = new NpgsqlCommand(sqlquery, conn);
           // NpgsqlDataReader reader = cmd.ExecuteReader();
            int userid = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            return userid;

        }
    }
}
